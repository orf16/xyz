using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Dtos.Planificacion
{
    public static class UsuarioCreacionPeligros
    {
        
        /// <summary>
        /// Obtiene y establece el nombre del profesional que elaboro la metodologia.
        /// </summary>
        public static string Nombre_Del_Profesional { get; set; }

        /// <summary>
        /// Obtiene y establece el numero de documento del profesional que elaboro la metodologia.
        /// </summary>
        public static string Numero_De_Documento { get; set; }

        /// <summary>
        /// Obtiene y establece el numero de la licencia del profesional que elaboro la metodologia.
        /// </summary>
        public static string Numero_De_Licencia_SST { get; set; }

        public static SG_SST.Controllers.Empresas.UsuariosController UsuariosController
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}