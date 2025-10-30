using LibraryDAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace LibraryDAL.Repos
{
    public class BorrowRepo
    {
        SmartLibraryEntities db;

        public BorrowRepo()
        {
            db = new SmartLibraryEntities();
        }

        // 1. Create a new borrow record
        public bool Create(BorrowTransaction entity)
        {
            db.BorrowTransactions.Add(entity);
            return db.SaveChanges() > 0;
        }

        // 2. Get all borrow records by user
        public List<BorrowTransaction> GetByUserId(int userId)
        {
            return db.BorrowTransactions
    .Include(b => b.Book)
    .Include(b => b.Status)
    .Include(b => b.User)
    .Where(b => b.UserID == userId)
    .ToList();
        }

        // 3. Update a borrow record (e.g. return date, status)
        public bool Update(BorrowTransaction entity)
        {
            var ex = db.BorrowTransactions.Find(entity.TransactionID);
            db.Entry(ex).CurrentValues.SetValues(entity);
            return db.SaveChanges() > 0;
        }

        // 4. Delete a borrow record (remove a borrow request completely)
        public bool Delete(int borrowId)
        {
            var ex = db.BorrowTransactions.Find(borrowId);
            db.BorrowTransactions.Remove(ex);
            return db.SaveChanges() > 0;
        }

        // 5. Get single borrow by ID
        public BorrowTransaction Get(int id)
        {
            return db.BorrowTransactions.Find(id);
        }

        // Get all borrow records (for admin)
        public List<BorrowTransaction> GetAll()
        {
            return db.BorrowTransactions
                .Include(b => b.Book)
                .Include(b => b.Status)
                .Include(b => b.User)
                .ToList();
        }

        public List<BorrowTransaction> GetByBookId(int bookId)
        {
            return db.BorrowTransactions
                .Include(b => b.Book)
                .Include(b => b.Status)
                .Include(b => b.User)
                .Where(b => b.BookID == bookId)
                .ToList();
        }
    }
}
