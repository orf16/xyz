using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDRequisitosLegalesOtros
    {

        public int PKRequisitoLegal { get; set; }
        public string TipoNorma { get; set; }
        public string NumeroNorma { get; set; }
        public DateTime FechaPublicacionReq { get; set; } 
        public string EnteReq { get; set; }     
        public string ArticuloReq { get; set; }     
        public string DescripcionReq { get; set; }   
        public string SugerenciasReq { get; set; }
        public string ClaseDePeligro { get; set; }
        public string PeligroReq { get; set; } 
        public string AspectosReq { get; set; } 
        public string ImpactosReq { get; set; }
        public string EvidenciaCumplimiento { get; set; }

        //public int FK_CumplimientoEvaluacionReq { get; set; }
        public string Descripcion_Cumplimiento_Evaluacion { get; set; }
        public string HallazgoReq { get; set; }
        //public int FK_Estado_RequisitoslegalesOtrosReq { get; set; }
        public string Descripcion_Estado_RequisitoslegalesOtros { get; set; }

        public string ResponsableReq { get; set; }
        public DateTime Fecha_Seguimiento_ControlReq { get; set; }
        public DateTime Fecha_ActualizacionReq { get; set; }
       
        public int FK_ActividadEconomica { get; set; }

    }
}
