//Clase Modelo maestra  para crear una configuracion de Prioridades

namespace SG_SST.Models.Aplicacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Configuracion_Inspeccion")]
    public class ConfiguracionInspeccion

    {

        [Key]
        public int Pk_Id_ConfiguracionInspeccion { get; set; }
        public string DescripcionPrioridadConf { get; set; }

        public int DiasDesdeConfig { get; set; }
        public int DiasHastaConfig { get; set; }
        public ICollection<ConfiguracionporInspeccion> configuracionporinspeccion { get; set; }
    }
}
