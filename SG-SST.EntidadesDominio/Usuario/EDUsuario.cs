using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Usuario
{
    public class EDUsuario
    {
        public int Pk_Id_Usuario { get; set; }
        public int Fk_Tipo_Documento { get; set; }     
        public int Numero_Documento { get; set; }
        public string ClaveAcceso { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Apellido_Usuario { get; set; }
        public string Imagen_Firma { get; set; }
        public string Correo { get; set; }
        public int Fk_Id_Empresa { get; set; }
        public string nit_Empresa { get; set; }
        public string RazonSocial { get; set; }
        public string Documento { get; set; }
    }
}
