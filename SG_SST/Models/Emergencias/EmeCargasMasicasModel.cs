using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Emergencias
{
    public class EmeCargasMasicasModel
    {
    }

    public class EntidadHojaExcel
    {
        public string sede { get; set; }
        public string entidad { get; set; }
        public string direccion { get; set; }
        public string Nombre {get; set;}
        public string Documento {get; set;}
        public string Genero {get; set;}
        public string EPS {get; set;}	
        public string RH {get; set;}	
        public string NombreContacto {get; set;}	
        public string Telefono {get; set;}	
        public string Parentesco {get; set;}	
        public string RequiereManejo  {get; set;}
        public string Cual { get; set; }
    }

    public class PlanAyuda
    {
        public string sede { get; set; }
        public string empresa { get; set; }
        public string recurso { get; set; }
        public string compensacion { get; set; }
        public string reintegro { get; set; }
        public string nombre_contacto { get; set; }
        public string telefono_contacto { get; set; }

    }


}