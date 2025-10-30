using LibraryBLL.DTO;
using LibraryBLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartLibraryMVC.Controllers
{
    public class BorrowController : Controller
    {

        //  Borrow a book
        public ActionResult BorrowBooks(int bookId)
        {

            var currentUser = (UserDTO)Session["User"];

            var dto = new UserBorrowDTO
            {
                User = new UserDTO { UserID = currentUser.UserID },
                Books = new BookDTO { BookID = bookId }
            };

            var result = BorrowServices.BorrowBooks(dto);
            TempData["Message"] = result;


            return RedirectToAction("Index", "Book");
        }



        //  Return a book
        [HttpPost]

        public ActionResult ReturnBooks(List<int> selectedBorrows)
        {
            var currentUser = (UserDTO)Session["User"];

            if (selectedBorrows == null || !selectedBorrows.Any())
            {
                TempData["Message"] = "No books selected for return.";
                return RedirectToAction("UserBorrows");
            }

            foreach (var transactionId in selectedBorrows)
            {
                BorrowServices.ReturnBook(transactionId);
            }

            TempData["Message"] = "Selected books returned successfully.";
            return RedirectToAction("UserBorrows");
        }

        //  Remove a borrow record (admin)
        [HttpPost]

        public ActionResult RemoveBorrow(int transactionId, int userId)
        {
            var result = BorrowServices.RemoveBorrow(transactionId);
            TempData["Message"] = result;

            return RedirectToAction("UserBorrows", new { userId });
        }

        // 4️⃣ List all borrows for a user (GET)
        [HttpGet]
        public ActionResult UserBorrows()
        {

            var currentUser = (UserDTO)Session["User"];
            if (currentUser == null)
            {
                return RedirectToAction("Login", "User");
            }

            
            var borrows = BorrowServices.GetBorrowsByUser(currentUser.UserID);

            return View(borrows);
        }

        //  Admin: List all borrows (GET)
        public ActionResult AdminBorrows()
        {
            var role = Session["Role"] as string;
            if (role != "Admin")
            {
                TempData["Message"] = "Access denied. Admin only.";
                return RedirectToAction("AdminIndex", "Book");
            }

            var borrows = BorrowServices.GetAllBorrows();
            return View(borrows);
        }

        //  Admin: Remove a borrow record (POST)

        [HttpPost]
        public ActionResult AdminRemoveBorrow(int transactionId)
        {
            var role = Session["Role"] as string;
            if (role != "Admin")
            {
                TempData["Message"] = "Access denied. Admin only.";
                return RedirectToAction("Index", "Book");
            }

            var result = BorrowServices.RemoveBorrow(transactionId);
            TempData["Message"] = result;

            return RedirectToAction("AdminBorrows");
        }

        // Admin: List borrows by specific user (GET)
        public ActionResult AdminUserBorrows(int userId)
        {
            var role = Session["Role"] as string;
            if (role != "Admin")
            {
                TempData["Message"] = "Access denied. Admin only.";
                return RedirectToAction("Index", "Book");
            }

            var currentUser = (UserDTO)Session["User"];
            var borrows = BorrowServices.GetBorrowsByUser(userId);
            var user = UserServices.GetUserByID(userId, currentUser);
            ViewBag.UserName = user?.FullName ?? "Unknown User";
            return View(borrows);
        }

        // admin: List borrows by specific book (GET)
        public ActionResult AdminBookBorrows(int bookId)
        {
            var role = Session["Role"] as string;
            if (role != "Admin")
            {
                TempData["Message"] = "Access denied. Admin only.";
                return RedirectToAction("Index", "Book");
            }

            var borrows = BorrowServices.GetBorrowsByBook(bookId);
            var book = BookServices.GetById(bookId);
            ViewBag.BookTitle = book?.Title ?? "Unknown Book";
            return View(borrows);
        }

    }
}