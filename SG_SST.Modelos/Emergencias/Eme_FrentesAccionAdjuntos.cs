using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_FrentesAccionAdjuntos")]
    public class Eme_FrentesAccionAdjuntos
    {
        [Key]
        public int pk_id_adjunto { get; set; }
        public int fk_id_sede { get; set; }
        public string plan_seguridad_fisica { get; set; }
        public string plan_atencion_medica { get; set; }
        public string plan_contraincendios { get; set; }
        public string plan_evacuacion { get; set; }
        public string plan_rutas_evacuacion { get; set; }
        public string NitEmpresa { get; set; }

    }
}
