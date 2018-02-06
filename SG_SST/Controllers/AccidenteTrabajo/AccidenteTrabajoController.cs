using SG_SST.Controllers.Base;
using SG_SST.Models.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Controllers.AccidenteTrabajo
{
    public class AccidenteTrabajoController : BaseController
    {
        // GET: AccidenteTrabajo
        public ActionResult ReporteAccidente()
        {
            var usuarioActual = ObtenerUsuarioEnSesion(System.Web.HttpContext.Current);
            if (usuarioActual == null)
            {
                ViewBag.Mensaje = "Su sesion a finalizado, por favor ingrese nuevamente.";
                return RedirectToAction("Login", "Home");
            }

            EvaluacionPositivaModel modelEvalPositiva = new EvaluacionPositivaModel();
            var login = new GestposService.ws_loginSoapClient();
            var parametro = new GestposService.paramObtenerLink();

            parametro.codi_usu = usuarioActual.Documento;
            parametro.xml_params = string.Format("<rt><anho_gest>{0}</anho_gest><tdoc_emp>{1}</tdoc_emp><ndoc_emp>{2}</ndoc_emp></rt>", DateTime.Now.Year, "NI", usuarioActual.NitEmpresa);
            parametro.modulo = GestposService.modulo.furat_poscuida;
            var ruta = new GestposService.rtaObtenerLink();
            try
            {
                ruta = login.obtenerLink(parametro);
            }
            catch
            {
                ruta = null;
            }
            if (ruta == null)
                modelEvalPositiva.url = "../Content/ErrorPage.html";
            else if (ruta.valido < 0)
                if (ruta.url_sitio == null)
                    modelEvalPositiva.Mensaje = ruta.mensaje.Split(':')[1].ToString();
                else
                    modelEvalPositiva.url = "../Content/ErrorPage.html";
            else
                modelEvalPositiva.url = ruta.url_sitio;

            return View(modelEvalPositiva);
        }
    }
}