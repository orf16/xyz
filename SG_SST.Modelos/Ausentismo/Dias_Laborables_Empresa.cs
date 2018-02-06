using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Ausentismo
{
    [Table("Tbl_Dias_Laborables_Empresa")]
    public class Dias_Laborables_Empresa
    {
        [Key]
        public int PK_Id_Dias_Laborables_Empresa { get; set; }
        public string Documento_empresa { get; set; }
        public Nullable<int> FK_Id_Dias_Laborables { get; set; }
    }
}
