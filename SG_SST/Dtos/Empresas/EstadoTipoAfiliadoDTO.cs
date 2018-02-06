using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Dtos.Empresas
{
    public class EstadoTipoAfiliadoDTO
    {
        public string tipoDocAfi { get; set; }
        public string docAfi { get; set; }
        public string estadoAfi { get; set; }
        public string tipoCotizante { get; set; }
        public string nombre1 { get; set; }
        public string nombre2 { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string fecNac { get; set; }
        public string emailAfi { get; set; }
        public string ocupacion { get; set; }
        public string cargo { get; set; }

        public EstadoTipoAfiliadoDTO()
        {

        }

        public EstadoTipoAfiliadoDTO(string tipoDocAfi,
        string docAfi,
        string estadoAfi,
        string tipoCotizante,
        string nombre1,
        string nombre2,
        string apellido1,
        string apellido2,
        string fecNac,
        string emailAfi,
        string ocupacion,
        string cargo)
        {
            this.tipoDocAfi = tipoDocAfi;
            this.docAfi = docAfi;
            this.estadoAfi = estadoAfi;
            this.tipoCotizante = tipoCotizante;
            this.nombre1 = nombre1;
            this.nombre2 = nombre2;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.fecNac = fecNac;
            this.emailAfi = emailAfi;
            this.ocupacion = ocupacion;
            this.cargo = cargo;
        }

    }
}