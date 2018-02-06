using SG_SST.Models.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.PlanTrabajoAnual
{
    public class PlanEmpresaModel
    {
        public int pk_id_plan_empresa { get; set; }
        public int IdSede { get; set; }
        public List<SelectListItem> Sedes { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public string FechaDesdeTemp { get; set; }
        public string FechaHastaTemp { get; set; }
        public List<SelectListItem> Vigencia { get; set; }
        public string anioVigencia { get; set; }

        public List<SelectListItem> objetivosst { get; set; }

        /// <summary>
        /// ACTIVIDADES
        /// </summary>
        public int pk_id_actividad { get; set; }
        public string ObjetivosDescripcion { get; set; }
        public string ObjetivosMetas { get; set; }
        public string Actividad { get; set; }
        public string Responsable { get; set; }
        

        /// <summary>
        /// RECURSOS
        /// </summary>
        public string RecursosHumanos { get; set; }
        public string RecursosTecnologico { get; set; }
        public string RecursosFinanciero { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FechaProg { get; set; }
        public string FechaReProg { get; set; }
        public string FechaEje { get; set; }
        public string HoraProgIni { get; set; }
        public string HoraProgFin {get; set; }
        public List<SelectListItem> Estado { get; set; }
        public string Estados { get; set; }
        public string PorcentajeEjecucion { get; set; }
        public string RepresentanteLegal { get; set; }
        public string RepresentanteSGSST { get; set; }
        public HttpPostedFileBase RepresentanteLegalFile { get; set; }
        public HttpPostedFileBase RepresentanteSGSSTFile { get; set; }


        public int Id { get; set; }
        public string name { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public bool Plan { get; set; }

    }

    public class PlanEmpresaResumen
    {
        public string actividad { get; set; }
        public int mes { get; set; }
        public int anio{ get; set; }
        public int programada { get; set; }
        public int ejecutada { get; set;}
        public decimal promedio { get; set; }

    }

    public class PlanActividad
    { 
        public int pk_id_actividad { get; set; }
        public int pk_id_plan_empresa { get; set; }
        public string Estado { get; set; }
        public string FechaProg { get; set; }
        public string FechaReProg { get; set; }
        public string FechaEje{ get; set; }
        public string HoraProgIni { get; set; } 
        public string HoraProgFin { get; set; }
        public string FechaDesde { get; set; }
        public string FechaHasta { get; set; }
        public int Vigencia { get; set; }
    }

    public class PlanPromedio
    {
        public int cantidad { get; set; }
        public int mes { get; set; }
        public int anio { get; set; }
    }
}