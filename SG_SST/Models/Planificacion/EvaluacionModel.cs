using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Planificacion
{
    public class EvaluacionModel
    {

        public int IdEmpresa { get; set; }
        [Required()]
        public string RazonSocial { get; set; }
        [Required()]
        public string Nit { get; set; }
        public string ActividadEconomica { get; set; }
        [Required()]
        public string ResponsableSGSST { get; set; }
        [Required()]
        public string ElaboradoPor { get; set; }
        [Required()]
        public string LicenciaSOSL { get; set; }
        [Required()]
        public string SedeCentroTrabajo { get; set; }
        [Required()]
        public DateTime FechaDiligenciamiento { get; set; }
        public string strFechaDiligenciamiento { get; set; }
        public AspectosModel NuevoAspecto { get; set; }

        [Required()]
        public int  CodActividadeEconomica { get; set; }
        public List<SelectListItem> CentrosDeTrabajo { get; set; }
        public List<AspectosModel> AspectosCreados { get; set; }
        public List<AspectosModel> AspectosBase { get; set; }
    }

    public class AspectosModel
    {
        public int IdAspecto { get; set; }
        public string AspectoEvaluar { get; set; }
        public bool Cumple { get; set; }
        public bool NoCumple { get; set; }
        public bool CumpleParcial { get; set; }
        public string Observaciones { get; set; }

    }
}