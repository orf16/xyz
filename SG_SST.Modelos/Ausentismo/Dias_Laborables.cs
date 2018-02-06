using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Ausentismo
{
    [Table("Tbl_Dias_Laborables")]
    public class Dia_Laborable
    {
        [Key]
        public int PK_Id_Dia_Laborable {get;set;}
        public string Descripcion { get; set; }
    }
}
