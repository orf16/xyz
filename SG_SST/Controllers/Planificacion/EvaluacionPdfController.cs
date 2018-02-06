using Rotativa;
using SG_SST.Models.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Controllers.Planificacion
{
    public class EvaluacionPdfController : Controller
    {
        // GET: Pdf
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GeneradorPdf()
        {
            var objEvaluacion = new EvaluacionModel();
            if (TempData["EvaluacionModel"] != null)
            {
                var objtemp = (EvaluacionModel) TempData["EvaluacionModel"];

                objEvaluacion.RazonSocial = objtemp.RazonSocial;
                objEvaluacion.ResponsableSGSST = objtemp.ResponsableSGSST;
                objEvaluacion.Nit  = objtemp.Nit;
                objEvaluacion.ElaboradoPor = objtemp.ElaboradoPor;
                objEvaluacion.CodActividadeEconomica = objtemp.CodActividadeEconomica;
                objEvaluacion.ActividadEconomica = objtemp.ActividadEconomica;
                objEvaluacion.LicenciaSOSL  = objtemp.LicenciaSOSL;
                objEvaluacion.SedeCentroTrabajo  = objtemp.SedeCentroTrabajo;
                objEvaluacion.strFechaDiligenciamiento = string.Format("{0}/{1}/{2}", objtemp.FechaDiligenciamiento.Year, objtemp.FechaDiligenciamiento.Month, objtemp.FechaDiligenciamiento.Day);
                objEvaluacion.AspectosCreados = objtemp.AspectosCreados;
            }
            return new ViewAsPdf("GeneradorPdf", objEvaluacion)
            {
                FileName = "FormularioEvaluacionIninial.pdf"
            };
            //return View(objEvaluacion);
        }
    }
}