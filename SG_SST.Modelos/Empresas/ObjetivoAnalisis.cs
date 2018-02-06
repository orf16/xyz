using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Empresas
{
    [Table("Tbl_ObjetivoAnalisis")]
    public class ObjetivoAnalisis
    {
        [Key]
        public int Pk_Id_ObjetivoAnalisis { get; set; }
        public string Nombre_ObjetivoAnalisis { get; set; }
    }
}
