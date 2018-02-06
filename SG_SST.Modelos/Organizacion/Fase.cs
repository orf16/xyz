// clase Modelo para Registrar las fases de los recursos


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

    [Table("Tbl_Fase")]
    public class Fase
    {
        [Key]
        public int Pk_Id_Fase { get; set; }

        [Display(Name = "Descripcion Fase")]
        [Required(ErrorMessage = "La Descripcion no puede ser vacia")]
        public string Descripcion_Fase { get; set; }

        public ICollection<RecursoFase> RecursosFase { get; set; }
    }
}
