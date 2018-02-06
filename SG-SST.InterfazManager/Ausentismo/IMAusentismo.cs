using SG_SST.Interfaces.Ausentismo;
using SG_SST.Repositorio.Ausentismo;
using SG_SST.Interfaces.Ausentismo;
using SG_SST.Repositorio.Ausentismo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.Ausentismo
{
    public class IMAusentismo
    {
        public static IConfiguracion Configuracion()
        {
            return new ConfiguracionManager();
        }

        public static IContingencia Contingencia()
        {
            return new ContingenciaManager();
        }
        public static IDiagnostico Diagnostico()
        {
            return new DiagnosticoManager();
        }
        public static IAusencia Ausencia()
        {
            return new AusenciaManager();
        }
        public static IDepartamento Departamento()
        {
            return new DepartamentoManager();
        }
        public static IMunicipio Municipio()
        {
            return new MunicipioManager();
        }
        public static IReportes Reportes()
        {
            return new ReportesManager();
        }
        public static IAlertas Alertas()
        {
            return new AlertasManager();
        }
        public static IIndicadores Indicadores()
        {
            return new IndicadoresManager();
        }
    }
}
