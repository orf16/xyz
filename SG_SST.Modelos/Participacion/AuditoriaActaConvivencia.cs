using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SG_SST.Models.Usuarios;

namespace SG_SST.Models.Participacion
{
    [Table("Tbl_AuditoriaActaConvivencia")]
    public class AuditoriaActaConvivencia
    {
        [Key]
        public int Pk_Id_AuditoriaActa { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [StringLength(200)]
        public string AccionRealizada { get; set; }
        [ForeignKey("UsuarioSistema")]
        public int Fk_Id_UsuarioSistema { get; set; }
        [ForeignKey("Pk_Id_UsuarioSistema")]
        public virtual UsuarioSistema UsuarioSistema { get; set; }
        [StringLength(60)]
        public string NombreUsuario { get; set; }
        [ForeignKey("ActasConvivencia")]
        public int Fk_Id_Acta { get; set; }
        [ForeignKey("PK_Id_Acta")]
        public virtual ActasConvivencia ActasConvivencia { get; set; }
    }
}
