using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.EnfermedadesLaborales
{
    [Table("Tbl_EstadosInstanciasRegistradas")]
    public class EstadoInstanciaRegistrada
    {
        [Key]
        public int PK_Id_EstadoInstancia { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public virtual ICollection<InstanciaEnfermedadLaboral> InstanciasRegistradasEnfermedadLaboral { get; set; }
    }
}
