using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Models.Politica;
using SG_SST.Models.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.EntidadesDominio.Usuario;
using SG_SST.Models.Revision;


namespace SG_SST.Services.Politica.IServices
{
    interface IpoliticaServicios
    {
        bool GrabarPolitica(mPolitica politica);

        bool GrabarPoliticaEmpresa(string politica, int nit, bool politicaFirma_Rep);

        // void CargarPolitica(mPolitica politica);

        string ObtenerPolitica(int nit);

        string ValidaExistePolitica(int nit);

        bool GrabarOtrasInteracciones(Mod_OtrasInteracciones OtrasInteracciones);

        //bool ModificarOtrasInteracciones(Mod_OtrasInteracciones OtrasInteracciones);

        bool ModificarDocumentoPrivado(int id);

        bool EliminarOtrasInteracciones(int id);

        /// <summary>
        /// Método para modificar el nombre del archivo subido - otras interacciones y directrices
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ModificarNombreOtrasInteracciones(int id, string Nombre);

        string ObtenerNombreArchivootrasInteracciones(int id);

        string ObtenerOtrasInteraccionesBusqueda(string Palabra_Busqueda);

        bool EliminarPolitica(int ID_Empresa);

        Usuario ValidarExisteFirma(int idempresa);

        bool ObtenerGuardar_Estadofirma(mPolitica objpolitic);

        bool ObtenerGuardar_Estadofirmas(ActaRevision acta);
        bool ObtenerGuardar_EstadofirmasR(ActaRevision acta);
        mPolitica validarestadofirma(int idEmpresa);
    }
}
