using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Ausentismo
{
    public class EDEmpresa
    {
        public int PK_Id_empresa { get; set; }
        public string IdEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public string DireccionEmpresa { get; set; }
        public string EmailEmpresa { get; set; }
        public string TelefonoEmpresa { get; set; }
        public string NumeroEmpleados { get; set; }
        public System.DateTime FechaActivacion { get; set; }
        public string Estado { get; set; }
        public string PaginaWeb { get; set; }
        public string FaxEmpresa { get; set; }
        public string ActEconoPrincipal { get; set; }
        public string ActEconoCentroTrabajo { get; set; }
        public string NumeroTrabajadoresCentroTrabajo { get; set; }
    }
}
