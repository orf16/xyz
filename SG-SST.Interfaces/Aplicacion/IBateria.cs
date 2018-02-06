using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Empleado;
using SG_SST.EntidadesDominio.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Aplicacion
{
    public interface IBateria
    {
        List<EDBateriaCuestionario> ConsultarFormulario(int Pagina, int IdBateria);
        int PaginaIntralaboralA(string formdata, EDBateriaUsuario EDBateriaUsuario);
        int PaginaIntralaboralB(string formdata, EDBateriaUsuario EDBateriaUsuario);
        int PaginaExtralaboral(string formdata, EDBateriaUsuario EDBateriaUsuario);
        int PaginaEstres(string formdata, EDBateriaUsuario EDBateriaUsuario);
        bool GuardarEncuesta(List<EDBateriaResultado> ListaResultado, string key, int form);
        bool GuardarEncuestaExtra(List<EDBateriaResultado> ListaResultado, string key, int form);
        bool TerminarEncuesta(int pkusuario);
        bool TerminarEncuestaRechazo(int pkusuario);
        void EditarCheck9y10(int pkusuario, int tipo, string val);
        void EditarSegunCheck(int pkusuario, string Check9, string Check10);
        List<EDBateria> ConsultarBaterias();
        EDBateriaGestion CrearGestionNuevo(EDBateriaGestion EDBateriaGestionC);
        bool CrearConvocado(EDBateriaUsuario EDBateriaUsuario);
        bool CrearConvocadoPublico(EDBateriaUsuario EDBateriaUsuario);
        List<EDBateriaUsuario> CrearConvocadoMasivo(List<EDBateriaUsuario> ListaEDBateriaUsuario, int IdGestion, bool extra);
        List<EDBateriaUsuario> ConsultarUsuariosCorreos(int IdEmpresa, int IdConv, int IdGestion);
        EDBateriaGestion ConsultarGestion(int IdGestion, int IdEmpresa);
        bool VerificarCorreoExistente(string email, string numeroId, int IdGestion);
        EDBateriaUsuario ConsultarConvocado(int IdConv, int IdEmpresa);
        
        EDBateriaUsuario ConsultarConvocadoExtra(int IdConv, int IdEmpresa);
        bool EliminarConvocado(int IdConv, int IdEmpresa);
        bool GestionConResultados(int Idgestion, int IdEmpresa);
        List<EDRol> ConsultarRolesEmpresa(int IdEmpresa);
        List<EDCargo> ListaCargos();
        List<EDBateriaGestion> ConsultarListaGestion(int IdEmpresa);
        bool EditarEstadoGestion(int Idgestion, int Estado);
        bool EliminarGestion(int Idgestion, int IdEmpresa);
        EDBateriaUsuario ConsultarConvocadoKey(string key, int Form);
        EDBateriaUsuario ConsultarConvocadoKeyExtra(string key, int Form);
        EDBateriaInicial ConsultarInicialKey(int Fk_IdUsuario);
        bool ActualizarResultados(EDBateriaUsuario EDBateriaUsuario, int pkEmpresa);
        bool[] GuardarInicial(EDBateriaInicial EDBateriaInicial);
        bool[] ActualizarInicial(EDBateriaInicial EDBateriaInicial);
        int NumeroPagina(EDBateriaInicial EDBateriaInicial);
        bool EncuestaCompleta(EDBateriaUsuario EDBateriaUsuario);
        bool TieneExtra(EDBateriaUsuario EDBateriaUsuario);
        void RecibirEditarDocumento(int pkusuario, string cedula);
        bool AceptarEncuesta(int pkusuario);
        string PlantillaCorreo(string nombre);
        void EditarEstadoCorreo(int pkusuario, string plantilla);
        void CambiarEstadosInactivos();

        List<EDRelacionesLaborales> ConsultarConvocadosRol(int rol);
        List<EDBateriaGestion> ConsultarListaGestionFiltros(int IdEmpresa, string Fantes, string Fdespues, int Tipo);
        EDBateriaGestion ConsultarGestionKey(string key, int Form);
        EDBateriaGestion ConsultarGestionKey1(EDBateriaUsuario EDBateriaUsuario);
        bool EmpresaCoincide(string nitempresa, int fkidempresa);
        EDBateriaUsuario ConsultarConvocadoCedula(string cedula, int pkIdgestion);
        EDBateriaUsuario ConsultarConvocadoId(int PkIdUsuario, int FkEmpresa);
        EDBateriaUsuario ConsultarConvocadoId1(int PkIdUsuario, int FkEmpresa);
        List<EDBateriaDimension> ListaDimensiones(int Iddominio, int bateria);
        List<EDBateriaResultado> ListaResultados(int fkUsuario);
    }
}
