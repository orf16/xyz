using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Estandares")]
    public partial class Estandar
    {
        public Estandar()
        {
            this.SubEstandares = new List<SubEstandar>();
        }
        [Key]
        public int Pk_Id_Estandar { get; set; }
        [ForeignKey("Ciclo")]
        public int Fk_Id_Ciclo { get; set; }
        [StringLength(500)]
        public string Descripcion { get; set; }
        public decimal Porcentaje_Max { get; set; }
        

        [ForeignKey("Pk_Id_Ciclo")]
        public virtual Ciclo Ciclo { get; set; }

        public virtual ICollection<SubEstandar> SubEstandares { get; set; }
    }
}
