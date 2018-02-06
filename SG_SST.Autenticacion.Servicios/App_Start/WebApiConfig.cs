using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SG_SST.Autenticacion.Servicios
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "Autenticacion",
                routeTemplate: "sg-sst/autenticacion/{action}/{id}",
                defaults: new
                {
                    controller = "Autenticacion",
                    id = RouteParameter.Optional
                }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
