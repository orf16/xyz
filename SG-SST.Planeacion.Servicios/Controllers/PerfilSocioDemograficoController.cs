using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Logica.Planificacion;
using SG_SST.EntidadesDominio.Ausentismo;

namespace SG_SST.Planeacion.Servicios.Controllers
{
    public class PerfilSocioDemograficoController : ApiController
    {     

       
        [HttpPost]
        [ActionName("perfilsociodemografico-grabar")] 
        public HttpResponseMessage GrabarPerfilSocioDemografico(EDPerfilSocioDemografico EDPerfil)
        {
            try
            {

                LNPerfilSocioDemografico logicas = new LNPerfilSocioDemografico();
                var resultado = logicas.GuardarerfilSocioDemografico(EDPerfil);
             
                if (resultado != null)
                {
                    var response = Request.CreateResponse<EDPerfilSocioDemografico>(HttpStatusCode.Created, resultado);

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

        [HttpPost]
        [ActionName("obtener-editar-perfil")]
        public HttpResponseMessage EditarPerfilSocioDemografico(EDPerfilSocioDemografico EDPerfil)
        {
            try
            {

                LNPerfilSocioDemografico logicas = new LNPerfilSocioDemografico();
                var resultado = logicas.EditarPerfilSocioDemografico(EDPerfil);

                if (resultado != null)
                {
                    var response = Request.CreateResponse<EDPerfilSocioDemografico>(HttpStatusCode.Created, resultado);

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





        //[HttpGet]
        //[ActionName("descargar-plantillaCarga")]
        //public HttpResponseMessage ObtenerReporteExcel(string Nit)
        //{
        //    HttpResponseMessage response = null;
        //    try
        //    {
        //        LNCarguePerfilSocioDemografico logica = new LNCarguePerfilSocioDemografico();
        //        var archivo = logica.DescargarPlantillaCargue(Nit);
        //        if (archivo != null)
        //        {
        //            response = Request.CreateResponse<byte[]>(HttpStatusCode.OK, archivo);
        //            return response;
        //        }
        //        else
        //        {
        //            response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        //            return response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response = Request.CreateResponse(HttpStatusCode.InternalServerError);
        //        return response;
        //    }
        //}

        //[HttpGet]
        //[ActionName("descargar-plantilla")]
        //public HttpResponseMessage ObtenerReporteContigenciasExcel(object dataReportes)
        //{
        //    HttpResponseMessage response = null;
        //    try
        //    {
        //        EDPerfilSocioDemografico data = Newtonsoft.Json.JsonConvert.DeserializeObject<EDPerfilSocioDemografico>(dataReportes.ToString());
        //        LNPerfilSocioDemografico logica = new LNPerfilSocioDemografico();
        //        var archivo = logica.(data);
        //        if (archivo != null)
        //        {
        //            response = Request.CreateResponse<byte[]>(HttpStatusCode.OK, archivo);
        //            return response;
        //        }
        //        else
        //        {
        //            response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        //            return response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response = Request.CreateResponse(HttpStatusCode.InternalServerError);
        //        return response;
        //    }
        //}

        [HttpDelete]
        [ActionName("Eliminar_Perfil_SocioDem")]
        public HttpResponseMessage EliminarPerfilSocioDemografico(int idPerfil)
        {
            try
            {
              
                var logica = new LNPerfilSocioDemografico();

                bool result = logica.EliminarPerfilSocioDemografico(idPerfil);
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

        [HttpGet]
        [ActionName("obtener-perfiles-id")]

        public HttpResponseMessage obtenerPerfilesPorID(int id)
        {

            try
            {
                var logica = new LNPerfilSocioDemografico();
                var result = logica.obtenerPerfilesPorID(id);
                if (result != null)
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


        [HttpGet]
        [ActionName("obtener-ciudad-sede")]

        public HttpResponseMessage BuscarMunicipiosDeSede(int fk_sede)
        {

            try
            {
                var logica = new LNPerfilSocioDemografico();
                var result = logica.BuscarMunicipiosDeSede(fk_sede);
                if (result != null)
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



        [HttpGet]
        [ActionName("obtener-condiciones-id")]

        public HttpResponseMessage obtenerCondicionesPorID(int id)
        {

            try
            {
                var logica = new LNPerfilSocioDemografico();
                var result = logica.ObtenerCondicionesRiesgoPerfilPorID(id);
                if (result != null)
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

        ////Descargar reporte en excel por identificacion 

        //[HttpGet]
        //[ActionName("descargar-perfilExcel-id")]
        //public HttpResponseMessage ObtenerRepExcelPorPerfil(int id)
        //{
        //    HttpResponseMessage response = null;
        //    try
        //    {
        //        var logica = new LNPerfilSocioDemografico();
        //        var archivo = logica.ObtenerRepExcelPorPerfil(id);
        //        if (archivo != null)
        //        {
        //            response = Request.CreateResponse<byte[]>(HttpStatusCode.OK, archivo);
        //            return response;
        //        }
        //        else
        //        {
        //            response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
        //            return response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response = Request.CreateResponse(HttpStatusCode.InternalServerError);
        //        return response;
        //    }
        //}


        [HttpGet]
        [ActionName("obtener-perfil-empresa")]

        public HttpResponseMessage obtenerPerfilesPorEmpresa(string nitEmpresa)
        {

            try
            {
                var logica = new LNPerfilSocioDemografico();
                var result = logica.obtenerPerfilesPorEmpresa(nitEmpresa);
                if (result != null)
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


        [HttpDelete]
        [ActionName("eliminar-expocion-pel")]
        public HttpResponseMessage ELiminarImagenReporte(int idExposicion)
        {
            try
            {
                var logica = new LNPerfilSocioDemografico();
                bool result = logica.EliminarExpocionPeligro(idExposicion);
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
                //RegistroInformacion.EnviarError<DxDeCondicionSaludController>(ex.Message);
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }



        [HttpGet]
        [ActionName("obtener-condiciones-empresa")]

        public HttpResponseMessage ObtenerCondicionesRiesgoPorEmpresa(string nitEmpresa)
        {

            try
            {
                var logica = new LNPerfilSocioDemografico();
                var result = logica.ObtenerCondicionesRiesgoPorEmpresa(nitEmpresa);
                if (result != null)
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

        [HttpPost]
        [ActionName("cargar-plantilla")]
        public HttpResponseMessage ObtenerReporteExcel(EDCarguePerfil cargue)
        {
            HttpResponseMessage response = null;
            try
            {
                LNCarguePerfilSocioDemografico logica = new LNCarguePerfilSocioDemografico();
                var archivo = logica.CargarPlantillaCarguePlanificacion(cargue);
                if (archivo != null)
                {
                    response = Request.CreateResponse<EDCarguePerfil>(HttpStatusCode.Created, archivo);
                    return response;
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    return response;
                }
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }
    }
}