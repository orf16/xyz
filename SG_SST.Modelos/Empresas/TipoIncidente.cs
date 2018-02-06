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
    [Table("Tbl_Tipo_Incidente")]
    public class TipoIncidente
    {
        [Key]
        public int Pk_Id_Tipo_Incidente { get; set; }

        [MaxLength(20)]
        [DisplayName("Incidente")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Nombre_Incidente { get; set; }

        /// <summary>
        /// Retorna la entidad de dominio equivalente para este objecto.
        /// </summary>
        /// <returns></returns>
        public EDTipoIncidente ObtenerED()
        {
            return new EDTipoIncidente
            {
                Pk_Id_Tipo_Incidente = Pk_Id_Tipo_Incidente,
                Nombre_Incidente = Nombre_Incidente
            };
        }
    }
}
