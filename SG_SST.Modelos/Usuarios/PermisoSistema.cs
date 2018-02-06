using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Usuarios
{
    [Table("Tbl_PermisosSistema")]
    public class PermisoSistema
    {
        [Key]
        public int Pk_Id_PermisoSistema { get; set; }
        public string Descripcion { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public string Vista { get; set; }

    }
}
