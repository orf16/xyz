namespace SG_SST.Models.Organizacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections;
    using System.ComponentModel.DataAnnotations;

    [Table("Tbl_TipoRecurso")]
    public class TipoRecurso
    {
        [Key]
        public int Pk_Id_TipoRecurso { get; set; }

        [Display(Name = "Descripcion Tipo Recurso")]
        [Required(ErrorMessage = "Tipo Recurso no puede estar vacio.")]
        public string Descripcion_Tipo_Recurso { get; set; }

        public ICollection<RecursoTipoRecurso> RecursosTipoRecursos { get; set; }
    }
}