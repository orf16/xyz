using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Empresa
{
    public class NotificarInconsistenciasModels
    {

        public string Documento_Empresa { get; set; }
        public string Nombre_Empresa
        {
            get; set;
        }
        public List<SelectListItem> lstTiposInconsistencias { get; set; }

    }
}