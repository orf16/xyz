using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Empresas;
using System;

namespace SG_SST.Models.Aplicacion
{
    [Table("Tbl_BateriaInicial")]
    public class BateriaInicial
    {
        [Key]
        public int Pk_Id_BateriaInicial { get; set; }

        [MaxLength(8000)]
        public string Nombre { get; set; }

        [MaxLength(8000)]
        public string Sexo { get; set; }

        [MaxLength(8000)]
        public string AñoNac { get; set; }

        [MaxLength(8000)]
        public string EstadoCivil { get; set; }

        [MaxLength(8000)]
        public string NivEstudios { get; set; }

        [MaxLength(8000)]
        public string Profesion { get; set; }

        [MaxLength(8000)]
        public string ResidenciaMun { get; set; }

        [MaxLength(8000)]
        public string ResidenciaDep { get; set; }

        [MaxLength(8000)]
        public string Estrato { get; set; }

        [MaxLength(8000)]
        public string TipoVivienda { get; set; }

        [MaxLength(8000)]
        public string PersonasDependen { get; set; }

        [MaxLength(8000)]
        public string LugarTrabajoMun { get; set; }

        [MaxLength(8000)]
        public string LugarTrabajoDep { get; set; }

        [MaxLength(8000)]
        public string AñosConEmpresa { get; set; }

        [MaxLength(8000)]
        public string AñosConEmpresaNum { get; set; }

        [MaxLength(8000)]
        public string CargoConEmpresa { get; set; }


        [MaxLength(8000)]
        public string TipoCargo { get; set; }

        [MaxLength(8000)]
        public string AñosOficio { get; set; }
        [MaxLength(8000)]
        public string AñosOficioNum { get; set; }

        [MaxLength(8000)]
        public string AreaEmpresa { get; set; }

        [MaxLength(8000)]
        public string TipoContrato { get; set; }

        [MaxLength(8000)]
        public string HorasEstablecidas { get; set; }

        [MaxLength(8000)]
        public string TipoSalario { get; set; }

        [Required()]
        public int Fk_Id_BateriaUsuario { get; set; }
    }
}
