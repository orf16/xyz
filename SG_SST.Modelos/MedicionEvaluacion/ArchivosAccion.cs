using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.MedicionEvaluacion
{
    [Table("Tbl_ArchivosAccion")]
    public class ArchivosAccion
    {
        [Key]
        public int Pk_Id_Archivo { get; set; }
        public string Token_Archivo { get; set; }

        [Required]
        [StringLength(250)]
        public string NombreArchivo { get; set; }
        [Required]
        [StringLength(220)]
        public string Ruta { get; set; }

        //FK
        public int Fk_Id_Accion { get; set; }
        public virtual Accion Accion { get; set; }
    }
}
