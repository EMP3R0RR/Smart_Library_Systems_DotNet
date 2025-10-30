using LibraryDAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDAL.Repos
{
    public class CategoryRepo
    {

        SmartLibraryEntities db;

        public CategoryRepo()
        {
            db = new SmartLibraryEntities();
        }
        public List<Category> GetAll()
        {
            return db.Categories.ToList();
        }

        public Category GetByID(int id)
        {
            return db.Categories.Find(id);
        }

    }
}
