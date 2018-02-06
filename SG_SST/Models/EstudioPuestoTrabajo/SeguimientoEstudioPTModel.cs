using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.EstudioPuestoTrabajo
{
    public class SeguimientoEstudioPTModel
    {
        public int IdEstudioPuestoTrabajo { get; set; }
        public string Actividad { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaStr { get; set; }
        public string Responsable { get; set; }
    }
}