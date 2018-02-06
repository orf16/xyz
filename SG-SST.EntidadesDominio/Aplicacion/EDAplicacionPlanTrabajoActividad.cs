using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDAplicacionPlanTrabajoActividad
    {
        public int Pk_Id_PlanTrabajoActividad { get; set; }
        public DateTime FechaProgramacionIncial { get; set; }
        public DateTime FechaEstado { get; set; }
        //0 Sin Estado, 1 Programada, 2 Reprogramada, 3 Ejecución
        public short Estado { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
        public string ResponsableNombre { get; set; }
        public string ResponsableDocumento { get; set; }
        public string ResponsableTipoDocumento { get; set; }
        public int Fk_Id_PlanTrabajoDetalle { get; set; }

        public string Horas { get; set; }

        public List<EDAplicacionPlanTrabajoProgramacion> ListaProgramacion { get; set; }
    }
}
