using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SG_SST.Participacion
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
            name: "Comite",
            routeTemplate: "sg-sst/ComiteServicio/{action}/{id}",///es la Url, que se tiene en el Webconfig de la capa presentacion.
              defaults: new
              {
                  controller = "ComiteServicio",////Controlador del servicio
                  id = RouteParameter.Optional
              }
         );

       config.Routes.MapHttpRoute(
       name: "Participacion",
       routeTemplate: "sg-sst/ReporteServicio/{action}/{id}",///es la Url, que se tiene en el Webconfig de la capa presentacion.
         defaults: new
         {
             controller = "ReporteServicio",////Controlador del servicio
             id = RouteParameter.Optional
         }
    );


            config.Routes.MapHttpRoute(
            name: "ConsultaSST",
            routeTemplate: "sg-sst/ConsultaSSTServicio/{action}/{id}",///es la Url, que se tiene en el Webconfig de la capa presentacion.
            defaults: new
            {
                controller = "ConsultaSSTServicio",////Controlador del servicio
                id = RouteParameter.Optional
            }
            );


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        ////    config.Routes.MapHttpRoute(
        ////   name: "Participacion",
        ////   routeTemplate: "sg-sst/ReporteServicio/{action}/{id}",///es la Url, que se tiene en el Webconfig de la capa presentacion.
        ////     defaults: new
        ////     {
        ////         controller = "ReporteServicio",////Controlador del servicio
        ////         id = RouteParameter.Optional
        ////     }
        ////);
        }
    }
}
