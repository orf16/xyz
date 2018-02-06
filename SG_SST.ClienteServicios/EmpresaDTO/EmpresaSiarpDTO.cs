using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.ClienteServicios.EmpresaDTO
{
    public class EmpresaSiarpDTO
    {
        public string tipoDoc { get; set; }
        public string idEmpresa { get; set; }
        public string idRepresentanteLegal { get; set; }
        public string razonSocial { get; set; }
        public string direccionEmpresa { get; set; }
        public string idDepartamento { get; set; }
        public string departamento { get; set; }
        public string idMunicipio { get; set; }
        public string municipio { get; set; }
        public string idSeccional { get; set; }
        public string idSectorEconomico { get; set; }
        public string actividadEconomica { get; set; }
        public string emailEmpresa { get; set; }
        public string telefonoEmpresa { get; set; }
        public string numeroDeTrabajadores { get; set; }
        public string fecAfiliaEfectiva { get; set; }
        public string estado { get; set; }
        public string paginaWeb { get; set; }
        public string faxEmpresa { get; set; }
        public string zona { get; set; }
        public string nomActEconomico { get; set; }
        public string riesgo { get; set; }
    }
}
