using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Models;
using SG_SST.Models.Emergencias;
using SG_SST.Models.PlanCapacitacion;
using SG_SST.ServiceRequest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SG_SST.Models.Vulnerabilidad;
using RestSharp;
using SG_SST.Dtos.Empresas;
using System.IO;
using System.Web;
using LinqToExcel;
using System.Drawing;
using System.Drawing.Imaging;
using SG_SST.Dtos.Organizacion;

namespace SG_SST.Controllers.Emergencias
{
    public class PlanEmergenciasController : BaseController
    {
        string UrlServicioAplicacion = ConfigurationManager.AppSettings["UrlServicioAplicacion"];
        private SG_SSTContext db = new SG_SSTContext();
        private static string RutaArchivos = "~/Descargas/";
        private static string RutaArchivosMasivos = "~/Content/PlanEmergenciaMasivos/";
        private static string SrcWhite = "data:image/png;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";

        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            var planemergencia = new EmergenciasModel();
            var resultSede = db.Tbl_Sede.Where(x => x.Empresa.Nit_Empresa == usuarioActual.NitEmpresa).ToList();
            if (resultSede != null && resultSede.Count() > 0)
            {
                planemergencia.Sedes = resultSede.Select(c => new SelectListItem()
                {
                    Value = c.Pk_Id_Sede.ToString(),
                    Text = c.Nombre_Sede
                }).ToList();
            }


            return View(planemergencia);
        }

        [HttpGet]
        public JsonResult ObtenerInfoSede(int isede)
        {
            var sede = db.Tbl_Sede.Where(x => x.Pk_Id_Sede == isede).SingleOrDefault();
            var empresa = db.Tbl_Empresa.Where(x => x.Pk_Id_Empresa == sede.Fk_Id_Empresa).SingleOrDefault();
            var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
            var request = new RestRequest(ConfigurationManager.AppSettings["consultaEmpresa"], RestSharp.Method.GET);
            request.RequestFormat = DataFormat.Xml;
            request.Parameters.Clear();
            request.AddParameter("tpDoc", "ni");
            request.AddParameter("doc", empresa.Nit_Empresa);
            request.AddParameter("color", "orange");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            //se omite la validación de certificado de SSL
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            IRestResponse<List<EmpresaSiarpDTO>> response = cliente.Execute<List<EmpresaSiarpDTO>>(request);
            var result = response.Content;
            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpresaSiarpDTO>>(result);

            var generalidades = db.Tbl_Eme_Generalidades.Where(x => x.fk_id_sede == isede).SingleOrDefault();
            var infogeneral = db.Tbl_Eme_InfoGeneral.Where(x => x.fk_id_sede == isede).SingleOrDefault();
            var desc_ocupa = db.Tbl_Eme_DescripcionOcupacion.Where(x => x.fk_id_sede == isede).SingleOrDefault();
            var cinstalaciones = db.Tbl_Eme_CaracteristicasInstalacion.Where(x => x.fk_id_sede == isede).SingleOrDefault();
            var elementos = db.Tbl_Eme_Elementos.Where(x => x.fk_id_sede == isede).SingleOrDefault();
            var georefe = db.Tbl_Eme_Georeferenciacion.Where(x => x.fk_id_sede == isede).SingleOrDefault();
            var frenteaccion = db.Tbl_Eme_FrentesAccion.Where(x => x.fk_id_sede == isede).SingleOrDefault();
            EmergenciasModel planemergencia = new EmergenciasModel();
            if (generalidades != null)
            {
                planemergencia.objetivos = generalidades.objetivos;
                planemergencia.alcance = generalidades.alcance;
            }

            if (respuesta != null)
            {
                planemergencia.razon_social = respuesta[0].razonSocial;
                planemergencia.identificacion_sede = empresa.Nit_Empresa;
                planemergencia.direccion_sede = respuesta[0].direccionEmpresa;
                planemergencia.telefono_sede = respuesta[0].telefonoEmpresa;
                planemergencia.correo_electronico = respuesta[0].emailEmpresa;
                planemergencia.departamento_sede = respuesta[0].departamento;
                planemergencia.municipio_sede = respuesta[0].municipio;
                planemergencia.actividad_economica = empresa.Descripcion_Actividad;
                planemergencia.representante = "";
            }

            if (infogeneral != null)
            {
                planemergencia.lindero_norte = infogeneral.lindero_norte;
                planemergencia.lindero_sur = infogeneral.lindero_sur;
                planemergencia.lindero_occidente = infogeneral.lindero_occidente;
                planemergencia.lindero_oriente = infogeneral.lindero_oriente;
                planemergencia.acceso_principales = infogeneral.acceso_principales;
                planemergencia.acceso_alternas = infogeneral.acceso_alternas;
            }

            if (desc_ocupa != null)
            {
                planemergencia.trabajadores_cantidad = desc_ocupa.trabajadores_cantidad;
                planemergencia.trabajadores_hdesde = desc_ocupa.trabajadores_hdesde;
                planemergencia.trabajadore_hhasta = desc_ocupa.trabajadore_hhasta;
                planemergencia.contratista_cantidad = desc_ocupa.contratista_cantidad;
                planemergencia.contratista_hdesde = desc_ocupa.contratista_hdesde;
                planemergencia.contratista_hhasta = desc_ocupa.contratista_hhasta;
                planemergencia.visitante_cantidad = desc_ocupa.visitante_cantidad;
                planemergencia.visitante_hdesde = desc_ocupa.visitante_hdesde;
                planemergencia.visitantte_hhasta = desc_ocupa.visitantte_hhasta;
                planemergencia.cliente_cantidad = desc_ocupa.cliente_cantidad;
                planemergencia.cliente_hdesde = desc_ocupa.cliente_hdesde;
                planemergencia.cliente_hhasta = desc_ocupa.cliente_hhasta;
                planemergencia.bo_tratamiento_especial = desc_ocupa.bo_tratamiento_especial;
                planemergencia.cual = desc_ocupa.cual;
            }

            if (cinstalaciones != null)
            {
                planemergencia.ventilacion_mecanica = cinstalaciones.ventilacion_mecanica;
                planemergencia.ascensores = cinstalaciones.ascensores;
                planemergencia.sotanos = cinstalaciones.sotanos;
                planemergencia.red_hidraulica = cinstalaciones.red_hidraulica;
                planemergencia.transformadores = cinstalaciones.transformadores;
                planemergencia.plantas_electricas = cinstalaciones.plantas_electricas;
                planemergencia.escaleras = cinstalaciones.escaleras;
                planemergencia.zonas_parqueo = cinstalaciones.zonas_parqueo;
                planemergencia.areas_especiales = cinstalaciones.areas_especiales;
            }

            if (elementos != null)
            {
                planemergencia.estructurales_descripcion = elementos.estructurales_descripcion;
                planemergencia.estructurales_ubicacion = elementos.estructurales_ubicacion;
                planemergencia.equipos_descripcion = elementos.equipos_descripcion;
                planemergencia.equipos_ubicacion = elementos.equipos_ubicacion;
                planemergencia.insumos_descripcion = elementos.insumos_descripcion;
                planemergencia.insumos_ubicacion = elementos.insumos_ubicacion;
            }

            if (georefe != null)
            {
                planemergencia.bo_externo = georefe.bo_externo;
                planemergencia.bo_colegio = georefe.bo_colegio;
                planemergencia.bo_iglesia = georefe.bo_iglesia;
                planemergencia.bo_comercial = georefe.bo_comercial;
                planemergencia.bo_centro_atencion = georefe.bo_centro_atencion;
                planemergencia.bo_parque = georefe.bo_parque;
                planemergencia.bo_otro = georefe.bo_otro;
                planemergencia.tab3_cual = georefe.cual;
                planemergencia.punto_encuentro = georefe.punto_encuentro;
                planemergencia.ubicacion_hidrantes = georefe.ubicacion_hidrantes;
            }

            if (frenteaccion != null)
            {
                planemergencia.plan_seguridadfisica = frenteaccion.plan_seguridadfisica;
                planemergencia.plan_primerosaux = frenteaccion.plan_primerosaux;
                planemergencia.plan_contraincendios = frenteaccion.plan_contraincendios;
                planemergencia.nombrecoordinador = frenteaccion.nombrecoordinador;
                planemergencia.tab7_objetivos = frenteaccion.objetivos;
                planemergencia.estructura = frenteaccion.estructura;
                planemergencia.proc_coordinacion = frenteaccion.proc_coordinacion;
                planemergencia.proc_internos = frenteaccion.proc_internos;
                planemergencia.proc_externos = frenteaccion.proc_externos;
                planemergencia.mecanismos_alarma = frenteaccion.mecanismos_alarma;
                planemergencia.simulacros = frenteaccion.simulacros;
                planemergencia.instructivo_evacuacion = frenteaccion.instructivo_evacuacion;
                planemergencia.proc_retorno = frenteaccion.proc_retorno;
            }

            return Json(planemergencia, JsonRequestBehavior.AllowGet);
        }

        #region Generalidades

        [HttpPost]
        public JsonResult GuardarGeneralidades(EmergenciasModel generalidades)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                var gen = db.Tbl_Eme_Generalidades.Where(x => x.fk_id_sede == generalidades.fk_id_sede_generalidades).SingleOrDefault();
                if (gen != null)
                {
                    db.Tbl_Eme_Generalidades.Remove(gen);
                    db.SaveChanges();
                }


                Eme_Generalidades EmeGeneralidades = new Eme_Generalidades()
                {
                    fk_id_sede = generalidades.fk_id_sede_generalidades,
                    alcance = generalidades.alcance,
                    objetivos = generalidades.objetivos,
                    NitEmpresa = usuarioActual.NitEmpresa
                };

                db.Tbl_Eme_Generalidades.Add(EmeGeneralidades);
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region InformacionGeneral

        [HttpPost]
        public JsonResult GuardarInformacionGeneral(EmergenciasModel informaciongeneral)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                var gen = db.Tbl_Eme_InfoGeneral.Where(x => x.fk_id_sede == informaciongeneral.fk_id_sede_infogeneral).SingleOrDefault();
                if (gen != null)
                {
                    db.Tbl_Eme_InfoGeneral.Remove(gen);
                    db.SaveChanges();
                }

                Eme_InfoGeneral InfoGeneral = new Eme_InfoGeneral()
                {
                    fk_id_sede = informaciongeneral.fk_id_sede_infogeneral,
                    razon_social = informaciongeneral.razon_social,
                    identificacion_sede = informaciongeneral.identificacion_sede,
                    direccion_sede = informaciongeneral.direccion_sede,
                    telefono_sede = informaciongeneral.telefono_sede,
                    correo_electronico = informaciongeneral.correo_electronico,
                    departamento_sede = informaciongeneral.departamento_sede,
                    municipio_sede = informaciongeneral.municipio_sede,
                    lindero_norte = informaciongeneral.lindero_norte,
                    lindero_sur = informaciongeneral.lindero_sur,
                    lindero_oriente = informaciongeneral.lindero_oriente,
                    lindero_occidente = informaciongeneral.lindero_sur,
                    acceso_principales = informaciongeneral.acceso_principales,
                    acceso_alternas = informaciongeneral.acceso_alternas,
                    actividad_economica = informaciongeneral.actividad_economica,
                    representante = informaciongeneral.representante,
                    NitEmpresa = usuarioActual.NitEmpresa
                };

                db.Tbl_Eme_InfoGeneral.Add(InfoGeneral);
                db.SaveChanges();
                Transaction.Commit();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Descripcion Ocupacion

        [HttpPost]
        public JsonResult GuardarDescripcionOcupacion(EmergenciasModel DescripcionOcupacion)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                var gen = db.Tbl_Eme_DescripcionOcupacion.Where(x => x.fk_id_sede == DescripcionOcupacion.fk_id_sede_descocupacion).SingleOrDefault();
                if (gen != null)
                {
                    db.Tbl_Eme_DescripcionOcupacion.Remove(gen);
                    db.SaveChanges();
                }

                Eme_DescripcionOcupacion descripcionOcupacion = new Eme_DescripcionOcupacion()
                {
                    fk_id_sede = DescripcionOcupacion.fk_id_sede_descocupacion,
                    trabajadores_cantidad = DescripcionOcupacion.trabajadores_cantidad,
                    trabajadores_hdesde = DescripcionOcupacion.trabajadores_hdesde,
                    trabajadore_hhasta = DescripcionOcupacion.trabajadore_hhasta,
                    contratista_cantidad = DescripcionOcupacion.contratista_cantidad,
                    contratista_hdesde = DescripcionOcupacion.contratista_hdesde,
                    contratista_hhasta = DescripcionOcupacion.contratista_hhasta,
                    visitante_cantidad = DescripcionOcupacion.visitante_cantidad,
                    visitante_hdesde = DescripcionOcupacion.visitante_hdesde,
                    visitantte_hhasta = DescripcionOcupacion.visitantte_hhasta,
                    cliente_cantidad = DescripcionOcupacion.cliente_cantidad,
                    cliente_hhasta = DescripcionOcupacion.cliente_hhasta,
                    cliente_hdesde = DescripcionOcupacion.cliente_hdesde,
                    bo_tratamiento_especial = DescripcionOcupacion.bo_tratamiento_especial,
                    cual = DescripcionOcupacion.cual,
                    NitEmpresa = usuarioActual.NitEmpresa
                };

                db.Tbl_Eme_DescripcionOcupacion.Add(descripcionOcupacion);
                db.SaveChanges();
                Transaction.Commit();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CaracteristicasInstalacion

        [HttpPost]
        public JsonResult GuardarCaracteristicasInstalacion(EmergenciasModel CaracteristicasInstalacion)
        {
            using (var Transaction = db.Database.BeginTransaction())
            {
                var gen = db.Tbl_Eme_CaracteristicasInstalacion.Where(x => x.fk_id_sede == CaracteristicasInstalacion.fk_id_sede_cinstalaciones).SingleOrDefault();
                if (gen != null)
                {
                    db.Tbl_Eme_CaracteristicasInstalacion.Remove(gen);
                    db.SaveChanges();
                }
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                Eme_CaracteristicasInstalacion caracteristicasInstalacion = new Eme_CaracteristicasInstalacion()
                {
                    fk_id_sede = CaracteristicasInstalacion.fk_id_sede_cinstalaciones,
                    ventilacion_mecanica = CaracteristicasInstalacion.ventilacion_mecanica,
                    ascensores = CaracteristicasInstalacion.ascensores,
                    sotanos = CaracteristicasInstalacion.sotanos,
                    red_hidraulica = CaracteristicasInstalacion.red_hidraulica,
                    transformadores = CaracteristicasInstalacion.transformadores,
                    plantas_electricas = CaracteristicasInstalacion.plantas_electricas,
                    escaleras = CaracteristicasInstalacion.escaleras,
                    zonas_parqueo = CaracteristicasInstalacion.zonas_parqueo,
                    areas_especiales = CaracteristicasInstalacion.areas_especiales,
                    NitEmpresa = usuarioActual.NitEmpresa
                };

                db.Tbl_Eme_CaracteristicasInstalacion.Add(caracteristicasInstalacion);
                db.SaveChanges();
                Transaction.Commit();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Elementos

        [HttpPost]
        public JsonResult GuardarElementos(EmergenciasModel elementos)
        {
            using (var Transaction = db.Database.BeginTransaction())
            {
                var gen = db.Tbl_Eme_Elementos.Where(x => x.fk_id_sede == elementos.fk_id_sede_elementos).SingleOrDefault();
                if (gen != null)
                {
                    db.Tbl_Eme_Elementos.Remove(gen);
                    db.SaveChanges();
                }
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                Eme_Elementos objEme_Elementos = new Eme_Elementos()
                {
                    fk_id_sede = elementos.fk_id_sede_elementos,
                    estructurales_descripcion = elementos.estructurales_descripcion,
                    estructurales_ubicacion = elementos.estructurales_ubicacion,
                    equipos_descripcion = elementos.equipos_descripcion,
                    equipos_ubicacion = elementos.equipos_ubicacion,
                    insumos_descripcion = elementos.insumos_descripcion,
                    insumos_ubicacion = elementos.insumos_ubicacion,
                    NitEmpresa = usuarioActual.NitEmpresa
                };
                db.Tbl_Eme_Elementos.Add(objEme_Elementos);
                db.SaveChanges();
                Transaction.Commit();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Georeferenciacion

        [HttpPost]
        public JsonResult GuardarGeoreferenciacion(EmergenciasModel Georeferenciacion)
        {
            using (var Transaction = db.Database.BeginTransaction())
            {
                var gen = db.Tbl_Eme_Georeferenciacion.Where(x => x.fk_id_sede == Georeferenciacion.fk_id_sede_georeferenciacion).SingleOrDefault();
                if (gen != null)
                {
                    db.Tbl_Eme_Georeferenciacion.Remove(gen);
                    db.SaveChanges();
                }
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                Eme_Georeferenciacion georeferenciacion = new Eme_Georeferenciacion()
                {
                    fk_id_sede = Georeferenciacion.fk_id_sede_georeferenciacion,
                    interno_img = Georeferenciacion.interno_img,
                    bo_externo = Georeferenciacion.bo_externo,
                    bo_colegio = Georeferenciacion.bo_colegio,
                    bo_iglesia = Georeferenciacion.bo_iglesia,
                    bo_comercial = Georeferenciacion.bo_comercial,
                    bo_centro_atencion = Georeferenciacion.bo_centro_atencion,
                    bo_parque = Georeferenciacion.bo_parque,
                    bo_otro = Georeferenciacion.bo_otro,
                    cual = Georeferenciacion.tab3_cual,
                    ubicacion_hidrantes = Georeferenciacion.ubicacion_hidrantes,
                    punto_encuentro = Georeferenciacion.punto_encuentro,
                    NitEmpresa = usuarioActual.NitEmpresa
                };

                db.Tbl_Eme_Georeferenciacion.Add(georeferenciacion);
                db.SaveChanges();
                Transaction.Commit();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Analisis de Vulnerabilidad

        //IDENTIFICACION AMENAZAS

        [HttpPost]
        public JsonResult ListarPreguntasIdentificacionAmenazas()
        {
            var identamen = db.Tbl_vul_Identificacion_Personas.ToList();
            return Json(identamen, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaPersonas()
        {
            var identamen = db.Tbl_vul_Personas.ToList();
            return Json(identamen, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaRecursos()
        {
            var identamen = db.Tbl_vul_Recursos.ToList();
            return Json(identamen, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaSistemasProcesos()
        {
            var identamen = db.Tbl_vul_SistemasProcesos.ToList();
            return Json(identamen, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ObtenerConsolidado(int sede)
        {
            var consolidados = db.Tbl_vul_eme_Consolidado.Where(x => x.fk_pk_id_plan_emergencia == sede).SingleOrDefault();
            return Json(consolidados, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GuardarVulnerabilidades(int num, string[] arreglo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                switch (num)
                {
                    case 1:
                        if (arreglo.Length>0)
                        {
                            var vul_IdentificacionAmenazas = db.Tbl_vul_eme_IdentificacionAmenazas.ToList();
                            db.Tbl_vul_eme_IdentificacionAmenazas.RemoveRange(vul_IdentificacionAmenazas);
                            db.SaveChanges();
                            for (int i = 0; i < arreglo.Length; i++)
                            {
                                string[] ar_identamen = arreglo[i].Split('|');
                                string color = string.Empty;

                                if (ar_identamen[4] == "P")
                                    color = "#009E11";

                                if (ar_identamen[4] == "PR")
                                    color = "#FFFF00";

                                if (ar_identamen[4] == "I")
                                    color = "#CC0000";

                                vul_eme_IdentificacionAmenazas obj_vul_eme_IdentificacionAmenazas = new vul_eme_IdentificacionAmenazas()
                                {
                                    fk_id_amenaza = int.Parse(ar_identamen[1]),
                                    fk_pk_id_plan_emergencia = int.Parse(ar_identamen[0]),
                                    origen = ar_identamen[2],
                                    fuenteriesgo = ar_identamen[3],
                                    calificacion = ar_identamen[4],
                                    color = color,
                                    tipo = "IdentificacionAmenzas",
                                    NitEmpresa = usuarioActual.NitEmpresa
                                };
                                db.Tbl_vul_eme_IdentificacionAmenazas.Add(obj_vul_eme_IdentificacionAmenazas);
                                db.SaveChanges();
                            }
                        }
                        
                        
                        break;
                    case 2:
                        if (arreglo.Length > 0) {
                            var vul_personas = db.Tbl_vul_eme_Personas.ToList();
                            db.Tbl_vul_eme_Personas.RemoveRange(vul_personas);
                            db.SaveChanges();
                            for (int i = 0; i < arreglo.Length; i++)
                            {
                                string[] ar_identamen = arreglo[i].Split('|');
                                vul_eme_Personas obj_vul_eme_Personas = new vul_eme_Personas()
                                {
                                    fk_id_aspecto = int.Parse(ar_identamen[1]),
                                    fk_pk_id_plan_emergencia = int.Parse(ar_identamen[0]),
                                    observacion = ar_identamen[2],
                                    recomendacion = ar_identamen[3],
                                    calificacion = ar_identamen[4],
                                    tipo = ar_identamen[5],
                                    NitEmpresa = usuarioActual.NitEmpresa
                                };
                                db.Tbl_vul_eme_Personas.Add(obj_vul_eme_Personas);
                                db.SaveChanges();
                            }
                        }
                        
                        break;
                    case 3:
                        if (arreglo.Length > 0)
                        {
                            var vul_Recursos = db.Tbl_vul_eme_Recursos.ToList();
                            db.Tbl_vul_eme_Recursos.RemoveRange(vul_Recursos);
                            db.SaveChanges();
                            for (int i = 0; i < arreglo.Length; i++)
                            {
                                string[] ar_identamen = arreglo[i].Split('|');
                                vul_eme_Recursos obj_vul_eme_Recursos = new vul_eme_Recursos()
                                {
                                    fk_id_aspecto = int.Parse(ar_identamen[1]),
                                    fk_pk_id_plan_emergencia = int.Parse(ar_identamen[0]),
                                    observacion = ar_identamen[2],
                                    recomendacion = ar_identamen[3],
                                    calificacion = ar_identamen[4],
                                    tipo = ar_identamen[5],
                                    NitEmpresa = usuarioActual.NitEmpresa
                                };
                                db.Tbl_vul_eme_Recursos.Add(obj_vul_eme_Recursos);
                                db.SaveChanges();
                            }
                        }
                        
                        break;
                    case 4:
                        if (arreglo.Length > 0)
                        {
                            var vul_sistemas_procesos = db.Tbl_vul_eme_sistemas_procesos.ToList();
                            db.Tbl_vul_eme_sistemas_procesos.RemoveRange(vul_sistemas_procesos);
                            db.SaveChanges();
                            for (int i = 0; i < arreglo.Length; i++)
                            {
                                string[] ar_identamen = arreglo[i].Split('|');
                                vul_eme_sistemas_procesos obj_vul_eme_sistemas_procesos = new vul_eme_sistemas_procesos()
                                {
                                    fk_id_aspecto = int.Parse(ar_identamen[1]),
                                    fk_pk_id_plan_emergencia = int.Parse(ar_identamen[0]),
                                    observacion = ar_identamen[2],
                                    recomendacion = ar_identamen[3],
                                    calificacion = ar_identamen[4],
                                    tipo = ar_identamen[5],
                                    NitEmpresa = usuarioActual.NitEmpresa
                                };
                                db.Tbl_vul_eme_sistemas_procesos.Add(obj_vul_eme_sistemas_procesos);
                                db.SaveChanges();
                            }
                        }
                        break;
                }

                Transaction.Commit();
            }

            return Json(num, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GuardarAnalisisRiesgos(int sede, string[] arAmenazas)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                var del = db.Tbl_Eme_AnalisisRiesgo.Where(x => x.fk_id_sede == sede).ToList();
                db.Tbl_Eme_AnalisisRiesgo.RemoveRange(del);
                db.SaveChanges();

                if (arAmenazas != null)
                {
                    for (int i = 0; i < arAmenazas.Length; i++)
                    {
                        string[] ar = arAmenazas[i].Split('|');
                        Eme_AnalisisRiesgo obj = new Eme_AnalisisRiesgo()
                        {
                            fk_id_sede = sede,
                            bo_intervencion = true,
                            fk_id_identamenaza = int.Parse(ar[0]),
                            plan_de_accion = ar[1],
                            NitEmpresa = usuarioActual.NitEmpresa
                        };
                        db.Tbl_Eme_AnalisisRiesgo.Add(obj);
                        db.SaveChanges();
                    }

                    Transaction.Commit();

                }

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GuardarConsolidado(int sede, string[] arOrganizacion, string[] arCapacitacion, string[] arDotacion, string[] arMateriales, string[] arEdificacion, string[] arEquipos, string[] arServiciosPublicos, string[] arSistemasAlternos, string[] arRecuperacion)
        {
            decimal Organizacion = 0, Capacitacion = 0, Dotacion = 0, Materiales = 0, Edificacion = 0, Equipos = 0, ServiciosPublicos = 0, SistemasAlternos = 0, Recuperacion = 0
                , calificacion_personas = 0, calificacion_recursos = 0, calificacion_sistemas_procesos = 0;

            if (arOrganizacion!=null)
            {
                if (arOrganizacion.Length > 0)
                {
                    for (int i = 0; i < arOrganizacion.Length; i++)
                    {
                        decimal TempData = decimal.Parse(arOrganizacion[i]);
                        Organizacion += (TempData / 10);
                    }
                }
            }

            if (arCapacitacion!=null)
            {
                if (arCapacitacion.Length > 0)
                {
                    for (int i = 0; i < arCapacitacion.Length; i++)
                    {
                        decimal TempData = decimal.Parse(arCapacitacion[i]);
                        Capacitacion += (TempData / 10);
                    }
                }
            }
            

            if (arDotacion!=null)
            {
                if (arDotacion.Length > 0)
                {
                    for (int i = 0; i < arDotacion.Length; i++)
                    {
                        decimal TempData = decimal.Parse(arDotacion[i]);
                        Dotacion += (TempData / 10);
                    }
                }
            }
            
            

            calificacion_personas = Organizacion + Capacitacion + Dotacion;
            calificacion_personas = calificacion_personas / 3;

            if (arMateriales!=null)
            {
                if (arMateriales.Length > 0)
                {
                    for (int i = 0; i < arMateriales.Length; i++)
                    {
                        decimal TempData = decimal.Parse(arMateriales[i]);
                        Materiales += (TempData / 10);
                    }
                }
            }


            if (arEdificacion!=null)
            {
                if (arEdificacion.Length > 0)
                {
                    for (int i = 0; i < arEdificacion.Length; i++)
                    {
                        decimal TempData = decimal.Parse(arEdificacion[i]);
                        Edificacion += (TempData / 10);
                    }
                } 
            }


            if (arEquipos!=null)
            {
                if (arEquipos.Length > 0)
                {
                    for (int i = 0; i < arEquipos.Length; i++)
                    {
                        decimal TempData = decimal.Parse(arEquipos[i]);
                        Equipos += (TempData / 10);
                    }
                }
            }
            


            calificacion_recursos = Materiales + Edificacion + Equipos;
            calificacion_recursos = calificacion_recursos / 3;

            if (arServiciosPublicos!=null)
            {
                if (arServiciosPublicos.Length > 0)
                {
                    for (int i = 0; i < arServiciosPublicos.Length; i++)
                    {
                        decimal TempData = decimal.Parse(arServiciosPublicos[i]);
                        ServiciosPublicos += (TempData / 10);
                    }
                }
            }

            if (arSistemasAlternos!=null)
            {
                if (arSistemasAlternos.Length > 0)
                {
                    for (int i = 0; i < arSistemasAlternos.Length; i++)
                    {
                        decimal TempData = decimal.Parse(arSistemasAlternos[i]);
                        SistemasAlternos += (TempData / 10);
                    }
                } 
            }


            if (arRecuperacion!=null)
            {
                if (arRecuperacion.Length > 0)
                {
                    for (int i = 0; i < arRecuperacion.Length; i++)
                    {
                        decimal TempData = decimal.Parse(arRecuperacion[i]);
                        Recuperacion += (TempData / 10);
                    }
                } 
            }
            


            calificacion_sistemas_procesos = ServiciosPublicos + SistemasAlternos + Recuperacion;
            calificacion_sistemas_procesos = calificacion_sistemas_procesos / 3;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                var consolidados = db.Tbl_vul_eme_Consolidado.Where(x => x.fk_pk_id_plan_emergencia == sede).ToList();
                db.Tbl_vul_eme_Consolidado.RemoveRange(consolidados);
                db.SaveChanges();
                vul_eme_Consolidado obj_vul_eme_Consolidado = new vul_eme_Consolidado()
                {
                    fk_pk_id_plan_emergencia = sede,
                    organizacion = Math.Round(Organizacion, 2),
                    capacitacion = Math.Round(Capacitacion, 2),
                    dotacion = Math.Round(Dotacion, 2),
                    calificacion_personas = Math.Round(calificacion_personas, 2),
                    interpretacion_personas = ObtenerInterpretacion(Math.Round(calificacion_personas, 2)),
                    color_personas = ObtenerColor(ObtenerInterpretacion(Math.Round(calificacion_personas, 2))),
                    materiales = Math.Round(Materiales, 2),
                    edificacion = Math.Round(Edificacion, 2),
                    equipos = Math.Round(Equipos, 2),
                    calificacion_recursos = Math.Round(calificacion_recursos, 2),
                    interpretacion_recursos = ObtenerInterpretacion(Math.Round(calificacion_recursos, 2)),
                    color_recursos = ObtenerColor(ObtenerInterpretacion(Math.Round(calificacion_recursos, 2))),
                    servicios_publicos = Math.Round(ServiciosPublicos, 2),
                    sistemas_alternos = Math.Round(SistemasAlternos, 2),
                    recuperacion = Math.Round(Recuperacion, 2),
                    calificacion_sistemas_procesos = Math.Round(calificacion_sistemas_procesos, 2),
                    interpretacion_sistemas_procesos = ObtenerInterpretacion(Math.Round(calificacion_sistemas_procesos, 2)),
                    color_sistemas_procesos = ObtenerColor(ObtenerInterpretacion(Math.Round(calificacion_sistemas_procesos, 2))),
                    NitEmpresa = usuarioActual.NitEmpresa
                };

                db.Tbl_vul_eme_Consolidado.Add(obj_vul_eme_Consolidado);
                db.SaveChanges();
                Transaction.Commit();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private string ObtenerInterpretacion(decimal parametro)
        {

            if (parametro > 0 && parametro <= 1)
                return "Baja";

            if (parametro > 1 && parametro <= 2)
                return "Media";

            if (parametro > 2 && parametro <= 3)
                return "Alta";

            return "";
        }

        private string ObtenerColor(string interpretacion)
        {
            if (interpretacion == "Baja")
                return "#009E11";

            if (interpretacion == "Media")
                return "#FFFF00";

            if (interpretacion == "Alta")
                return "#CC0000";

            return "#FFFFFF";
        }

        [HttpGet]
        public JsonResult ObtenerIdentificacionAmenaza(int pk_id_sede)
        {
            var IdentificacionAmenazas = db.Tbl_vul_eme_IdentificacionAmenazas.Where(x => x.fk_pk_id_plan_emergencia == pk_id_sede).ToList();
            return Json(IdentificacionAmenazas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerPersona(int pk_id_sede)
        {
            var personas = db.Tbl_vul_eme_Personas.Where(x => x.fk_pk_id_plan_emergencia == pk_id_sede).ToList();
            return Json(personas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerRecurso(int pk_id_sede)
        {
            var recursos = db.Tbl_vul_eme_Recursos.Where(x => x.fk_pk_id_plan_emergencia == pk_id_sede).ToList();
            return Json(recursos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerSistemaProceso(int pk_id_sede)
        {
            var sistemas_proceso = db.Tbl_vul_eme_sistemas_procesos.Where(x => x.fk_pk_id_plan_emergencia == pk_id_sede).ToList();
            return Json(sistemas_proceso, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Esquema Organizacional

        [HttpGet]
        public JsonResult ListarRoles(int isede)
        {
            var roles = db.Tbl_Eme_Roles.Where(x => x.fk_id_sede == isede).ToList();
            return Json(roles, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarRoles(EmergenciasModel rolesmodel)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                Eme_Roles roles = new Eme_Roles()
                {
                    fk_id_sede = rolesmodel.fk_id_sede_roles,
                    nombre = rolesmodel.nombre,
                    antes = rolesmodel.antes,
                    durante = rolesmodel.durante,
                    despues = rolesmodel.despues,
                    NitEmpresa = usuarioActual.NitEmpresa
                };

                db.Tbl_Eme_Roles.Add(roles);
                db.SaveChanges();
                Transaction.Commit();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Niveles de Emergencia

        [HttpGet]
        public JsonResult ListarNivelesEmergencia(int isede)
        {
            var emergencia = db.Tbl_Eme_NivelEmergencia.Where(x => x.fk_id_sede == isede).ToList();
            return Json(emergencia, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarNivelEmergencia(EmergenciasModel NivelEmergencia)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                Eme_NivelEmergencia _nivelEmergencia = new Eme_NivelEmergencia()
                {
                    fk_id_sede = NivelEmergencia.fk_id_sede_nivelemergencia,
                    nivel = NivelEmergencia.nivel,
                    emergencia = NivelEmergencia.emergencia,
                    NitEmpresa = usuarioActual.NitEmpresa
                };

                db.Tbl_Eme_NivelEmergencia.Add(_nivelEmergencia);
                db.SaveChanges();
                Transaction.Commit();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Fuentes de Accion

        [HttpPost]
        public JsonResult GuardarFrentesAccion(EmergenciasModel FrentesAccion)
        {
            using (var Transaction = db.Database.BeginTransaction())
            {
                var gen = db.Tbl_Eme_FrentesAccion.Where(x => x.fk_id_sede == FrentesAccion.fk_id_sede_frenteaccion).SingleOrDefault();
                if (gen != null)
                {
                    db.Tbl_Eme_FrentesAccion.Remove(gen);
                    db.SaveChanges();
                }
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                Eme_FrentesAccion frentesAccion = new Eme_FrentesAccion()
                {
                    fk_id_sede = FrentesAccion.fk_id_sede_frenteaccion,
                    plan_seguridadfisica = FrentesAccion.plan_seguridadfisica,
                    plan_primerosaux = FrentesAccion.plan_primerosaux,
                    plan_contraincendios = FrentesAccion.plan_contraincendios,
                    plan_eval_pdf = FrentesAccion.plan_eval_pdf,
                    nombrecoordinador = FrentesAccion.nombrecoordinador,
                    objetivos = FrentesAccion.tab7_objetivos,
                    estructura = FrentesAccion.estructura,
                    proc_coordinacion = FrentesAccion.proc_coordinacion,
                    proc_internos = FrentesAccion.proc_internos,
                    proc_externos = FrentesAccion.proc_externos,
                    simulacros = FrentesAccion.simulacros,
                    mecanismos_alarma = FrentesAccion.mecanismos_alarma,
                    rutas_evac_pdf = FrentesAccion.rutas_evac_pdf,
                    instructivo_evacuacion = FrentesAccion.instructivo_evacuacion,
                    proc_retorno = FrentesAccion.proc_retorno,
                    NitEmpresa = usuarioActual.NitEmpresa
                };

                db.Tbl_Eme_FrentesAccion.Add(frentesAccion);
                db.SaveChanges();
                Transaction.Commit();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Recursos

        [HttpGet]
        public JsonResult ListarHR(int isede)
        {
            var hr = db.Tbl_Eme_RecursosHumanos.Where(x => x.fk_id_sede == isede).ToList();
            return Json(hr, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarHR(EmergenciasModel hr)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                Eme_RecursosHumanos _recursosHumanos = new Eme_RecursosHumanos()
                {
                    fk_id_sede = hr.fk_id_sede_recursosh,
                    bpaux_nombre = hr.bpaux_nombre,
                    bcontra_nombre = hr.bcontra_nombre,
                    bevalresc_nombre = hr.bevalresc_nombre,
                    NitEmpresa = usuarioActual.NitEmpresa
                };

                db.Tbl_Eme_RecursosHumanos.Add(_recursosHumanos);
                db.SaveChanges();
                Transaction.Commit();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarRTecnicos(int isede)
        {
            var hr = db.Tbl_Eme_RecursosTecnicos.Where(x => x.fk_id_sede == isede).ToList();
            return Json(hr, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarRTecnicos(EmergenciasModel rt)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                Eme_RecursosTecnicos _rt = new Eme_RecursosTecnicos()
                {
                    fk_id_sede = rt.fk_id_sede_recursostecnicos,
                    tipo = rt.tipo,
                    cantidad = rt.cantidad,
                    ubicacion = rt.ubicacion,
                    NitEmpresa = usuarioActual.NitEmpresa
                };
                db.Tbl_Eme_RecursosTecnicos.Add(_rt);
                db.SaveChanges();
                Transaction.Commit();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ProcedimientosOperativosNormalizados

        [HttpGet]
        public JsonResult ListarProcedimientosOperativosNormalizados(int isede)
        {
            var procopera = db.Tbl_Eme_ProcOpera_Normalizados.Where(x => x.fk_id_sede == isede).ToList();
            return Json(procopera, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarProcedimientosOperativosNormalizados(EmergenciasModel ProcedimientosOperativosNormalizados)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                Eme_ProcOpera_Normalizados procOpera_Normalizados = new Eme_ProcOpera_Normalizados()
                {
                    fk_id_sede = ProcedimientosOperativosNormalizados.fk_id_sede_proc_normalizados,
                    nombre_proc = ProcedimientosOperativosNormalizados.nombre_proc,
                    responsable = ProcedimientosOperativosNormalizados.responsable,
                    proc_antes = ProcedimientosOperativosNormalizados.proc_antes,
                    proc_durante = ProcedimientosOperativosNormalizados.proc_durante,
                    proc_despues = ProcedimientosOperativosNormalizados.proc_despues,
                    proc_recursos = ProcedimientosOperativosNormalizados.proc_recursos,
                    NitEmpresa = usuarioActual.NitEmpresa
                };

                db.Tbl_Eme_ProcOpera_Normalizados.Add(procOpera_Normalizados);
                db.SaveChanges();
                Transaction.Commit();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CARGA MASIVA DE ARCHIVOS


        [HttpPost]
        public virtual ActionResult SubirArchivoIMG()
        {
            HttpPostedFileBase myFile = Request.Files["adjuntos"];
            bool isUploaded = false;
            string message = "File upload failed";
            string fullpath = "error";

            if (myFile != null && myFile.ContentLength != 0)
            {
                string pathForSaving = Server.MapPath("~/Descargas");
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        var mes = DateTime.Now.Month.ToString();
                        var dia = DateTime.Now.Day.ToString();
                        var anio = DateTime.Now.Year.ToString();
                        var currentmes = mes + dia + anio + "_";
                        string filen = currentmes + myFile.FileName;
                        myFile.SaveAs(Path.Combine(pathForSaving, filen));
                        isUploaded = true;
                        //message = "File uploaded successfully!";
                        fullpath = filen;//Path.Combine(pathForSaving, filen);
                    }
                    catch (Exception ex)
                    {
                        message = string.Format("File upload failed: {0}", ex.Message);
                    }
                }
            }

            return Json(new { isUploaded = isUploaded, message = fullpath }, "text/html");
        }

        [HttpPost]
        public virtual ActionResult CargarEncuentro()
        {
            HttpPostedFileBase myFile = Request.Files["adjuntos1"];
            bool isUploaded = false;
            string message = "File upload failed";
            string fullpath = "error";

            if (myFile != null && myFile.ContentLength != 0)
            {
                string pathForSaving = Server.MapPath("~/Descargas");
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        var mes = DateTime.Now.Month.ToString();
                        var dia = DateTime.Now.Day.ToString();
                        var anio = DateTime.Now.Year.ToString();
                        var currentmes = mes + dia + anio + "_";
                        string filen = currentmes + myFile.FileName;
                        myFile.SaveAs(Path.Combine(pathForSaving, filen));
                        isUploaded = true;
                        //message = "File uploaded successfully!";
                        fullpath = filen;//Path.Combine(pathForSaving, filen);
                    }
                    catch (Exception ex)
                    {
                        message = string.Format("File upload failed: {0}", ex.Message);
                    }
                }
            }

            return Json(new { isUploaded = isUploaded, message = fullpath }, "text/html");
        }

        [HttpPost]
        public virtual ActionResult CargarHidrantes()
        {
            HttpPostedFileBase myFile = Request.Files["adjuntos2"];
            bool isUploaded = false;
            string message = "File upload failed";
            string fullpath = "error";

            if (myFile != null && myFile.ContentLength != 0)
            {
                string pathForSaving = Server.MapPath("~/Descargas");
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        var mes = DateTime.Now.Month.ToString();
                        var dia = DateTime.Now.Day.ToString();
                        var anio = DateTime.Now.Year.ToString();
                        var currentmes = mes + dia + anio + "_";
                        string filen = currentmes + myFile.FileName;
                        myFile.SaveAs(Path.Combine(pathForSaving, filen));
                        isUploaded = true;
                        //message = "File uploaded successfully!";
                        fullpath = filen;//Path.Combine(pathForSaving, filen);
                    }
                    catch (Exception ex)
                    {
                        message = string.Format("File upload failed: {0}", ex.Message);
                    }
                }
            }

            return Json(new { isUploaded = isUploaded, message = fullpath }, "text/html");
        }




        [HttpGet]
        public JsonResult ActualizarAdjuntosIMG(int isede, string adjunto)
        {
            using (var Transaction = db.Database.BeginTransaction())
            {
                var del = db.Tbl_Eme_Georeferenciacion.Where(x => x.fk_id_sede == isede).SingleOrDefault();
                del.interno_img = adjunto;
                db.Tbl_Eme_Georeferenciacion.Attach(del);
                var entry = db.Entry(del);
                entry.State = System.Data.Entity.EntityState.Modified;
                entry.Property(x => x.interno_img).IsModified = true;
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        /// ///////////////////////////////////////////////////////////////////////

        [HttpGet]
        public JsonResult ActualizarEncuentro(int isede, string adjunto)
        {
            using (var Transaction = db.Database.BeginTransaction())
            {
                var del = db.Tbl_Eme_Georeferenciacion.Where(x => x.fk_id_sede == isede).SingleOrDefault();
                del.punto_encuentro_img = adjunto;
                db.Tbl_Eme_Georeferenciacion.Attach(del);
                var entry = db.Entry(del);
                entry.State = System.Data.Entity.EntityState.Modified;
                entry.Property(x => x.punto_encuentro_img).IsModified = true;
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ActualizarHidrantes(int isede, string adjunto)
        {
            using (var Transaction = db.Database.BeginTransaction())
            {
                var del = db.Tbl_Eme_Georeferenciacion.Where(x => x.fk_id_sede == isede).SingleOrDefault();
                del.ubicacion_hidrantes_img = adjunto;
                db.Tbl_Eme_Georeferenciacion.Attach(del);
                var entry = db.Entry(del);
                entry.State = System.Data.Entity.EntityState.Modified;
                entry.Property(x => x.ubicacion_hidrantes_img).IsModified = true;
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerImagenIMG1(int IdSede)
        {
            string Thumbnails = SrcWhite;
            string NombreArchivos = string.Empty;
            var del = db.Tbl_Eme_Georeferenciacion.Where(x => x.fk_id_sede == IdSede).SingleOrDefault();

            if (del.punto_encuentro_img != "")
            {
                try
                {
                    string PathImage = Server.MapPath(Path.Combine(RutaArchivos, del.punto_encuentro_img));
                    Bitmap bitmap;
                    using (var bmpTemp = new Bitmap(PathImage))
                    {
                        bitmap = new Bitmap(bmpTemp);
                    }
                    using (var newImage = ScaleImage(bitmap, 300, 300))
                    {
                        Thumbnails = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                Thumbnails = string.Empty;
            }


            return Json(Thumbnails, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerImagenIMG2(int IdSede)
        {
            string Thumbnails = SrcWhite;
            string NombreArchivos = string.Empty;
            var del = db.Tbl_Eme_Georeferenciacion.Where(x => x.fk_id_sede == IdSede).SingleOrDefault();

            if (del.ubicacion_hidrantes_img != "")
            {
                try
                {
                    string PathImage = Server.MapPath(Path.Combine(RutaArchivos, del.ubicacion_hidrantes_img));
                    Bitmap bitmap;
                    using (var bmpTemp = new Bitmap(PathImage))
                    {
                        bitmap = new Bitmap(bmpTemp);
                    }
                    using (var newImage = ScaleImage(bitmap, 300, 300))
                    {
                        Thumbnails = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                Thumbnails = string.Empty;
            }


            return Json(Thumbnails, JsonRequestBehavior.AllowGet);
        }

        /// ////////////////////////////////////////////////////////////////////


        [HttpPost]
        public virtual ActionResult SubirArchivoMasivo()
        {
            HttpPostedFileBase myFile = Request.Files["adjunto1"];
            bool isUploaded = false;
            string fullpath = "error";
            if (myFile.FileName.Contains(".xlsx"))
            {
                if (myFile != null && myFile.ContentLength != 0)
                {
                    string pathForSaving = Server.MapPath("~/Descargas");
                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        try
                        {
                            var mes = DateTime.Now.Month.ToString();
                            var dia = DateTime.Now.Day.ToString();
                            var anio = DateTime.Now.Year.ToString();
                            var currentmes = mes + dia + anio + "_";
                            string filen = currentmes + myFile.FileName;
                            myFile.SaveAs(Path.Combine(pathForSaving, filen));
                            isUploaded = true;
                            fullpath = Path.Combine(pathForSaving, filen);
                            var book = new ExcelQueryFactory(fullpath);
                            var resultado = (from row in book.Worksheet("masivo")
                                             let item = new EntidadHojaExcel
                                             {
                                                 sede = row["sede"].Cast<string>(),
                                                 Nombre = row["nombre"].Cast<string>(),
                                                 Documento = row["documento"].Cast<string>(),
                                                 Genero = row["genero"].Cast<string>(),
                                                 EPS = row["eps"].Cast<string>(),
                                                 RH = row["rh"].Cast<string>(),
                                                 NombreContacto = row["nombre_contacto"].Cast<string>(),
                                                 Telefono = row["telefono_contacto"].Cast<string>(),
                                                 Parentesco = row["parentesco_contacto"].Cast<string>(),
                                                 RequiereManejo = row["requiere_manejo"].Cast<string>(),
                                                 Cual = row["cual"].Cast<string>()
                                             }
                                             select item).ToList();
                            using (var Transaction = db.Database.BeginTransaction())
                            {
                                int sede = int.Parse(resultado[0].sede);
                                var del = db.Tbl_Eme_bd_Interna.Where(x => x.fk_id_sede == sede).ToList();
                                if (del != null)
                                {
                                    db.Tbl_Eme_bd_Interna.RemoveRange(del);
                                    db.SaveChanges();
                                }
                                foreach (var item in resultado)
                                {
                                    Eme_bd_Interna objEme_bd_Interna = new Eme_bd_Interna()
                                    {
                                        fk_id_sede = int.Parse(item.sede),
                                        nombre = item.Nombre,
                                        numdocumento = item.Documento,
                                        genero = item.Genero,
                                        eps = item.EPS,
                                        rh = item.RH,
                                        contacto_nombre = item.NombreContacto,
                                        contacto_telefono = item.Telefono,
                                        contacto_parentesco = item.Parentesco,
                                        requiere_manejo = item.RequiereManejo,
                                        cual_manejo = item.Cual
                                    };
                                    db.Tbl_Eme_bd_Interna.Add(objEme_bd_Interna);
                                    db.SaveChanges();
                                }
                                Transaction.Commit();
                            }

                        }
                        catch (Exception ex) { }
                    }
                }
            }


            return Json(new { isUploaded = isUploaded, message = fullpath }, "text/html");
        }

        [HttpPost]
        public virtual ActionResult SubirArchivoMasivo2()
        {
            HttpPostedFileBase myFile = Request.Files["adjunto2"];
            bool isUploaded = false;
            string fullpath = "error";
            if (myFile.FileName.Contains(".xlsx"))
            {
                if (myFile != null && myFile.ContentLength != 0)
                {
                    string pathForSaving = Server.MapPath("~/Descargas");
                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        try
                        {
                            var mes = DateTime.Now.Month.ToString();
                            var dia = DateTime.Now.Day.ToString();
                            var anio = DateTime.Now.Year.ToString();
                            var currentmes = mes + dia + anio + "_";
                            string filen = currentmes + myFile.FileName;
                            myFile.SaveAs(Path.Combine(pathForSaving, filen));
                            isUploaded = true;
                            fullpath = Path.Combine(pathForSaving, filen);
                            var book = new ExcelQueryFactory(fullpath);
                            var resultado = (from row in book.Worksheet("masivo")
                                             let item = new EntidadHojaExcel
                                             {
                                                 sede = row["sede"].Cast<string>(),
                                                 entidad = row["entidad"].Cast<string>(),
                                                 direccion = row["direccion"].Cast<string>(),
                                                 Telefono = row["telefono"].Cast<string>(),
                                                 NombreContacto = row["nombre_contacto"].Cast<string>()
                                             }
                                             select item).ToList();
                            using (var Transaction = db.Database.BeginTransaction())
                            {
                                int sede = int.Parse(resultado[0].sede);
                                var del = db.Tbl_Eme_Bd_Externa.Where(x => x.fk_id_sede == sede).ToList();
                                if (del != null)
                                {
                                    db.Tbl_Eme_Bd_Externa.RemoveRange(del);
                                    db.SaveChanges();
                                }
                                foreach (var item in resultado)
                                {
                                    Eme_Bd_Externa objEme_bd_Externa = new Eme_Bd_Externa()
                                    {
                                        fk_id_sede = int.Parse(item.sede),
                                        entidad = item.entidad,
                                        direccion = item.direccion,
                                        telefono = item.Telefono,
                                        nombre_contacto = item.NombreContacto

                                    };
                                    db.Tbl_Eme_Bd_Externa.Add(objEme_bd_Externa);
                                    db.SaveChanges();
                                }
                                Transaction.Commit();
                            }

                        }
                        catch (Exception ex) { }
                    }
                }
            }


            return Json(new { isUploaded = isUploaded, message = fullpath }, "text/html");
        }

        [HttpPost]
        public virtual ActionResult SubirArchivoMasivo3()
        {
            HttpPostedFileBase myFile = Request.Files["adjunto3"];
            bool isUploaded = false;
            string fullpath = "error";
            if (myFile.FileName.Contains(".xlsx") || myFile.FileName.Contains(".xls"))
            {
                if (myFile != null && myFile.ContentLength != 0)
                {
                    string pathForSaving = Server.MapPath("~/Descargas");
                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        try
                        {
                            var mes = DateTime.Now.Month.ToString();
                            var dia = DateTime.Now.Day.ToString();
                            var anio = DateTime.Now.Year.ToString();
                            var currentmes = mes + dia + anio + "_";
                            string filen = currentmes + myFile.FileName;
                            myFile.SaveAs(Path.Combine(pathForSaving, filen));
                            isUploaded = true;
                            fullpath = Path.Combine(pathForSaving, filen);
                            var book = new ExcelQueryFactory(fullpath);
                            var resultado = (from row in book.Worksheet("masivo")
                                             let item = new PlanAyuda
                                             {
                                                 sede = row["sede"].Cast<string>(),
                                                 empresa = row["empresa"].Cast<string>(),
                                                 recurso = row["recurso"].Cast<string>(),
                                                 compensacion = row["compensacion"].Cast<string>(),
                                                 reintegro = row["reintegro"].Cast<string>(),
                                                 nombre_contacto = row["nombre_contacto"].Cast<string>(),
                                                 telefono_contacto = row["telefono_contacto"].Cast<string>()
                                             }
                                             select item).ToList();
                            using (var Transaction = db.Database.BeginTransaction())
                            {
                                int sede = int.Parse(resultado[0].sede);
                                var del = db.Tbl_Eme_PlanAyuda.Where(x => x.fk_id_sede == sede).ToList();
                                if (del != null)
                                {
                                    db.Tbl_Eme_PlanAyuda.RemoveRange(del);
                                    db.SaveChanges();
                                }
                                foreach (var item in resultado)
                                {
                                    Eme_PlanAyuda objPlanAyuda = new Eme_PlanAyuda()
                                    {
                                        fk_id_sede = int.Parse(item.sede),
                                        empresa = item.empresa,
                                        recurso = item.recurso,
                                        compensacion = item.compensacion,
                                        reintegro = item.reintegro,
                                        nombre_contacto = item.nombre_contacto,
                                        telefono_contacto = item.telefono_contacto

                                    };
                                    db.Tbl_Eme_PlanAyuda.Add(objPlanAyuda);
                                    db.SaveChanges();
                                }
                                Transaction.Commit();
                            }

                        }
                        catch (Exception ex) { }
                    }
                }
            }


            return Json(new { isUploaded = isUploaded, message = fullpath }, "text/html");
        }

        [HttpPost]
        public virtual ActionResult CargarEstructuraOrg()
        {
            HttpPostedFileBase myFile = Request.Files["adjunto4"];
            bool isUploaded = false;
            string message = "File upload failed";
            string fullpath = "error";

            if (myFile != null && myFile.ContentLength != 0)
            {
                string pathForSaving = Server.MapPath("~/Descargas");
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        var mes = DateTime.Now.Month.ToString();
                        var dia = DateTime.Now.Day.ToString();
                        var anio = DateTime.Now.Year.ToString();
                        var currentmes = mes + dia + anio + "_";
                        string filen = currentmes + myFile.FileName;
                        myFile.SaveAs(Path.Combine(pathForSaving, filen));
                        isUploaded = true;
                        //message = "File uploaded successfully!";
                        fullpath = filen;//Path.Combine(pathForSaving, filen);
                    }
                    catch (Exception ex)
                    {
                        message = string.Format("File upload failed: {0}", ex.Message);
                    }
                }
            }

            return Json(new { isUploaded = isUploaded, message = fullpath }, "text/html");
        }


        [HttpGet]
        public JsonResult ActualizarAdjuntos(int isede, string adjunto)
        {
            int fk_id_sede = 0;
            using (var Transaction = db.Database.BeginTransaction())
            {
                var del = db.Tbl_Eme_EsquemaOrganizacional.Where(x => x.fk_id_sede == isede).SingleOrDefault();
                if (del != null)
                {
                    db.Tbl_Eme_EsquemaOrganizacional.Remove(del);
                    db.SaveChanges();
                }

                Eme_EsquemaOrganizacional adjuntos = new Eme_EsquemaOrganizacional()
                {
                    fk_id_sede = isede,
                    esquema_img = adjunto
                };
                db.Tbl_Eme_EsquemaOrganizacional.Add(adjuntos);
                db.SaveChanges();
                Transaction.Commit();
                fk_id_sede = adjuntos.fk_id_sede;
            }
            return Json(fk_id_sede, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public virtual ActionResult CargrPlanSeguridadFisica()
        {
            HttpPostedFileBase myFile = Request.Files["frenteaccion_adjunto1"];
            bool isUploaded = false;
            string message = "File upload failed";
            string fullpath = "error";

            if (myFile != null && myFile.ContentLength != 0)
            {
                string pathForSaving = Server.MapPath("~/Descargas");
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        var mes = DateTime.Now.Month.ToString();
                        var dia = DateTime.Now.Day.ToString();
                        var anio = DateTime.Now.Year.ToString();
                        var currentmes = mes + dia + anio + "_";
                        string filen = currentmes + myFile.FileName;
                        myFile.SaveAs(Path.Combine(pathForSaving, filen));
                        isUploaded = true;
                        //message = "File uploaded successfully!";
                        fullpath = filen;//Path.Combine(pathForSaving, filen);
                    }
                    catch (Exception ex)
                    {
                        message = string.Format("File upload failed: {0}", ex.Message);
                    }
                }
            }

            return Json(new { isUploaded = isUploaded, message = fullpath }, "text/html");
        }

        [HttpPost]
        public virtual ActionResult CargarPlanAtencion()
        {
            HttpPostedFileBase myFile = Request.Files["frenteaccion_adjunto2"];
            bool isUploaded = false;
            string message = "File upload failed";
            string fullpath = "error";

            if (myFile != null && myFile.ContentLength != 0)
            {
                string pathForSaving = Server.MapPath("~/Descargas");
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        var mes = DateTime.Now.Month.ToString();
                        var dia = DateTime.Now.Day.ToString();
                        var anio = DateTime.Now.Year.ToString();
                        var currentmes = mes + dia + anio + "_";
                        string filen = currentmes + myFile.FileName;
                        myFile.SaveAs(Path.Combine(pathForSaving, filen));
                        isUploaded = true;
                        //message = "File uploaded successfully!";
                        fullpath = filen;//Path.Combine(pathForSaving, filen);
                    }
                    catch (Exception ex)
                    {
                        message = string.Format("File upload failed: {0}", ex.Message);
                    }
                }
            }

            return Json(new { isUploaded = isUploaded, message = fullpath }, "text/html");
        }

        [HttpPost]
        public virtual ActionResult CargarPlanContraincendios()
        {
            HttpPostedFileBase myFile = Request.Files["frenteaccion_adjunto3"];
            bool isUploaded = false;
            string message = "File upload failed";
            string fullpath = "error";

            if (myFile != null && myFile.ContentLength != 0)
            {
                string pathForSaving = Server.MapPath("~/Descargas");
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        var mes = DateTime.Now.Month.ToString();
                        var dia = DateTime.Now.Day.ToString();
                        var anio = DateTime.Now.Year.ToString();
                        var currentmes = mes + dia + anio + "_";
                        string filen = currentmes + myFile.FileName;
                        myFile.SaveAs(Path.Combine(pathForSaving, filen));
                        isUploaded = true;
                        //message = "File uploaded successfully!";
                        fullpath = filen;//Path.Combine(pathForSaving, filen);
                    }
                    catch (Exception ex)
                    {
                        message = string.Format("File upload failed: {0}", ex.Message);
                    }
                }
            }

            return Json(new { isUploaded = isUploaded, message = fullpath }, "text/html");
        }

        [HttpPost]
        public virtual ActionResult CargarPlanEvacuacion()
        {
            HttpPostedFileBase myFile = Request.Files["frenteaccion_adjunto4"];
            bool isUploaded = false;
            string message = "File upload failed";
            string fullpath = "error";

            if (myFile != null && myFile.ContentLength != 0)
            {
                string pathForSaving = Server.MapPath("~/Descargas");
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        var mes = DateTime.Now.Month.ToString();
                        var dia = DateTime.Now.Day.ToString();
                        var anio = DateTime.Now.Year.ToString();
                        var currentmes = mes + dia + anio + "_";
                        string filen = currentmes + myFile.FileName;
                        myFile.SaveAs(Path.Combine(pathForSaving, filen));
                        isUploaded = true;
                        //message = "File uploaded successfully!";
                        fullpath = filen;//Path.Combine(pathForSaving, filen);
                    }
                    catch (Exception ex)
                    {
                        message = string.Format("File upload failed: {0}", ex.Message);
                    }
                }
            }

            return Json(new { isUploaded = isUploaded, message = fullpath }, "text/html");
        }

        [HttpPost]
        public virtual ActionResult CargarRutasEvacuacion()
        {
            HttpPostedFileBase myFile = Request.Files["frenteaccion_adjunto5"];
            bool isUploaded = false;
            string message = "File upload failed";
            string fullpath = "error";

            if (myFile != null && myFile.ContentLength != 0)
            {
                string pathForSaving = Server.MapPath("~/Descargas");
                if (this.CreateFolderIfNeeded(pathForSaving))
                {
                    try
                    {
                        var mes = DateTime.Now.Month.ToString();
                        var dia = DateTime.Now.Day.ToString();
                        var anio = DateTime.Now.Year.ToString();
                        var currentmes = mes + dia + anio + "_";
                        string filen = currentmes + myFile.FileName;
                        myFile.SaveAs(Path.Combine(pathForSaving, filen));
                        isUploaded = true;
                        //message = "File uploaded successfully!";
                        fullpath = filen;//Path.Combine(pathForSaving, filen);
                    }
                    catch (Exception ex)
                    {
                        message = string.Format("File upload failed: {0}", ex.Message);
                    }
                }
            }

            return Json(new { isUploaded = isUploaded, message = fullpath }, "text/html");
        }

        [HttpGet]
        public JsonResult CargarFrentesAccionAdjuntos(int isede, string adjunto1, string adjunto2, string adjunto3, string adjunto4, string adjunto5)
        {
            using (var Transaction = db.Database.BeginTransaction())
            {
                var del = db.Tbl_Eme_FrentesAccionAdjuntos.Where(x => x.fk_id_sede == isede).SingleOrDefault();
                if (del != null)
                {
                    db.Tbl_Eme_FrentesAccionAdjuntos.Remove(del);
                    db.SaveChanges();
                }

                Eme_FrentesAccionAdjuntos adjuntos = new Eme_FrentesAccionAdjuntos()
                {
                    fk_id_sede = isede,
                    plan_seguridad_fisica = adjunto1,
                    plan_atencion_medica = adjunto2,
                    plan_contraincendios = adjunto3,
                    plan_evacuacion = adjunto4,
                    plan_rutas_evacuacion = adjunto5
                };
                db.Tbl_Eme_FrentesAccionAdjuntos.Add(adjuntos);
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public string DescargarArchivoFrente(int isede, int tipo)
        {
            var del = db.Tbl_Eme_FrentesAccionAdjuntos.Where(x => x.fk_id_sede == isede).SingleOrDefault();
            string file = string.Empty;
            switch (tipo)
            {
                case 1: file = del.plan_seguridad_fisica;
                    break;
                case 2: file = del.plan_atencion_medica;
                    break;
                case 3: file = del.plan_contraincendios;
                    break;
                case 4: file = del.plan_evacuacion;
                    break;
                case 5: file = del.plan_rutas_evacuacion;
                    break;
            }

            return file;
        }

        [HttpPost]
        public string DescargarExcel(int parametro)
        {
            string file = string.Empty;
            switch (parametro)
            {
                case 1: file = "bd_interna.xlsx";
                    break;
                case 2: file = "bd_externa.xlsx";
                    break;
                case 3: file = "bd_planayuda.xlsx";
                    break;
            }

            return file;
        }

        [HttpGet]
        public virtual ActionResult DownloadExcel(string file)
        {
            string contentType = string.Empty;
            string PathFile = Server.MapPath(Path.Combine(RutaArchivosMasivos, file));

            if (file.Contains(".xls"))
            {
                contentType = "application/xlsx";
            }

            return File(PathFile, contentType, file);
        }

        [HttpGet]
        public virtual ActionResult Download(string file)
        {
            string contentType = string.Empty;
            string PathFile = Server.MapPath(Path.Combine(RutaArchivos, file));

            if (file.Contains(".txt"))
            {
                contentType = "text/plain";
            }
            if (file.Contains(".html"))
            {
                contentType = "text/html";
            }
            if (file.Contains(".pdf"))
            {
                contentType = "application/pdf";
            }
            else if (file.Contains(".docx"))
            {
                contentType = "application/docx";
            }
            else if (file.Contains(".xls"))
            {
                contentType = "application/xlsx";
            }
            else if (file.Contains(".xls"))
            {
                contentType = "application/xlsx";
            }
            else if (file.Contains(".jpeg"))
            {
                contentType = "image/jpeg";
            }
            else if (file.Contains(".png"))
            {
                contentType = "image/png";
            }
            else if (file.Contains(".gif "))
            {
                contentType = "image/gif ";
            }
            else if (file.Contains(".jpg"))
            {
                contentType = "image/jpeg";
            }

            return File(PathFile, contentType, file);
        }

        [HttpGet]
        public JsonResult ObtenerImagen(int IdSede)
        {
            string Thumbnails = SrcWhite;
            string NombreArchivos = string.Empty;
            var del = db.Tbl_Eme_EsquemaOrganizacional.Where(x => x.fk_id_sede == IdSede).SingleOrDefault();

            if (del.esquema_img != "")
            {
                try
                {
                    string PathImage = Server.MapPath(Path.Combine(RutaArchivos, del.esquema_img));
                    Bitmap bitmap;
                    using (var bmpTemp = new Bitmap(PathImage))
                    {
                        bitmap = new Bitmap(bmpTemp);
                    }
                    using (var newImage = ScaleImage(bitmap, 300, 300))
                    {
                        Thumbnails = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                Thumbnails = string.Empty;
            }


            return Json(Thumbnails, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerImagenIMG(int IdSede)
        {
            string Thumbnails = SrcWhite;
            string NombreArchivos = string.Empty;
            var del = db.Tbl_Eme_Georeferenciacion.Where(x => x.fk_id_sede == IdSede).SingleOrDefault();

            if (del != null)
            {
                if (del.interno_img != "")
                {
                    try
                    {
                        string PathImage = Server.MapPath(Path.Combine(RutaArchivos, del.interno_img));
                        Bitmap bitmap;
                        using (var bmpTemp = new Bitmap(PathImage))
                        {
                            bitmap = new Bitmap(bmpTemp);
                        }
                        using (var newImage = ScaleImage(bitmap, 300, 300))
                        {
                            Thumbnails = "data:image/png;base64," + ImageToBase64String(newImage, ImageFormat.Png);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    Thumbnails = string.Empty;
                }
            }
            else
                Thumbnails = string.Empty;


            return Json(Thumbnails, JsonRequestBehavior.AllowGet);
        }



        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }
        private string ImageToBase64String(System.Drawing.Image image, ImageFormat imageFormat)
        {
            using (MemoryStream memStream = new MemoryStream())
            {
                image.Save(memStream, imageFormat);
                string result = Convert.ToBase64String(memStream.ToArray());
                memStream.Close();

                return result;
            }

        }



        [HttpGet]
        public JsonResult AnalisisRiesgo(int pk_id_sede)
        {
            var analisisriesgo = db.Tbl_vul_eme_IdentificacionAmenazas.Join(db.Tbl_vul_Identificacion_Personas, a => a.fk_id_amenaza, b => b.pk_id_identificacion_amenazas, (a, b) => new { a, b })
                .Where(x => x.a.fk_pk_id_plan_emergencia == pk_id_sede)
                .Select(x => new { pk_id_amenazas = x.b.pk_id_identificacion_amenazas, amenaza = x.b.amenaza }).OrderBy(x => x.amenaza).ToList();
            return Json(analisisriesgo, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ObtenerAnalisisRiesgo(int pk_id_sede)
        {
            var analisisriesgo = db.Tbl_Eme_AnalisisRiesgo.Where(x => x.fk_id_sede == pk_id_sede).ToList();
            return Json(analisisriesgo, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarBDPlanAyuda(int isede)
        {
            var bdinterna = db.Tbl_Eme_PlanAyuda.Where(x => x.fk_id_sede == isede).ToList();
            return Json(bdinterna, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListarBDInterna(int isede)
        {
            var bdinterna = db.Tbl_Eme_bd_Interna.Where(x => x.fk_id_sede == isede).ToList();
            return Json(bdinterna, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarBDExterna(int isede)
        {
            var bdexterna = db.Tbl_Eme_Bd_Externa.Where(x => x.fk_id_sede == isede).ToList();
            return Json(bdexterna, JsonRequestBehavior.AllowGet);
        }

        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }




        #endregion

        #region Nivel de Riesgo

        
        [HttpGet]
        public JsonResult ObtenerNivelesRiesgo(int isede)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var hr = db.Tbl_vul_eme_IdentificacionAmenazas.Join(db.Tbl_vul_Identificacion_Personas,
                a => a.fk_id_amenaza,
                b => b.pk_id_identificacion_amenazas,
                (a, b) => new {a,b}).Where(x => (x.a.fk_pk_id_plan_emergencia == isede && x.a.NitEmpresa == usuarioActual.NitEmpresa)).ToList();

            var consolidado = db.Tbl_vul_eme_Consolidado.Where(x => (x.fk_pk_id_plan_emergencia == isede && x.NitEmpresa == usuarioActual.NitEmpresa)).SingleOrDefault();
            List<NivelesRiesgoModel> listNivelesRiesgoModel = new List<NivelesRiesgoModel>();
            foreach (var item in hr)
            {               
                NivelesRiesgoModel objNivelesRiesgoModel = new NivelesRiesgoModel() { 
                    tipo = item.b.tipo,
                    amenaza = item.b.amenaza,
                    color_a = AColor(item.a.calificacion),
                    color_p = consolidado.color_personas,
                    color_r = consolidado.color_recursos,
                    color_s = consolidado.color_sistemas_procesos,
                    interpretacion = InterpretacionRiesgo(AColor(item.a.calificacion), consolidado.color_recursos, consolidado.color_recursos, consolidado.color_sistemas_procesos)
                };
                listNivelesRiesgoModel.Add(objNivelesRiesgoModel);
            }
            return Json(listNivelesRiesgoModel, JsonRequestBehavior.AllowGet);
        }


        private string InterpretacionRiesgo(string ca, string cp, string cr, string cs)
        {
            int rojo=0, amarillo=0,verde=0;
            string inter = string.Empty;

            if (ca == "#009E11")
                verde++;

            if (cp == "#009E11")
                verde++;

            if (cr == "#009E11")
                verde++;

            if (cs == "#009E11")
                verde++;

            if (ca == "#FFFF00")
                amarillo++;

            if (cp == "#FFFF00")
                amarillo++;

            if (cr == "#FFFF00")
                amarillo++;

            if (cs == "#FFFF00")
                amarillo++;

            if (ca == "#CC0000")
                rojo++;

            if (cp == "#CC0000")
                rojo++;

            if (cr == "#CC0000")
                rojo++;

            if (cs == "#CC0000")
                rojo++;

            if (rojo >= 3)
                inter = "Alto";

            if (amarillo >= 3)
                inter = "Medio";

            if (verde >= 3)
                inter = "Bajo";

            return inter;
        
        }

        private string AColor(string param) {
            string _color = "#FFFFFF";
            if (param == "P")
                return "#009E11";

            if (param == "PR")
                return "#FFFF00";

            if (param == "I")
                return "#CC0000";

            return _color;
        }


        #endregion


    }
}
