using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_CaracteristicasInstalacion")]
    public class Eme_CaracteristicasInstalacion
    {
        [Key]
        public int pk_id_cinstalaciones { get; set; }
        public int fk_id_sede { get; set; }
        public string ventilacion_mecanica { get; set; }
        public string ascensores { get; set; }
        public string sotanos { get; set; }
        public string red_hidraulica { get; set; }
        public string transformadores { get; set; }
        public string plantas_electricas { get; set; }
        public string escaleras { get; set; }
        public string zonas_parqueo { get; set; }
        public string areas_especiales { get; set; }
        public string NitEmpresa { get; set; }

    }
}
