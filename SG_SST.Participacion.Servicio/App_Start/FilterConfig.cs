using System.Web;
using System.Web.Mvc;


namespace SG_SST.Participacion.Servicios
{
    public class FilterConfig
    { 
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
