using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Emergencias
{
    [Table("Tbl_Eme_DescripcionOcupacion")]
    public class Eme_DescripcionOcupacion
    {
        [Key]
        public int pk_id_descocupacion { get; set; }
        public int fk_id_sede { get; set; }
        public string trabajadores_cantidad { get; set; }
        public string trabajadores_hdesde { get; set; }
        public string trabajadore_hhasta { get; set; }
        public string contratista_cantidad { get; set; }
        public string contratista_hdesde { get; set; }
        public string contratista_hhasta { get; set; }
        public string visitante_cantidad { get; set; }
        public string visitante_hdesde { get; set; }
        public string visitantte_hhasta { get; set; }
        public string cliente_cantidad { get; set; }
        public string cliente_hdesde { get; set; }
        public string cliente_hhasta { get; set; }
        public bool bo_tratamiento_especial { get; set; }
        public string cual { get; set; }
        public string NitEmpresa { get; set; }

    }
}
