using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.EstudioPuestoTrabajo
{
    public class EstudioPuestoTrabajoModel
    {
        public string Documento { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Cargo { get; set; }
        public DateTime FechaAnalisis { get; set; }
        public string FechaAnalisisStr { get; set; }
        public int idSede { get; set; }
        public List<SelectListItem> Sedes { get; set; }
        public int idProceso { get; set; }
        public int idDiagnostico { get; set; }
        public List<SelectListItem> Procesos { get; set; }
        public int idObjetivo { get; set; }
        public List<SelectListItem> ObjetivosAnalisis { get; set; }
        public int idTipoAnalisisPT { get; set; }
        public List<SelectListItem> TipoAnalisisPT { get; set; }

        
    }
}