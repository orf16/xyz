using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_AnalisisRiesgo")]
    public class Eme_AnalisisRiesgo
    {
        [Key]
        public int pk_id_analisisriesgo { get; set; }
        public int fk_id_sede { get; set; }
        public int fk_id_identamenaza { get; set; }
        public bool bo_intervencion { get; set; }
        public string plan_de_accion { get; set; }
        public string NitEmpresa { get; set; }

    }
}
