// Clase Modelo para  RecursospoFase


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

    [Table("Tbl_RecursoFase")]
    public class RecursoFase
    {

        [Key]
        public int Pk_Id_RecursoFase { get; set; }

        [ForeignKey("Recurso")]
        public int Fk_Id_Recurso { get; set; }
        [ForeignKey("Pk_Id_Recurso")]
        public virtual Recurso Recurso { get; set; }

        [ForeignKey("Fase")]
        public int Fk_Id_Fase { get; set; }
        [ForeignKey("Pk_Id_Fase")]
        public virtual Fase Fase { get; set; }
    }
}
