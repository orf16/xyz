using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SG_SST.MedicionEvaluacion.Servicios
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
              name: "PlanAccionServicio",
              routeTemplate: "sg-sst/PlanAccionServicio/{action}/{id}",///es la Url, que se tiene en el Webconfig de la capa presentacion.
                defaults: new
                {
                    controller = "PlanAccionServicio",////Controlador del servicio
                    id = RouteParameter.Optional
                });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
