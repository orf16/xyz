using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Usuarios
{
    [Table("Tbl_RecursosSistema")]
    public class RecursoSistema
    {
        [Key]
        public int Pk_Id_RecursoSistema { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UrlRecurso { get; set; }
    }
}
