using SG_SST.Models.Empleado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Empresa
{
    public class UsuarioModel
    {

        public int Pkusuario { get; set; }

        public int FkDocumento { get; set; }
        public int NumeroDocumento { get; set; }
        public string NombreUsuario { get; set; }

        public string ImagenFirma { get; set; }
        public string NitEmpresa { get; set; }

        public int FkEmpresa { get; set; }



    }
}