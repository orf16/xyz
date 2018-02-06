using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.AdminUsuarios
{
    public class EmpresaAfiliadoModel
    {
        public string tipoDoc { get; set; }
        public string idPersona { get; set; }
        public string nomVinLaboral { get; set; }
        public int idOcupacion { get; set; }
        public string nombre1 { get; set; }
        public string nombre2 { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string fechaNacimiento { get; set; }
        public string ocupacion { get; set; }
        public string cargo { get; set; }
        public string emailPersona { get; set; }
        public string dirPersona { get; set; }
        public string telPersona { get; set; }
        public string sexoPersona { get; set; }
        public string tipoDocEmp { get; set; }
        public string documentoEmp { get; set; }
        public string idRepresentante { get; set; }
        public string razonSocial { get; set; }
        public string dirEmpresa { get; set; }
        public int idDeparEmpresa { get; set; }
        public int idMuniEmpresa { get; set; }
        public string nomDepEmpresa { get; set; }
        public string nomMunEmpresa { get; set; }
        public int idSeccional { get; set; }
        public int idActividadEconomica { get; set; }
        public int idSectordEconomica { get; set; }
        public string riesgo { get; set; }
        public string Estado { get; set; }
        public int numeroTrabajadores { get; set; }
        public string emailEmpresa { get; set; }
        public string telefonoEmpresa { get; set; }
        public string fechaAfiliacionEfectiva { get; set; }
        public string estadoEmpresa { get; set; }
        public string paginaWebEmpresa { get; set; }
        public string faxEmpresa { get; set; }
        public string idZona { get; set; }
        public string actividadEconomica { get; set; }
        public int idDepAfiliado { get; set; }
        public int idMunAfiliado { get; set; }
        public string nomDepAfiliado { get; set; }
        public string nomMunAfiliado { get; set; }
        public string fechaInicioVinculacion { get; set; }
        public string fechaFinVinculacion { get; set; }
        public string estadoPersona { get; set; }
        public int idAfp { get; set; }
        public string nombreAfp { get; set; }
        public string idEps { get; set; }
        public string nombreEps { get; set; }
        public string idArl { get; set; }
        public string nombreArl { get; set; }
        public decimal salario { get; set; }
    }
}