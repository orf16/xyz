using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_InfoGeneral")]
    public class Eme_InfoGeneral
    {
        [Key]
        public int pk_id_infogeneral { get; set; }
        public int fk_id_sede { get; set; }
        public string razon_social { get; set; }
        public string identificacion_sede { get; set; }
        public string direccion_sede { get; set; }
        public string telefono_sede { get; set; }
        public string correo_electronico { get; set; }
        public string departamento_sede { get; set; }
        public string municipio_sede { get; set; }
        public string lindero_norte { get; set; }
        public string lindero_sur { get; set; }
        public string lindero_oriente { get; set; }
        public string lindero_occidente { get; set; }
        public string acceso_principales { get; set; }
        public string acceso_alternas { get; set; }
        public string actividad_economica { get; set; }
        public string representante { get; set; }
        public string NitEmpresa { get; set; }

    }
}
