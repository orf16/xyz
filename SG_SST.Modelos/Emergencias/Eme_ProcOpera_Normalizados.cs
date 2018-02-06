using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_ProcOpera_Normalizados")]
    public class Eme_ProcOpera_Normalizados
    {
        [Key]
        public int pk_id_proc_normalizados { get; set; }
        public int  fk_id_sede { get; set;}
        public string nombre_proc { get; set; }
        public string responsable { get; set; }
        public string proc_antes { get; set; }
        public string proc_durante { get; set; }
        public string proc_despues { get; set; }
        public string proc_recursos { get; set; }
        public string NitEmpresa { get; set; }

    }
}
