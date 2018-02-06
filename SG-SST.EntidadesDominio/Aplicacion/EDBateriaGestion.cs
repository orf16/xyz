using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.Mvc;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDBateriaGestion
    {
        public int Pk_Id_BateriaGestion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public bool Finalizada { get; set; }
        public string TokenPublico { get; set; }
        public int Fk_Id_Empresa { get; set; }
        public int Fk_Id_Bateria { get; set; }

        public string Tipo { get; set; }
        public string Estado { get; set; }
        public int EstadoInt { get; set; }
        public int bateriaExtra { get; set; }
        public string NombreBateria { get; set; }
        public List<EDBateriaUsuario> ListaUsuarios { get; set; }

        public EDBateriaUsuario Informe { get; set; }
        //[AllowHtml]
        public string MailBody { get; set; }

    }
}
