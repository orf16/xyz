using SG_SST.Controllers.Base;
using SG_SST.EntidadesDominio.ComunicadosAPP;
using SG_SST.Models;
using SG_SST.Models.Comunicaciones;
using SG_SST.Models.ComunicadosAPP;
using SG_SST.Models.Organizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Configuration;
using RestSharp;
using SG_SST.Dtos.Organizacion;
using System.Threading;
using System.Text.RegularExpressions;
using System.Web;


namespace SG_SST.Controllers.Comunicaciones
{
    public class ComunicadosAPPController : BaseController
    {
        private SG_SSTContext db = new SG_SSTContext();

        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }

            ComunicadosAPPModel objcomunicadosapp = new ComunicadosAPPModel();
            //objcomunicadosapp.ListarComunicadosAPP = ListarComunicadosAPP();
            return View(objcomunicadosapp);
        }

        public JsonResult ListarComunicadosAPP()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            var comunicadoapp = db.Tbl_ComunicadosAPP.Where(x => x.NitEmpresa==usuarioActual.NitEmpresa).ToList();
            return Json(comunicadoapp, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [ValidateInput(false)]
        public JsonResult EliminarComunicado(int? IDComunicadosAPP)
        {

            using (var Transaction = db.Database.BeginTransaction())
            {
                if (IDComunicadosAPP > 0)
                {
                    var comunicaciones = db.Tbl_UsuarioComunicadoAPP.Where(x => x.FK_Id_ComunicadosAPP == IDComunicadosAPP).ToList();
                    db.Tbl_UsuarioComunicadoAPP.RemoveRange(comunicaciones);
                    var comunicaciones1 = db.Tbl_ComunicadosAPP.Where(x => x.IDComunicadosAPP == IDComunicadosAPP).SingleOrDefault();
                    db.Tbl_ComunicadosAPP.Remove(comunicaciones1);
                }

                db.SaveChanges();
                Transaction.Commit();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
       
        [HttpGet]
        [ValidateInput(false)]
        public JsonResult GuardarComunicado(int? IDComunicadosAPP, string Titulo, string Asunto, string arreglo, string Origen)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            int PK_Id_Comunicado_temp = 0;
            if (Origen == "E")
            {
                using (var Transaction = db.Database.BeginTransaction())
                {
                    var comunicado = db.Tbl_ComunicadosAPP.Where(x => x.IDComunicadosAPP == IDComunicadosAPP).SingleOrDefault();
                    comunicado.Estado = "Enviado";
                    comunicado.FechaEnvio = DateTime.Now.ToString();
                    db.Tbl_ComunicadosAPP.Attach(comunicado);
                    var entry = db.Entry(comunicado);
                    entry.State = System.Data.Entity.EntityState.Modified;
                    entry.Property(x => x.Estado).IsModified = true;
                    db.SaveChanges();
                    Transaction.Commit();
                    Thread myNewThread = new Thread(() => EnviarNotificacionesPush(IDComunicadosAPP));
                    myNewThread.Start();
                }
            }
            else {
                string FechaEnvio = string.Empty;
                string EstadoComunicado = "En Espera";
                using (var Transaction = db.Database.BeginTransaction())
                {
                    if (IDComunicadosAPP > 0)
                    {
                        var comunicaciones = db.Tbl_UsuarioComunicadoAPP.Where(x => x.FK_Id_ComunicadosAPP == IDComunicadosAPP).ToList();
                        db.Tbl_UsuarioComunicadoAPP.RemoveRange(comunicaciones);
                        db.SaveChanges();

                        var comunicaciones1 = db.Tbl_ComunicadosAPP.Where(x => x.IDComunicadosAPP == IDComunicadosAPP).SingleOrDefault();
                        db.Tbl_ComunicadosAPP.Remove(comunicaciones1);
                        db.SaveChanges();

                    }

                    string asun = StripTagsRegex(Asunto);
                    asun = HttpUtility.HtmlDecode(asun);
                    ComunicacionesAPP comunicadosapp = new ComunicacionesAPP()
                    {
                        Titulo = Titulo,
                        Asunto = Asunto,
                        AsuntoAPP = asun,
                        Destinatarios = arreglo,
                        FechaCreacion = DateTime.Now.ToString(),
                        FechaEnvio = FechaEnvio,
                        Estado = EstadoComunicado,
                        NitEmpresa = usuarioActual.NitEmpresa
                    };
                    db.Tbl_ComunicadosAPP.Add(comunicadosapp);
                    db.SaveChanges();
                    PK_Id_Comunicado_temp = comunicadosapp.IDComunicadosAPP;
                    Transaction.Commit();
                    Transaction.Dispose();
                    Thread myNewThread = new Thread(() => GuardarUsuariosComunicados(comunicadosapp, arreglo, usuarioActual.NitEmpresa));
                    myNewThread.Start();
                    }

            }

            var parametros = new { PK_Id_Comunicado_temp = PK_Id_Comunicado_temp, origen = Origen };
            return Json(parametros, JsonRequestBehavior.AllowGet);
        }

        public void GuardarUsuariosComunicados(ComunicacionesAPP comunicadosapp, string arreglo, string NitEmpresa)
        {
            using (var Transaction = db.Database.BeginTransaction())
            {
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
                bool boTransaction = false;
                string[] arreglos = arreglo.Split(',');
                for (int i = 0; i < arreglos.Length; i++)
                {
                    string[] persona = arreglos[i].Split('-');
                    if (persona[0] != null)
                    {
                        string cedula = persona[0];
                        var usuariosuscrito = db.Tbl_ComunicacionesUsuariosSuscritosAPP.Where(x => x.IdentificacionUsuario == cedula).SingleOrDefault();
                        if (usuariosuscrito != null)
                        {
                            var usuarioexiste = db.Tbl_UsuarioComunicadoAPP.Where(x => (x.IdentificacionUsuario == cedula && x.FK_Id_ComunicadosAPP == comunicadosapp.IDComunicadosAPP)).SingleOrDefault();
                            if (usuarioexiste == null)
                            {
                                UsuarioComunicadoAPP objusuarios = new UsuarioComunicadoAPP()
                                {
                                    FK_Id_ComunicadosAPP = comunicadosapp.IDComunicadosAPP,
                                    IdentificacionUsuario = cedula,
                                    PlayerID = usuariosuscrito.PlayerID,
                                    IDEstadoComunicado = 4
                                };
                                db.Tbl_UsuarioComunicadoAPP.Add(objusuarios);
                                db.SaveChanges();
                                boTransaction = true;
                            }
                        }
                        else
                        {
                            string parametro = arreglos[i];
                            var cargos_temporales = respuesta.Where(x => x.cargo == parametro).ToList();
                            if (cargos_temporales.Count > 0)
                            {
                                foreach (var item in cargos_temporales)
                                {
                                    string cedula1 = item.idPersona;
                                    var usuariosuscrito1 = db.Tbl_ComunicacionesUsuariosSuscritosAPP.Where(x => x.IdentificacionUsuario == cedula1).SingleOrDefault();
                                    if (usuariosuscrito1 != null)
                                    {
                                        var usuarioexiste = db.Tbl_UsuarioComunicadoAPP.Where(x => (x.IdentificacionUsuario == cedula && x.FK_Id_ComunicadosAPP == comunicadosapp.IDComunicadosAPP)).SingleOrDefault();
                                        if (usuarioexiste == null)
                                        {
                                            UsuarioComunicadoAPP objusuarios = new UsuarioComunicadoAPP()
                                            {
                                                FK_Id_ComunicadosAPP = comunicadosapp.IDComunicadosAPP,
                                                IdentificacionUsuario = usuariosuscrito1.IdentificacionUsuario,
                                                PlayerID = usuariosuscrito1.PlayerID,
                                                IDEstadoComunicado = 4
                                            };
                                            db.Tbl_UsuarioComunicadoAPP.Add(objusuarios);
                                            db.SaveChanges();
                                            boTransaction = true;
                                        }
                                    }
                                }
                            }

                        }
                    }
                }

                if (boTransaction)
                {
                    Transaction.Commit();
                }
            }
        }

        public void EnviarNotificacionesPush(int? pk_id_comunicado)
        {
            var usuariosapp = db.Tbl_UsuarioComunicadoAPP.Where(x => x.FK_Id_ComunicadosAPP == pk_id_comunicado).ToList();
            using (var Transaction = db.Database.BeginTransaction())
            {
                foreach (var item in usuariosapp)
                {
                    item.IDEstadoComunicado = 1;
                    db.Tbl_UsuarioComunicadoAPP.Attach(item);
                    var entry = db.Entry(item);
                    entry.State = System.Data.Entity.EntityState.Modified;
                    entry.Property(x => x.IDEstadoComunicado).IsModified = true;
                    db.SaveChanges();
                }
                Transaction.Commit();
            }
            
            var comunicado = db.Tbl_ComunicadosAPP.Where(x => x.IDComunicadosAPP == pk_id_comunicado).SingleOrDefault();
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;
            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("authorization", "Basic NGEwMGZmMjItY2NkNy0xMWUzLTk5ZDUtMDAwYzI5NDBlNjJj");
            string asunto = comunicado.Titulo;
            string contenido = comunicado.AsuntoAPP;
            string[] in_player_ids = new string[usuariosapp.Count];

            for (int i = 0; i < usuariosapp.Count; i++)
            {
                if (usuariosapp[i].PlayerID != "") { 
                    in_player_ids[i] = usuariosapp[i].PlayerID;            
                }
                
            }

            if (in_player_ids.Length>0)
            {
                var serializer = new JavaScriptSerializer();
                var obj = new
                {
                    app_id = "117eabf4-4cdd-4bf9-ad76-c9e62d373568",
                    contents = new { en = contenido, es = contenido },
                    headings = new { en = asunto, es = asunto },
                    ios_badgeType = "Increase",
                    ios_badgeCount = 1,
                    include_player_ids = in_player_ids
                };

                var param = serializer.Serialize(obj);
                byte[] byteArray = Encoding.UTF8.GetBytes(param);

                string responseContent = null;

                try
                {
                    using (var writer = request.GetRequestStream())
                    {
                        writer.Write(byteArray, 0, byteArray.Length);
                    }

                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            responseContent = reader.ReadToEnd();
                        }
                    }
                }
                catch (WebException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
                    using (var Transaction = db.Database.BeginTransaction())
                    {
                        ComunicacionesLog log = new ComunicacionesLog()
                        {
                            fk_id_comunicaciones = (int)pk_id_comunicado,
                            modulo = "app",
                            enviado_rechazado = false

                        };
                        db.Tbl_ComunicacionesLog.Add(log);
                        db.SaveChanges();
                        Transaction.Commit();
                    }
                    
                }

                System.Diagnostics.Debug.WriteLine(responseContent);
            }
            
        }

        #region DECODE HTML

        private string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        private string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

        private string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        #endregion

        [HttpGet]
        [ValidateInput(false)]
        public JsonResult EditarComunicado(int? IDComunicadosAPP)
        {
            var comunicadoapp = db.Tbl_ComunicadosAPP.Where(x => x.IDComunicadosAPP == IDComunicadosAPP).SingleOrDefault();
            return Json(comunicadoapp, JsonRequestBehavior.AllowGet);
        }


    }
}
