using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Empresa
{
    public class EmpleadoTerceroModel
    {
        public int ID_Empleado { get; set; }


        public int FK_Tipo_Documento_Empl { get; set; }

        public string TipoDocumento  { get; set; }


        public string Numero_Documento_Empl { get; set; }

        public string Nombre1 { get; set; }

        public string Nombre2 { get; set; }

        public string Apellido1 { get; set; }


        public string Apellido2 { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string Email { get; set; }

        public string Ocupacion_Empl { get; set; }


        public string Cargo_Empl { get; set; }


        public string Email_Empl { get; set; }


        public string PK_Nit_Empresa { get; set; }
        

        
        public int FKRelacionLaboralTercero { get; set; }

        public string RelacionesLaboralesTercero { get; set; }
    }
}