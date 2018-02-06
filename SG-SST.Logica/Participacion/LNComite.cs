using SG_SST.EntidadesDominio.Participacion;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Interfaces.Participacion;
using SG_SST.InterfazManager.Participacion;
using OfficeOpenXml;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Audotoria;
using System;
using System.Globalization;
using System.IO;
using SG_SST.Models;
using System.Configuration;
using System.Web;
using System.Net;
using RestSharp;

namespace SG_SST.Logica.Participacion
{
   public class LNComite
    {
        private static IComite Comite = IMComite.Comite();

        public EDSede ObtenerInformacionSede(int IdSede)
        {
            return Comite.ObtenerInformacionSede(IdSede);
        }
        public List<EDActasCopasst> ObtenerInformacionActaCopasst(int IdSede)
        {
            return Comite.ObtenerInformacionActaCopasst(IdSede);
        }
         public List<EDTipoPrincipalActa> ObtenerTipoPrincipal()
        {
            return Comite.ObtenerTipoPrincipal();
        }
        public List<EDTipoPrioridadActa> ObtenerTipoPrioridad()
        {
            return Comite.ObtenerTipoPrioridad();
        }
        public List<EDActasCopasst> ObtenerActasCopasstPorEmpresa(int id)
        {
            return Comite.ObtenerActasCopasstPorEmpresa(id);
        }
        public EDActasCopasst ObtenerActasCopasstPorId(int id)
        {
            return Comite.ObtenerActasCopasstPorId(id);
        }
        public EDMiembrosCopasst GuardarMiembrosActaCopasst(EDMiembrosCopasst MiembrosCopasst)
        {

            return Comite.GuardarMiembrosActaCopasst(MiembrosCopasst);
        }
        public List<EDParticipantes> GuardarParticipantesActaCopasst(EDParticipantes ParticipantesCopasst)
        {

            return Comite.GuardarParticipantesActaCopasst(ParticipantesCopasst);
        }
        public List<EDTemasActasCopasst> GuardarTemasActaCopasst(EDTemasActasCopasst TemasCopasst)
        {

            return Comite.GuardarTemasActaCopasst(TemasCopasst);
        }
        public EDActaConvivenciaQuejas GuardarActasQueja(EDActaConvivenciaQuejas acta)
        {
            return Comite.GuardarActasQueja(acta);
        }
        public EDSeguimientoActaConvivencia GuardarActasSeguimiento(EDSeguimientoActaConvivencia acta)
        {
            return Comite.GuardarActasSeguimiento(acta);
        }
        public List<EDTemasActasCopasst> ActualizarTemasActaCopasst(EDTemasActasCopasst TemasCopasst)
        {

            return Comite.ActualizarTemasActaCopasst(TemasCopasst);
        }
        public List<EDAccionesActaCopasst> GuardarAccionesCopasst(EDAccionesActaCopasst AccionesCopasst)
        {

            return Comite.GuardarAccionesCopasst(AccionesCopasst);
        }
        public List<EDMiembrosCopasst> ObtenerMiembrosCopasstPorActa(int Id_Acta)
        {
            return Comite.ObtenerMiembrosCopasstPorActa(Id_Acta);
        }
        public List<EDParticipantes> ObtenerParticipantesCopasstPorActa(int Id_Acta)
        {
            return Comite.ObtenerParticipantesCopasstPorActa(Id_Acta);
        }
        public List<EDTemasActasCopasst> ObtenerTemasCopasstPorActa(int Id_Acta)
        {
            return Comite.ObtenerTemasCopasstPorActa(Id_Acta);
        }
        public List<EDAccionesActaCopasst> ObtenerAccionesCopasstPorActa(int Id_Acta)
        {
            return Comite.ObtenerAccionesCopasstPorActa(Id_Acta);
        }
        public List<EDMiembrosCopasst> EliminarMiembroActaCopasst(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            return Comite.EliminarMiembroActaCopasst(Documento, Usuario, NombreUsuario, PK_Id_Acta);
        }
        public List<EDParticipantes> EliminarParticipanteCopasst(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            return Comite.EliminarParticipanteCopasst(Documento, Usuario, NombreUsuario, PK_Id_Acta);
        }
        public List<EDTemasActasCopasst> EliminarTemaActaCopasst(int PK_Id_TemaActa, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            return Comite.EliminarTemaActaCopasst(PK_Id_TemaActa, Usuario, NombreUsuario, PK_Id_Acta);
        }
        public List<EDAccionesActaCopasst> EliminarAccionActaCopasst(int Pk_Id_AccionActaCopasst, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            return Comite.EliminarAccionActaCopasst(Pk_Id_AccionActaCopasst, Usuario, NombreUsuario, PK_Id_Acta);
        }
        public EDActasCopasst ImportarActaCopasst(EDActasCopasst ImportarActaCopasst)
        {
            return Comite.ImportarActaCopasst(ImportarActaCopasst);
        }
        public EDActasCopasst ActualizarActaCopasst(EDActasCopasst InformacionActaCopasst)
        {
            return Comite.ActualizarActaCopasst(InformacionActaCopasst);
        }


       ///////////////////////////////////////////////////////////////////////////////////////

        public List<EDActasConvivencia> ObtenerInformacionActaConvivencia(int IdSede)
        {
            return Comite.ObtenerInformacionActaConvivencia(IdSede);
        }

        public List<EDActasConvivencia> ObtenerActasConvivenciaPorEmpresa(int id)
        {
            return Comite.ObtenerActasConvivenciaPorEmpresa(id);
        }
        public EDActasConvivencia ObtenerActasConvivenciaPorId(int id)
        {
            return Comite.ObtenerActasConvivenciaPorId(id);
        }
        public EDMiembrosConvivencia GuardarMiembrosActaConvivencia(EDMiembrosConvivencia MiembrosConvivencia)
        {

            return Comite.GuardarMiembrosActaConvivencia(MiembrosConvivencia);
        }
        public List<EDParticipantesActaConvivencia> GuardarParticipantesActaConvivencia(EDParticipantesActaConvivencia ParticipantesConvivencia)
        {

            return Comite.GuardarParticipantesActaConvivencia(ParticipantesConvivencia);
        }
        public List<EDResponsablesQuejas> GuardarResponsablesQueja(EDResponsablesQuejas Responsable)
        {

            return Comite.GuardarResponsablesQueja(Responsable);
        }
        public List<EDTemasActasConvivencia> GuardarTemasActaConvivencia(EDTemasActasConvivencia TemasConvivencia)
        {

            return Comite.GuardarTemasActaConvivencia(TemasConvivencia);
        }
        public List<EDTemasActasConvivencia> ActualizarTemasActaConvivencia(EDTemasActasConvivencia TemasConvivencia)
        {

            return Comite.ActualizarTemasActaConvivencia(TemasConvivencia);
        }
        public List<EDAccionesActaConvivencia> GuardarAccionesConvivencia(EDAccionesActaConvivencia AccionesConvivencia)
        {

            return Comite.GuardarAccionesConvivencia(AccionesConvivencia);
        }
        public List<EDAccionesActaQuejas> GuardarAccionesActaQueja(EDAccionesActaQuejas Acciones)
        {
            return Comite.GuardarAccionesActaQueja(Acciones);
        }
        public List<EDCompromisosPendientes> GuardarCompromisosSeguimiento(EDCompromisosPendientes Compromiso)
        {
            return Comite.GuardarCompromisosSeguimiento(Compromiso);
        }
        public List<EDMiembrosConvivencia> ObtenerMiembrosConvivenciaPorActa(int Id_Acta)
        {
            return Comite.ObtenerMiembrosConvivenciaPorActa(Id_Acta);
        }
        public List<EDParticipantesActaConvivencia> ObtenerParticipantesConvivenciaPorActa(int Id_Acta)
        {
            return Comite.ObtenerParticipantesConvivenciaPorActa(Id_Acta);
        }
        public List<EDTemasActasConvivencia> ObtenerTemasConvivenciaPorActa(int Id_Acta)
        {
            return Comite.ObtenerTemasConvivenciaPorActa(Id_Acta);
        }
        public List<EDAccionesActaConvivencia> ObtenerAccionesConvivenciaPorActa(int Id_Acta)
        {
            return Comite.ObtenerAccionesConvivenciaPorActa(Id_Acta);
        }
        public List<EDActaConvivenciaQuejas> ObtenerActasConvivenciaQueja(int IdSede)
        {
            return Comite.ObtenerActasConvivenciaQueja(IdSede);
        }
        public List<EDSeguimientoActaConvivencia> ObtenerActasConvivenciaSeguimiento(int IdSede)
        {
            return Comite.ObtenerActasConvivenciaSeguimiento(IdSede);
        }
        public List<EDAccionesActaQuejas> ObtenerAccionesActasQueja(int PK_Id_Queja)
        {
            return Comite.ObtenerAccionesActasQueja(PK_Id_Queja);
        }
        public List<EDCompromisosPendientes> ObtenerCompromisosActaConvivencia(int PK_Id_Seguimiento)
        {
            return Comite.ObtenerCompromisosActaConvivencia( PK_Id_Seguimiento);
        }
        public List<EDResponsablesQuejas> ObtenerResponsablesQueja(int PK_Id_Queja)
        {
            return Comite.ObtenerResponsablesQueja(PK_Id_Queja);
        }
       public List<EDMiembrosConvivencia> EliminarMiembroActaConvivencia(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            return Comite.EliminarMiembroActaConvivencia(Documento, Usuario, NombreUsuario, PK_Id_Acta);
        }
        public List<EDParticipantesActaConvivencia> EliminarParticipanteConvivencia(int Documento, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            return Comite.EliminarParticipanteConvivencia(Documento, Usuario, NombreUsuario, PK_Id_Acta);
        }
        public List<EDResponsablesQuejas> EliminarResponsableQueja(int Documento, int Usuario, string NombreUsuario, int Fk_Id_Queja, int PK_Id_Acta)
        {
            return Comite.EliminarResponsableQueja( Documento,  Usuario,  NombreUsuario,  Fk_Id_Queja,  PK_Id_Acta);
        }
        public List<EDTemasActasConvivencia> EliminarTemaActaConvivencia(int PK_Id_TemaActa, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            return Comite.EliminarTemaActaConvivencia(PK_Id_TemaActa, Usuario, NombreUsuario, PK_Id_Acta);
        }
        public List<EDAccionesActaConvivencia> EliminarAccionActaConvivencia(int Pk_Id_AccionActaConvivencia, int Usuario, string NombreUsuario, int PK_Id_Acta)
        {
            return Comite.EliminarAccionActaConvivencia(Pk_Id_AccionActaConvivencia, Usuario, NombreUsuario, PK_Id_Acta);
        }
        public List<EDAccionesActaQuejas> EliminarAccionActaQueja(int Pk_Id_AccionQueja, int Usuario, string NombreUsuario, int Fk_Id_Queja, int PK_Id_Acta)
        {
            return Comite.EliminarAccionActaQueja( Pk_Id_AccionQueja,  Usuario,  NombreUsuario,  Fk_Id_Queja,  PK_Id_Acta);
        }
        public List<EDCompromisosPendientes> EliminarCompromisoSeguimiento(int Pk_Id_Compromiso, int Usuario, string NombreUsuario, int FK_Id_Seguimiento, int PK_Id_Acta)
        {
            return Comite.EliminarCompromisoSeguimiento( Pk_Id_Compromiso,  Usuario,  NombreUsuario,  FK_Id_Seguimiento,  PK_Id_Acta);
        }
        public EDActasConvivencia ImportarActaConvivencia(EDActasConvivencia ImportarActaConvivencia)
        {
            return Comite.ImportarActaConvivencia(ImportarActaConvivencia);
        }
        public EDActasConvivencia ActualizarActaConvivencia(EDActasConvivencia InformacionActaConvivencia)
        {
            return Comite.ActualizarActaConvivencia(InformacionActaConvivencia);
        }
        public EDActaConvivenciaQuejas ActualizarActaConvivenciaQueja(EDActaConvivenciaQuejas InformacionActaConvivenciaQueja)
        {
            return Comite.ActualizarActaConvivenciaQueja(InformacionActaConvivenciaQueja);
        }
        public EDSeguimientoActaConvivencia ActualizarActaConvivenciaSeguimiento(EDSeguimientoActaConvivencia InformacionActaConvivenciaSeg)
        {
            return Comite.ActualizarActaConvivenciaSeguimiento(InformacionActaConvivenciaSeg);
        }

   }
}
