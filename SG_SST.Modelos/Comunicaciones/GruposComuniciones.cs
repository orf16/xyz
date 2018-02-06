using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Comunicaciones
{
    [Table("Tbl_GruposComunicaciones")]
    public class GruposComuniciones
    {
        [Key]
        public int pk_id_grupo { get; set; }
        public string NombreGrupo { get; set; }
        public string NitEmpresa { get; set; }

    }
}
