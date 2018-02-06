using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Usuarios
{
    [Table("Tbl_CausalesRechazoUsuariosSistema")]
    public class CausalRechazoUsuariosSistema
    {
        [Key]
        public int Pk_Id_CausalRechazo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
