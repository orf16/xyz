using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Vulnerabilidad
{
    [Table("Tbl_vul_SistemasProcesos")]
    public class vul_SistemasProcesos
    {

        [Key]
        public int pk_id_sistemas_procesos { get; set; }
        public string aspectos { get; set; }
        public string tipo { get; set; }
        public string NitEmpresa { get; set; }

    }
}
