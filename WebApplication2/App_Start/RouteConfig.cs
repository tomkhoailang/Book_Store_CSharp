using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
               name: "StockReceivedNoteDetail",
               url: "Manager/STOCK_RECEIVED_NOTE_DETAIL/Index/{stockReceivedNoteID}",
              defaults: new { area = "Manager", controller = "STOCK_RECEIVED_NOTE_DETAIL", action = "Index" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "BOOK_EDITION", action = "Index", Areas = "Manager", id = UrlParameter.Optional },
                new[] { "WebApplication2.Controllers" }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
            //).DataTokens.Add("Area", "Manager");
        }
    }
}
