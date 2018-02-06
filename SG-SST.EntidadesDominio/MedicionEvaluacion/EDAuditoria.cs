using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.MedicionEvaluacion
{
    public class EDAuditoria
    {
        public int Pk_Id_Auditoria { get; set; }
        public string Periodo { get; set; }
        public string Objetivo { get; set; }
        public string Alcance { get; set; }
        public string Criterios { get; set; }
        public DateTime FechaRealizacion { get; set; }
        public string Auditado { get; set; }
        public string Auditador { get; set; }
        public string Requisito { get; set; }
        public string Duracion { get; set; }
        public string Ubicacion_inf { get; set; }
        public string Observaciones_inf { get; set; }
        public int Fk_Id_Programa { get; set; }
        public string NombreProceso1 { get; set; }
        public string NombreProceso2 { get; set; }
        public int Fk_Id_Proceso { get; set; }

    }
}
