using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SG_SST.Models.Metodologia
{
    public class GenerarPeligroModel
    {
        public int id_sedeSeleccionada { get; set; }

        public int id_metodologiaSeleccionada { get; set; }
        public List<SelectListItem> Sedes { get; set; }
        public List<SelectListItem> Metodologias { get; set; }
    }    
}