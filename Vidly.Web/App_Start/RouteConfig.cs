using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Vidly.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // recommendation is to use attribute routes
            // this will allow us to use annotations above actions in controllers
            // to specify custom rout
            routes.MapMvcAttributeRoutes();

            // routes order is important
            // from more specific to the most generic one
            // so usually we add new routes above the default route

            /*routes.MapRoute(
                name: "MoviesByReleaseDate",
                url: "movies/released/{year}/{month}",
                defaults: new { controller = "Movies", action = "ByReleaseDate" },
                // adding constraints, year 4 digits, and month can be 4 or 7
                new { year = @"\d{4}", month = @"4|7" }
            );*/

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
