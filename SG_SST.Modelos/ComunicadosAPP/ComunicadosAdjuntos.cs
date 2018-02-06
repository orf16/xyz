using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.ComunicadosAPP
{
    [Table("Tbl_ComunicadosAdjutos")]
    public class ComunicadosAdjuntos
    {
        [Key]
        public int pk_id_comadjunto { get; set; }
        public string nombre { get; set; }
        public string entidad { get; set; }
        public string descripcion { get; set; }
        public string fecha { get; set; }
        public string adjunto { get; set; }
        public string requiere { get; set; }
        public string respuesta { get; set; }
        public string tipo { get; set; }
        public string NitEmpresa { get; set; }

    }
}
