using SG_SST.EntidadesDominio.Ausentismo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Ausentismo
{
    public interface IConfiguracion
    {
        bool GuardarConfiguracion(EDConfiguracion configuracion);
        List<EDAusencia> ObtieneAusenciasDelMismoPerido(DateTime fechaInicial, DateTime FechaFinal, int IdEmpresa);
        List<EDAusencia> ObtieneAusenciasDesdePeridoAterior(DateTime fechaInicial, DateTime FechaFinal, int IdEmpresa);
        List<EDAusencia> ObtieneAusenciasPeridoConFinMesSiguiente(DateTime fechaInicial, DateTime FechaFinal, int IdEmpresa);
        List<EDAusencia> ObtieneAusenciasPeriodosAtrasYAdelante(DateTime fechaInicial, DateTime FechaFinal, int IdEmpresa);
        List<EDConfiguracion> ObtenerConfiguracionesEmpresa(string NitEmpresa, int ano);
        bool EliminarConfiguracion(string NitEmpresa, int idconfiguracion);

    }
}
