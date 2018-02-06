using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.MedicionEvaluacion
{
    public class EDAuditoriaActividad
    {
        public int Pk_Id_Cronograma_Auditoria { get; set; }
        public string Tema { get; set; }
        public string Responsable { get; set; }
        public DateTime Fecha_Hora { get; set; }
        public string TiempoEstimado { get; set; }
        public string Lugar { get; set; }
        public int Fk_Id_Auditoria { get; set; }

        public List<EDAuditoriaActividad> ListaActividad { get; set; }
    }
}
