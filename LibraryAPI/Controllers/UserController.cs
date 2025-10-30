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
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login([FromBody] LoginDTO credentials)
        {
            if (credentials == null || string.IsNullOrEmpty(credentials.Username) || string.IsNullOrEmpty(credentials.Password))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid credentials.");
            }

            var user = UserServices.Authenticate(credentials.Username, credentials.Password, credentials.Email);
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }

            return Request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid username or password.");
        }

        [HttpPost]
        [Route("logout")]
        public HttpResponseMessage Logout()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Logged out successfully.");
        }

        [HttpGet]
        [Route("all")]
        public HttpResponseMessage GetAllUsers()
        {
            var users = UserServices.GetAllUsers(new UserDTO { Role = "Admin" });  
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        [HttpGet]
        [Route("profile/{id}")]
        public HttpResponseMessage GetUserProfile(int id)
        {
            var currentUser = new UserDTO { UserID = id };  
            var user = UserServices.GetUserByID(id, currentUser);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        [HttpGet]
        [Route("details/{id}")]
        public HttpResponseMessage GetUserDetails(int id)
        {
            var currentUser = new UserDTO { Role = "Admin" };  // Simplified for access
            var user = UserServices.GetUserByID(id, currentUser);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }

        [HttpPut]
        [Route("changepassword/{id}")]
        public HttpResponseMessage ChangePassword(int id, [FromBody] ChangePasswordDTO request)
        {
            if (request == null || string.IsNullOrEmpty(request.OldPassword) || string.IsNullOrEmpty(request.NewPassword))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid password data.");
            }

            var currentUser = new UserDTO { UserID = id };  
            var message = UserServices.ChangePassword(id, request.OldPassword, request.NewPassword);
            return Request.CreateResponse(HttpStatusCode.OK, message);
        }
    }
}
