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

    [Table("Tbl_Maestro_Configuracion_Prioridad")]
    public class MaestroConfiguracionPrioridades
    {

        [Key]
        public int Pk_Id_MaestroConfiguracion { get; set; }
        public string DescripcionPrioridad { get; set; }

        public int DiasDesde { get; set; }
        public int DiasHasta { get; set; }
        //public ICollection<ConfiguracionporInspeccion> configuracionporinspeccion { get; set; }
    }
}
