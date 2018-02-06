using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Revision
{
    public class EDParticipanteRevision
    {
        public int PK_Id_ParticipanteRevision { get; set; }

        public string Nombre { get; set; }

        public string Documento { get; set; }

        public string Cargo { get; set; }

        public int FK_ActaRevision { get; set; }
    }
}
