using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
   public class EDEmpresas
    {

        public int Id_Empresa { get; set; }
        public string Nit_Empresa { get; set; }
        public string Tipo_Documento { get; set; }
        public int Identificacion_Representante { get; set; }
        public string Razon_Social { get; set; }
        public string Direccion { get; set; }
        public int Telefono { get; set; }
        public int Fax { get; set; }
        public int Riesgo { get; set; }
        public int Total_Empleados { get; set; }
        public int IdSeccional { get; set; }
        public int IdSectorEconomico { get; set; }
        public string Email { get; set; }
        public string SitioWeb { get; set; }
        public int Codigo_Actividad { get; set; }
        public string Fecha_Vigencia_Actual { get; set; }
        public string Flg_Estado { get; set; }
        public string Zona { get; set; }
        public string Descripcion_Actividad { get; set; }
        public EDDepartamento Departamento { get; set; }
        public EDMunicipio Municipio { get; set; }
        public string NombreArchivo { get; set; }
        //public List<Sede> { get; set; }
    }
}
