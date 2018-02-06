using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Empresa
{
    public class EmpleadoRelLabModel
    {
        public List<SelectListItem> lstEstados { get; set; }

        public List<SelectListItem> lstTiposCotizantes { get; set; }

        public List<SelectListItem> lstTiposTerceros { get; set; }

        public List<SelectListItem> lstRazonesSociales { get; set; }

    }
}