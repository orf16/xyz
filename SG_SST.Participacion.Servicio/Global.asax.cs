using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace SG_SST.Participacion.Servicios
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
           GlobalConfiguration.Configure(WebApiConfig.Register);
           // System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
