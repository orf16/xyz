﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SG_SST.EnfermedadLaboral.Servicios
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "EnfermedadLaboral",
                routeTemplate: "sg-sst/enfermedad-laboral/{action}/{id}",
                defaults: new
                {
                    controller = "EnfermedadLaboral",
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