using LibraryBLL.DTO;
using LibraryBLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartLibraryMVC.Controllers
{
    public class UserController : Controller
    {
        
        public ActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        [HttpPost]
        public ActionResult Login(string username, string password , string email)
        {
            var user = UserServices.Authenticate(username, password , email);
            if (user != null)
            {
                Session["User"] = user; 
                Session["Role"] = user.Role;
                if (user.Role == "Admin")
                {
                    return RedirectToAction("AdminIndex", "Book");
                }
                else
                {
                    return RedirectToAction("Index", "Book");
                } // redirect to home/dashboard
            }

            ViewBag.Message = "Invalid username or password";
            return View();
        }

        // GET: Users/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        // GET: Users/Index for admin
        public ActionResult AllUser()
        {
            var currentUser = (UserDTO)Session["User"];
            if (currentUser == null)
                return RedirectToAction("Login");

            var users = UserServices.GetAllUsers(currentUser);
            return View(users);
        }

        public ActionResult MyProfile()
        {
            var currentUser = (UserDTO)Session["User"];
            if (currentUser == null)
                return RedirectToAction("Login");

            var user = UserServices.GetUserByID(currentUser.UserID, currentUser);
            return View(user);
        }

        public ActionResult AdminProfile()
        {
            var currentUser = (UserDTO)Session["User"];
            if (currentUser == null)
                return RedirectToAction("Login");

            var user = UserServices.GetUserByID(currentUser.UserID, currentUser);
            return View(user);
        }

        
        // GET: Users/Delete/5
        public ActionResult DeleteUser(int id)
        {
            var currentUser = (UserDTO)Session["User"];
            if (currentUser == null || currentUser.Role != "Admin")
                return RedirectToAction("Login");

            var user = UserServices.GetUserByID(id, currentUser);
            if (user == null)
                return RedirectToAction("AllUser");

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var currentUser = (UserDTO)Session["User"];
            if (currentUser == null || currentUser.Role != "Admin")
                return RedirectToAction("Login");

            string message = UserServices.DeleteUser(id, currentUser);
            TempData["Message"] = message;

            return RedirectToAction("AllUser");
        }

        // GET: Users/ChangePassword
        public ActionResult ChangePassword()
        {
            var currentUser = (UserDTO)Session["User"];
            if (currentUser == null)
                return RedirectToAction("Login");

            return View();
        }

        // POST: Users/ChangePassword
        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword)
        {
            var currentUser = (UserDTO)Session["User"];
            if (currentUser == null)
                return RedirectToAction("Login");

            string message = UserServices.ChangePassword(currentUser.UserID, oldPassword, newPassword);
            TempData["Message"] = message;

            return RedirectToAction("ChangePassword");
        }

        public ActionResult AdminChangePassword()
        {
            var currentUser = (UserDTO)Session["User"];
            if (currentUser == null)
                return RedirectToAction("Login");

            return View();
        }

        // POST: Admin/ChangePassword
        [HttpPost]
        public ActionResult AdminChangePassword(string oldPassword, string newPassword)
        {
            var currentUser = (UserDTO)Session["User"];
            if (currentUser == null)
                return RedirectToAction("Login");

            string message = UserServices.ChangePassword(currentUser.UserID, oldPassword, newPassword);
            TempData["Message"] = message;

            return RedirectToAction("ChangePassword");
        }

        // GET: Users/Details/5
        public ActionResult UserDetails(int id)
        {
            var currentUser = (UserDTO)Session["User"];
            if (currentUser == null || currentUser.Role != "Admin")
                return RedirectToAction("Login");

            var user = UserServices.GetUserByID(id, currentUser);
            if (user == null)
                return RedirectToAction("Index");

            return View(user);
        }
    }
}