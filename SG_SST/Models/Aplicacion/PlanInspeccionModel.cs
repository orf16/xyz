using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Aplicacion
{
    public class PlanInspeccionModel
    {
        public int Idplaninspeccion { get; set; }
        public string responsable { get; set; }

        public int Idtipoinspeccion { get; set; }

        public int DescripcionTipoInspeccion { get; set; }

        public string DescripcionTipoInspeccionse { get; set; }
        
        public DateTime Fecha { get; set; }

        //public List<SelectListItem> TipoInspecciones { get; set; }

        public int idEmpresaVM { get; set; }
        public string EstadoPlaneacionVM { get; set; }
        public int ConsecutivoPlanVM { get; set; }


       
    }
}