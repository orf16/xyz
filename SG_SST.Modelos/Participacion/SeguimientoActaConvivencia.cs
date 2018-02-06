using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SG_SST.Models.Empresas;

namespace SG_SST.Models.Participacion
{
    [Table("Tbl_SeguimientoActaConvivencia")]
    public class SeguimientoActaConvivencia
    {
        [Key]
        public int PK_Id_Seguimiento { get; set; }
        [Required]
        public int Consecutivo_Evento { get; set; }
        public DateTime Fecha { get; set; }
        [StringLength(50)]
        public string NombreParteInvolucrada { get; set; }
        [StringLength(1000)]
        public string CompromisosAdquiridos { get; set; }
        [StringLength(1000)]
        public string Observaciones { get; set; }
        public int Fk_Id_Sede { get; set; }
        [ForeignKey("ActasConvivencia")]
        public int Fk_Id_Acta { get; set; }
        [ForeignKey("PK_Id_Acta")]
        public virtual ActasConvivencia ActasConvivencia { get; set; }
        public List<CompromisosPendientes> CompromisosPendientes { get; set; }
     }
} 
