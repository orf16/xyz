using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Aplicacion
{
    public class InspeccionModel
    {
        public int IdTipoInspeccion { get; set; }
        public string DescripcionTipoInspeccion { get; set; }
        public  int intpkinspeccion { get; set; }
        public string DescripcionInspeccion { get; set; }
        public string area { get; set; }
        public string Responsable { get; set; }
        public List<InspeccionModel> tiposinspeccion { get; set; }
        public List<ConfiguracionInspeccion> ConfiguracionesPI { get; set; }

    }

 
}