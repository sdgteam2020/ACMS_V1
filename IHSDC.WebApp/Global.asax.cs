using IHSDC.Common.Helpers.Database;
using IHSDC.Common.Models;
using IHSDC.WebApp.Controllers;
using IHSDC.Common.Helpers.MVC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using IHSDC.WebApp.Services;
using System.IO;

namespace IHSDC.WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string DocPath = "";
        public static Dictionary<string, SearchService> DocPathServices = new Dictionary<string, SearchService>();
        public static string[] AllPath;
        protected void Application_Start()
        {
            ApplicationDatabaseHelper.Initialize();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            string rootPath = Server.MapPath("~/Policy");
            string[] uploadsDirectoryPath = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly);
            AllPath = uploadsDirectoryPath;
            foreach (var item in uploadsDirectoryPath)
            {
                DocPathServices[item] = new SearchService(SearchService.InitializeSearchData(item));
            }
            DependencyResolver.SetResolver(new MyDependencyResolver(DocPathServices));
            

        }

        public class MyDependencyResolver : IDependencyResolver
        {
            private readonly Dictionary<string, SearchService> _docPathServices;

            public MyDependencyResolver(Dictionary<string, SearchService> docPathServices)
            {
                _docPathServices = docPathServices;
            }

            public object GetService(Type serviceType)
            {
                if (serviceType == typeof(PolicyCornerController))
                {
                    try
                    {
                        //call controller to initialize this service for controller
                        return new PolicyCornerController(AllPath[0], _docPathServices);
                    }
                    catch 
                    {
                        return new PolicyCornerController(null, _docPathServices);
                    }
                }

                return null;
            }



            public IEnumerable<object> GetServices(Type serviceType)
            {
                return new List<object>();
            }
        }

        public void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //string referer = Request.ServerVariables["HTTP_REFERER"];
            //string login = Request.CurrentExecutionFilePath;
            //if (referer == null && login != "/")
            //{
            //    if ((referer == null && login != "/Account/Login"))
            //    {
            //        Response.Redirect("/Account/Login");
            //    }
            //}
        }
    }

}
