using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Usuarios
{
    [Table("Tbl_ParametrosSistema")]
    public class ParametroSistema
    {
        [Key]
        public int IdParametro { get; set; }
        public string NombreParametro { get; set; }
        public string Valor { get; set; }
    }
}
