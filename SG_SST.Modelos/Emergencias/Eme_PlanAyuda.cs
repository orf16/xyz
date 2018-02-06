using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_PlanAyuda")]
    public class Eme_PlanAyuda
    {
        [Key]
        public int pk_id_planayuda { get; set; }
        public int fk_id_sede { get; set; }
        public string empresa { get; set; }
        public string recurso { get; set; }
        public string compensacion { get; set; }
        public string reintegro { get; set; }
        public string nombre_contacto { get; set; }
        public string telefono_contacto { get; set; }
        public string NitEmpresa { get; set; }

    }
}
