using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Usuarios
{
    [Table("Tbl_DatosAdicionalesUsuario")]
    public class DatoAdicionalUsuario
    {
        [Key]
        public int Pk_Id_DatoAdicionalUsuario { get; set; }
        public string NombreDato { get; set; }
        public string ValorDato { get; set; }
        public int? CodUsuarioAprobar { get; set; }
        public int? CodUsuarioSistema { get; set; }
    }
}
