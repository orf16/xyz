using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.MedicionEvaluacion
{
    public class EDActividadAuditoria
    {
        public int Pk_Id_Actividad { get; set; }
        public string Actividad { get; set; }
        public string Responsable { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public int Fk_Id_Auditoria { get; set; }
    }
}
