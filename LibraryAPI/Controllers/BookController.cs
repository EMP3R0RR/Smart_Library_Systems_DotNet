using LibraryBLL.DTO;
using LibraryBLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryAPI.Controllers
{
    [RoutePrefix("api/book")]
    public class BookController : ApiController
    {
        
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAll()
        {
            var data = BookServices.Get();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("details/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            var book = BookServices.GetById(id);
            if (book == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, book);
        }


       

        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(BookDTO model)
        {
            
            if (model == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid request body: Model is null.");
            }

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);  
            }

            model.CreatedAt = DateTime.Now;
            var success = BookServices.Create(model);
            if (success)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Book created successfully.");
            }

            
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Failed to create book.");
        }


        [HttpPut]
        [Route("edit/{id}")]
        public HttpResponseMessage Update(int id, BookDTO model) //issues
        {
            if (!ModelState.IsValid || model.BookID != id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var success = BookServices.Update(model);
            if (success)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Book updated successfully.");
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Failed to update book.");
        }

        
        [HttpDelete]
        [Route("delete/{id}")]
        public HttpResponseMessage Delete(int id) //cant execute as book has existing borrow records in db
        {
            var book = BookServices.GetById(id);
            if (book == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var success = BookServices.Delete(book);
            if (success)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Book deleted successfully.");
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError, "Failed to delete book.");
        }

        [HttpGet]
        [Route("search")]
        public HttpResponseMessage Search(string keyword = "")
        {
            var data = BookServices.Search(keyword ?? string.Empty);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpGet]
        [Route("available")]
        public HttpResponseMessage GetAvailable()
        {
            var data = BookServices.GetAvailableBooks();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
    }
}
    

