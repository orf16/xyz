using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace SG_SST.Models.Aplicacion
{
    public class CondicionInseguraModel
    {
        public int PkCondicionVM { get; set; }

        public string DescribeCondicionVM { get; set; }


        public int idpeligro { get; set; }
        public string Peligros { get; set; }
        public List<SelectListItem> peligros { get; set; }
        public List<TiposPeligroVM> TiposPeligro { get; set; }
    }

    public class TiposPeligroVM
    {

        public int idpeligro { get; set; }
        public string Descripcionpeligro { get; set; }
    }
}