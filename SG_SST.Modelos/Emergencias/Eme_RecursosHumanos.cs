using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_RecursosHumanos")]
    public class Eme_RecursosHumanos
    {
        [Key]
        public int pk_id_recursosh { get; set; }
        public int fk_id_sede { get; set; }
        public string bpaux_nombre { get; set; }
        public string bcontra_nombre { get; set; }
        public string bevalresc_nombre { get; set; }
        public string NitEmpresa { get; set; }

    }
}
