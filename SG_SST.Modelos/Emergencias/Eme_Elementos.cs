using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_Elementos")]
    public class Eme_Elementos
    {
        [Key]
        public int pk_id_elementos { get; set; }
        public int fk_id_sede { get; set; }
        public string estructurales_descripcion { get; set; }
        public string estructurales_ubicacion { get; set; }
        public string equipos_descripcion { get; set; }
        public string equipos_ubicacion { get; set; }
        public string insumos_descripcion { get; set; }
        public string insumos_ubicacion { get; set; }
        public string NitEmpresa { get; set; }

    }
}
