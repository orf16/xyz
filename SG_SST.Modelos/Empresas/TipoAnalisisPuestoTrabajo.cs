using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Empresas
{
    [Table("Tbl_Tipo_Analisis_Puesto_Trabajo")]
    public class TipoAnalisisPuestoTrabajo
    {
        [Key]
        public int Pk_Id_Tipo_Analisis_Puesto_Trabajo { get; set; }
        public string Nombre_Tipo_Analisis_Puesto_Trabajo { get; set; }
    }
}
