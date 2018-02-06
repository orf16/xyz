using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Revision
{
    public class EDAgendaRevision
    {
        public int PK_Id_Agenda { get; set; }

        public int FK_ActaRevision { get; set; }

        public string Titulo { get; set; }

        public string Desarrollo { get; set; }

        public int ConsecutivoActaEmpresaED { get; set; }

        public List<EDAdjuntoAgendaRevision> AdjuntosAgendaRevision { get; set; }

    }
}
