using BLL.DTO;
using BLL.Interface;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace PL.Controllers
{
    /// <summary>Class AccountController.
    /// Implements the <see cref="System.Web.Http.ApiController"/></summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class AccountController : ApiController
    {
        private IUserService UserService;
        /// <summary>Initializes a new instance of the <see cref="T:PL.Controllers.AccountController"/> class.</summary>
        /// <param name="_myUserService">My user service.</param>
        public AccountController(IUserService _myUserService)
        {
            UserService = _myUserService;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        /// <summary>Logins the specified model.</summary>
        /// <param name="model">The model.</param>
        /// <returns>IHttpActionResult.</returns>
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("api/login")]
        public IHttpActionResult Login(LoginModel model)
        {
           
                ClaimsIdentity claim = null;
                dynamic response = null;
                if (ModelState.IsValid)
                {
                    UserDTO userDto = new UserDTO { UserName = model.UserName, Password = model.Password };
                    claim = UserService.Authenticate(userDto);
                    if (claim == null)
                    {
                    var message = string.Format("Неверный логин или пароль.");
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, message));
                    }
                    else
                    {
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claim);
                        response = UserService.GetJwt(userDto);
                    }

                }
            return Ok(response);
        }

        /// <summary>Logouts this instance.</summary>
        /// <returns>IHttpActionResult.</returns>
        public IHttpActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return Ok();
        }

        /// <summary>Registers the specified model.</summary>
        /// <param name="model">The model.</param>
        /// <returns>IHttpActionResult.</returns>
        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        [System.Web.Http.Route("api/signup")]
        public IHttpActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Password = model.Password,
                    UserName = model.UserName,
                    LastName=model.LastName,
                    FirstName=model.FirstName,
                    Role = "user"
                };
                UserService.Create(userDto);
            }
            return Ok();
        }
        /// <summary>Sets the initial data.</summary>
        private void SetInitialData()
        {
            UserService.SetInitialData(new UserDTO
            {
                UserName = "someuser",
                Password = "admin",
                Role = "admin",
            }, new List<string> { "user", "admin" });
             UserService.SetInitialData(new UserDTO
            {
                UserName = "moderator",
                Password = "1111",
                Role = "moderator",
            }, new List<string> { "user", "moderator" });
        }
    }
}
