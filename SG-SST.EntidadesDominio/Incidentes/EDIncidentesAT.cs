using SG_SST.EntidadesDominio.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Incidentes
{
    public class EDIncidentesAT
    {
        public int PK_Incidentes_AT { get; set; }
        /// <summary>
        /// I. Informe sobre la Investigacion
        /// </summary>
        public string FechaInvestigacionI { get; set; }
        public List<EDDepartamento> DepartamentoI { get; set; }
        public List<EDMunicipio> MunicipioI { get; set; }
        public string DireccionI { get; set; }
        public string HoraInicialI { get; set; }
        public string HoraFinalI { get; set; }
        public string ResponsablesI { get; set; }
        public bool FotografiasI { get; set; }
        public bool VideosI { get; set; }
        public bool CintasAudioI { get; set; }
        public bool IlustracionesI { get; set; }
        public bool DiagramasI { get; set; }
        public bool OtrosI { get; set; }
        public string CualesI { get; set; }

    }
}
