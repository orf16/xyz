using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Usuarios
{
    [Table("Tbl_PlantillasCorreosSistema")]
    public class PlantillaCorreosSistema
    {
        [Key]
        public int IdPlantilla { get; set; }
        public string NombrePlantilla { get; set; }

        [Column(TypeName = "ntext")]
        public string Plantilla { get; set; }
    }
}
