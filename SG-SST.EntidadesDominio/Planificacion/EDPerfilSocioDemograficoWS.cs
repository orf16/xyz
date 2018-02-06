
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
   public class EDPerfilSocioDemograficoWS
    {
           public EDPerfilSocioDemograficoWS()
        {
        }

           public EDPerfilSocioDemograficoWS(

            string documentoEmp,
            string tpDocEmpresa,
            string idEmpresa,
            string tipoDoc,
            string idPersona,
            int idDepAfiliado,
            string nomDepAfiliado,
            int idMunAfiliado,
            string idZona,
            string nomMunAfiliado,
            string nombre1,
            string nombre2,
            string apellido1,
            string apellido2,
            string emailPersona,
            string telPersona,
            string sexo,
            DateTime fechaInicioVinculacion,
            string fechaFinVinculacion,
            string estado,
            DateTime fechaNacimiento,
            string afp,
            string nombreAfp,
            string eps,
            string nombreEps,
            string dirPersona,
            string  nombreArl,
            string idArl,
            string ocupacion,
            string idOcupacion,
            string sexoPersona,
            string salario,
            string actividadEconomica,
            int idActividadEconomica
         
            )
        {
            this.tpDocEmpresa = tpDocEmpresa;
            this.idEmpresa = idEmpresa;
            this.tipoDoc = tipoDoc;
            this.idPersona = idPersona;
            this.idDepAfiliado = idDepAfiliado;
            this.nomDepAfiliado = nomDepAfiliado;
            this.idMunAfiliado = idMunAfiliado;
            this.nomMunAfiliado = nomMunAfiliado;
            this.nombre1 = nombre1;
            this.nombre2 = nombre2; 
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.emailPersona = emailPersona;
            this.telPersona = telPersona;
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
            this.sexoPersona = sexoPersona;
            this.actividadEconomica = actividadEconomica;
            this.idActividadEconomica = idActividadEconomica;
            this.idZona = idZona;
        }

           public string sexoPersona { get; set; }

           public string idZona { get; set; }
           public string documentoEmp { get; set; }
            public string tpDocEmpresa { get; set; }
            public string idEmpresa { get; set; } 
            public string tipoDoc { get; set; }
            public string idPersona { get; set; }
            public int idDepAfiliado { get; set; }
            public string nomDepAfiliado { get; set; }
            public int idMunAfiliado { get; set; }
            public string nomMunAfiliado { get; set; }
            public string  nombre1 { get; set; }
            public string  nombre2 { get; set; } 
            public string  apellido1 { get; set; }
            public string  apellido2 { get; set; }
            public string  emailPersona { get; set; }
            public string telPersona { get; set; }
            public string  sexo { get; set; }
            public DateTime  fechaInicioVinculacion { get; set; }
            public string  fechaFinVinculacion { get; set; }
            public string  estado { get; set; }
            public DateTime  fechaNacimiento { get; set; }
            public string  afp { get; set; }
            public string  nombreAfp { get; set; }
            public string  eps { get; set; }
            public string  nombreEps { get; set; }
            public string dirPersona { get; set; }
            public string  nombreArl { get; set; }
            public string  idArl { get; set; }
            public string  ocupacion { get; set; }
            public string  idOcupacion { get; set; }
            public string salario { get; set; }

            public string actividadEconomica { get; set; }

            public int idActividadEconomica { get; set; }



    }
    
}
