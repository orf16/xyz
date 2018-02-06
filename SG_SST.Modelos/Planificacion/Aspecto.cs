using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Aspectos")]
    public partial class Aspecto
    {
        public Aspecto ()
        {
            this.EmpresaAspectos = new List<Empresa_Aspecto>();
        }
        [Key]
        public int Pk_Id_Aspecto { get; set; }
        [ForeignKey("Valoracion_Aspecto")]
        public int Fk_Id_Valoracion_Aspecto { get; set; }
        public string Descripcion { get; set; }
        [StringLength(1000)]
        public string Observacion { get; set; }

        [ForeignKey("Pk_Id_Valoracion_Aspecto")]
        public virtual Valoracion_Aspecto Valoracion_Aspecto { get; set; }
        public virtual ICollection<Empresa_Aspecto> EmpresaAspectos { get; set; }
    }
}
