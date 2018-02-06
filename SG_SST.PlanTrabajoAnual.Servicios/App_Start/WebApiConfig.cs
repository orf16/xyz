using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.WebHost;


namespace SG_SST.PlanTrabajoAnual.Servicios
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "PlanEmpresas",///Cualquier nombre
            //    routeTemplate: "sg-sst/PlanAnualEmpresa/{action}/{id}",///es la Url, que se tiene en el WEbconfig de la capa presentacion.
            //    defaults: new
            //    {
            //        controller = "PlanAnualEmpresa",////Controlador del servicio
            //        id = RouteParameter.Optional
            //    }
            //);

            config.Routes.MapHttpRoute(
                name: "ComunicacionesAPP",///Cualquier nombre
                routeTemplate: "sg-sst/ComunicadosAPP/{action}/{id}",///es la Url, que se tiene en el WEbconfig de la capa presentacion.
                defaults: new
                {
                    controller = "ComunicadosAPP",////Controlador del servicio
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
