using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Vulnerabilidad
{
    [Table("Tbl_vul_eme_Consolidado")]
    public class vul_eme_Consolidado
    {
        [Key]
        public int pk_id_vul_consolidado { get; set; }
        public int fk_pk_id_plan_emergencia { get; set; }
        public decimal organizacion { get; set; }
        public decimal capacitacion { get; set; }
        public decimal dotacion { get; set; }
        public decimal calificacion_personas { get; set; }
        public string interpretacion_personas { get; set; }
        public string color_personas { get; set; }
        public decimal materiales { get; set; }
        public decimal edificacion { get; set; }
        public decimal equipos { get; set; }
        public decimal calificacion_recursos { get; set; }
        public string interpretacion_recursos { get; set; }
        public string color_recursos { get; set; }
        public decimal servicios_publicos { get; set; }
        public decimal sistemas_alternos { get; set; }
        public decimal recuperacion { get; set; }
        public decimal calificacion_sistemas_procesos { get; set; }
        public string interpretacion_sistemas_procesos { get; set; }
        public string color_sistemas_procesos { get; set; }
        public string NitEmpresa { get; set; }
    }
}
