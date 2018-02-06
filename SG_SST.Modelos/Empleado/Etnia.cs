using System.Data.Entity.SqlServer;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Empleado
{

        [Table("Tbl_Etnia")]
  public class Etnia
    {
        [Key]
        public int PK_Etnia { get; set; }
        public string Descripcion_Etnia { get; set; }

    }
}
