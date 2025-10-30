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
    [RoutePrefix("api/borrow")]
    public class BorrowController : ApiController
    {
        [HttpPost]
        [Route("borrow")]
        public HttpResponseMessage BorrowBooks([FromBody] BorrowRequestDTO request)
        {
            if (request == null || request.UserID <= 0 || request.BookID <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid borrow request.");
            }

            var dto = new UserBorrowDTO
            {
                User = new UserDTO { UserID = request.UserID },
                Books = new BookDTO { BookID = request.BookID }
            };

            var result = BorrowServices.BorrowBooks(dto);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("return")]
        public HttpResponseMessage ReturnBooks([FromBody] ReturnRequestDTO request)
        {
            if (request == null || request.TransactionIds == null || !request.TransactionIds.Any())
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No books selected for return.");
            }

            foreach (var transactionId in request.TransactionIds)
            {
                BorrowServices.ReturnBook(transactionId);
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Selected books returned successfully.");
        }

        [HttpDelete]
        [Route("{transactionId}")]
        public HttpResponseMessage RemoveBorrow(int transactionId, int userId)
        {
            if (transactionId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid transaction ID.");
            }

            var result = BorrowServices.RemoveBorrow(transactionId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("user/{userId}")]
        public HttpResponseMessage UserBorrows(int userId)
        {
            if (userId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid user ID.");
            }

            var borrows = BorrowServices.GetBorrowsByUser(userId);
            return Request.CreateResponse(HttpStatusCode.OK, borrows);
        }

        [HttpGet]
        [Route("all")]
        public HttpResponseMessage AdminBorrows()
        {
            var borrows = BorrowServices.GetAllBorrows();
            return Request.CreateResponse(HttpStatusCode.OK, borrows);
        }

        [HttpDelete]
        [Route("admin/{transactionId}")]
        public HttpResponseMessage AdminRemoveBorrow(int transactionId)
        {
            if (transactionId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid transaction ID.");
            }

            var result = BorrowServices.RemoveBorrow(transactionId);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("admin/user/{userId}")]
        public HttpResponseMessage AdminUserBorrows(int userId)
        {
            if (userId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid user ID.");
            }

            var borrows = BorrowServices.GetBorrowsByUser(userId);
            return Request.CreateResponse(HttpStatusCode.OK, borrows);
        }

        [HttpGet]
        [Route("admin/book/{bookId}")]
        public HttpResponseMessage AdminBookBorrows(int bookId)
        {
            if (bookId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid book ID.");
            }

            var borrows = BorrowServices.GetBorrowsByBook(bookId);
            return Request.CreateResponse(HttpStatusCode.OK, borrows);
        }

    }
}
