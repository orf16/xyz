using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Usuarios
{

    [Table("Tbl_RespuestasAPreguntasSeguridad")]
    public class RespuestaAPreguntaSeguridad
    {
        [Key]
        public int Pk_Id_RespuestaAPreguntaSeguridad { get; set; }
        [ForeignKey("PreguntaSeguridad")]
        public int Fk_Id_PreguntaSeguridad { get; set; }
        public string RespuestareguntaSeguridad { get; set; }
        public int? CodUsuarioAprobar { get; set; }
        public int? CodUsuarioSistema { get; set; }
        [ForeignKey("Pk_Id_PreguntaSeguridad")]
        public virtual PreguntaSeguridad PreguntaSeguridad { get; set; }
    }
}
