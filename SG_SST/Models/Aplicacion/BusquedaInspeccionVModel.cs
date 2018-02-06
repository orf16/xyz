using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Aplicacion
{
    public class BusquedaInspeccionVModel
    {
        public string RazonSocial { get; set; }
        public string idempresa { get; set; }
        public int idSede { get; set; }
        public List<SelectListItem> Sedes { get; set; }

        public DateTime FechaInicialB { get; set; }
        public DateTime FechaFinal { get; set; }

        public int idInspeccion { get; set; }
        public List<SelectListItem> Inspecciones { get; set; }

    }

    public class InspeccionesVM
    {

        public int IdTipoInspeccion { get; set; }
        public string DescripcionInspeccione { get; set; }
        public string Area { get; set; }
        public string Responsable { get; set; }
    }
}