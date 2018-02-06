using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;



namespace SG_SST
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            // "ModuloEmpresa",
            // "modulo-empresa/obtener-empresa",
            //     defaults: new { controller = "Empresas", action = "Create", id = UrlParameter.Optional }
            // );

            //routes.MapRoute
            //("Inspeccion",
            // "modulo-aplicacion/Planear-Inspeccion",
            //     defaults: new { controller = "PlandeInspeccion", action = "CrearPlaneacionInspeccion", id = UrlParameter.Optional }
            // );
          

            routes.MapRoute(
                name:"Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Login", id = UrlParameter.Optional });
        }
    }
}
