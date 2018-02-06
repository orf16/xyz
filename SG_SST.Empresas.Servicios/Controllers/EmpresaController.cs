
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Logica.Ausentismo;
using SG_SST.Logica.Empresas;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SG_SST.Empresas.Servicios.Controllers
{
    public class EmpresaController : ApiController
    {



        [HttpGet]
        [ActionName("empresa-sedes")]
        public HttpResponseMessage ConsultarSedesPorEmpresa(int idEmpresa)
        {
            try
            {
                var logica = new LNEmpresa();
                var result = logica.ObtenerSedesPorEmpresa(idEmpresa);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }

        [HttpGet]
        [ActionName("empresa-sedesnit")]
        public HttpResponseMessage ConsultarSedesPorEmpresa(string Nit)
        {
            try
            {
                var logica = new LNEmpresa();
                var result = logica.ObtenerSedesPorNit(Nit);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }


        [HttpGet]
        [ActionName("empresa-sedesid")]
        public HttpResponseMessage ConsultarSedesPorIdSede(int IdSede)
        {
            try
            {
                var logica = new LNEmpresa();
                var result = logica.ObtenerSedesPorIdSede(IdSede);
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
        [ActionName("empresa-procesos")]
        public HttpResponseMessage ObtenerProcesosPorEmpresa(string Nit)
        {
            try
            {
                var logica = new LNProcesos();
                var result = logica.ObtenerProcesosPorEmpresa(Nit);
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
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError,ex.ToString());
                return response;
            }
        }

          [HttpGet]
          [ActionName("empresa-procesosprnivel")]
        public HttpResponseMessage ObtenerProcesosPorEmpresaprnivel(string Nit)
        {
            try
            {
                var logica = new LNEmpresa();
                var result = logica.ObtenerProcesosPorEmpresaprnivel(Nit);
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
          [ActionName("Cargar-Logo-Empresa")]

          public HttpResponseMessage GuardarLogoEmpresa(EDEmpresas logo)
          {
              try
              {
                  var logica = new LNEmpresa();
                  var result = logica.GuardarLogoEmpresa(logo);
                  if (result != null)
                  {
                      var response = Request.CreateResponse<EDEmpresas>(HttpStatusCode.Created, result);
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
          [ActionName("Obtener-Logo-Empresa")]

          public HttpResponseMessage ObtenerLogoEmpresa(string nitempresa)
          {
              try
              {
                  var logica = new LNEmpresa();
                  var result = logica.ObtenerLogoEmpresa(nitempresa);
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
        [ActionName("empresa-ciiu")]
        public HttpResponseMessage ConsultarActividadesEconomicas()
        {
            try
            {
                var logica = new LNEmpresa();
                var result = logica.ObtenerActividadesEconomicas();
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
        [ActionName("empresa-grabar")]
        public HttpResponseMessage GrabarEmpresa(EDEmpresas empresa)
        {

            try
            {
                LNEmpresa logicas = new LNEmpresa();
                var resultado = logicas.GuardarEmpresaYSusRelaciones(empresa);
                if (resultado != null)
                {
                    var response = Request.CreateResponse<EDEmpresas>(HttpStatusCode.Created, resultado);

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
        [ActionName("Obtener-Empresas-Usuarias")]
        public HttpResponseMessage ObtenerEmpresasUsuariasPorEmpresa(string Nit)
        {
            try
            {
                var logica = new LNEmpresa();
                var result = logica.ObtenerEmpresasUsuariasPorEmpresa(Nit);
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
        [ActionName("empresa-usuario-departamentos")]
        public HttpResponseMessage ConsultarDepartamentos()
        {
            try
            {
                LNEmpresaUsuaria lnEU = new LNEmpresaUsuaria();
                if (lnEU.lstDepartamentos != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, lnEU.lstDepartamentos);
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
        [ActionName("empresa-usuario-muncipios")]
        public HttpResponseMessage ConsultarMunicipios()
        {
            try
            {
                LNEmpresaUsuaria lnEU = new LNEmpresaUsuaria();
                var result = lnEU.lstMunicipios;
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
        [ActionName("empresa-usuario-muncipiosxdep")]
        public HttpResponseMessage ConsultarMunicipios(int departamento)
        {
            try
            {
                LNEmpresaUsuaria lnEU = new LNEmpresaUsuaria();
                var result = lnEU.DevuelveMunicipios(departamento);
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
        [ActionName("empresa-usuario-documentos")]
        public HttpResponseMessage ConsultarDocumentos()
        {
            try
            {
                LNEmpresaUsuaria lnEU = new LNEmpresaUsuaria();

                if (lnEU.lstDocumentos != null)
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, lnEU.lstDocumentos);
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
        [ActionName("empresa-usuaria-grabar")]
        public HttpResponseMessage GrabarEmpresasUsuarias(List<EDEmpresa_UsuariaA> lstempUsu)
        {

            try
            {
                LNEmpresaUsuaria lnEU = new LNEmpresaUsuaria();
                string mensaje_bd = "";
                bool rta = lnEU.GrabarEmpresasUsuarias(lstempUsu, out mensaje_bd);
                List<EDEmpresa_UsuariaA> lstEU = new List<EDEmpresa_UsuariaA>();
                EDEmpresa_UsuariaA edEU = new EDEmpresa_UsuariaA();
                edEU.Estado_bd = mensaje_bd;
                edEU.rta = rta;
                lstEU.Add(edEU);
                var response = Request.CreateResponse<List<EDEmpresa_UsuariaA>>(HttpStatusCode.Created, lstEU);
                return response;


            }
            catch (Exception ex)
            {
                var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
                return response;

            }
        }

        [HttpGet]
        [ActionName("tranversal-municipios")]
        public HttpResponseMessage ObtenerMunicipiosPorDepto(int IdDepartamento)
        {
            try
            {
               
                var logica = new LNEmpresa();
                var result = logica.ObtenerMunicipiosporDepartamento(IdDepartamento);
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
        [ActionName("tranversal-Departamentos")]
        public HttpResponseMessage ObtenerDepartamentos()
        {
            try
            {
               
                var logica = new LNEmpresa();
                var result = logica.ObtenerDepartamentos();
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
    }
    }



    //[HttpPost]
    // [ActionName("empresa-modificar")]
    // public HttpResponseMessage modificarEmpresa(int Pk_Id_Empresa)
    //  {

    //    try
    //    {
    //        LNEmpresa logicas = new LNEmpresa();
    //        var resultado = logicas.modificarEmpresa(Pk_Id_Empresa);
    //        if (resultado!=null)
    //        {
    //            var response = Request.CreateResponse(HttpStatusCode.OK, resultado);
    //            return response;
    //        }
    //        else
    //        {
    //            var response = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
    //            return response;
    //        }


    //    }
    //    catch (Exception ex)
    //    {
    //        var response = Request.CreateResponse(HttpStatusCode.InternalServerError);
    //        return response;

    //    }
    //}





