using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Aplicacion
{
    public class PlanAccionVM
    {
        public int pkplanaccionvm { get; set; }
        public string actividadvm { get; set; }
        public string responsablevm { get; set; }
        public DateTime fechafinvm { get; set; }
        public int estadovm { get; set; }
        public List<CondicionesInsegurasVM> Condiciones { get; set; }
        public int idSede { get; set; }

        public string FechaIniVer { get; set; }
        public string FechaFinVer { get; set; }



        public class CondicionesInsegurasVM
        {
            public int pkcondicionvm { get; set; }
            public string DescribeCondicionvm { get; set; }
            public string Ubicacionespecificavm { get; set; }
            public string Riesgopeligrovm { get; set; }
            public string ClasificacionRiesgovm { get; set; }
            public string Evidenciacondicionvm { get; set; }
            public int Configuracioncondicionvm { get; set; }
            public string Prioridad { get; set; }
            public string Describelaconfiguracion { get; set; }
            public int Diadesde { get; set; }
            public int Diahasta { get; set; }
            public string Diasdesde { get; set; }
            public string Diashasta { get; set; }

        }
    }

}