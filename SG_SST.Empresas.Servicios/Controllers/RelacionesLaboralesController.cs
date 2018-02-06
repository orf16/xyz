using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Logica.Empresas;
using SG_SST.Logica.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace SG_SST.Empresas.Servicios.Controllers
{
    public class RelacionesLaboralesController : ApiController
    {
        [HttpGet]
        [ActionName("relacionlaboral-estadosempleado")]
        public HttpResponseMessage DevuelveEstadosEmpleados()
        {
            try
            {
                LNRelacionesLaborales lnRL = new LNRelacionesLaborales();/// Defino variable gs

                var lstEstadosEmpleados = lnRL.DevuelveEstadosEmpleados();


                if (lstEstadosEmpleados != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, lstEstadosEmpleados);
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
        [ActionName("relacionlaboral-TipoCotizante")]
        public HttpResponseMessage DevuelveTipoCotizante()
        {
            try
            {
                LNRelacionesLaborales lnRL = new LNRelacionesLaborales();/// Defino variable gs
                var resultados = lnRL.DevuelveTiposCotizantes();
                if (resultados != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, resultados);
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
        [ActionName("relacionlaboral-TiposTerceros")]
        public HttpResponseMessage DevuelveTiposTerceros()
        {
            try
            {
                LNRelacionesLaborales lnRL = new LNRelacionesLaborales();/// Defino variable gs
                var resultados = lnRL.DevuelveTiposTerceros();
                if (resultados != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, resultados);
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
        [ActionName("relacionlaboral-DescargaArchivoExcelEmpleado")]
        public HttpResponseMessage DescargaArchivoExcelEmpleado(string nit, string estado, string tipoCotizante)
        {
            try
            {
                LNRelacionesLaborales lnRL = new LNRelacionesLaborales();/// Defino variable gs
                EDEmpresa_Usuaria eu = new EDEmpresa_Usuaria();
                List<EDEmpresa_Usuaria> lstEDEmpresa_Usuaria = new List<EDEmpresa_Usuaria>();
                eu.DT = lnRL.DescargaArchivoExcelEmpleado(nit, estado, tipoCotizante);
                lstEDEmpresa_Usuaria.Add(eu);

                if (eu.DT != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, lstEDEmpresa_Usuaria);
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
        [ActionName("relacionlaboral-ListarRelacionesLabTerceros")]
        public HttpResponseMessage ListarRelacionesLabTerceros(string estado, string tipoCotizante, string DocumentoEmpresa, int pageIndex, int numeroRegistros)
        {
            try
            {
                LNRelacionesLaborales lnRL = new LNRelacionesLaborales();/// Defino variable gs
                int pageCount = 0;
                var ListEmpleadoRelLab = lnRL.ListarRelacionesLabTerceros(estado, tipoCotizante, DocumentoEmpresa, pageIndex, numeroRegistros, out pageCount);

                if (ListEmpleadoRelLab != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, ListEmpleadoRelLab);
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
        [ActionName("relacionlaboral-ListarRelacionesLaborales")]
        public HttpResponseMessage ListarRelacionesLaborales(string razonSocialnit, string tipoTercero, string DocumentoEmpresa, int pageIndex, int numeroRegistros)
        {
            try
            {
                LNRelacionesLaborales lnRL = new LNRelacionesLaborales();/// Defino variable gs
                int pageCount = 0;
                tipoTercero = (tipoTercero == null) ? "" : tipoTercero;
                razonSocialnit = (razonSocialnit == null) ? "" : razonSocialnit;
                var ListEmpleadoTercero = lnRL.ListarRelacionesLaborales(razonSocialnit, tipoTercero, DocumentoEmpresa, pageIndex, numeroRegistros, out pageCount);

                if (ListEmpleadoTercero != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, ListEmpleadoTercero);
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
        [ActionName("relacionlaboral-DescargaArchivoExcelTerceroRelLab")]
        public HttpResponseMessage DescargaArchivoExcelTerceroRelLab(string nit, string razonSocialnit, string tipoTercero)
        {
            try
            {
                LNRelacionesLaborales lnRL = new LNRelacionesLaborales();/// Defino variable gs
                EDEmpresa_Usuaria eu = new EDEmpresa_Usuaria();
                List<EDEmpresa_Usuaria> lstEDEmpresa_Usuaria = new List<EDEmpresa_Usuaria>();
                System.Data.DataTable dtRows = lnRL.DescargaArchivoExcelTerceroRelLab(nit, razonSocialnit, tipoTercero);
                eu.DT = dtRows.Copy();
                lstEDEmpresa_Usuaria.Add(eu);

                if (eu.DT != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, lstEDEmpresa_Usuaria);
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
        [ActionName("relacionlaboral-ArchivoRelacionesLaborales")]

        public HttpResponseMessage GrabarArchivoRelacionesLaborales(List<EDRelacionesLaborales> lstRelLabTer)
        {

            try
            {
                LNRelacionesLaborales lnRL = new LNRelacionesLaborales();/// Defino variable gs
                string mensaje_bd = "";
                EDRelacionesLaborales edRL = new EDRelacionesLaborales();
                edRL.Rta = lnRL.GrabarArchivoRelacionesLaborales(lstRelLabTer, out mensaje_bd);
                edRL.Mensaje_validacion = mensaje_bd;
                List<EDRelacionesLaborales> lstedRL = new List<EDRelacionesLaborales>();
                lstedRL.Add(edRL);
                var response = Request.CreateResponse<List<EDRelacionesLaborales>>(HttpStatusCode.Created, lstedRL);
                return response;


            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;

            }
        }


        [HttpGet]
        [ActionName("relacionlaboral-ListarRazonesSociales")]
        public HttpResponseMessage ListarRazonesSociales(string NitEmpresaLogueada)
        {
            try
            {
                LNRelacionesLaborales lnRL = new LNRelacionesLaborales();/// Defino variable gs
                var ListRazonesSociales = lnRL.DevuelveRazonesSocialesdeTerceros(NitEmpresaLogueada);

                if (ListRazonesSociales != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, ListRazonesSociales);
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
        [ActionName("relacionlaboral-TiposInconsistencias")]
        public HttpResponseMessage DevuelveTiposInconsistencias()
        {
            try
            {
                LNRelacionesLaborales lnRL = new LNRelacionesLaborales();/// Defino variable gs
                var resultados = lnRL.ObtenerTiposInconsistencias();
                if (resultados != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, resultados);
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
        [ActionName("relacionlaboral-GrabarNotificarInconsistenciaLaboral")]
        public HttpResponseMessage GrabarNotificarInconsistenciaLaboral(EDNotificarInconsistencia notIncon)
        {
            try
            {
                LNRelacionesLaborales lnRL = new LNRelacionesLaborales();/// Defino variable gs
                EDNotificarInconsistencia notInconRes = lnRL.GrabarNotificarInconsistenciaLaboral(notIncon);
                var response = Request.CreateResponse<EDNotificarInconsistencia>(HttpStatusCode.Created, notInconRes);
                return response;
            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;
            }
        }


        [HttpPost]
        [ActionName("relacionlaboral-EnviarNotificarInconsistenciaLaboral")]
        public HttpResponseMessage EnviarNotificarInconsistenciaLaboral(EDNotificarInconsistencia notIncon)
        {
            try
            {
                LNRelacionesLaborales lnRL = new LNRelacionesLaborales();/// Defino variable gs
                LNUsuario lnUs = new LNUsuario();/// Defino variable gs

                List<EDTiposS> lstCorreos = lnRL.DevuelveCorreoGerente(notIncon.empresa_nit_sistema);
                if (lstCorreos != null && lstCorreos.Count > 0)
                {
                    string NombrePlantilla = notIncon.nombrePlantilla;
                    notIncon.Email_Gerente = lstCorreos[0].Id_Tipo;
                    notIncon.Nombre_Gerente = lstCorreos[0].Descripcion;

                    EDNotificarInconsistencia notInconRes = lnUs.EnviaNotificacionInconsistenciaLaborales(NombrePlantilla, notIncon);
                    var response = Request.CreateResponse<EDNotificarInconsistencia>(HttpStatusCode.Created, notInconRes);
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
