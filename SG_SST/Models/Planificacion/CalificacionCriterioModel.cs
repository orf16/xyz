using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Planificacion
{
    public class CalificacionCriterioModel
    {
        public int IdEvalEstandarMinimo { get; set; }
        [Required()]
        public int IdEmpresaEvaluar { get; set; }
        [Required()]
        public int IdCriterio { get; set; }
        [Required()]
        public int IdCiclo { get; set; }
        [Required()]
        public int IdValoracionCriterio { get; set; }
        public string Justificacion { get; set; }
        public IEnumerable<ActividadModel> Actividades { get; set; }
    }

    public class ActividadModel
    {
        public int Id_Actividad { get; set; }
        [Required()]
        public string Descripcion { get; set; }
        [Required()]
        public string Responsable { get; set; }
        [Required()]
        public DateTime FechaFin { get; set; }
        public string  strFechaFin { get; set; }
    }
}