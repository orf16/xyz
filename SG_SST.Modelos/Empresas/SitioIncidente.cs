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
    [Table("Tbl_Sitio_Incidente")]
    public class SitioIncidente
    {
        [Key]
        public int Pk_Id_Sitio_Incidente { get; set; }

        [MaxLength(50)]
        [DisplayName("Sitio donde ocurrió el incidente")]
        [Required(ErrorMessage = "Debe ingresar el valor de {0}")]
        public string Nombre_Sitio { get; set; }

        /// <summary>
        /// Equivale a la opción de selección "Es otro"
        /// </summary>
        public bool EsOtro { get; set; }

        /// <summary>
        /// Retorna la entidad de dominio equivalente para este objecto.
        /// </summary>
        /// <returns></returns>
        public EDSitioIncidente ObtenerED()
        {
            return new EDSitioIncidente
            {
                Pk_Id_Sitio_Incidente = Pk_Id_Sitio_Incidente,
                Nombre_Sitio = Nombre_Sitio,
                EsOtro = EsOtro
            };
        }
    }
}
