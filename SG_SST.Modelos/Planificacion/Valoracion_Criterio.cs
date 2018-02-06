using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Valoracion_Criterios")]
    public partial class Valoracion_Criterio
    {
        public Valoracion_Criterio()
        {
            this.ConfiguracionesEstandar = new List<Config_Valoracion_SubEstand>();
        }

        [Key]
        public int Pk_Id_Valoracion_Criterio { get; set; }
        public int? Id_Valoracion_Criterio_Padre { get; set; }
        [StringLength(100)]
        public string Descripcion { get; set; }

        public virtual ICollection<Config_Valoracion_SubEstand> ConfiguracionesEstandar { get; set; }

    }
}
