using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_Roles")]
    public class Eme_Roles
    {
        [Key]
        public int pk_id_roles { get; set; }
        public int fk_id_sede { get; set; }
        public string nombre { get; set; }
        public string antes { get; set; }
        public string durante { get; set; }
        public string despues { get; set; }
        public string NitEmpresa { get; set; }

    }
}
