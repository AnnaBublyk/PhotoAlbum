using BLL.Interface;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PL.App_Start.Startup))]
namespace PL.App_Start
{

    public class Startup
    {
         IUserService userService;
        public void Configuration(IAppBuilder app)
        {

        }


    }
}