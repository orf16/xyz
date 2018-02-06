using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SG_SST.Models.Politica;
using SG_SST.Services.Politica.IServices;
using SG_SST.Services.Politica.Services;
using System.IO;
using SG_SST.Models;
using System.Data.Entity;
using SG_SST.Models.Empresas;
//using SG_SST.Utilidades.Seguridad;
using SG_SST.EntidadesDominio.Usuario;
using SG_SST.Controllers.Base;



namespace SG_SST.Controllers.Politica
{
    public class PoliticasstController : BaseController
    {
        IpoliticaServicios gs;/// Defino variable gs
        private SG_SSTContext db = new SG_SSTContext();
        public static string strvalor_checkbox = "false";
        public static int intvalorvalidacion = 0;


        // GET: Politicasst
        public ActionResult Index(mPolitica politica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }

            mPolitica OBJPOL = new mPolitica();
            //CargarPolitica2();
            //gs = new PoliticaServicios();
            //string politica2 = gs.ObtenerPolitica(SessionDataSG_SST.EmpresaSession.Pk_Id_Empresa);
            //ViewBag.strDescripcion_Politica = politica2;
            //return RedirectToAction("Index");  

            // OBJPOL.strDescripcion_Politica = db.Tbl_Politica.Find(SessionDataSG_SST.EmpresaSession.Pk_Id_Empresa);

            gs = new PoliticaServicios();
            string politicatextarea = gs.ObtenerPolitica(usuarioActual.IdEmpresa);

            OBJPOL.strDescripcion_Politica = politicatextarea;

            mPolitica mpol = db.Tbl_Politica.Find(usuarioActual.IdEmpresa);

            List<mPolitica> politicas = db.Tbl_Politica.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa && p.Archivo_Politica != null && p.strDescripcion_Politica == null).ToList();
            if (politicas.Count() > 0)
            {
                ViewBag.Messages = "Ya se encuentra cargada una Politica de Seguridad y Salud en el Trabajo";
                return View("MenuPolitica");
            }
            else
            {
                mPolitica objpoliticas = db.Tbl_Politica.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa).FirstOrDefault();


                return View(objpoliticas);
            }
        }



        [ValidateInput(false)]
        public ActionResult GrabarPolitica(mPolitica politica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            if (politica.strDescripcion_Politica != null)
            {
                politica.FK_Empresa = usuarioActual.IdEmpresa;
                
                if(intvalorvalidacion ==1 ){
                    politica.Firma_Rep = true;
                    }
                gs = new PoliticaServicios();
                if (gs.GrabarPoliticaEmpresa(politica.strDescripcion_Politica, politica.FK_Empresa,politica.Firma_Rep) != false)
                {
                    ViewBag.Messages2 = "Politica guardada correctamente";
                    return View("Index");
                }
                else
                {

                    ViewBag.Messages2 = "no se pudo guardar la información";
                    return View("Index");
                }
            }
            else
            {
                ViewBag.Messages2 = "Por favor ingrese la Politica de Seguridad y Salud en el Trabajo";
                return View("Index");
            }
        }

 

        public ActionResult EliminarPolitica(int IntELiminar)
        {
            intvalorvalidacion = 0;
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            gs = new PoliticaServicios();

            if (gs.EliminarPolitica(usuarioActual.IdEmpresa) == true)
            {

                if (IntELiminar == 1) //se elimina desde crear politica -Index
                {
                    ViewBag.Messages2 = "Politica eliminada exitosamente";
                    return View("Index");
                }
                else
                    if (IntELiminar == 0)
                    {// se elimina desde MostrarPolitica
                        ViewBag.Messages2 = "Archivo eliminado exitosamente";
                        return View("MenuPolitica");
                    }
                return View("Index");
            }
            else
            {
                if (IntELiminar == 1) //se elimina desde crear politica -Index
                {
                    ViewBag.Messages2 = "No se encuentra registro para eliminar";
                    return View("Index");
                }
                else
                    if (IntELiminar == 0)
                    {// se elimina desde MostrarPolitica
                        ViewBag.Messages2 = "No se encuentra registro para eliminar";
                        return View("MenuPolitica");
                    }
            }
            return View("MenuPolitica");



        }



        /// <summary>
        /// action result que maneja la vista - q muestra la descripcion de la politica y la firma del rep legal
        /// </summary>
        /// <param name="pdf"></param>
        /// <returns></returns>
        public ActionResult GetReporte(int fkempresa, bool pdf = false)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }


            gs = new PoliticaServicios();

            mPolitica objpol = new mPolitica();
            objpol.FK_Empresa = usuarioActual.IdEmpresa;

            objpol = gs.validarestadofirma(usuarioActual.IdEmpresa);//SE VALIDA EN BASE DE DATOS QUE SE HAYA ANEXADO LA FIRMA (CHEQUEADO EL CHEKBOX MODULO POLITICA)

            if (objpol.Firma_Rep == true)
            {
                ViewBag.mostrarFirma = true;
            }

            gs = new PoliticaServicios();
            string politica = gs.ObtenerPolitica(usuarioActual.IdEmpresa);
            ViewBag.PDF = pdf;
            ViewBag.DescripcionPolitica = politica;



            return View();
        }



        /// <summary>
        /// Metodo que muestra la firma digital en el PDF de Politica
        /// </summary>
        /// <returns></returns>
        public ActionResult GetImagenRepresentanteLegal()
        {
            if (TempData["FirmaPolitica"] != null)
            {
                var objfirma = (PoliticaModel)TempData["FirmaPolitica"]; //se saca el objeto
                return File(objfirma.FirmaFullPath, "image/png", objfirma.FirmaFile);
            }
            else
            {
                return View("GetReporte");
            }

        }
        public ActionResult MenuPolitica()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            mPolitica politicas = db.Tbl_Politica.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa).FirstOrDefault();
            if (politicas != null)
            {
                ViewBag.Imprimir = 1;
                return View();
            }
            ViewBag.Messages = "No tiene generada una Politica de Seguridad y Salud en el Trabajo, por favor proceda a generarla";
            return View("MenuPolitica");
        }


        public ActionResult CargarArchivoPolitica()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            List<mPolitica> politicas = db.Tbl_Politica.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa).ToList();
            if (politicas.Count() > 0)
            {
                ViewBag.Messages = "Ya se encuentra cargada o creada una Politica de Seguridad y Salud en el Trabajo";
                return View("MenuPolitica");
            }
            else
            {
                return View("CargarPolitica");
            }
        }

        public ActionResult CargarArchivo(mPolitica politica, HttpPostedFileBase ArchivoPolitica)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            var path = "";

            if (ArchivoPolitica != null)
            {
                if (ArchivoPolitica.ContentLength > 0)
                {
                    if (Path.GetExtension(ArchivoPolitica.FileName).ToLower() == ".pdf")
                    {
                        path = Path.Combine(Server.MapPath("~/Content/Images"), ArchivoPolitica.FileName);
                        ArchivoPolitica.SaveAs(path);
                        ViewBag.UploadSuccess = true;
                        politica.FK_Empresa = usuarioActual.IdEmpresa;
                        politica.Archivo_Politica = ArchivoPolitica.FileName;
                        ViewBag.Messages2 = "Politica cargada correctamente";
                    }
                }
            }
            gs = new PoliticaServicios();

            if (gs.GrabarPolitica(politica) != true)
            {
                ViewBag.Messages = "Por favor ingrese un archivo en formato PDF";
                return View("CargarPolitica");
            }
            else
            {
                return View("MenuPolitica");
            }
        }

        public ActionResult Pdf()
        {


            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            //    return new Rotativa.PartialViewAsPdf("GetReporte", new { valorid = usuarioActual.IdEmpresa, PDF = true });
            return new Rotativa.ActionAsPdf("GetReporte", new { valorid = usuarioActual.IdEmpresa, PDF = true });
        }

        public ActionResult CargarPolitica()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            gs = new PoliticaServicios();
            string politica = gs.ObtenerPolitica(usuarioActual.IdEmpresa);
            if (politica != "")
            {
                return Json(new { success = true, Politica = politica }
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.Messages = "Ingrese una Politica";
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }



        /// <summary>
        /// se visualizan la politica - pdf cargado
        /// </summary>
        /// <returns></returns>
        public ActionResult MostrarPoliticaPDF()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
                return View();
            }
            List<mPolitica> politicas = db.Tbl_Politica.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa).ToList();
            if (politicas.Count() > 0)
            {
                if (politicas.FirstOrDefault().Archivo_Politica != null && politicas.FirstOrDefault().Archivo_Politica != "")
                {
                    ViewBag.FK_Empresa = usuarioActual.IdEmpresa;
                    return View();
                }
                else
                {
                    if (politicas.FirstOrDefault().strDescripcion_Politica != null && politicas.FirstOrDefault().strDescripcion_Politica != "")
                    {
                        return RedirectToAction("Pdf", new { usuarioActual.IdEmpresa });
                    }
                }

            }
            ViewBag.Messages = "No tiene creada o cargada una Politica de Seguridad y Salud en el Trabajo, por favor proceda a crearla o cargarla";
            return View("MenuPolitica");
        }

        //Metodo para Mostrar la politica cuando es cargada en PDF.
        public FileStreamResult PoliticaPDF()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            List<mPolitica> politicas = db.Tbl_Politica.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa).ToList();
            var path = Server.MapPath("~/Content/Images");
            var file = politicas.FirstOrDefault().Archivo_Politica;
            var fullPath = Path.Combine(path, file);
            FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            return File(fs, "application/pdf");
        }

        /// <summary>
        /// Este método permite que el textarea de politica sea editable
        /// </summary>
        /// <returns></returns>
        public ActionResult EdicionTextoPoliica()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            gs = new PoliticaServicios();
            string politica = gs.ObtenerPolitica(usuarioActual.IdEmpresa);
            int Politica_Existe;
            if (politica != "")
            {
                Politica_Existe = 1;
                return Json(new { success = true, Politica = Politica_Existe }
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.Messages = "Ingrese una Politica";
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }


        /// <summary>
        /// método que permite ver el reporte de politica (pdf) dentro del portal - positiva
        /// </summary>
        /// <param name="pdf"></param>
        /// <returns></returns>

        public ActionResult ReportePolitica_Aplicativo()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            List<mPolitica> politicas = db.Tbl_Politica.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa).ToList();
            if (politicas.Count() > 0)
            {
                if (politicas.FirstOrDefault().Archivo_Politica != null && politicas.FirstOrDefault().Archivo_Politica != "")//si hay archivo
                {
                    return RedirectToAction("PoliticaPDF");
                }
                else
                    if (politicas.FirstOrDefault().strDescripcion_Politica != null && politicas.FirstOrDefault().strDescripcion_Politica != "")
                    {
                        //return RedirectToAction("Pdf", new { SessionDataSG_SST.EmpresaSession.Pk_Id_Empresa });
                        // return RedirectToAction("MostrarPoliticaPDF");
                        ViewBag.DescripcionPolitica = politicas.FirstOrDefault().strDescripcion_Politica;
                        return new Rotativa.PartialViewAsPdf("PoliticaVP");
                    }
                    else
                    {
                        ViewBag.Messages = "No tiene generada una Politica de Seguridad y Salud en el Trabajo, por favor proceda a generarla";
                        return View("PoliticaInicio");
                    }
            }
            ViewBag.Messages = "No tiene generada una Politica de Seguridad y Salud en el Trabajo, por favor proceda a generarla";
            return View("MenuPolitica");
        }



        public ActionResult Reporte_Documento()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }

            List<mPolitica> politicas = db.Tbl_Politica.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa).ToList();

            PoliticaModel objpolmodel = new PoliticaModel();

            if (politicas.Count() > 0)
            {
                if (politicas.FirstOrDefault().Archivo_Politica != null && politicas.FirstOrDefault().Archivo_Politica != "")//si hay archivo
                {
                    //ViewBag.FK_Empresa = SessionDataSG_SST.EmpresaSession.Pk_Id_Empresa;
                    return RedirectToAction("PoliticaPDF");
                }
                else
                {
                    if (politicas.FirstOrDefault().strDescripcion_Politica != null && politicas.FirstOrDefault().strDescripcion_Politica != "")
                    {
                        gs = new PoliticaServicios(); 
                        mPolitica objpol = new mPolitica();
                        objpolmodel.DescripcionPolitica = politicas.FirstOrDefault().strDescripcion_Politica;
                        objpol = gs.validarestadofirma(usuarioActual.IdEmpresa);//SE VALIDA EN BASE DE DATOS QUE SE HAYA ANEXADO LA FIRMA (CHEQUEADO EL CHEKBOX MODULO POLITICA)

                        if (objpol.Firma_Rep == true)
                        {
                            objpolmodel.MostrarFirma = true;

                            Usuario objusur = gs.ValidarExisteFirma(usuarioActual.IdEmpresa);//se valida que la empresa tenga usuario representante legal

                            if (objusur != null && objusur.Imagen_Firma != null)
                            {
                                string nombreFirma = objusur.Imagen_Firma;
                                var path = Server.MapPath("~/Content/Images");
                                //var file = string.Format("{0}.png", nombreFirma);
                                var file = nombreFirma;
                                var fullPath = Path.Combine(path, file);
                                objpolmodel.FirmaFullPath = fullPath;
                            }
                        }
                        return new Rotativa.ViewAsPdf("GetReporte", objpolmodel);                       
                    }
                }
            }
            ViewBag.Messages2 = "No tiene generada una Politica de Seguridad y Salud en el Trabajo, por favor proceda a generarla";
            return View("Index");
        }


      
        //metodo que valida si existe una firma cargada del representante legal
        public ActionResult Validar_ExisteFirmaRepLegal()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            gs = new PoliticaServicios();

            Usuario objusur = gs.ValidarExisteFirma(usuarioActual.IdEmpresa);//se valida que la empresa tenga usuario representante legal
            string objUsurObt = gs.ObtenerPolitica(usuarioActual.IdEmpresa);


            intvalorvalidacion = 0;//la variable estatica vuelve a tener el valor 0


            if (objusur != null && objusur != null && objusur.Imagen_Firma != null)
            {
            
            
         


            if (objusur.Imagen_Firma != "")//se valida que el representante legal tenga cargada la firma
            {
                mPolitica objpol = new mPolitica();

                objpol.FK_Empresa = usuarioActual.IdEmpresa;
                objpol.Firma_Rep = true;

                gs.ObtenerGuardar_Estadofirma(objpol);
                intvalorvalidacion = 1;
                return Json(new { success = true, mensaje = "¡Firma anexada al documento con éxito!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, mensaje = "¡Señor Usuario, no se encuentra cargada la firma del representante legal!" }, JsonRequestBehavior.AllowGet);
            }
            }
            else
            {
                return Json(new { success = false, mensaje = "¡Señor Usuario, no se encuentra generado el usuario representante legal, por favor proceda a generarlo!" }, JsonRequestBehavior.AllowGet);
            }


        }
    


        public ActionResult PoliticaInicio()
        {
            return View();
        }




        public ActionResult Validar_ExistePoliticaCreada()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            mPolitica politicas = db.Tbl_Politica.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa).FirstOrDefault();
         

            gs = new PoliticaServicios();
            string objUsurObt = gs.ObtenerPolitica(usuarioActual.IdEmpresa);


            if (objUsurObt != "")//se valida que esista una politica creada
            {
                return Json(new { success = true, mensaje = "se encuientra una politica" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, mensaje = "¡por favor crear un apolitica!" }, JsonRequestBehavior.AllowGet);
            }
        }

        /*
        public ActionResult Validar_ExisteFirmaRepLegal()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            gs = new PoliticaServicios();

            string objUsurObt = gs.ObtenerPolitica(usuarioActual.IdEmpresa);


            if (objUsurObt != "")//se valida que esista una politica creada
            {
                return Json(new { success = true, mensaje = "¡Firma anexada al documento con éxito!" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, mensaje = "¡Señor Usuario, no se encuentra cargada la firma del representante legal!" }, JsonRequestBehavior.AllowGet);
            }
        }
        */





        /// <summary>
        /// metodo que se utiliza para validar si exixte politica creada antes de abrir el modal
        /// </summary>
        /// <returns></returns>
        public ActionResult ValidarPoliticaModal()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            mPolitica politicas = db.Tbl_Politica.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa).FirstOrDefault();
            if (politicas != null)
            {
                ViewBag.Imprimir = 1;
                mPolitica objpoliticas = db.Tbl_Politica.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa).FirstOrDefault();

                return View("Index",objpoliticas);
            }
            ViewBag.Messages2 = "No tiene generada una Politica de Seguridad y Salud en el Trabajo, por favor proceda a generarla";
            
            
            
            
            return View("Index", db.Tbl_Politica.FirstOrDefault());

            //mPolitica objpoliticas = db.Tbl_Politica.Where(p => p.FK_Empresa == usuarioActual.IdEmpresa).FirstOrDefault();
            
            //return View(objpoliticas);



        }













    }
}

