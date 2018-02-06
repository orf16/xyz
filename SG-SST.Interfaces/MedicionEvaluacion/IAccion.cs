using SG_SST.EntidadesDominio.Empleado;
using SG_SST.EntidadesDominio.MedicionEvaluacion;
using SG_SST.Models.MedicionEvaluacion;
using System.Collections.Generic;


namespace SG_SST.Interfaces.MedicionEvaluacion
{
    public interface IAccion
    {
        List<EDAccion> ListadeAccionesPorEmpresa(int Pk_Id_Empresa);
        List<EDAccion> ListadeAccionesPorEmpresaID(int Pk_Id_Empresa);
        bool GuardarAccionbool(EDAccion Accion);
        EDAccion UltimoPkId(int Pk_Id_Empresa);
        void GuardarHallazgo(EDHallazgo EDHallazgo);
        void GuardarAnalisis(EDAnalisis EDAnalisis);
        bool GuardarSeguimiento(EDSeguimiento EDSeguimiento);
        bool GuardarActividad(EDActividad EDActividad);
        bool GuardarArchivosAccion(EDArchivosAcciones EDArchivosAcciones);
        bool EditarAccion(EDAccion Accion);
        bool EditarHallazgo(EDHallazgo Hallazgo);
        bool EditarSeguimiento(EDSeguimiento Seguimiento);
        bool EditarActividad(EDActividad Actividad);
        bool EditarArchivo(EDArchivosAcciones Archivo);
        int UltimoIdArchivo(int idAccion, int idEmpresa);
        void EliminarHallazgos(EDHallazgo EDHallazgo);
        bool EliminarArchivos(EDArchivosAcciones EDArchivosAcciones);
        void EliminarAnalisis(EDAnalisis Analisis);
        List<EDAccion> ConsultarAcciones(string Id, string Nombre, string Estado, int idEmpresa, string Sede);
        void ConsultaAccionEstado(int idEmpresa);
        EDActividad ConsultarActividad(int IdActividad);
        EDSeguimiento ConsultarSeguimiento(int IdSeguimiento);
        List<EDAnalisis> ConsultarAnalisis(int Tipo, int IdAccion);
        List<EDHallazgo> ListaHallazgos(int IdAccion);
        List<EDArchivosAcciones> ListaArchivos(int IdAccion);
        List<EDAnalisis> ListaAnalisis(int IdAccion);
        bool EliminarEncontrarAccion(int IdAccion, int IdEmpresa);
        EDAccion ConsultaAccion(int IdAccion, int idEmpresa);
        bool EliminarAnalisis(List<Analisis> ListaAnalisisGuardar, int Tipo, int IdAccion);
        bool EditarAnalisis(List<EDAnalisis> ListaAnalisisGuardar, int Tipo, int IdAccion);
        List<EDCargo> ListaCargos();
        List<EDAnalisis> ConsultaAnalisisEdicion(int IdAccion, int idEmpresa, short Tipo);
        bool GuardarCambiosAnalisis(List<EDAnalisis> EDAnalisisGuardar, List<EDAnalisis> EDAnalisisEditar, List<EDAnalisis> EDAnalisisEliminar, short Tipo, int IdAccion);

    }
}

