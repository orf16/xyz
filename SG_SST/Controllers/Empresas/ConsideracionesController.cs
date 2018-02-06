
namespace SG_SST.Controllers.Empresas
{
    using SG_SST.Models;
    using SG_SST.Models.Empresas;
    using SG_SST.Services.Empresas.IServices;
    using SG_SST.Services.Empresas.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    //using Export_word.Models;
    using System.IO;
    using System.Web.UI.WebControls;
    using System.Web.UI;
    using SG_SST.Controllers.Base;



    public class ConsideracionesController : BaseController
    {
        // GET: Consideraciones
        SG_SSTContext dbConsider;
        IMatrizServicios matrizServicios = new MatrizServicios();
        int tipoDOFA = 1;// Clave Primaria del tipo de analisis dofa en la base de datos
        int tipoPEST = 2;
        /*
        public SG_SST.Services.Empresas.Services.MatrizServicios MatrizServicios
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }//Clave Primaria del tipo de analisis Pest en la base de datos
        */

        public ActionResult Index()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            return View();
        }

        /// <summary>
        /// Retorna la vista del menu para seleccionar si crear o consultar o tipo de analisis DOFA Y PEST
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuAnalisis()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            return View();
        }

        /// <summary>
        /// Retorna la vista para crear un matriz de analisis DOFA
        /// </summary>
        /// <returns></returns>
        public ActionResult DOFA()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            //  string nit = SessionDataSG_SST.UsuarioSession.Empresa.Nit_Empresa;   
            List<TipoElementoAnalisis> elementosAnalisis = matrizServicios.ObtenerTipoElementosPorAnalissis(tipoDOFA);
            ViewBag.elementosMatriz = new SelectList(elementosAnalisis, "PK_Tipo_Elemneto_Analisis", "Descripcion_Elemento");
            return View(matrizServicios.ObtenerElementosMatriz(tipoDOFA, usuarioActual.NitEmpresa));
        }

        /// <summary>
        /// Retorna la vista para crear un matriz de analisis PEST
        /// </summary>
        /// <returns></returns>
        public ActionResult PEST()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            List<TipoElementoAnalisis> elementosAnalisis = matrizServicios.ObtenerTipoElementosPorAnalissis(tipoPEST);
            ViewBag.elementosMatriz = new SelectList(elementosAnalisis, "PK_Tipo_Elemneto_Analisis", "Descripcion_Elemento");
            return View(matrizServicios.ObtenerElementosMatriz(tipoPEST, usuarioActual.NitEmpresa));
        }

        /// <summary>
        /// Metodo que retonar el elemento agregado o guardado
        /// </summary>
        /// <param name="elementoMatriz">elemento de la matriz a guardar</param>
        /// <returns></returns>
        public ActionResult elementoMatriz(ElementoMatriz elementoMatriz)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            ElementoMatriz elementoGuardado = matrizServicios.AgregarElementoMatriz(elementoMatriz, usuarioActual.IdEmpresa);
         
          
            if (elementoGuardado.Descripcion_Elemento != null)
            {
                return Json(new
                {
                    success = true,
                    PK_Elemento_Matriz = elementoGuardado.PK_Elemento_Matriz,
                    Descripcion_Elemento = elementoGuardado.Descripcion_Elemento,
                    FK_Matriz = elementoGuardado.FK_Matriz,
                    FK_TipoElementoAnalisis = elementoGuardado.FK_TipoElementoAnalisis
                }
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Eliminar un elemento de la matriz
        /// </summary>
        /// <param name="Pk_elementoMatriz">pk o clave primaria del elemento a eliminar </param>
        /// <returns></returns>
        public ActionResult EliminarElementoMatriz(int Pk_elementoMatriz)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            return Json(new
            {
                success = matrizServicios.EliminarElementoMatriz(Pk_elementoMatriz)
            }
                 , JsonRequestBehavior.AllowGet);
        }



        public ActionResult DOFA_PDF()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            return View(matrizServicios.ObtenerElementosMatriz(tipoDOFA, usuarioActual.NitEmpresa));
        }

        public ActionResult PEST_PDF(int id)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            return View();
        }






        /// <summary>
        /// Controlador para mostrar la matria DOFA
        /// </summary>
        /// <returns></returns>
        public ActionResult MostrarDOFA()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
        }
            return View(matrizServicios.ObtenerElementosMatriz(tipoDOFA, usuarioActual.NitEmpresa));
        }

        /// <summary>
        /// Controlador para mostrar la matriz PEST
        /// </summary>
        /// <returns></returns>
        public ActionResult MostrarPEST()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            return View(matrizServicios.ObtenerElementosMatriz(tipoPEST, usuarioActual.NitEmpresa));
        }




        public ActionResult CargarMatrizElemento2(int customerIDs)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            string matriz_Cargar;
            try
            {
                matriz_Cargar = matrizServicios.ObtenerElementoDofa(customerIDs);

                return Json(new { success = true, jsmatriz_Cargar = matriz_Cargar }
                     , JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }



        public ActionResult MatrizEditarElemento(ElementoMatriz elementoMatriz)
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            ElementoMatriz elementoGuardado = matrizServicios.GrabarElementoMatrizEdicion(elementoMatriz);
            if (elementoGuardado.FK_Matriz != null)
            {
                return Json(new
                {
                    success = true,
                    PK_Elemento_Matriz = elementoGuardado.PK_Elemento_Matriz,
                    Descripcion_Elemento = elementoGuardado.Descripcion_Elemento,
                    FK_Matriz = elementoGuardado.FK_Matriz,
                    FK_TipoElementoAnalisis = elementoGuardado.FK_TipoElementoAnalisis
                }
                , JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        





        public ActionResult cargarMatBoton(int id) {
            string matriz_Cargar;

            matriz_Cargar = matrizServicios.ObtenerElementoDofa(id);

            return RedirectToAction("DOFA");        
        
        }
        


        /*
        public ActionResult ReporteDOFA_PDF()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            // View(matrizServicios.ObtenerElementosMatriz(tipoDOFA));
            //var data = recursoporsede.ObtenerRecursoSede(Pk_Id_Sede, Periodo);
            return new Rotativa.PartialViewAsPdf("DOFA_PDF", matrizServicios.ObtenerElementosMatriz(tipoDOFA)) { FileName = "DOFA.pdf" };

        }
        */
        /*
        public ActionResult ReportePEST_PDF()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            // View(matrizServicios.ObtenerElementosMatriz(tipoDOFA));
            //var data = recursoporsede.ObtenerRecursoSede(Pk_Id_Sede, Periodo);
            return new Rotativa.PartialViewAsPdf("PEST_PDF", matrizServicios.ObtenerElementosMatriz(tipoPEST, usuarioActual.NitEmpresa)) { FileName = "PEST.pdf" };

        }
        */

        /*
        public ActionResult ReporteDOFA_WORD(object sender, EventArgs e)
        {
            try
            {
                string filename = "StreamTest.docx";
                string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                string html = System.IO.File.ReadAllText(Server.MapPath("~/Views/Consideraciones/DOFA_WORD.html"));

                using (MemoryStream generatedDocument = new MemoryStream())
                {
                    using (WordprocessingDocument package = WordprocessingDocument.Create(generatedDocument, WordprocessingDocumentType.Document))
                    {
                        MainDocumentPart mainPart = package.MainDocumentPart;
                        if (mainPart == null)
                        {
                            mainPart = package.AddMainDocumentPart();
                            new Document(new Body()).Save(mainPart);
                        }
                        HtmlConverter converter = new HtmlConverter(mainPart);
                        //http://html2openxml.codeplex.com/wikipage?title=ImageProcessing&referringTitle=Documentation
                        //to process an image you must provide a base url
                        converter.BaseImageUrl = new Uri(Request.Url.Scheme + "://" + Request.Url.Authority);
                        Body body = mainPart.Document.Body;

                        var paragraphs = converter.Parse(html);
                        for (int i = 0; i < paragraphs.Count; i++)
                        {
                            body.Append(paragraphs[i]);
                        }

                        mainPart.Document.Save();
                    }

                    byte[] bytesInStream = generatedDocument.ToArray(); // simpler way of converting to array
                    generatedDocument.Close();

                    Response.Clear();
                    Response.ContentType = contentType;
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename);

                    //this will generate problems
                    Response.BinaryWrite(bytesInStream);
                    try
                    {
                        Response.End();
                    }
                    catch (Exception ex)
                    {
                        //Response.End(); generates an exception. if you don't use it, you get some errors when Word opens the file...
        }

                }
                return View("MostrarDOFA", matrizServicios.ObtenerElementosMatriz(tipoDOFA));
                //lblError.Visible = false;
                //lblFeedback.Visible = true;
            }
            catch (Exception ex)
            {
                //lblError.Text = "Error: " + ex.Message + " (see exception details)";
                //lblError.Visible = true;
                //lblFeedback.Visible = false;
                return View("MostrarDOFA", matrizServicios.ObtenerElementosMatriz(tipoDOFA));
            }
        }  

        */


        public ActionResult ReporteDOFA_PDF()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            // View(matrizServicios.ObtenerElementosMatriz(tipoDOFA));
            //var data = recursoporsede.ObtenerRecursoSede(Pk_Id_Sede, Periodo);
            return new Rotativa.PartialViewAsPdf("DOFA_PDF", matrizServicios.ObtenerElementosMatriz(tipoDOFA, usuarioActual.NitEmpresa)) { FileName = "DOFA.pdf" };


            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        }

        public ActionResult ReportePEST_PDF()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
            }
            // View(matrizServicios.ObtenerElementosMatriz(tipoDOFA));
            //var data = recursoporsede.ObtenerRecursoSede(Pk_Id_Sede, Periodo);
            return new Rotativa.PartialViewAsPdf("PEST_PDF", matrizServicios.ObtenerElementosMatriz(tipoPEST, usuarioActual.NitEmpresa)) { FileName = "PEST.pdf" };

            //Response.Write(stringWrite.ToString());
            //Response.End();
            return View();

}




        /*
        public ActionResult ReporteDOFA_WORD(object sender, EventArgs e)
        {
            try
            {
                string filename = "StreamTest.docx";
                string contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                string html = System.IO.File.ReadAllText(Server.MapPath("~/Views/Consideraciones/DOFA_WORD.html"));


        private ExportWord db = new ExportWord();

        public ActionResult ExportData()
        {
            GridView gv = new GridView();
            gv.DataSource = matrizServicios.ObtenerElementosMatriz(tipoDOFA);
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Marklist.doc");
            Response.ContentType = "application/vnd.ms-word ";
            Response.Charset = string.Empty;

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("MostarDOFA");
        }

                    Response.Clear();
                    Response.ContentType = contentType;
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename);

             protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        /*
             public ActionResult Exportar_WORD(object sender, EventArgs e)
             {
                 Response.Clear();
                 Response.AddHeader("content-disposition", "attachment;filename=DOFA.doc");
                 Response.Charset = "";
                 Response.Cache.SetCacheability(HttpCacheability.NoCache);
                 Response.ContentType = "application/vnd.word";

                 System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                 System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                 UserControl uc = new UserControl();
                 //MyCustomUserControl mu = (MyCustomUserControl)uc.LoadControl("~/Empresas/MyCustomUserControl.ascx");
                 uc.LoadControl("~/Content/Images/");
                 uc.RenderControl(htmlWrite);
                 Response.Write(stringWrite.ToString());
                 Response.End();
                 return View();
             }
        */

        
      
        //private ExportWord db = new ExportWord();
        
        public ActionResult ExportData()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "El usuario no ha iniciado sesión el sistema";
        }
            GridView gv = new GridView();
            gv.DataSource = matrizServicios.ObtenerElementosMatriz(tipoDOFA, usuarioActual.NitEmpresa);
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Marklist.doc");
            Response.ContentType = "application/vnd.ms-word ";
            Response.Charset = string.Empty;

            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("MostarDOFA");
        }


        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            base.Dispose(disposing);
        }
    }
    

}
 
