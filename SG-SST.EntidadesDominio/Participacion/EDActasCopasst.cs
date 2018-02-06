using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Participacion
{
    public class EDActasCopasst
    {
        public int PK_Id_Acta { get; set; }
        public int Consecutivo_Acta { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }
        public string TemaReunion { get; set; }
        public string Conclusiones { get; set; }
        public int Fk_Id_Sede { get; set; }
        public string NombreSede { get; set; }
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public int Fk_Id_UsuarioSistema { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreArchivo { get; set; }

        public List<EDActasCopasst> ActasCopasst { get; set; }
        public List<EDMiembrosCopasst> MiembrosActaCopasst { get; set; }
    }
}
