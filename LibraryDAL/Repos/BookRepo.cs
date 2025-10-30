using LibraryDAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDAL.Repos
{
    public class BookRepo
    {
        SmartLibraryEntities db;
        
        public BookRepo() 
        {
            db= new SmartLibraryEntities();
        }
        public List<Book> Get()
        {
            return db.Books.ToList();
        }

        public bool Create(Book s)
        {
            db.Books.Add(s);
            return db.SaveChanges() > 0;
        }

        public bool Delete(Book s)
        {
            var ex = db.Books.Find(s.BookID);
            db.Books.Remove(ex);
            return db.SaveChanges() > 0;
        }

        public bool Update(Book s)
        {
            var ex = db.Books.Find(s.BookID);
            db.Entry(ex).CurrentValues.SetValues(s);
            return db.SaveChanges() > 0;
        }

        public Book GetByID(int id)
        {
            return db.Books.Find(id);
        }

        public List<Book> Search(string keyword)
        {
            return db.Books.Where(s => s.Title.Contains(keyword) 
            || s.Author.Contains(keyword) 
            || s.ISBN.Contains(keyword)).ToList();
        }

        public List<Book> GetByCategory(int categoryId)
        {
            return db.Books.Where(b => b.CategoryID == categoryId).ToList();
        }

        public List<Book> GetAvailableBooks()
        {
            return db.Books.Where(b => b.AvailableCopies > 0).ToList();
        }
    }
}
