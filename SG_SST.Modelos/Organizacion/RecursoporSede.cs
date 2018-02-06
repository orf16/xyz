namespace SG_SST.Models.Organizacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.Collections;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using SG_SST.Models.Empresas;

    [Table("Tbl_RecursoporSede")]


    public class RecursoporSede
    {

        [Key]
        public int Pk_Id_RecursoporSede { get; set; }

        [ForeignKey("Recurso")]
        public int Fk_Id_Recurso { get; set; }
        [ForeignKey("Pk_Id_Recurso")]
        public virtual Recurso Recurso { get; set; }

        [ForeignKey("Sede")]
        public int Fk_Id_Sede { get; set; }
        [ForeignKey("Pk_Id_Sede")]
        public virtual Sede Sede { get; set; }
    }
}
