//Clase Modelo que contiene la relacion de una inspeccion con la Configuracion de Prioridad.
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

    [Table("Tbl_ConfiguracionporInspeccion")]
    public class ConfiguracionporInspeccion
    {
         
        [Key]
        public int Pk_Id_ConfiguracionPorInspeccion { get; set; }


       [ForeignKey("ConfiguracionInspeccion")]
        public int Fk_Id_ConfiguracionInspeccion { get; set; }
        [ForeignKey("Pk_Id_ConfiguracionInspeccion")]
       public virtual ConfiguracionInspeccion ConfiguracionInspeccion { get; set; }



        [ForeignKey("Inspecciones")]
        public int Fk_Id_Inspecciones { get; set; }
        [ForeignKey("Pk_Id_Inspecciones")]
        public virtual Inspecciones Inspecciones { get; set; }

    }
}
