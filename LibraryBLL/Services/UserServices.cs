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
    public class UserServices
    {
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>().ReverseMap();
            });
            return new Mapper(config);
        }

        // Authentication
        public static UserDTO Authenticate(string uname, string pass , string email)
        {
            var user = new UserRepo().Authenticate(uname, pass , email);
            return GetMapper().Map<UserDTO>(user); 
        }

        // Admin creates a new user
        public static string CreateUser(UserDTO newUser, UserDTO currentUser)
        {
            if (currentUser.Role != "Admin")
                return "Only admins can create users.";

            newUser.CreatedAt = DateTime.Now;

            var repo = new UserRepo();
            bool success = repo.Create(GetMapper().Map<User>(newUser));

            return success ? "User created successfully." : "Failed to create user.";
        }

        // Admin updates a user
        public static string UpdateUser(UserDTO updatedUser, UserDTO currentUser)
        {
            if (currentUser.Role != "Admin")
                return "Only admins can update users.";

            var repo = new UserRepo();
            bool success = repo.Update(GetMapper().Map<User>(updatedUser));

            return success ? "User updated successfully." : "Failed to update user.";
        }

        // Admin deletes a user
        public static string DeleteUser(int id, UserDTO currentUser)
        {
            if (currentUser.Role != "Admin")
                return "Only admins can delete users.";

            if (id == currentUser.UserID)
                return "Admins cannot delete themselves.";

            bool success = new UserRepo().Delete(id);
            return success ? "User deleted successfully." : "Failed to delete user.";
        }

        // Get all users (admin) or single user (normal)
        public static List<UserDTO> GetAllUsers(UserDTO currentUser)
        {
            if (currentUser.Role != "Admin")
                return new List<UserDTO> { currentUser }; // user can only see own info

            var data = new UserRepo().GetAll();
            return GetMapper().Map<List<UserDTO>>(data);
        }

        // Get a single user by ID
        public static UserDTO GetUserByID(int id, UserDTO currentUser)
        {
            var repo = new UserRepo();
            if (currentUser.Role == "Admin" || currentUser.UserID == id)
            {
                var user = repo.GetByID(id);
                return GetMapper().Map<UserDTO>(user);
            }

            return null; // normal users cannot access other users
        }

        // Change password for logged-in user
        public static string ChangePassword(int userId, string oldPass, string newPass)
        {
            var repo = new UserRepo();
            var user = repo.GetByID(userId);

            if (user == null || user.Password != oldPass)
                return "Old password incorrect.";
            user.Password = newPass;
            bool success = repo.Update(user);
            return success ? "Password changed successfully." : "Failed to change password.";
        }
    }
}
