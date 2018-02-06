using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Empleado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Aplicacion
{
    public interface IEPP
    {
        bool GuardarMasivoEPP(List<EDEPP> ListaEPP);
        bool GuardarEPP(EDEPP EDEPP);
        bool EditarEPP(EDEPP EDEPP);
        List<EDEPP> ConsultaMatrizEpp(string Nombre, int IdClasPel, int IdCargo, int idEmpresa);
        List<EDEPP> ConsultaMatrizEppCargo(int IdCargo, int idEmpresa);
        List<EDEPP> ConsultaMatrizEppPersona(string IdPersona, int idEmpresa);
        EDEPP ConsultarEPPDownload(int IdEPP, int idEmpresa);
        EDEPP ConsultarEPP(int IdEPP, int idEmpresa);
        bool EliminarEPP(int IdElemento, int IdEmpresa);
        bool ComprobarAsignacionEPP(int IdElemento, int IdEmpresa);
        bool GuardarControlSuministro(EDEPPSuministro EDEPPSuministro, List<EDEPPSuministroEPP> ListaSuministros);
        EDEPPSuministro UltimoSuministro(int Id_Empresa);
        List<EDEPPSuministro> ConsultaListaAsignacion(string FechaAntes, string FechaDespues, int Cargo, string Cedula, int Riesgo, int Sede, int idEmpresa);
        EDEPPSuministro ConsultaListaAsignacionId(int id, int idEmpresa);
        bool EliminarAsigEPP(int IdAsig, int IdEmpresa);
        List<EDCargo> ListaCargos();
        List<EDEPP> ConsultaMatrizEppUsuario(EDCargo EDCargo, List<EDCargo> ListaEDCargo, string Nit);
        EDEPP ConsultarEPPAPP(int IdEPP, string Nit);


        List<EDEPP> ConsultaMatrizEppCargo2(string NIT);
    }
}
