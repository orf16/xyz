using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{

    using Aplicacion;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SG_SST.Models.Empresas;

    [Table("Tbl_Condiciones_Riesgo_Perfil")]
    public class Condiciones_Riesgo_Perfil
    {
        [Key]
        public int PK_Condiciones_Riesgo_Perfil { get; set; }

     

       

        /// Obtiene y establece una clave foranea a Tbl_Clasificacion_De_Peligro
        /// </summary>
        [ForeignKey("Tbl_Clasificacion_De_Peligro")]
        public int FK_Clasificacion_De_Peligro { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo Clasificacion_De_Peligro
        /// </summary>
        [ForeignKey("PK_Clasificacion_De_Peligro")]
        public virtual ClasificacionDePeligro Tbl_Clasificacion_De_Peligro { get; set; }


        

        public string OtroPeligro { get; set; }

        public string tiempoExposicion { get; set; }


        /// Obtiene y establece una clave foranea con ocupacion
        /// </summary>
        [ForeignKey("Tbl_PerfilSocioDemograficoPlanificacion")]
        public int FK_PerfilSocioDemografico { get; set; }
        /// <summary>
        /// Obtiene y establece un objeto de tipo con ocupacion perfil
        /// </summary>
        [ForeignKey("IDEmpleado_PerfilSocioDemoGrafico")]
        public virtual PerfilSocioDemograficoPlanificacion Tbl_PerfilSocioDemograficoPlanificacion { get; set; }


    }
}
