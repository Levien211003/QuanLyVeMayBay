using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MayBay.Controllers;

namespace MayBay
{
   
        public class RouteConfig
        {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "KhachHang", action = "TrangChu", id = UrlParameter.Optional }
            );
        }
        }
    }

