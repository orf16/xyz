﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace SG_SST
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
          // config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });


           // config.Formatters.JsonFormatter.SupportedMediaTypes
           //.Add(new MediaTypeHeaderValue("text/html") );

           // var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
           // config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

        }
    }
}