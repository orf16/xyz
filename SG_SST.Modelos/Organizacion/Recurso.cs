//Clase modelo para Registrar Recursos

namespace SG_SST.Models.Organizacion
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data;
    using System.ComponentModel.DataAnnotations;
    using System.Collections;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Tbl_Recurso")]
    public class Recurso
    {

        [Key]

        public int Pk_Id_Recurso { get; set; }
        [Display(Name = "Descripcion Recurso")]
        [Required]
        //[RegularExpression (@"^ ([A-za-z] + [\s] {1} [A-za-z] +) | ([A-Za-z] +)) $",ErrorMessage = "El Nombre del Recurso es Requerido y solo son Letras, no se pueden ingresar caracteres especiales")]
        public string Nombre_Recurso { get; set; }
        public int Periodo { get; set; }

        public ICollection<RecursoFase> RecursosFase { get; set; }
        public ICollection<RecursoTipoRecurso> RecursosTipoRecursos { get; set; }
        public ICollection<RecursoporSede> RecursosporSede { get; set; }
    }
}
