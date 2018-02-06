using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_SubEstandares")]
    public partial class SubEstandar
    {
        public SubEstandar()
        {
            this.Criterios = new List<Criterio>();
            this.ConfiguracionesEstandar = new List<Config_Valoracion_SubEstand>();
        }

        [Key]
        public int Pk_Id_SubEstandar { get; set; }
        [ForeignKey("Estandar")]
        public int Fk_Id_Estandar { get; set; }
        [StringLength(1000)]
        public string Descripcion { get; set; }
        [StringLength(500)]
        public string Descripcion_Corta { get; set; }
        public int Procentaje_Max { get; set; }
        

        [ForeignKey("Pk_Id_Estandar")]
        public virtual Estandar Estandar { get; set; }

        public virtual ICollection<Criterio> Criterios { get; set; }
        public virtual ICollection<Config_Valoracion_SubEstand> ConfiguracionesEstandar { get; set; }
    }
}
