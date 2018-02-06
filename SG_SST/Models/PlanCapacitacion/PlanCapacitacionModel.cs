using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.PlanCapacitacion
{
    public class PlanCapacitacionModel
    {
        public int pk_id_plan_capacitacion { get; set; }
        public int fk_id_tipo_actividad { get; set; }
        public string tema { get; set; }
        public int fk_id_rol { get; set; }
        public int fk_id_competencia { get; set; }
        public string fecha_programada { get; set; }
        public string hora_inicio { get; set; }
        public string hora_fin { get; set; }
        public int pk_id_soporte { get; set; }
        public HttpPostedFileBase adjunto { get; set; }
    }

    public class Calenario
    {
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

    public class CompetenciasModel
    {
        public int pk_id_tematica { get; set; }
        public int desc_tematica { get; set; }
        public int tipo_tematicas { get; set; }
        public string nit_empresa { get; set; }
    }
}