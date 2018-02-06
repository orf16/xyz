using RestSharp;
using SG_SST.Controllers.Base;
using SG_SST.Dtos.Organizacion;
using SG_SST.EntidadesDominio.Usuario;
using SG_SST.Models;
using SG_SST.Models.Comunicaciones;
using SG_SST.Models.ComunicadosAPP;
using SG_SST.Models.Organizacion;
using SG_SST.Utilidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Controllers.Comunicaciones
{
    public class ComunicacionesExternasController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();
        private static string RutaArchivos = "~/Descargas/";
        #region EXTERNAS

        public ActionResult Index() {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }

           // ObtenerCargos("directores");
            return View();
        }

        [HttpGet]
        [ValidateInput(false)]
        public JsonResult GuardarComunicado(int? PK_Id_Comunicado, string Titulo, string Asunto, string CuerpoMensaje, string arreglo, string Origen) 
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            string FechaEnvio = string.Empty;
            string EstadoComunicado = "En Espera";
            int PK_Id_Comunicado_temp = 0;
            if (Origen == "E")
            {
                using (var Transaction = db.Database.BeginTransaction())
                {
                    var comunicado = db.Tbl_ComunicacionesExternas.Where(x => x.PK_Id_Comunicado == PK_Id_Comunicado).SingleOrDefault();
                    comunicado.EstadoComunicado = "Enviado";
                    comunicado.FechaEnvio = DateTime.Now.ToString();
                    db.Tbl_ComunicacionesExternas.Attach(comunicado);
                    var entry = db.Entry(comunicado);
                    entry.State = System.Data.Entity.EntityState.Modified;
                    entry.Property(x => x.EstadoComunicado).IsModified = true;
                    entry.Property(x => x.FechaEnvio).IsModified = true;
                    db.SaveChanges();
                    Transaction.Commit();
                    Thread myNewThread = new Thread(() => SendEmail(PK_Id_Comunicado,Titulo, Asunto, CuerpoMensaje, arreglo, usuarioActual.NitEmpresa));
                    myNewThread.Start();
                }
            }
            else
            {
                using (var Transaction = db.Database.BeginTransaction())
                {
                    if (PK_Id_Comunicado > 0)
                    {
                        var comunicado = db.Tbl_ComunicacionesExternas.Where(x => x.PK_Id_Comunicado == PK_Id_Comunicado).SingleOrDefault();
                        comunicado.Titulo = Titulo;
                        comunicado.Asunto = Asunto;
                        comunicado.CuerpoMensaje = CuerpoMensaje;
                        comunicado.Destinatarios = arreglo;
                        comunicado.FechaCreacion = DateTime.Now.ToString();
                        comunicado.EstadoComunicado = EstadoComunicado;
                        db.Tbl_ComunicacionesExternas.Attach(comunicado);
                        var entry = db.Entry(comunicado);
                        entry.State = System.Data.Entity.EntityState.Modified;
                        entry.Property(x => x.Titulo).IsModified = true;
                        entry.Property(x => x.CuerpoMensaje).IsModified = true;
                        entry.Property(x => x.Destinatarios).IsModified = true;
                        entry.Property(x => x.FechaCreacion).IsModified = true;
                        entry.Property(x => x.EstadoComunicado).IsModified = true;
                        db.SaveChanges();
                        PK_Id_Comunicado_temp = comunicado.PK_Id_Comunicado;
                    }
                    else {
                        ComunicacionesExternas comunicados = new ComunicacionesExternas()
                        {
                            Titulo = Titulo,
                            Asunto = Asunto,
                            CuerpoMensaje = CuerpoMensaje,
                            Destinatarios = arreglo,
                            EstadoComunicado = EstadoComunicado,
                            FechaCreacion = DateTime.Now.ToString(),
                            FechaEnvio = FechaEnvio,
                            NitEmpresa = usuarioActual.NitEmpresa
                        };

                        db.Tbl_ComunicacionesExternas.Add(comunicados);
                        db.SaveChanges();
                        PK_Id_Comunicado_temp = comunicados.PK_Id_Comunicado;
                    }
                    Transaction.Commit();
                }
            }
            var parametros = new { PK_Id_Comunicado_temp = PK_Id_Comunicado_temp, origen = Origen };
            return Json(parametros, JsonRequestBehavior.AllowGet);

        }

        private void SendEmail(int? PK_Id_Comunicado, string Titulo, string Asunto, string CuerpoMensaje, string arreglo, string NitEmpresa)
        {
            string[] cargos = arreglo.Split(',');
            var parametros = db.Tbl_ParametrosSistema.ToList();
            var rutaHttpSitio = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RutaHttpSitio).Select(p => p).FirstOrDefault().Valor;
            var servidorSTMP = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.ServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var remitente = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.RemitenteNotificaion).Select(p => p).FirstOrDefault().Valor;
            var correoRemitente = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.CorreoRemitente).Select(p => p).FirstOrDefault().Valor;
            var puertoServidorStmp = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PuertoServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var usuarioServidorStmp = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.UsuarioServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var passwordServidorStmp = parametros.Where(p => p.IdParametro == (int)Enumeraciones.EnumAdministracionUsuarios.ParametrosSistema.PasswordServidorStmp).Select(p => p).FirstOrDefault().Valor;
            var plantilla = db.Tbl_PlantillasCorreosSistema.Where(x => x.IdPlantilla == 5).SingleOrDefault();
            var plantillaHtml = plantilla.Plantilla.Replace("[[RutaHttpSitio]]", rutaHttpSitio);
            var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
            var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliados"], RestSharp.Method.GET);
            request.RequestFormat = DataFormat.Xml;
            request.Parameters.Clear();
            request.AddParameter("tpEm", "NI");
            request.AddParameter("docEm", NitEmpresa);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            //se omite la validación de certificado de SSL
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            IRestResponse<List<EmpleadosWSDTO>> response = cliente.Execute<List<EmpleadosWSDTO>>(request);
            var result = response.Content;
            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpleadosWSDTO>>(result);
            for (int i = 0; i < cargos.Length; i++)
            {
                string[] persona = cargos[i].Split('-');
                if (persona[0]=="N/A")
                {
                    string[] nombre = persona[1].Split(' ');
                    string nombre1 = nombre[0];
                    string nombre2 = string.Empty;
                    if (nombre.Length>1)
                        nombre2 = nombre[1];
                    
                    string email = cargos[i+1];
                    EmailParametersSending(PK_Id_Comunicado,nombre1, nombre2, email, NitEmpresa, Asunto, CuerpoMensaje, Titulo, correoRemitente, remitente, passwordServidorStmp, puertoServidorStmp, servidorSTMP, plantillaHtml);
                }
                else if (persona[0] == "C")
                {
                    string[] usuario = persona[2].Split(' ');
                    string nombre1 = usuario[0] + ' ' + usuario[1];
                    string nombre2 = usuario[2] + ' ' + usuario[3];
                    string email = usuario[4];
                    EmailParametersSending(PK_Id_Comunicado,nombre1, nombre2, email, NitEmpresa, Asunto, CuerpoMensaje, Titulo, correoRemitente, remitente, passwordServidorStmp, puertoServidorStmp, servidorSTMP, plantillaHtml);
                }
                else
                {
                    try
                    {
                        string posicion = cargos[i];
                        var cargotemp = respuesta.Where(x => x.cargo == posicion).ToList();
                        if (cargotemp.Count > 0)
                        {
                            foreach (var item in cargotemp)
                            {
                                string nombre = item.nombre1 + ' ' + item.nombre2;
                                string apellido = item.apellido1 + ' ' + item.apellido2;
                                EmailParametersSending(PK_Id_Comunicado,nombre, apellido, item.emailPersona, NitEmpresa, Asunto, CuerpoMensaje, Titulo, correoRemitente, remitente, passwordServidorStmp, puertoServidorStmp, servidorSTMP, plantillaHtml);
                            }
                        }
                        else
                        {
                            var grupo = db.Tbl_GruposComuniciones.Where(x => x.NombreGrupo == posicion).SingleOrDefault();
                            if (grupo != null)
                            {
                                var cargotem = db.Tbl_GrupoUsuariosComunicaciones.Where(x => x.fk_id_grupo_comunicaciones == grupo.pk_id_grupo).ToList();
                                foreach (var item in cargotem)
                                {
                                    string[] nombre = item.nombre_contacto.Split(' ');
                                    string nombre1 = nombre[0];
                                    string nombre2 = string.Empty;
                                    if (nombre.Length > 1)
                                        nombre2 = nombre[1];


                                    EmailParametersSending(PK_Id_Comunicado,nombre1, nombre2, item.email, NitEmpresa, Asunto, CuerpoMensaje, Titulo, correoRemitente, remitente, passwordServidorStmp, puertoServidorStmp, servidorSTMP, plantillaHtml);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        string message = e.Message;
                    }
                    finally
                    {
                        string posicion = cargos[i];
                        var grupo = db.Tbl_GruposComuniciones.Where(x => x.NombreGrupo == posicion).SingleOrDefault();
                        if (grupo != null)
                        {
                            var cargotem = db.Tbl_GrupoUsuariosComunicaciones.Where(x => x.fk_id_grupo_comunicaciones == grupo.pk_id_grupo).ToList();
                            foreach (var item in cargotem)
                            {
                                string[] nombre = item.nombre_contacto.Split(' ');
                                string nombre1 = nombre[0];
                                string nombre2 = string.Empty;
                                if (nombre.Length > 1)
                                    nombre2 = nombre[1];


                                EmailParametersSending(PK_Id_Comunicado,nombre1, nombre2, item.email, NitEmpresa, Asunto, CuerpoMensaje, Titulo, correoRemitente, remitente, passwordServidorStmp, puertoServidorStmp, servidorSTMP, plantillaHtml);
                            }
                        }
                    }
                }    
            }   
        }

        private void EmailParametersSending(int? PK_Id_Comunicado,string nombre, string apellido, string email, string NitEmpresa, string Asunto, string CuerpoMensaje, string Titulo, string correoRemitente,
            string remitente, string passwordServidorStmp, string puertoServidorStmp, string servidorSTMP, string plantillaHtml)
        {
            plantillaHtml = plantillaHtml.Replace("[[NombreUsuario]]", string.Format("{0} {1}", nombre, apellido));
            plantillaHtml = plantillaHtml.Replace("[[EmailUsuario]]", email);
            plantillaHtml = plantillaHtml.Replace("[[RazonSocial]]", ObtenerRazonSocial(NitEmpresa));
            plantillaHtml = plantillaHtml.Replace("[[Asunto]]", Asunto);
            plantillaHtml = plantillaHtml.Replace("[[Cuerpo]]", CuerpoMensaje);
            bool param_correo = EnvioCorreos.EnviarCorreo(plantillaHtml, correoRemitente, remitente, true, passwordServidorStmp, Convert.ToInt32(puertoServidorStmp), servidorSTMP, "[ALISSTA Comunicaciones] " + Titulo, email);
            using (var Transaction = db.Database.BeginTransaction())
            {
                ComunicacionesLog log = new ComunicacionesLog()
                {
                    fk_id_comunicaciones = (int)PK_Id_Comunicado,
                    modulo = "externos",
                    enviado_rechazado = param_correo

                };
                db.Tbl_ComunicacionesLog.Add(log);
                db.SaveChanges();
                Transaction.Commit();
            }
        }

        private string ObtenerRazonSocial(string NitEmpresa)
        {
            var empresa = db.Tbl_Empresa.Where(x => x.Nit_Empresa == NitEmpresa).SingleOrDefault();
            return empresa.Razon_Social;
        }

        [HttpPost]
        /// <summary>
        /// Método para obtener cargos
        /// </summary>
        /// <param</param>
        /// <returns>List<Cargo></returns>
        public JsonResult ObtenerCargos(string cargotemp)
        { 
            try
            {
                var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
                var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliados"], RestSharp.Method.GET);
                request.RequestFormat = DataFormat.Xml;
                request.Parameters.Clear();
                request.AddParameter("tpEm", "NI");
                request.AddParameter("docEm", usuarioActual.NitEmpresa);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                //se omite la validación de certificado de SSL
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                IRestResponse<List<EmpleadosWSDTO>> response = cliente.Execute<List<EmpleadosWSDTO>>(request);
                var result = response.Content;
                var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpleadosWSDTO>>(result);

                var cargosSiarp = (from c in respuesta
                                    select c.cargo).Distinct();
                List<Cargo> Cargos = new List<Cargo>();
                foreach (string cargo in cargosSiarp)
                {
                    Cargo cargoExist = db.Tbl_Cargo.Where(x => x.Nombre_Cargo.Equals(cargo)).FirstOrDefault();
                    if (cargoExist == null)
                    {
                        cargoExist = new Cargo();
                        cargoExist.Nombre_Cargo = cargo;
                        db.Tbl_Cargo.Add(cargoExist);
                        db.SaveChanges();
                    }
                    Cargos.Add(db.Tbl_Cargo.Where(x => x.Nombre_Cargo.Equals(cargo)).FirstOrDefault());
                }
                Cargos.OrderBy(x => x.Nombre_Cargo).ToList();

                var cargos_temporales = Cargos.Where(x => (x.Nombre_Cargo).Contains(cargotemp.ToUpper())).ToList();
                return Json(cargos_temporales, JsonRequestBehavior.AllowGet);
            }

            catch (Exception)
            {
                return null;
                throw;
            }
        }

        [HttpGet]
        public JsonResult GuardarGrupo(string NombreGrupo)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                GruposComuniciones gpcomunicaciones = new GruposComuniciones() { 
                    NombreGrupo = NombreGrupo.ToUpper(),
                    NitEmpresa = usuarioActual.NitEmpresa
                };
                db.Tbl_GruposComuniciones.Add(gpcomunicaciones);
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarGrupos()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            List<GruposComuniciones> grupos = db.Tbl_GruposComuniciones.Where(x => x.NitEmpresa==usuarioActual.NitEmpresa).ToList();
            return Json(grupos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarComunicacionesExternas() 
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            List<ComunicacionesExternas> comunicaciones = db.Tbl_ComunicacionesExternas.Where(x => x.NitEmpresa == usuarioActual.NitEmpresa).ToList();
            return Json(comunicaciones, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EditarComunicado(int PK_Id_Comunicado)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var comunicaciones = db.Tbl_ComunicacionesExternas.Where(x => (x.PK_Id_Comunicado == PK_Id_Comunicado && x.NitEmpresa==usuarioActual.NitEmpresa)).SingleOrDefault();
            return Json(comunicaciones, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EliminarComunicado(int PK_Id_Comunicado)
        {
            var comunicaciones = db.Tbl_ComunicacionesExternas.Where(x => x.PK_Id_Comunicado == PK_Id_Comunicado).SingleOrDefault();
            using (var Transaction = db.Database.BeginTransaction())
            {
                db.Tbl_ComunicacionesExternas.Remove(comunicaciones);
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    
        [HttpPost]
        public JsonResult ListarComunicacionesEnviadas() 
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            List<ComunicacionesExternas> comunicaciones = db.Tbl_ComunicacionesExternas.Where(x => x.NitEmpresa==usuarioActual.NitEmpresa).ToList();
            return Json(comunicaciones, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarComunicacionesRecibidas()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            List<ComunicacionesExternas> comunicaciones = db.Tbl_ComunicacionesExternas.Where(x => x.NitEmpresa==usuarioActual.NitEmpresa).ToList();
            return Json(comunicaciones, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarCargos(string cargo) 
        {
            var cargos = db.Tbl_Empleado_Tematica.Where(x => (x.Cargo_Empleado).Contains(cargo)).GroupBy(x => x.Cargo_Empleado).ToList();
            string[] cargoempleado = new string[cargos.Count];
            for (int i = 0; i < cargos.Count; i++)
            {
                cargoempleado[i] = cargos[i].Key;
            }
            return Json(cargoempleado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarPersonas(int Numero_Documento) 
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
            var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliados"], RestSharp.Method.GET);
            request.RequestFormat = DataFormat.Xml;
            request.Parameters.Clear();
            request.AddParameter("tpEm", "NI");
            request.AddParameter("docEm", usuarioActual.NitEmpresa);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            //se omite la validación de certificado de SSL
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            IRestResponse<List<EmpleadosWSDTO>> response = cliente.Execute<List<EmpleadosWSDTO>>(request);
            var result = response.Content;
            var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpleadosWSDTO>>(result);
            var empleado = respuesta.Where(x => x.idPersona == Numero_Documento.ToString()).SingleOrDefault();
            return Json(empleado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarGrupo(int idgrupo)
        {
            var grupos = db.Tbl_GruposComuniciones.Where(x => x.pk_id_grupo == idgrupo).SingleOrDefault();
            using (var Transaction = db.Database.BeginTransaction())
            {
                db.Tbl_GruposComuniciones.Remove(grupos);
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GuardarMiembro(int idgrupo, int? pk_id_grupo_usuario_comunicaciones, string txtnombre, string txtcorreo)
        {
            using (var Transaction = db.Database.BeginTransaction())
            {
                if (pk_id_grupo_usuario_comunicaciones != null)
                {
                    var _grupoUsuariosComunicaciones = db.Tbl_GrupoUsuariosComunicaciones.Where(x => x.pk_id_grupo_usuario_comunicaciones == pk_id_grupo_usuario_comunicaciones).SingleOrDefault();
                    _grupoUsuariosComunicaciones.nombre_contacto = txtnombre;
                    _grupoUsuariosComunicaciones.email = txtcorreo;
                    db.Tbl_GrupoUsuariosComunicaciones.Attach(_grupoUsuariosComunicaciones);
                    var entry = db.Entry(_grupoUsuariosComunicaciones);
                    entry.State = System.Data.Entity.EntityState.Modified;
                    entry.Property(x => x.nombre_contacto).IsModified = true;
                    entry.Property(x => x.email).IsModified = true;
                }
                else {
                    GrupoUsuariosComunicaciones gpusuarioscomunicaciones = new GrupoUsuariosComunicaciones()
                    {
                        fk_id_grupo_comunicaciones = idgrupo,
                        nombre_contacto = txtnombre,
                        email = txtcorreo
                    };
                    db.Tbl_GrupoUsuariosComunicaciones.Add(gpusuarioscomunicaciones);
                }
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
   
        [HttpPost]
        public JsonResult ConsultarMiembro(int pk_id_grupo_usuario_comunicaciones) 
        {
            var gpusuarioscomunicaciones = db.Tbl_GrupoUsuariosComunicaciones.Where(x => x.pk_id_grupo_usuario_comunicaciones == pk_id_grupo_usuario_comunicaciones).SingleOrDefault();
            return Json(gpusuarioscomunicaciones, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarMiembros(int idgrupo) 
        {
            var gpusuarioscomunicaciones = db.Tbl_GrupoUsuariosComunicaciones.Where(x => x.fk_id_grupo_comunicaciones == idgrupo).ToList();
            return Json(gpusuarioscomunicaciones, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarGrupo(int PK_Id_grupo, string txtmiembro, string[] armiembros)
        {
            using (var Transaction = db.Database.BeginTransaction())
            {
                bool boTransanct = false;
                if (!string.IsNullOrEmpty(txtmiembro))
                {
                    var grupo = db.Tbl_GruposComuniciones.Where(x => x.pk_id_grupo == PK_Id_grupo).SingleOrDefault();
                    grupo.NombreGrupo = txtmiembro;
                    db.Tbl_GruposComuniciones.Attach(grupo);
                    var entry = db.Entry(grupo);
                    entry.State = System.Data.Entity.EntityState.Modified;
                    entry.Property(x => x.NombreGrupo).IsModified = true;
                    boTransanct = true;
                }

                if (armiembros != null)
                {
                    for (int i = 0; i < armiembros.Length; i++)
                    {
                        string[] ars = armiembros[i].Split(',');
                        int pk_id_grupo_usuario_comunicaciones = int.Parse(ars[0]);
                        bool Status = Convert.ToBoolean(ars[1]);
                        var grupomiembros = db.Tbl_GrupoUsuariosComunicaciones.Where(x => x.pk_id_grupo_usuario_comunicaciones == pk_id_grupo_usuario_comunicaciones).SingleOrDefault();
                        grupomiembros.Status = Status;
                        db.Tbl_GrupoUsuariosComunicaciones.Attach(grupomiembros);
                        var entry = db.Entry(grupomiembros);
                        entry.State = System.Data.Entity.EntityState.Modified;
                        entry.Property(x => x.Status).IsModified = true;
                    }

                    boTransanct = true;
                }

                if (boTransanct)
                {
                    db.SaveChanges();
                    Transaction.Commit();
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EliminarMiembros(int pk_id_grupo_usuario_comunicaciones)
        {
            var grupos = db.Tbl_GrupoUsuariosComunicaciones.Where(x => x.pk_id_grupo_usuario_comunicaciones == pk_id_grupo_usuario_comunicaciones).SingleOrDefault();
            using (var Transaction = db.Database.BeginTransaction())
            {
                db.Tbl_GrupoUsuariosComunicaciones.Remove(grupos);
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Comunicaciones Aduntas

        public ActionResult IndexExternas()
        {
            SetDDL();
            return View();
        }

        private void SetDDL()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text = "Si Requiere", Value = "S" });
            lst.Add(new SelectListItem() { Text = "No Requiere", Value = "N" });
            ViewBag.requiere = new SelectList(lst, "Text", "Value");

            List<SelectListItem> lst1 = new List<SelectListItem>();
            lst1.Add(new SelectListItem() { Text = "Enviado", Value = "E" });
            lst1.Add(new SelectListItem() { Text = "Recibido", Value = "R" });
            ViewBag.tipo = new SelectList(lst1, "Text", "Value");
        }

        [HttpGet]
        public JsonResult ListarRecibidas()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var ComunicadosAdjuntos = db.Tbl_ComunicadosAdjuntos.Where(x => (x.tipo=="R" && x.NitEmpresa==usuarioActual.NitEmpresa)).ToList();
            return Json(ComunicadosAdjuntos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarEnviadas()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var ComunicadosAdjuntos = db.Tbl_ComunicadosAdjuntos.Where(x => (x.tipo == "E" && x.NitEmpresa==usuarioActual.NitEmpresa)).ToList();
            return Json(ComunicadosAdjuntos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EditarComunicadoAdjunto(int pk_id_comadjunto)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var ComunicadosAdjuntos = db.Tbl_ComunicadosAdjuntos.Where(x => (x.pk_id_comadjunto == pk_id_comadjunto && x.NitEmpresa==usuarioActual.NitEmpresa)).SingleOrDefault();
            return Json(ComunicadosAdjuntos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarComunicadosAdjuntos(ComunicadosAdjuntosModel common)
        {
            int pk_id_comadjunto = common.pk_id_comadjunto;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            using (var Transaction = db.Database.BeginTransaction())
            {
                if (pk_id_comadjunto != 0)
                {
                    var grupo = db.Tbl_ComunicadosAdjuntos.Where(x => x.pk_id_comadjunto == pk_id_comadjunto).SingleOrDefault();
                    grupo.nombre = common.nombre;
                    grupo.entidad = common.entidad;
                    grupo.descripcion = common.descripcion;
                    grupo.fecha = common.fecha;
                    grupo.adjunto = common.adjunto;
                    grupo.respuesta = common.respuesta;
                    grupo.requiere = common.requiere;
                    grupo.tipo = common.tipo;
                    var entry = db.Entry(grupo);
                    entry.State = System.Data.Entity.EntityState.Modified;
                    entry.Property(x => x.nombre).IsModified = true;
                    entry.Property(x => x.entidad).IsModified = true;
                    entry.Property(x => x.descripcion).IsModified = true;
                    entry.Property(x => x.fecha).IsModified = true;
                    entry.Property(x => x.adjunto).IsModified = true;
                    entry.Property(x => x.respuesta).IsModified = true;
                    entry.Property(x => x.requiere).IsModified = true;
                    entry.Property(x => x.tipo).IsModified = true;
                }
                else {
                    ComunicadosAdjuntos commun = new ComunicadosAdjuntos()
                    {
                        nombre = common.nombre,
                        entidad = common.entidad,
                        descripcion = common.descripcion,
                        fecha = common.fecha,
                        adjunto = common.adjunto,
                        respuesta = common.respuesta,
                        requiere = common.requiere,
                        tipo = common.tipo,
                        NitEmpresa = usuarioActual.NitEmpresa
                    };
                    db.Tbl_ComunicadosAdjuntos.Add(commun);
                    db.SaveChanges();
                    pk_id_comadjunto = commun.pk_id_comadjunto;
                }                
                
                Transaction.Commit();
                
            }

            return Json(pk_id_comadjunto, JsonRequestBehavior.AllowGet);
        }
      
        [HttpGet]
        public JsonResult EliminarComunicadoAdjunto(int pk_id_comadjunto)
        {
            var comunicaciones = db.Tbl_ComunicadosAdjuntos.Where(x => x.pk_id_comadjunto == pk_id_comadjunto).SingleOrDefault();
            using (var Transaction = db.Database.BeginTransaction())
            {
                db.Tbl_ComunicadosAdjuntos.Remove(comunicaciones);
                db.SaveChanges();
                Transaction.Commit();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult SubirArchivo()
        {
            HttpPostedFileBase myFile = Request.Files["adjunto"];
            bool isUploaded = false;
            string message = "File upload failed";
            string fullpath = string.Empty;
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

        /// <summary>
        /// Creates the folder if needed.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
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

        [HttpGet]
        public JsonResult ActualizarAdjuntos(int pk_id_comadjunto, string adjunto)
        {
            var comunicaciones_externas = db.Tbl_ComunicadosAdjuntos.Where(x => x.pk_id_comadjunto == pk_id_comadjunto).SingleOrDefault();
            comunicaciones_externas.adjunto = adjunto;
            db.Tbl_ComunicadosAdjuntos.Attach(comunicaciones_externas);
            var entry = db.Entry(comunicaciones_externas);
            entry.State = System.Data.Entity.EntityState.Modified;
            entry.Property(x => x.adjunto).IsModified = true;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

       [HttpPost]
        public string DescargarArchivo(int pk_id_comadjunto)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var comunicaciones_externas = db.Tbl_ComunicadosAdjuntos.Where(x => (x.pk_id_comadjunto == pk_id_comadjunto && x.NitEmpresa==usuarioActual.NitEmpresa)).SingleOrDefault();
            // Obtener contenido del archivo
            string text = comunicaciones_externas.adjunto;
            return text;
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

        #endregion

    }


}
