using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_NivelEmergencia")]
    public class Eme_NivelEmergencia
    {
        [Key]
        public int pk_id_nivelemergencia { get; set; }
        public int fk_id_sede { get; set; }
        public string nivel { get; set; }
        public string emergencia { get; set; }
        public string NitEmpresa { get; set; }

    }
}
