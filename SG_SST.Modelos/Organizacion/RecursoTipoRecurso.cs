
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

[Table("Tbl_RecursoTipoRecurso")]


    public class RecursoTipoRecurso
    {

        [Key]
    public int Pk_Id_RecursoTipoRecurso { get; set; }

        [ForeignKey("Recurso")]
        public int Fk_Id_Recurso { get; set; }
        [ForeignKey("Pk_Id_Recurso")]
        public virtual Recurso Recurso { get; set; }

        [ForeignKey("TipoRecurso")]
        public int Fk_Id_TipoRecurso { get; set; }
        [ForeignKey("Pk_Id_TipoRecurso")]
        public virtual TipoRecurso TipoRecurso { get; set; }
    }
}
