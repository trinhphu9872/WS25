using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WatchStore25
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                         name: "AdHome",
                         url: "{controller}/{action}",
                         defaults: new { controller = "Admin", action = "HomeAdmin" }
                     );
            routes.MapRoute(
                         name: "AdCus",
                         url: "{controller}/{action}",
                         defaults: new { controller = "Admin", action = "CustomerManager" }
                     );
            routes.MapRoute(
                         name: "AdOrder",
                         url: "{controller}/{action}",
                         defaults: new { controller = "Admin", action = "OrderManager" }
                     );
            routes.MapRoute(
                         name: "AdProduct",
                         url: "{controller}/{action}",
                         defaults: new { controller = "Admin", action = "ProductManager" }
                     );
            routes.MapRoute(
                           name: "AdAddProduct",
                           url: "{controller}/{action}",
                           defaults: new { controller = "Admin", action = "AddNewProductManager" }
                       );
        }
    }
}
