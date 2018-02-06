using SG_SST.EntidadesDominio.Ausentismo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Ausentismo
{
    public interface IIndicadores
    {
        List<EDIndicadoresGenerados> CantidadEventos(int anio, int idEmpresaUsuaria, string Nit, int IdContingenia);
        List<ResultadoConfiguracion> Configuracion(int anio, string Nit);
        List<EDAcumuladoTotalContingencias> ObtenerAcumuladoTotalContingencias(int anio, int tipoContingenciaComparar, string Nit, int idEmpresaUsuaria, int IdContingenia);
        List<EDAcumuladoTotalContingencias> ObtenerAcumuladoTotalContingencia(int anio, int tipoContingenciaComparar, string Nit, int idEmpresaUsuaria, int IdContingenia);
    }
}
