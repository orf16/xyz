using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Dtos.Planificacion
{
    public class PerfilSocioDemograficoDTO
    {
        public PerfilSocioDemograficoDTO()
        {
        }

        public PerfilSocioDemograficoDTO(

            string documentoEmp,
            string tpDocEmpresa,
            string idEmpresa,
            string tipoDoc,
            string idtipodoc,
            string cargo,
            string idPersona,
            string idDepartamento,
            string departamento,
            string idMunicipio,
            string municipio,
            string nombre1,
            string nombre2,
            string apellido1,
            string apellido2,
            string emailPersona,
            string telefonoPersona,
            string sexo,

            string fechaFinVinculacion,
            string estado,
            string fechaNacimiento,
            string afp,
            string nombreAfp,
            string eps,
            string nombreEps,
            string dirPersona,
            string  nombreArl,
            string idArl,
            string ocupacion,
            string idOcupacion,
            string salario,
            string docEmpleado,
            string fechaInicioVinculacion
            
            )
        {
            this.tpDocEmpresa = tpDocEmpresa;
            this.idEmpresa = idEmpresa;
            this.tipoDoc = tipoDoc;
            this.idPersona = idPersona;
            this.idDepartamento = idDepartamento;
            this.departamento = departamento;
            this.idMunicipio = idMunicipio;
            this.municipio = municipio;
            this.nombre1 = nombre1;
            this.nombre2 = nombre2; 
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.emailPersona = emailPersona;
            this.telefonoPersona = telefonoPersona;
            this.sexo = sexo;
            this.fechaInicioVinculacion = fechaInicioVinculacion;
            this.fechaFinVinculacion = fechaFinVinculacion;
            this.estado = estado;
            this.fechaNacimiento = fechaNacimiento;
            this.afp = afp;
            this.nombreAfp = nombreAfp;
            this.eps = eps;
            this.nombreEps = nombreEps;
            this.dirPersona = dirPersona;
            this.nombreArl = nombreArl;
            this.idArl = idArl;
            this.ocupacion = ocupacion;
            this.idOcupacion = idOcupacion;
            this.salario = salario;
            this.documentoEmp = documentoEmp;
            this.cargo=cargo;
        }

        public string documentoEmp { get; set; }

            public string tpDocEmpresa { get; set; }
            public string idEmpresa { get; set; } 
            public string tipoDoc { get; set; }
            public string idPersona { get; set; }
            public string idDepartamento { get; set; }
            public string  departamento { get; set; }
            public string  idMunicipio { get; set; }
            public string  municipio { get; set; }
            public string  nombre1 { get; set; }
            public string  nombre2 { get; set; } 
            public string  apellido1 { get; set; }
            public string  apellido2 { get; set; }
            public string  emailPersona { get; set; }
            public string  telefonoPersona { get; set; }
            public string  sexo { get; set; }
            public string fechaInicioVinculacion { get; set; }
            public string  fechaFinVinculacion { get; set; }
            public string  estado { get; set; }
            public string  fechaNacimiento { get; set; }
            public string  afp { get; set; }
            public string  nombreAfp { get; set; }
            public string  eps { get; set; }
            public string  nombreEps { get; set; }
            public string  dirPersona { get; set; }
            public string  nombreArl { get; set; }
            public string  idArl { get; set; }
            public string  ocupacion { get; set; }
            public string  idOcupacion { get; set; }
            public string salario { get; set; }

            public string cargo { get; set; }





    }
}