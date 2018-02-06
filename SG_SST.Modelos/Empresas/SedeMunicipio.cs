using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SG_SST.Models.Empresas;

namespace SG_SST.Models
{
    [Table("Tbl_SedeMunicipio")]
    public class SedeMunicipio
    {
        [Key]
        public int id_sedeMunicipio { get; set; }

        [ForeignKey("Sede")]
        public int Fk_id_Sede { get; set; }
        [ForeignKey("Pk_Id_Sede")]
        public virtual Sede Sede { get; set; }

        [ForeignKey("Municipio")]
        public int Fk_Id_Municipio { get; set; }
        [ForeignKey("Pk_Id_Municipio")]
        public virtual Municipio Municipio { get; set; }
    }
}