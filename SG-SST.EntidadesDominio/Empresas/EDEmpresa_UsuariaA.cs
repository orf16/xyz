using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Empresas
{
    public class EDEmpresa_UsuariaA
    {
        public int IdEmpresaUsuaria { get; set; }
        public string DocumentoEmpresa { get; set; }
        public string DocumentoEmpresaUsuaria { get; set; }
        public int IdTipoDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public int IdDepartamento { get; set; }
        public string Departamento { get; set; }
        public string Municipio { get; set; }

        public int Id_Municipio { get; set; }
        public string Estado_bd { get; set; }

        public bool rta { get; set; }

    }

}
