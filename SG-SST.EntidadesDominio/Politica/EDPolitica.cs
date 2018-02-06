using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Politica
{
    public class EDPolitica
    {
        public int intCod_Politica { get; set; }

        //public int nit_Empresa { get; set; }
       
        public bool Firma_Rep { get; set; }
        public string strDescripcion_Politica { get; set; }

        public String Archivo_Politica { get; set; }  
     
        public int FK_Empresa { get; set; }  




    }
}
