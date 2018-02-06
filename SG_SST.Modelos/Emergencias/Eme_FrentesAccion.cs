using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_FrentesAccion")]
    public class Eme_FrentesAccion
    {
        [Key]
        public int pk_id_frenteaccion { get; set; }
        public int fk_id_sede { get; set; }
        public string plan_seguridadfisica { get; set; }
        public string plan_primerosaux { get; set; }
        public string plan_contraincendios { get; set; }
        public string plan_eval_pdf { get; set; }
        public string nombrecoordinador { get; set; }
        public string objetivos { get; set; }
        public string estructura { get; set; }
        public string proc_coordinacion { get; set; }
        public string proc_internos { get; set; }
        public string proc_externos { get; set; }
        public string mecanismos_alarma { get; set; }
        public string rutas_evac_pdf { get; set; }
        public string simulacros { get; set; }
        public string instructivo_evacuacion { get; set; }
        public string proc_retorno { get; set; }
        public string NitEmpresa { get; set; }

    }
}
