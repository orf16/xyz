using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Vulnerabilidad
{
    [Table("Tbl_vul_eme_IdentificacionAmenazas")]
    public class vul_eme_IdentificacionAmenazas
    {
        [Key]
        public int pk_id_vul_identificacion_amenazas { get; set; }
        public int fk_pk_id_plan_emergencia { get; set; }
        public int fk_id_amenaza { get; set; }
        public string origen { get; set; }
        public string fuenteriesgo { get; set; }
        public string calificacion { get; set; }
        public string color { get; set; }
        public string tipo { get; set; }
        public string NitEmpresa { get; set; }
        public decimal subtotal { get; set; }

    }
}
