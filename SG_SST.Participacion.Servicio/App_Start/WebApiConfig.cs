using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SG_SST.Participacion.Servicios
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes.
            //config.MapHttpAttributeRoutes();
        

        config.Routes.MapHttpRoute(
        name: "Participacion",
         routeTemplate: "sg-sst/ReporteServicio/{action}/{id}",///es la Url, que se tiene en el Webconfig de la capa presentacion.
           defaults: new
           {
               controller = "ReporteServicio",////Controlador del servicio
               id = RouteParameter.Optional
           }
    );

        
        }
    }
}
