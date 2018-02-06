using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Data.Entity;
using SG_SST.Models.Empresas;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.EntidadesDominio.Empresas;

namespace SG_SST.Models.Empresas
{
    [Table("Tbl_Incidente_Consecuencia")]
    public class IncidenteConsecuencia
    {
        [Key]
        public int Pk_Id_Incidente_Consecuencia { get; set; }

        [MaxLength(80)]
        [DisplayName("Posible consecuencia")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Nombre_consecuencia { get; set; }

        /// <summary>
        /// Retorna la entidad de dominio equivalente para este objecto.
        /// </summary>
        /// <returns></returns>
        public EDIncidenteConsecuencia ObtenerED()
        {
            return new EDIncidenteConsecuencia
            {
                Pk_Id_Incidente_Consecuencia = Pk_Id_Incidente_Consecuencia,
                Nombre_consecuencia = Nombre_consecuencia
            };
        }
    }
}
