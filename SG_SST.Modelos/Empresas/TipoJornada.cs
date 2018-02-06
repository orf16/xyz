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
    [Table("Tbl_Tipo_Jornada")]
    public class TipoJornada
    {
        [Key]
        public int Pk_Id_Tipo_Jornada { get; set; }

        [MaxLength(20)]
        [DisplayName("Jornada de trabajo")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Nombre_Jornada { get; set; }

        /// <summary>
        /// Retorna la entidad de dominio equivalente para este objecto.
        /// </summary>
        /// <returns></returns>
        public EDTipoJornada ObtenerED()
        {
            return new EDTipoJornada
            {
                Pk_Id_Tipo_Jornada = Pk_Id_Tipo_Jornada,
                Nombre_Jornada = Nombre_Jornada
            };
        }
    }
}
