using AutoMapper;
using LibraryBLL.DTO;
using LibraryDAL.EF;
using LibraryDAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBLL.Services
{
    public class BorrowServices
    {
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Book, BookDTO>().ReverseMap();
                cfg.CreateMap<Status, StatusDTO>().ReverseMap();
                cfg.CreateMap<User, UserDTO>().ReverseMap();
                cfg.CreateMap<BorrowTransaction, BorrowDTO>().ReverseMap();
                cfg.CreateMap<BorrowTransaction, UserBorrowDTO>()
                 .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
                 .ForMember(dest => dest.BookID, opt => opt.MapFrom(src => src.BookID))
                 .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Book))
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                 .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
            });

            return new Mapper(config);
        }

        // 1. Borrow books
        public static string BorrowBooks(UserBorrowDTO dto)
        {
            var borrowRepo = new BorrowRepo();
            var bookRepo = new BookRepo();

            var currentBorrows = borrowRepo.GetByUserId(dto.User.UserID)
                              .Where(b => b.StatusID == 1)
                              .ToList();
            
            if (currentBorrows.Any(b => b.BookID == dto.Books.BookID))
            {
                return "The book is already Borrowed.";
            }

            if (currentBorrows.Count + 1 > 3)
            {
                return "Cannot borrow more than 3 books at a time.";
            }

            var book = bookRepo.GetByID(dto.Books.BookID);
            if (book.AvailableCopies <= 0)
                return $"Book '{book.Title}' is not available.";

            var borrow = new BorrowTransaction
            {
                UserID = dto.User.UserID,
                BookID = dto.Books.BookID,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddMonths(1),
                StatusID = 1,
                FineAmount = 0
            };

            borrowRepo.Create(borrow);

            book.AvailableCopies -= 1;
            bookRepo.Update(book);

            return "Book borrowed successfully.";
        }

        // 2. Return book
        public static string ReturnBook(int transactionId)
        {
            var borrowRepo = new BorrowRepo();
            var bookRepo = new BookRepo();

            
            var borrow = borrowRepo.Get(transactionId);
            if (borrow == null) return "Borrow record not found.";

            var book = bookRepo.GetByID(borrow.BookID);

            if (DateTime.Now > borrow.DueDate)
            {
                borrow.FineAmount = 300;
            }

            borrow.StatusID = 2; 
            
            borrowRepo.Update(borrow);

            book.AvailableCopies += 1;
            bookRepo.Update(book);

            return borrow.FineAmount > 0
                ? $"Book returned with a fine of ${borrow.FineAmount}."
                : "Book returned successfully.";
        }

        // 3. Remove borrow (admin)
        public static string RemoveBorrow(int transactionId)
        {
            var borrowRepo = new BorrowRepo();
            var bookRepo = new BookRepo();


            var borrow = borrowRepo.Get(transactionId);
            if (borrow == null) return "Borrow record not found.";

            var book = bookRepo.GetByID(borrow.BookID);
            book.AvailableCopies += 1;
            bookRepo.Update(book);

            
            borrowRepo.Delete(transactionId);

            return "Borrow record removed successfully.";
        }

        // 4. List all borrows for a user
        public static List<UserBorrowDTO> GetBorrowsByUser(int userId)
        {
            var borrowRepo = new BorrowRepo();

            var data = borrowRepo.GetByUserId(userId)
                        .Where(b => b.StatusID == 1) 
                        .ToList();

            return GetMapper().Map<List<UserBorrowDTO>>(data);
        }

        public static List<UserBorrowDTO> GetAllBorrows()
        {
            var repo = new BorrowRepo();
            var entities = repo.GetAll();
            var mapper = GetMapper();  
            return mapper.Map<List<UserBorrowDTO>>(entities);
        }

        public static List<UserBorrowDTO> GetBorrowsByBook(int bookId)
        {
            var borrowRepo = new BorrowRepo();
            var data = borrowRepo.GetByBookId(bookId);
            return GetMapper().Map<List<UserBorrowDTO>>(data);
        }


    }
}
