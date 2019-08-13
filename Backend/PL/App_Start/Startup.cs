using BLL.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(PL.App_Start.Startup))]
namespace PL.App_Start
{
    public class Startup
    {
        private IUserService userService;
        public Startup() { }
        public Startup(IUserService _myUserService)
        {
            userService = _myUserService;
        }

        public void Configuration(IAppBuilder app)
        {
        }

        private IUserService CreateUserService()
        {
            return userService;
        }
    }
}