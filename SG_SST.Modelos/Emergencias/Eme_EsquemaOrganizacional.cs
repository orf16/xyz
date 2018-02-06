using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_EsquemaOrganizacional")]
    public class Eme_EsquemaOrganizacional
    {
        [Key]
        public int pk_id_esquemaorganizacional { get; set; }
        public int fk_id_sede { get; set; }
        public string esquema_img { get; set; }
        public string NitEmpresa { get; set; }

    }
}
