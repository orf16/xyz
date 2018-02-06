using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.Empresas;
using System.IO;
using SG_SST.EntidadesDominio.Usuario;

namespace SG_SST.Interfaces.Empresas
{
    public interface IRelacionesLaborales
    {
        bool GrabarArchivoRelacionesLaborales(System.Data.DataTable drEmpresas, System.Data.DataTable drEmpleados);

        bool ValidacionCampoNumerico(string valor, out string numero);
        bool ValidacionCampoFecha(string valor, out DateTime fecha);
        bool ValidacionCampoCadena(string valor);
        int ValidacionTipoTercero(string valor);

        int ValidacionTipoOcupacion(int valor);
        int ValidacionTipoDocumento(string valor);
        bool ValidacionEmail(string valor);
        bool ValidaExisteEmpresa(string nit);
        bool ValidaExisteEmpresaEmpleado(string fk_nit, string empleadoDocumento);

        System.Data.DataTable EmpresaTercero();

        System.Data.DataTable EmpleadoTercero();

        void cargarTablas();

        List<EDEmpleadoTercero> ListarTerceroRelLab(string razonSocialnit, string tipoTercero, string DocumentoEmpresa, int pageIndex, int pageSize, out int pageCount);

        List<EDEmpleadoRelLab> ListarEmpleadoRelLab(string estado, string tipoCotizante, string DocumentoEmpresa, int pageIndex, int pageSize, out int pageCount);

        List<EDTipos> ObtenerEstadosEmpleados();

        List<EDTipos> ObtenerTiposCotizantes();

        List<EDTipos> ObtenerTiposTerceros();

        System.Data.DataTable TablaEmpleados();

        System.Data.DataTable DescargaArchivoExcelEmpleado(string nit, string estado, string tipoCotizante);

        System.Data.DataTable DescargaArchivoExcelTerceroRelLab(string nit, string razonSocialNit, string tipoTercero);

        System.Data.DataTable TablaTerceroRelLab();

        List<EDTiposS> DevuelveRazonesSocialesdeTerceros(string NitEmpresaLogueada);
        List<EDTipos> ObtenerTiposInconsistencias();

        EDNotificarInconsistencia GrabarNotificacionInconsistenciaLaborales(EDNotificarInconsistencia notIncon);

        List<EDTiposS> DevuelveCorreoGerente(string razonSocialnit);

        EDParametroSistema ObtenerParametrosSistema(string NombrePlantilla);


    }
}
