using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryBLL.Services;
using LibraryBLL.DTO;

namespace SmartLibraryMVC.Controllers
{
    public class BookController : Controller
    {
        // show books both for user and admin
        public ActionResult Index()
        {
            var books = BookServices.Get();
            return View(books);
        }

        public ActionResult AdminIndex()
        {
            var books = BookServices.Get();
            return View(books);
        }

        // GET: Books/Details/5 both for user and admin
        public ActionResult Details(int id)
        {
            var book = BookServices.GetById(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult AdminDetails(int id)
        {
            var book = BookServices.GetById(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create only for admin
        public ActionResult Create()
        {

            ViewBag.Categories = new SelectList(CategoryServices.GetAll(), "CategoryID", "Name");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        public ActionResult Create(BookDTO model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedAt = DateTime.Now;
                var success = BookServices.Create(model);
                if (success)
                    return RedirectToAction("AdminIndex");
            }

            ViewBag.Categories = new SelectList(CategoryServices.GetAll(), "CategoryID", "Name", model.CategoryID);
            return View(model);
        }

        // GET: Books/Edit/5 only for admin
        public ActionResult Edit(int id)
        {
            var book = BookServices.GetById(id);
            if (book == null)
                return HttpNotFound();
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        
        public ActionResult Edit(BookDTO model)
        {
            if (ModelState.IsValid)
            {
                var success = BookServices.Update(model);
                if (success)
                    return RedirectToAction("AdminIndex");
                ModelState.AddModelError("", "Could not update book.");
            }
            return View(model);
        }

        public ActionResult Delete(int id) // only for admin
        {
            var book = BookServices.GetById(id);
            if (book == null)
                return HttpNotFound();
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost]
        
        public ActionResult DeleteConfirmed(int id)
        {
            var book = BookServices.GetById(id);
            if (book == null)
                return HttpNotFound();

            var success = BookServices.Delete(book);
            return RedirectToAction("Index");
        }

        // GET: Books/Search?keyword=abc
        public ActionResult Search(string keyword)
        {
            var books = BookServices.Search(keyword ?? string.Empty);
            return View("Index", books); 
        }

        public ActionResult AdminSearch(string keyword)
        {
            var books = BookServices.Search(keyword ?? string.Empty);
            return View("AdminIndex", books);
        }

        // GET: Books/Available
        public ActionResult Available()
        {
            var books = BookServices.GetAvailableBooks();
            return View("Index", books);
        }

        public ActionResult AdminBookAvailable()
        {
            var books = BookServices.GetAvailableBooks();
            return View("AdminIndex", books);
        }
    }

}
 