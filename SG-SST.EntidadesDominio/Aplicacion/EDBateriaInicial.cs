using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDBateriaInicial
    {
        public int Pk_Id_BateriaInicial { get; set; }
        public string Nombre { get; set; }
        public string Sexo { get; set; }
        public int AñoNac { get; set; }
        public string AnioNacS { get; set; }
        public string EstadoCivil { get; set; }
        public string NivEstudios { get; set; }
        public string Profesion { get; set; }
        public string ResidenciaMun { get; set; }
        public string ResidenciaDep { get; set; }
        public string Estrato { get; set; }
        public string TipoVivienda { get; set; }
        public int PersonasDependen { get; set; }
        public string PersonasDependenS { get; set; }
        public string LugarTrabajoMun { get; set; }
        public string LugarTrabajoDep { get; set; }
        public string AñosConEmpresa { get; set; }
        public int AñosConEmpresaNum { get; set; }
        public string AñosConEmpresaNumS { get; set; }
        public string CargoConEmpresa { get; set; }
        public string CargoConEmpresaAños { get; set; }
        public string TipoCargo { get; set; }
        public string AñosOficio { get; set; }
        public int AñosOficioNum { get; set; }
        public string AñosOficioNumS { get; set; }
        public string AreaEmpresa { get; set; }
        public string TipoContrato { get; set; }
        public int HorasEstablecidas { get; set; }
        public string HorasEstablecidasS { get; set; }
        public string TipoSalario { get; set; }
        public int Fk_Id_BateriaUsuario { get; set; }
    }
}
