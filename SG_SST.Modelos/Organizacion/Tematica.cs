using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace SG_SST.Models.Organizacion
{
    [Table("Tbl_Tematica")]
    public class Tematica
    {
        [Key]
        public int Id_Tematica { get; set; }
        public string Tematicas { get; set; }
        public string Area { get; set; }
        public string Diseno { get; set; }
        public string Objetivo { get; set; }
        public string DirigidoA { get; set; }
        public int TipoTematica { get; set; }
        public string NombreDocumento { get; set; }
        public int ? SesionEmpresa { get; set; }

        public ICollection<RolPorTematica> RolPorTematica { get; set; }
        public ICollection<TematicaPorEmpresa> TematicaPorEmpresa { get; set; }

    }
}
