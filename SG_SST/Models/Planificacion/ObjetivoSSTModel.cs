using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Planificacion
{
    public class ObjetivoSSTModel
    {
        public int IdObjetivo { get; set; }
        public string Descripcion { get; set; }
        public string Meta { get; set; }
        public bool EsPorcentaje { get; set; }
        public string NitEmpresa { get; set; }
        public List<string> ListaID { get; set; }
    }
}