using RestSharp;
using SG_SST.ClienteServicios.AfiliadoDTO;
using SG_SST.ClienteServicios.EmpresaDTO;
using SG_SST.EntidadesDominio.Empleado;
using SG_SST.EntidadesDominio.Empresas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace SG_SST.ClienteServicios
{
    public class ClienteEmpresa
    {
        public static EDEmpresas ObtenerEmpresasSiarp(string tid, string nid)
        {
            var datos = string.Empty;
            EDEmpresas resultado = null;
            var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
            var request = new RestRequest(ConfigurationManager.AppSettings["consultaEmpresa"], RestSharp.Method.GET);
            request.RequestFormat = DataFormat.Xml;
            request.Parameters.Clear();
            request.AddParameter("tpDoc", tid);
            request.AddParameter("doc", nid);
            request.AddParameter("color", "orange");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            //se omite la validación de certificado de SSL
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            IRestResponse<List<EmpresaSiarpDTO>> response = cliente.Execute<List<EmpresaSiarpDTO>>(request);
            var result = response.Content;
            var empresas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpresaSiarpDTO>>(result);
            if (empresas != null && empresas.Count > 0)
            {
                var respuesta = empresas.Find(e => e.estado.ToUpper().Equals("ACTIVA"));
                int _fax = 0;
                int _telefono = 0;
                if (respuesta.faxEmpresa == null || string.IsNullOrEmpty(respuesta.faxEmpresa))
                    _fax = 0;
                else if (respuesta.faxEmpresa.Length > 7)
                    _fax = Convert.ToInt32(respuesta.faxEmpresa.Substring(0, 6));
                else
                    _fax = Convert.ToInt32(respuesta.faxEmpresa);

                if (respuesta.telefonoEmpresa == null || string.IsNullOrEmpty(respuesta.telefonoEmpresa))
                    _telefono = 0;
                else if (respuesta.telefonoEmpresa.Length > 7)
                    _telefono = Convert.ToInt32(respuesta.telefonoEmpresa.Substring(0, 6));
                else
                    _telefono = Convert.ToInt32(respuesta.telefonoEmpresa);

                if (respuesta.idDepartamento.Length == 1)
                    respuesta.idDepartamento = string.Format("0{0}", respuesta.idDepartamento);
                if (respuesta.idMunicipio.Length == 1)
                    respuesta.idMunicipio = string.Format("00{0}", respuesta.idMunicipio);
                else if (respuesta.idMunicipio.Length == 2)
                    respuesta.idMunicipio = string.Format("0{0}", respuesta.idMunicipio);


                resultado = new EDEmpresas()
                {
                    Razon_Social = string.IsNullOrEmpty(respuesta.razonSocial) ? "" : respuesta.razonSocial,
                    Nit_Empresa = string.IsNullOrEmpty(respuesta.idEmpresa) ? "" : respuesta.idEmpresa,
                    Codigo_Actividad = string.IsNullOrEmpty(respuesta.actividadEconomica) ? 0 : Convert.ToInt32(respuesta.actividadEconomica),
                    Tipo_Documento = respuesta.tipoDoc,
                    Telefono = _telefono,
                    Fax = _fax,
                    Identificacion_Representante = string.IsNullOrEmpty(respuesta.idRepresentanteLegal) ? 0 : int.Parse(respuesta.idRepresentanteLegal),
                    IdSeccional = string.IsNullOrEmpty(respuesta.idSeccional) ? 0 : Convert.ToInt32(respuesta.idSeccional),
                    IdSectorEconomico = string.IsNullOrEmpty(respuesta.idSectorEconomico) ? 0 : int.Parse(respuesta.idSectorEconomico),
                    Riesgo = string.IsNullOrEmpty(respuesta.riesgo) ? 0 : Convert.ToInt32(respuesta.riesgo),
                    Fecha_Vigencia_Actual = string.IsNullOrEmpty(respuesta.fecAfiliaEfectiva) ? "" : respuesta.fecAfiliaEfectiva,
                    Flg_Estado = string.IsNullOrEmpty(respuesta.estado) ? "" : respuesta.estado,
                    Zona = respuesta.zona,
                    SitioWeb = string.IsNullOrEmpty(respuesta.paginaWeb) ? string.Empty : respuesta.paginaWeb,
                    Total_Empleados = string.IsNullOrEmpty(respuesta.numeroDeTrabajadores) ? 0 : Convert.ToInt32(respuesta.numeroDeTrabajadores),
                    Direccion = string.IsNullOrEmpty(respuesta.direccionEmpresa) ? "" : respuesta.direccionEmpresa,
                    Descripcion_Actividad = string.IsNullOrEmpty(respuesta.nomActEconomico) ? "" : respuesta.nomActEconomico,
                    Email = string.IsNullOrEmpty(respuesta.emailEmpresa) ? "" : respuesta.emailEmpresa,
                    Departamento = new EDDepartamento()
                    {
                        Codigo_Departamento = respuesta.idDepartamento,
                        Nombre_Departamento = respuesta.departamento
                    },
                    Municipio = new EDMunicipio()
                    {
                        CodigoMunicipio = respuesta.idMunicipio,
                        NombreMunicipio = respuesta.municipio
                    }
                };
            }
            return resultado;
        }
        public static EDCargo ObtenerCargoUsuarioSiarp(string documento, string Nit, string clienteS, string requestS)
        {
            EDCargo EDCargo = new EDCargo();
            string NombreCargo = "";
            var cliente = new RestSharp.RestClient(clienteS);
            var request = new RestRequest(requestS, RestSharp.Method.GET);
            request.RequestFormat = DataFormat.Xml;
            request.Parameters.Clear();
            request.AddParameter("tpDoc", "cc");
            request.AddParameter("doc", documento);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            ServicePointManager.ServerCertificateValidationCallback = delegate
            { return true; };
            IRestResponse<List<AfiliadoModel>> response = cliente.Execute<List<AfiliadoModel>>(request);
            var result = response.Content;

            if (!string.IsNullOrWhiteSpace(result))
            {
                var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<AfiliadoModel>>(result);
                var afiliado = respuesta.Where(a => a.Estado == "Activo").FirstOrDefault();
                if (afiliado == null)
                { return EDCargo; }
                else
                {
                    if (afiliado.Ocupacion != null)
                    {
                        string valorcargo = afiliado.Ocupacion;
                        valorcargo = afiliado.Ocupacion.Replace("á", "A");
                        valorcargo = afiliado.Ocupacion.Replace("é", "E");
                        valorcargo = afiliado.Ocupacion.Replace("í", "I");
                        valorcargo = afiliado.Ocupacion.Replace("ó", "O");
                        valorcargo = afiliado.Ocupacion.Replace("ú", "U");
                        valorcargo = afiliado.Ocupacion.Replace("Á", "A");
                        valorcargo = afiliado.Ocupacion.Replace("É", "E");
                        valorcargo = afiliado.Ocupacion.Replace("Í", "I");
                        valorcargo = afiliado.Ocupacion.Replace("Ó", "O");
                        valorcargo = afiliado.Ocupacion.Replace("Ú", "U");
                        NombreCargo = valorcargo;
                        string textoNormalizado = NombreCargo.Normalize(NormalizationForm.FormD);
                        string textoSinAcentos = Regex.Replace(textoNormalizado, @"[^a-zA-z0-9 ]+", "");
                        textoSinAcentos = textoSinAcentos.Replace(" ", "");
                        EDCargo.NombreCargo = textoSinAcentos;
                    }
                    else
                    {
                        return EDCargo;
                    }
                }

                
            }
            return EDCargo;
        }
    }
}
