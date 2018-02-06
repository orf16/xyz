using System.Web.Http;
using System.Web;

namespace SG_SST.Aplicacion.Servicios
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
             name: "Inspeccion",
              routeTemplate: "sg-sst/TipoInspeccion/{action}/{id}",///es la Url, que se tiene en el Webconfig de la capa presentacion.
                defaults: new
                {
                    controller = "Inspecciones",////Controlador del servicio
                    id = RouteParameter.Optional
                }
         );

            config.Routes.MapHttpRoute(
            name: "PlanInspeccion",
             routeTemplate: "sg-sst/PlanInspeccion/{action}/{id}",///es la Url, que se tiene en el Webconfig de la capa presentacion.
               defaults: new
               {
                   controller = "Inspecciones",////Controlador del servicio
                   id = RouteParameter.Optional
               }
        );



                config.Routes.MapHttpRoute(
                name: "GestionDelCambio",
                routeTemplate: "sg-sst/GestionDelCambio/{action}/{id}",///es la Url, que se tiene en el Webconfig de la capa presentacion.
                defaults: new
                {
                    controller = "GestionDelCambio",////Controlador del servicio
                    id = RouteParameter.Optional
                }
                );

                config.Routes.MapHttpRoute(
                   name: "AdquisicionDeBienes",
                    routeTemplate: "sg-sst/AdquisicionDeBienes/{action}/{id}",///es la Url, que se tiene en el Webconfig de la capa presentacion.
                      defaults: new
                      {
                          controller = "AdquisicionDeBienes",////Controlador del servicio
                          id = RouteParameter.Optional
                      }
               );

                config.Routes.MapHttpRoute(
                   name: "CriteriosDeSST",
                    routeTemplate: "sg-sst/CriteriosDeSST/{action}/{id}",///es la Url, que se tiene en el Webconfig de la capa presentacion.
                      defaults: new
                      {
                          controller = "CriteriosDeSST",////Controlador del servicio
                          id = RouteParameter.Optional
                      }
               );

            config.Routes.MapHttpRoute(
                   name: "AdmoEPP",
                    routeTemplate: "sg-sst/AdmoEPP/{action}/{id}",///es la Url, que se tiene en el Webconfig de la capa presentacion.
                      defaults: new
                      {
                          controller = "AdmoEPP",////Controlador del servicio
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
