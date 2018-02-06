using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SG_SST.Planeacion.Servicios
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
            name: "PerfilsocioDemoGrafico",
            routeTemplate: "sg-sst/planeacion/{action}/{id}",
            defaults: new
            {
                controller = "PerfilSocioDemografico",
                id = RouteParameter.Optional
            });


            config.Routes.MapHttpRoute(
                name: "EvaluacionInicial",
                routeTemplate: "sg-sst/evaluacion/{action}/{id}",
                defaults: new
                {
                    controller = "Evaluacion",
                    id = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "ReportesAusentismo",
                routeTemplate: "sg-sst/ausentismo/{action}/{id}",
                defaults: new
                {
                    controller = "ReportesAusentismo",
                    id = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "Objetivosst",
                routeTemplate: "sg-sst/objetivo/{action}/{id}",
                defaults: new
                {
                    controller = "ObjetivoSST",
                    id = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "IndicadoresAusentismo",
                routeTemplate: "sg-sst/indicadores/{action}/{id}",
                defaults: new
                {
                    controller = "Indicadores",
                    id = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
               name: "Ausentismo",
               routeTemplate: "sg-sst/cargueausentismo/{action}/{id}",
               defaults: new
               {
                   controller = "Ausentismo",
                   id = RouteParameter.Optional
               }
           );


            config.Routes.MapHttpRoute(
               name: "ConfigAusentismo",
               routeTemplate: "sg-sst/configuracionhht/{action}/{id}",
               defaults: new
               {
                   controller = "Configuracionhht",
                   id = RouteParameter.Optional
               }
           );


            config.Routes.MapHttpRoute(
              name: "ReportesEvaluacionEstandaresMinimos",
              routeTemplate: "sg-sst/reportes/{action}/{id}",
              defaults: new
              {
                  controller = "Reportes",
                  id = RouteParameter.Optional
              }
          );

            config.Routes.MapHttpRoute(
               name: "Metodologias",
               routeTemplate: "sg-sst/Metodologia/{action}/{id}",
               defaults: new
               {
                   controller = "Metodologias",
                   id = RouteParameter.Optional
               }
           );

            config.Routes.MapHttpRoute(
               name: "Peligros",
               routeTemplate: "sg-sst/peligro/{action}/{id}",
               defaults: new
               {
                   controller = "Peligro",
                   id = RouteParameter.Optional
               }
           );

            config.Routes.MapHttpRoute(
               name: "DxGralSalud",
               routeTemplate: "sg-sst/DxDeCondicionSalud/{action}/{id}/{sid}",
               defaults: new
               {
                   controller = "DxDeCondicionSalud",
                   id = RouteParameter.Optional,
                   sid = RouteParameter.Optional
               }
           );

            config.Routes.MapHttpRoute(
           name: "EstudioPuestoTrabajo",
           routeTemplate: "sg-sst/EstudioPuestoTrabajo/{action}/{id}",///es la Url, que se tiene en el Webconfig de la capa presentacion.
            defaults: new
            {
                controller = "EstudioPuestosTrabajo",////Controlador del servicio
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
