using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Dtos.Participacion
{
    public class ReporteCondicionesInsegurasDTO
    {
    


        public ReporteCondicionesInsegurasDTO()
        {
        }

        public ReporteCondicionesInsegurasDTO(
              string idEmpresa,
            string nombre1,
            string nombre2,
            string apellido1,
            string apellido2,

            string ocupacion


            )
        {
            this.nombre1 = nombre1;
            this.nombre2 = nombre2;
            this.apellido1=apellido1;
            this.apellido2 = apellido2;
            this.ocupacion = ocupacion;
            this.idEmpresa = idEmpresa;
        }
        public string nombre1 { get; set; }

        public string nombre2 { get; set; }

        public string apellido1 { get; set; }

        public string apellido2 { get; set; }
        public string ocupacion { get; set; }
        public string idEmpresa { get; set; } 
    }
}