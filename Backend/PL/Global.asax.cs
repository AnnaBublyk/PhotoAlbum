using BLL.Infrastructure;
using DAL.DataModel;
using Ninject;
using Ninject.Modules;
using Ninject.Web.WebApi;
using PL.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PL
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PhotoAlbumContext>());
            using (var context = new PhotoAlbumContext("PhotoAlbum1"))
            {
                context.Database.Initialize(force: true);
            }
        }
    }
}
