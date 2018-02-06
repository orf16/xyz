using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Participacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Participacion
{
    public interface IComite
    {
      EDSede ObtenerInformacionSede(int IdSede);
      List<EDActasCopasst> ObtenerInformacionActaCopasst(int IdSede);
      List<EDTipoPrincipalActa> ObtenerTipoPrincipal();
      List<EDTipoPrioridadActa> ObtenerTipoPrioridad();
      List<EDActasCopasst> ObtenerActasCopasstPorEmpresa(int id);
      EDMiembrosCopasst GuardarMiembrosActaCopasst(EDMiembrosCopasst MiembrosCopasst);
      List<EDParticipantes> GuardarParticipantesActaCopasst(EDParticipantes ParticipantesCopasst);
      List<EDTemasActasCopasst> GuardarTemasActaCopasst(EDTemasActasCopasst TemasCopasst);
      List<EDTemasActasCopasst> ActualizarTemasActaCopasst(EDTemasActasCopasst TemasCopasst);
      List<EDAccionesActaCopasst> GuardarAccionesCopasst(EDAccionesActaCopasst AccionesCopasst);
      List<EDMiembrosCopasst> ObtenerMiembrosCopasstPorActa(int Id_Acta);
      List<EDParticipantes> ObtenerParticipantesCopasstPorActa(int Id_Acta);
      List<EDTemasActasCopasst> ObtenerTemasCopasstPorActa(int Id_Acta);
      List<EDAccionesActaCopasst> ObtenerAccionesCopasstPorActa(int Id_Acta);
      EDActasCopasst ObtenerActasCopasstPorId(int Id_Acta);
      List<EDMiembrosCopasst> EliminarMiembroActaCopasst(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta);
      List<EDParticipantes> EliminarParticipanteCopasst(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta);
      List<EDTemasActasCopasst> EliminarTemaActaCopasst(int PK_Id_TemaActa, int Usuario, string NombreUsuario, int PK_Id_Acta);
      List<EDAccionesActaCopasst> EliminarAccionActaCopasst(int Pk_Id_AccionActaCopasst, int Usuario, string NombreUsuario, int PK_Id_Acta);
      EDActasCopasst ImportarActaCopasst(EDActasCopasst ImportarActaCopasst);
      EDActasCopasst ActualizarActaCopasst(EDActasCopasst InformacionActaCopasst);

    /////////////////////////////////////////////////////////////////////////////////////////////////

      List<EDActasConvivencia> ObtenerInformacionActaConvivencia(int IdSede);
      List<EDActasConvivencia> ObtenerActasConvivenciaPorEmpresa(int id);
      EDMiembrosConvivencia GuardarMiembrosActaConvivencia(EDMiembrosConvivencia MiembrosConvivencia);
      List<EDParticipantesActaConvivencia> GuardarParticipantesActaConvivencia(EDParticipantesActaConvivencia ParticipantesConvivencia);
      List<EDResponsablesQuejas> GuardarResponsablesQueja(EDResponsablesQuejas Responsable);
      List<EDTemasActasConvivencia> GuardarTemasActaConvivencia(EDTemasActasConvivencia TemasConvivencia);
      List<EDTemasActasConvivencia> ActualizarTemasActaConvivencia(EDTemasActasConvivencia TemasConvivencia);
      List<EDAccionesActaConvivencia> GuardarAccionesConvivencia(EDAccionesActaConvivencia AccionesConvivencia);
      List<EDAccionesActaQuejas> GuardarAccionesActaQueja(EDAccionesActaQuejas Acciones);
      List<EDCompromisosPendientes> GuardarCompromisosSeguimiento(EDCompromisosPendientes Compromiso);
      EDActaConvivenciaQuejas GuardarActasQueja(EDActaConvivenciaQuejas acta);
      EDSeguimientoActaConvivencia GuardarActasSeguimiento(EDSeguimientoActaConvivencia acta);
      List<EDActaConvivenciaQuejas> ObtenerActasConvivenciaQueja(int IdSede);
      List<EDSeguimientoActaConvivencia> ObtenerActasConvivenciaSeguimiento(int IdSede);
      List<EDAccionesActaQuejas> ObtenerAccionesActasQueja(int PK_Id_Queja);
      List<EDCompromisosPendientes> ObtenerCompromisosActaConvivencia(int PK_Id_Seguimiento);
      List<EDResponsablesQuejas> ObtenerResponsablesQueja(int PK_Id_Queja);
     List<EDMiembrosConvivencia> ObtenerMiembrosConvivenciaPorActa(int Id_Acta);
      List<EDParticipantesActaConvivencia> ObtenerParticipantesConvivenciaPorActa(int Id_Acta);
      List<EDTemasActasConvivencia> ObtenerTemasConvivenciaPorActa(int Id_Acta);
      List<EDAccionesActaConvivencia> ObtenerAccionesConvivenciaPorActa(int Id_Acta);
      EDActasConvivencia ObtenerActasConvivenciaPorId(int Id_Acta);
      List<EDMiembrosConvivencia> EliminarMiembroActaConvivencia(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta);
      List<EDParticipantesActaConvivencia> EliminarParticipanteConvivencia(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta);
      List<EDResponsablesQuejas> EliminarResponsableQueja(int Documento, int Usuario, string NombreUsuario, int Fk_Id_Queja, int PK_Id_Acta);
      List<EDTemasActasConvivencia> EliminarTemaActaConvivencia(int PK_Id_TemaActa, int Usuario, string NombreUsuario, int PK_Id_Acta);
      List<EDAccionesActaConvivencia> EliminarAccionActaConvivencia(int Pk_Id_AccionActaConvivencia, int Usuario, string NombreUsuario, int PK_Id_Acta);
      List<EDAccionesActaQuejas> EliminarAccionActaQueja(int Pk_Id_AccionQueja, int Usuario, string NombreUsuario, int Fk_Id_Queja, int PK_Id_Acta);
      List<EDCompromisosPendientes> EliminarCompromisoSeguimiento(int Pk_Id_Compromiso, int Usuario, string NombreUsuario, int FK_Id_Seguimiento, int PK_Id_Acta);
      EDActasConvivencia ImportarActaConvivencia(EDActasConvivencia ImportarActaConvivencia);
      EDActasConvivencia ActualizarActaConvivencia(EDActasConvivencia InformacionActaConvivencia);
      EDActaConvivenciaQuejas ActualizarActaConvivenciaQueja(EDActaConvivenciaQuejas InformacionActaConvivenciaQueja);
      EDSeguimientoActaConvivencia ActualizarActaConvivenciaSeguimiento(EDSeguimientoActaConvivencia InformacionActaConvivenciaSeg);

    
    }
}
