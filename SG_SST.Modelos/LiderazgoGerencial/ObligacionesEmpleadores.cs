
namespace SG_SST.Models.LiderazgoGerencial
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.ComponentModel.DataAnnotations;
    using System.Collections;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Obligaciones_Empleadores")]
    public class ObligacionesEmpleadores
    {
        [Key]
        public int Pk_Id_Obligaciones_Empleadores { get; set; }

        public string Descripcion { get; set; }
    }
}
