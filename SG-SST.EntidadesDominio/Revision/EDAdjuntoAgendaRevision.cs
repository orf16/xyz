using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Revision
{
    public class EDAdjuntoAgendaRevision
    {
        public int PK_Id_AdjuntoAgendaRevision { get; set; }

        public int FK_AgendaRevision { get; set; }
        
        public string Nombre_Archivo { get; set; }
    }
}
