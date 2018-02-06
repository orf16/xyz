using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SG_SST.Models.Aplicacion
{
    public class VerificarInspeccionVModel
    {
        public string RazonSocial { get; set; }
        public string idempresa { get; set; }
        public string Sede { get; set; }
        public int idSede { get; set; }
        public  List<SelectListItem> sedes { get; set; }

        public string FechaInicial { get; set; }
        public string FechaFinal { get; set; }
    }
}