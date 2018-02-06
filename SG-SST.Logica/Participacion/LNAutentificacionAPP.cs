using SG_SST.Audotoria;
using System;
using System.Globalization;
using System.IO;
using SG_SST.Models;
using System.Configuration;
using System.Web;
using System.Net;
using RestSharp;
using System.Web.Http;
using System.Drawing;
using SG_SST.Interfaces.Participacion;
using SG_SST.InterfazManager.Participacion;


namespace SG_SST.Logica.Participacion
{
    

    public class LNAutentificacionAPP
    {

        private static IAuntentificarApp Reportes = IMReporte.autentificacion();
        public string AutentificarAPP(string nitEmpresa, string documentoEmpleado,string url,string capacidad)
        {
            try
            {

                var cliente = new RestSharp.RestClient(url);
                var request = new RestRequest(capacidad, RestSharp.Method.GET);


                request.Parameters.Clear();
                request.AddParameter("tpEm", "ni");
                request.AddParameter("docEm", nitEmpresa);
                request.AddParameter("tpAfiliado", "cc");
                request.AddParameter("docAfiliado", documentoEmpleado);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");



                //se omite la validación de certificado de SSL
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                IRestResponse<Object> response = cliente.Execute<Object>(request);
                var result = response.Content;
                var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<Object>(result);

                return respuesta.ToString();
            }
            catch (Exception e)
            {
                return "[{}]";
            }
        }

    }
}
