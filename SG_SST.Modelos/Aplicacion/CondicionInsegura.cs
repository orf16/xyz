using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SG_SST.Models.Aplicacion
{
    [Table("Tbl_CondicionInsegura")]

   public  class CondicionInsegura
    {
        [Key]
        public int Pk_Id_CondicionInsegura { get; set; }

        public string Descripcion_Condicion { get; set; }

        public string UbicacionEspecificaInspeccion { get; set; }

        public string RiesgoPeligroIdentificado { get; set; }

        public string DescripcionRiesgoIdentificado { get; set; }

        public string Evidencia { get; set; }

        public int PKConfiguracionPrioridadInspeccion { get; set; }

        public int Estado_Condicion { get; set; }

        public string OtroTipoPeligro { get; set; }
       public ICollection<CondicionesInsegurasporInspeccion> CondicionesInsegurasporInspeccion { get; set; }
       
    } 
}
