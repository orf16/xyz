using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Empresa
{
    public class GrabarEmpresaModel
    {
        
        public string Nit_Empresa { get; set; }

        public string Tipo_Documento { get; set; }

        public int Identificacion_Representante { get; set; }

        public string Razon_Social { get; set; }

        public string Direccion { get; set; }

        public int Telefono { get; set; }
        public int Fax { get; set; }
        public int Riesgo { get; set; }

        public int Total_Empleados { get; set; }

        public int Id_Seccional { get; set; }

        public int Id_SectorEconomico { get; set; }

        public string Email { get; set; }

        public string Sitio_Web { get; set; }

        public int Codigo_Actividad { get; set; }

        public string Fecha_Vigencia_Actual { get; set; }

        public string Flg_Estado { get; set; }

        public string Descripcion_Actividad { get; set; }
        public string Zona { get; set; }

        public string Sede { get; set; }


    }
}