using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MayBay.Areas.Admin.Controllers;
using MayBay.Controllers;

namespace MayBay
{

    public class RouteConfig
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
     name: "AdminArea_default",
     url: "Admin/{controller}/{action}/{id}",
     defaults: new { area = "Admin", controller = "Home", action = "Index", id = UrlParameter.Optional },
     namespaces: new[] { typeof(HomeController).Namespace }
 ).DataTokens["area"] = "Admin";

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );


                    }
    }
}
    