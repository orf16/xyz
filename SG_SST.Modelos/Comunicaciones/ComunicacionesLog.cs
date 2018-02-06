using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Comunicaciones
{
    [Table("Tbl_ComunicacionesLog")]
    public class ComunicacionesLog
    {
        [Key]
        public int pk_id_log { get; set; }
        public int fk_id_comunicaciones { get; set; }
        public bool enviado_rechazado { get; set; }
        public string modulo { get; set; }
    }
}
