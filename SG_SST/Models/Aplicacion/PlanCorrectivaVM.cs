using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Aplicacion
{
    public class PlanCorrectivaVM
    {
        public string seguimiento { get; set; }
        public string verificador { get; set; }
        public string accioncorrectiva { get; set; }

        public List<PlanAccionCorrectivaVM> correctivas { get; set; }


        public class PlanAccionCorrectivaVM
        {
            public int pkplan { get; set; }
            public int pkplanea { get; set; }
            public string actividad { get; set; }
            

        }

        
    }
}