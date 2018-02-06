using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SG_SST.Revision.Servicios
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Revision",
                routeTemplate: "sg-sst/RevisionServicio/{action}/{id}",///es la Url, que se tiene en el Webconfig de la capa presentacion.
                defaults: new
                {
                    controller = "RevisionServicio",////Controlador del servicio
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
