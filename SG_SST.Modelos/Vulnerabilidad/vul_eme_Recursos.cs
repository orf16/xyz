using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Vulnerabilidad
{
    [Table("Tbl_vul_eme_Recursos")]
    public class vul_eme_Recursos
    {
        [Key]
        public int pk_id_vul_recursos { get; set; }
        public int fk_pk_id_plan_emergencia { get; set; }
        public int fk_id_aspecto { get; set; }
        public string observacion { get; set; }
        public string recomendacion { get; set; }
        public string calificacion { get; set; }
        public string tipo { get; set; }
        public string NitEmpresa { get; set; }
        public decimal subtotal { get; set; }

    }
}
