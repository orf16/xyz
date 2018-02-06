using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_Georeferenciacion")]
    public class Eme_Georeferenciacion
    {
        [Key]
        public int pk_id_georeferenciacion { get; set; }
        public int fk_id_sede { get; set; }
        public string interno_img { get; set; }
        public bool bo_externo { get; set; }
        public bool bo_colegio { get; set; }
        public bool bo_iglesia { get; set; }
        public bool bo_comercial { get; set; }
        public bool bo_centro_atencion { get; set; }
        public bool bo_parque { get; set; }
        public bool bo_otro { get; set; }
        public string cual { get; set; }
        public string ubicacion_hidrantes { get; set; }
        public string punto_encuentro { get; set; }
        public string punto_encuentro_img { get; set; }
        public string ubicacion_hidrantes_img { get; set; }
        public string NitEmpresa { get; set; }

    }
}
