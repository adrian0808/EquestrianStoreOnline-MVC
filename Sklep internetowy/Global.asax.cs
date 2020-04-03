using Sklep_internetowy.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Sklep_internetowy
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundle(BundleTable.Bundles);

            BundleTable.EnableOptimizations = true;
        }
    }
}
