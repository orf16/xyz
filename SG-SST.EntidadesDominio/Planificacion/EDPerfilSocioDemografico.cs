using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.Planificacion;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDPerfilSocioDemografico
    {
        public int IDEmpleado_PerfilSocioDemoGrafico { get; set; }
        public string PK_Numero_Documento_Empl { get; set; }
       
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

        public string GradoEscolaridad { get; set; }
        public string Ingresos { get; set; }
        public int Fk_Id_Departamento { get; set; }
        public string departamento { get; set; }
        public int Fk_Id_Municipio { get; set; }
        public string municipio { get; set; }
        public string Direccion { get; set; }
        public bool Conyuge { get; set; }
        public bool Hijos { get; set; }
        public int FK_Estrato { get; set; }
        public string estrato { get; set; }
        public int FK_Estado_Civil { get; set; }
        public string estadoCivil{get;set;}
        public int FK_Etnia { get; set; }
        public string etnia { get; set; }
        public string OcupacionPerfil { get; set; }
        public String Sexo { get; set; }
        public String GrupoEtarios { get; set; }
        public int FK_VinculacionLaboral { get; set; }
        public string vinculacionLabotal { get; set; }

        public String TurnoTrabajo { get; set; }
        public String Cargo { get; set; }
        public DateTime fechaIngresoEmpresa { get; set; }
        public DateTime FechaIngresoUltimoCargo { get; set; }
        public String caracteristicasFisicas { get; set; }

        public String caracteristicasPsicologicas { get; set; }

        public String evaluacionesMedicasRequeridas { get; set; }

        public String Otro { get; set; }

        public String tipoPeligro { get; set; }

        public int codPeligro { get; set; }

        public int EdadPersona { get; set; }

        public int idFechaNacimiento { get; set; }

        public string idtipodoc { get; set; }
      

        public List<EDCondicionesRiesgoPerfil> condicionesRiesgo { get; set; }

        public string nitEmpresa { get; set; }

        public int? Procesos { get; set; }

        public int Pk_Id_Sede { get; set; }

        public string nombreSede { get; set; }
        public string ZonaLugar { get; set; }

        public string nombreProceso { get; set;}


        public string ciudadSede { get; set; }

        public string departamentoSede { get; set; }

        public string AFP { get; set; }

        public string EPS { get; set; }

        public int anyos { get; set; }

        public int mes { get; set; }

        public int dia { get; set; }

        public string razonSocialEmpresa { get; set; }

       


    }
}
