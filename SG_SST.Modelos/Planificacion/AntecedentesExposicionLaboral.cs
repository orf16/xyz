using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.SqlServer;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Planificacion
{
      [Table("Tbl_AntecedentesExposicionLaboral")]
    public class AntecedentesExposicionLaboral
    {
             [Key]
        public int PK_AntecedentesExposicionLaboral { get; set; }
        public string Descripcion_AntecedentesExposicionLaboral { get; set; }



    }
}
