using LibraryDAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDAL.Repos
{
    public class UserRepo
    {
        SmartLibraryEntities db;

        public UserRepo()
        {
            db = new SmartLibraryEntities();
        }
        public User Authenticate(string uname, string pass , string email)
        {
            var user = (from u in db.Users
                        where u.FullName.Equals(uname) &&
                        u.Password.Equals(pass) &&
                        u.Email.Equals(email)
                        select u).SingleOrDefault();

            return user;
        }

        public List<User> GetAll()
        {
            return db.Users.ToList();
        }

        public User GetByID(int id)
        {
            return db.Users.Find(id);
        }

        public bool Create(User u)
        {
            db.Users.Add(u);
            return db.SaveChanges() > 0;
        }

        public bool Update(User u)
        {
            var ex = db.Users.Find(u.UserID);
            if (ex != null)
            {
                db.Entry(ex).CurrentValues.SetValues(u);
                return db.SaveChanges() > 0;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var ex = db.Users.Find(id);
            if (ex != null)
            {
                db.Users.Remove(ex);
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}
