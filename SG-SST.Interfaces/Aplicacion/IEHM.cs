using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Aplicacion
{
    public interface IEHM
    {
        bool GuardarHojaVidaEMH(EDAdmoEMH EDAdmoEMH, List<EDPeligroEMH> ListaPeligro);
        bool EditarHojaVidaEMH(EDAdmoEMH EDAdmoEMH, List<EDPeligroEMH> ListaPeligro);
        bool DarBajaEMH(EDAdmoEMH EDAdmoEMH);
        bool SubirEMH(EDAdmoEMH EDAdmoEMH);
        List<EDAdmoEMH> ConsultaAdmoEMH(string Tipo, string Nombre, int idEmpresa);
        bool EliminarEHM(int IdElemento, int IdEmpresa);
        EDAdmoEMH ConsultarEHM(int IdElemento, int IdEmpresa);
        List<EDProceso> ConsultaSubProcesos(int IdProceso, int Idempresa);
        List<EDEHMInspecciones> ConsultaInspeccion(string FechaI, string FechaD, int IdEHM,int IdEmpresa);
        EDEHMInspecciones ConsultaEHMInspeccion(int IdEHMIns, int IdEmpresa);
    }
}
