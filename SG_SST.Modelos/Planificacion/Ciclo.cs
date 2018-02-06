using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Ciclos")]
    public partial class Ciclo
    {
        public Ciclo()
        {
            this.Estandares = new List<Estandar>();
        }

        [Key]
        public int Pk_Id_Ciclo { get; set; }
        [StringLength(50)]
        public string  Nombre { get; set; }
        public decimal Porcentaje_Max { get; set; }
        

        public virtual ICollection<Estandar> Estandares { get; set; }
    }
}
