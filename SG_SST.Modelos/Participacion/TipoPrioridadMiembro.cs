using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SG_SST.Models.Participacion
{
    [Table("Tbl_TipoPrioridadMiembroComite")]
    public class TipoPrioridadMiembro
    {
        [Key]
        public int PK_Id_TipoPrioridadMiembro { get; set; }
        [Required(ErrorMessage = "El Tipo de Prioridad es Requerido...")]
        [StringLength(20)]
         public string DescripcionTipoMiembro { get; set; }
        
    }
}
