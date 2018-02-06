using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.PlanTrabajoAnual
{
    public class FechaProgModel
    {
        public int pk_id_actividad { get; set; }
        public string NombreActividad { get; set; }
        public DateTime FechaProgTemp { get; set; }
        public string HoraIni { get; set; }
        public string HoraFin { get; set; }
   
    }
}