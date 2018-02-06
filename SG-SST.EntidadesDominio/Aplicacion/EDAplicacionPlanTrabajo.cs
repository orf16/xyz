using SG_SST.EntidadesDominio.Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDAplicacionPlanTrabajo
    {
        public int Pk_Id_PlanTrabajo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int Vigencia { get; set; }
        public string RepLegalImagen { get; set; }
        public string RepSGSSTImagen { get; set; }
        public string RepLegalRuta { get; set; }
        public string RepSGSSTRuta { get; set; }
        public string RepLegalNombre { get; set; }
        public string RepSGSSTNombre { get; set; }
        public string RepLegalTipoDocumento { get; set; }
        public string RepSGSSTTipoDocumento { get; set; }
        public string RepLegalDocumento { get; set; }
        public string RepSGSSTDocumento { get; set; }
        public int Fk_Id_Sede { get; set; }
        public string NombreSede { get; set; }
        public List<EDAplicacionPlanTrabajoDetalle> ListaDetalles { get; set; }
        public string Tipo { get; set; }
        public Nullable<System.DateTime> FechaAplicacion { get; set; }

        public List<EDPlanTrabajoMeses> ListaMeses { get; set; }
        


    }
}
