    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using SG_SST.Models.Planificacion;

namespace SG_SST.Models.Planificacion
{

     [Table("Tbl_Ocupaciones_De_Perfil")]
   public class Ocupaciones_Perfil
    {
         [Key]
        public int PK_OcupacionPerfil { get; set; }

       public string codigo { get; set; }

       public string grupoPrimario { get; set; }

        public ICollection<PerfilSocioDemograficoPlanificacion> perfilSocioDemograficoPlanificacion { get; set; }
    }
}
