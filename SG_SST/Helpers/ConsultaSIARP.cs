using RestSharp;
using SG_SST.Audotoria;
using SG_SST.Models.AdminUsuarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace SG_SST.Helpers
{
    internal class ConsultaSIARP
    {
        /// <summary>
        /// Consulta la información de la empresa y el afiliado a SIARP
        /// para verficar que dicha empresa y afiliado existan y se encuentren
        /// en estado afiliado.
        /// </summary>
        /// <param name="tipoDocumentoEmp"></param>
        /// <param name="numDocumentoEmp"></param>
        /// <param name="tipoDocumento"></param>
        /// <param name="numDucumento"></param>
        /// <param name="resultadoAfi"></param>
        /// <returns></returns>
        internal static EmpresaAfiliadoModel ConsultarAfiliadoEmpresaActivos(string tipoDocumentoEmp, string numDocumentoEmp, string tipoDocumento, string numDucumento, out int resultadoEmp, out int resultadoAfi)
        {
            try
            {
                EmpresaAfiliadoModel objEmpresaAfi = null;
                //variable para manejar el resultado: 0: No existe la empresa,
                //1: Existe pero se encuentra inactiva, 2: Existe y se encuentra activa
                //3: No existe el afiliado, 4: Existe el afiliado pero se encuentra inactivo
                //5: Existe el afiliado y se encuentra activo.
                resultadoEmp = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteEmp;
                resultadoAfi = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteAfi;
                var consultaAfiliadoEmpresaActivo = ConfigurationManager.AppSettings["consultaAfiliadoEmpresaActivo"];
                var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                var request = new RestRequest(consultaAfiliadoEmpresaActivo, RestSharp.Method.GET);
                request.RequestFormat = DataFormat.Xml;
                request.Parameters.Clear();
                request.AddParameter("tpEm", tipoDocumentoEmp);
                request.AddParameter("docEm", numDocumentoEmp);
                request.AddParameter("tpAfiliado", tipoDocumento);
                request.AddParameter("docAfiliado", numDucumento);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                //se omite la validación de certificado de SSL
                ServicePointManager.ServerCertificateValidationCallback = delegate
                { return true; };
                IRestResponse<List<EmpresaAfiliadoModel>> response = cliente.Execute<List<EmpresaAfiliadoModel>>(request);
                var result = response.Content;
                var logAuditoria = new RegistraLog();
                //si está habilitado el registro de logs, se guarda la información que devuelve el servicio de SIARP
                var registrarLogAuditioria = ConfigurationManager.AppSettings["RegistrarLogAuditioria"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["registrarLogAuditioria"]);
                if (registrarLogAuditioria)
                    logAuditoria.RegistrarMsgInformacion(typeof(ConsultaSIARP), response.Content);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpresaAfiliadoModel>>(result);
                    if (respuesta.Count == 0)
                        resultadoEmp = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteEmp; //No existe
                    var EmpresaSystem = respuesta.Where(a => a.estadoEmpresa.ToUpper().Equals("ACTIVA")).FirstOrDefault();
                    if (EmpresaSystem == null)
                        resultadoEmp = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteInactivoEmp; //Existe y está Inactiva
                    else
                        resultadoEmp = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteActivoEmp; //Existe y está Activa
                    var AfilSystem = respuesta.Where(a => a.estadoPersona.ToUpper().Equals("ACTIVO")).FirstOrDefault();
                    if (AfilSystem == null)
                        resultadoAfi = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteInactivoAfi; //Existe y está Inactivo
                    else
                        resultadoAfi = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.ExisteActivoAfi; //Existe y está Activo

                    if (EmpresaSystem != null && AfilSystem != null)
                        objEmpresaAfi = AfilSystem;
                }
                return objEmpresaAfi;
            }
            catch (Exception ex)
            {
                var registroLog = new RegistraLog();
                registroLog.RegistrarError(typeof(ConsultaSIARP), string.Format("Error en la Acción ConsultarAfiliadoEmpresaActivos: {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                resultadoEmp = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteEmp;
                resultadoAfi = (int)Enumeraciones.EnumAdministracionUsuarios.ValidaEstadoEmpAfi.NoExisteAfi;
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipoDocumento"></param>
        /// <param name="numDucumento"></param>
        /// <returns></returns>
        internal static int ConsultarDatosEmpresa(string tipoDocumento, string numDucumento)
        {
            try
            {
                //variable para manejar el resultado: 0: No existe la empresa,
                //1: Existe pero se encuentra inactiva, 2: Existe y se encuentra activa
                var resultado = 0;
                var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                var request = new RestRequest(ConfigurationManager.AppSettings["consultaEmpresa"], RestSharp.Method.GET);
                request.RequestFormat = DataFormat.Xml;
                request.Parameters.Clear();
                request.AddParameter("tpDoc", tipoDocumento);
                request.AddParameter("doc", numDucumento);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                //se omite la validación de certificado de SSL
                ServicePointManager.ServerCertificateValidationCallback = delegate
                { return true; };
                IRestResponse<List<AdministrarUsuariosModel>> response = cliente.Execute<List<AdministrarUsuariosModel>>(request);
                var result = response.Content;
                if (!string.IsNullOrWhiteSpace(result))
                {
                    var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AdministrarUsuariosModel>>(result);
                    if (respuesta.Count == 0)
                        return 0; //No existe
                    var usuSystem = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
                    if (usuSystem == null)
                        resultado = 1; //Existe y está Inactiva
                    else
                        resultado = 2; //Existe y está Activa
                }
                return resultado;
            }
            catch (Exception ex)
            {
                var registroLog = new RegistraLog();
                registroLog.RegistrarError(typeof(ConsultaSIARP), string.Format("Error en la Acción ConsultarDatosTrabajador: {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                return 0;
            }

        }

        /// <summary>
        /// Valida si existe un empleado en SIARP asociado al tipo y número de documento
        /// </summary>
        /// <param name="numeroDocumento"></param>
        /// <returns></returns>
        internal static int ConsultarDatosTrabajador(string tipoDocumento, string numeroDocumento)
        {
            try
            {
                //variable para manejar el resultado: 0: No existe la empresa,
                //1: Existe pero se encuentra inactiva, 2: Existe y se encuentra activa
                var resultado = 0;
                if (!string.IsNullOrEmpty(numeroDocumento))
                {
                    var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                    var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliado"], RestSharp.Method.GET);
                    request.RequestFormat = DataFormat.Xml;
                    request.Parameters.Clear();
                    request.AddParameter("tpDoc", tipoDocumento);
                    request.AddParameter("doc", numeroDocumento);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Accept", "application/json");

                    //se omite la validación de certificado de SSL
                    ServicePointManager.ServerCertificateValidationCallback = delegate
                    { return true; };
                    IRestResponse<List<AdministrarUsuariosModel>> response = cliente.Execute<List<AdministrarUsuariosModel>>(request);
                    var result = response.Content;
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AdministrarUsuariosModel>>(result);
                        if (respuesta.Count == 0)
                            return 0; //No existe
                        var usuSystem = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
                        if (usuSystem == null)
                            resultado = 1; //Existe y está Inactivo
                        else
                            resultado = 2; //Existe y está Activo
                    }
                }
                return resultado;
            }
            catch (Exception ex)
            {
                var registroLog = new RegistraLog();
                registroLog.RegistrarError(typeof(ConsultaSIARP), string.Format("Error en la Acción ConsultarDatosTrabajador: {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                return 0;
            }
        }
    }
}