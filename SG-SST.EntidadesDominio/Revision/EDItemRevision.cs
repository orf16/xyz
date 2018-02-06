using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Revision
{
    public class EDItemRevision
    {
        public int PK_Id_ItemRevision { get; set; }
        
        public string Tema { get; set; }

        public bool Agregado { get; set; }
    }
}
