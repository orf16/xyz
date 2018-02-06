using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.ComunicadosAPP
{
    [Table("Tbl_EstadosComunicadosAPP")]
    public class EstadosComunicadosAPP
    {
        [Key]
        public int PK_Id_EstadoComunicado { get; set; }
        public string Nombre { get; set; }
        public string Valor { get; set; }
 
    }
}
