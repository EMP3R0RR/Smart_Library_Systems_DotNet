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
    public class BookServices
    {
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Book, BookDTO>().ForMember(dest => dest.CategoryName, 
                    opt => opt.MapFrom(src => src.Category.Name)).ReverseMap();
            });
            return new Mapper(config);
        }

        public static List<BookDTO> Get()
        {
            var data = new BookRepo().Get();
            return GetMapper().Map<List<BookDTO>>(data);
        }

        public static bool Create(BookDTO s)
        {
            var st = GetMapper().Map<Book>(s);
            return new BookRepo().Create(st);

        }

        public static bool Delete(BookDTO s) {
            var st = GetMapper().Map<Book>(s);
            return new BookRepo().Delete(st);
        }

        public static bool Update(BookDTO s) {
            var st = GetMapper().Map<Book>(s);
            return new BookRepo().Update(st);
        }

        public static BookDTO GetById(int id) 
        {
            var data = new BookRepo().GetByID(id);
            return GetMapper().Map<BookDTO>(data);
        }

        public static List<BookDTO> Search(string keyword)
        {
            var data = new BookRepo().Search(keyword);
            return GetMapper().Map<List<BookDTO>>(data);
            
        }

        public static List<BookDTO> GetAvailableBooks()
        {
            var data = new BookRepo().GetAvailableBooks();
            return GetMapper().Map<List<BookDTO>>(data);
        }
    }    
}
