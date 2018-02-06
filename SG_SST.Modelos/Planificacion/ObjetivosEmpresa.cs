using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Planificacion
{
    [Table("Tbl_Objetivos_SST")]
    public class ObjetivoSST
    {
        [Key]
        public int PK_Id_Objetivo_Empresa { get; set; }
        public int FK_Id_Empresa { get; set; }
        public string Objetivo { get; set; }
        public string Meta { get; set; }
    }
}
