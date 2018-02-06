using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.ClienteServicios.AfiliadoDTO
{
    public class AfiliadoModel
    {
        public string TpDocEmpresa { get; set; }
        public string IdEmpresa { get; set; }
        public string RazonSocialEmpresa { get; set; }
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string EmailPersona { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public string estadoEmpresa { get; set; }
        public DateTime FechaInicioVinculacion { get; set; }
        public DateTime? FechaFinVinculacion { get; set; }
        public string Estado { get; set; }
        public string FechaNacimiento { get; set; }
        public string Afp { get; set; }
        public string NombreAfp { get; set; }
        public string Eps { get; set; }
        public string NombreEps { get; set; }
        public string Direccion { get; set; }
        public string NombreArl { get; set; }
        public string IdArl { get; set; }
        public string Ocupacion { get; set; }
        public string Cargo { get; set; }
        public int IdOcupacion { get; set; }
        public decimal Salario { get; set; }
        public int CantidadDiasLaborales { get; set; }
    }
}
