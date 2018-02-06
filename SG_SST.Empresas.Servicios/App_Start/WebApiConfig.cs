using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SG_SST.Empresas.Servicios
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Empresas",///Cualquier nombre
                routeTemplate: "sg-sst/Empresa/{action}/{id}",///es la Url, que se tiene en el WEbconfig de la capa presentacion.
                defaults: new
                {
                    controller = "Empresa",////Controlador del servicio
                    id = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "Relacionlaboral",///Cualquier nombre
                routeTemplate: "sg-sst/Relacionlaboral/{action}/{id}",///es la Url, que se tiene en el WEbconfig de la capa presentacion.
                defaults: new
                {
                    controller = "RelacionesLaborales",////Controlador del servicio
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
