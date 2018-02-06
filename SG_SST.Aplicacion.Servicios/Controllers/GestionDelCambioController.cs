using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.Logica;
using SG_SST.Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SG_SST.Logica.Aplicacion;
using SG_SST.Audotoria;


namespace SG_SST.Aplicacion.Servicios.Controllers
{
    public class GestionDelCambioController : ApiController
    {       

       
        [HttpPost]
        [ActionName("GestionDelCambio-grabar")]
        public HttpResponseMessage GrabarPerfilSocioDemografico(EDGestionDelCambio EDPerfil)
        {
            try
            {

                LNGestionDelCambio logicas = new LNGestionDelCambio();
                var resultado = logicas.GuardarGestionDelCambio(EDPerfil);

                if (resultado != null)
                {
                    var response = Request.CreateResponse<EDGestionDelCambio>(HttpStatusCode.Created, resultado);

                    return response;
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    return response;
                }



            }
            catch (Exception ex)
            {

                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;

            }
        }





        [HttpDelete]
        [ActionName("Eliminar_Gestion_DelCambio")]
        public HttpResponseMessage EliminarGestionDelCambio(int PK_Matriz)
        {
            try
            {
                var logica = new LNGestionDelCambio();


                bool result = logica.EliminarGestionDelCambio(PK_Matriz);
                if (result)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, result);
                    return response;
                }
                else
                {
                    var response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    return response;
                }
            }
            catch (Exception ex)
            {

                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }

















    }
}