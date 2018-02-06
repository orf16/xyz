using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Dtos.Organizacion
{
    public class EmpleadosWSDTO
    {
        public EmpleadosWSDTO()
        {
        }
        public EmpleadosWSDTO(
            string idPersona,
            string nombre1,
            string nombre2,
            string apellido1,
            string apellido2,
            string cargo,
            string emailPersona)
        {
            this.idPersona = idPersona;
            this.nombre1 = nombre1;
            this.nombre2 = nombre2;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.cargo = cargo;
            this.emailPersona = emailPersona;
        }
        public string idPersona { get; set; }
        public string nombre1 { get; set; }
        public string nombre2 { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string cargo { get; set; }
        public string emailPersona { get; set; }
    }
}