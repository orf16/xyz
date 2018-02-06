using SG_SST.Interfaces.Revision;
using SG_SST.Repositorio.Revision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.Revision
{
    public class IMRevision
    {
        public static IRevision Revision()
        {
            return new RevisionManager();
        }

    }
}
