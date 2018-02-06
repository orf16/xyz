using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Aplicacion
{
    public class AccionCorrectivaVM
    {
        public int pkplanaccionvm { get; set; }
        public string respuestavm { get; set; }

        public string resumenvm { get; set; }

        public int procesoVM { get; set; }

        public string DescribeProcesoVM { get; set; }

        public int sedeVM { get; set; }

        public string DescribeSedeVM { get; set; }

        public List<AccionCorrectivaVM> acciones { get; set; }
        public List<AccionCorrectivaVM> todasacciones { get; set; }
    }
}