using RestSharp;
using RestSharp.Authenticators;
using SG_SST.Controllers.Base;
using SG_SST.Models.Ausentismo;
using SG_SST.Models.Login;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Controllers.Ausentismo
{
    public class AfiliadoController : BaseController
    {
        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ActionResult Afiliado()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroDocumento"></param>
        /// <returns></returns>
        //[HttpPost]
        //public JsonResult ConsultarAfiliado(string numeroDocumento)
        //{
        //    try
        //    {
        //        var datos = string.Empty;
        //        if (!string.IsNullOrEmpty(numeroDocumento))
        //        {
        //            var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
        //            var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
        //            request.RequestFormat = DataFormat.Xml;
        //            request.Parameters.Clear();
        //            request.AddParameter("tpDoc", "cc");
        //            request.AddParameter("doc", numeroDocumento);
        //            request.AddHeader("Content-Type", "application/json");
        //            request.AddHeader("Accept", "application/json");

        //            //se omite la validación de certificado de SSL
        //            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        //            IRestResponse<List<AfiliadoModel>> response = cliente.Execute<List<AfiliadoModel>>(request);
        //            var result = response.Content;
        //            if (!string.IsNullOrWhiteSpace(result))
        //            {
        //                var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AfiliadoModel>>(result);
        //                var afiliado = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
        //                if (afiliado == null)
        //                    return Json(new { Data = "El afiliado asociado al documento ingresado se encuentra inactivo.", Mensaje = "NOVALIDO" });
        //                //afiliado = respuesta.FirstOrDefault();
        //                else
        //                {
        //                    GuardarSesionAfiliado(afiliado);
        //                    datos = RenderRazorViewToString("_DatosTrabajador", afiliado);
        //                }
        //            }
        //            else
        //                datos = result;
        //        }
        //        if (datos != "")
        //        {
        //            return Json(new { Data = datos, Mensaje = "Success" });
        //        }
        //        else
        //            return Json(new { Data = string.Empty, status = "Warning", Mensaje = "No se encontró ningun afiliado con el número de documento ingresado" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { Data = string.Empty, Mensaje = "No se logró establecer una conexión con SIARP" });
        //    }
        //}
    }
}