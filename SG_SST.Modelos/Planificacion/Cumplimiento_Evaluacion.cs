
namespace SG_SST.Models.Planificacion
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SG_SST.Models.Planificacion;
    using System.Data.Entity.SqlServer;
    using System;


    [Table("Tbl_Cumplimiento_Evaluacion")]
    public class Cumplimiento_Evaluacion
    {
     [Key]
     public int PK_Cumplimiento_Evaluacion { get; set; }
     public string Descripcion_Cumplimiento_Evaluacion { get; set; }

    }
}
