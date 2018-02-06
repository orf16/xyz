using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Comunicaciones
{
    public class ComunicadosAdjuntosModel
    {
        public int pk_id_comadjunto { get; set; }
        public string nombre { get; set; }
        public string entidad { get; set; }
        public string descripcion { get; set; }
        public string fecha { get; set; }
        public string adjunto { get; set; }
        public string requiere { get; set; }
        public string respuesta { get; set; }
        public string tipo { get; set; }
    }
}