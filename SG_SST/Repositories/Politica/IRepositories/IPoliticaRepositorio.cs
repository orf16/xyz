

namespace SG_SST.Repositories.Politica.IRepositories
{
    using SG_SST.Models.Empresas;
    using SG_SST.Models.Politica;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SG_SST.EntidadesDominio.Planificacion;
    using SG_SST.EntidadesDominio.Usuario;
    using SG_SST.Models.Revision;
    interface IPoliticaRepositorio
    {
        bool GrabarPolitica(mPolitica politica);
        mPolitica ObtenerPolitica(int ID_Empresa);
        ActaRevision ObtenerActa(int ID_Empresa, int idActa);
        string ObtenerPoliticaEmpresa(int ID_Empresa);
        bool ModficarPolitica(mPolitica politica);
        bool ModficarActaRevision(ActaRevision actarevision);
        bool ModficarActaRevisionR(ActaRevision actarevision);
        bool GrabarOtrasInteracciones(Mod_OtrasInteracciones Interacciones);
        bool EliminarOtrasInteracciones(int id);
        bool ModificaOtrasInteracciones(Mod_OtrasInteracciones OtrasInteracciones);
        Mod_OtrasInteracciones ObtenerDocumentoPrivado(int id);
        string ObtenerNombreArchivoOtrasInteracciones(int id);
        string ObtenerOtrasInteraccionesBusqueda(string Palabra_Busqueda);
        bool EliminaPolitica(int ID_Empresa);
        /// <summary>
        /// consulta para saber si esta cargada la firma digital del representante legal de la empresa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Usuario ValidarExisteFirma(int idempresa);

        bool FirmarepLegal(mPolitica politica);


    }
}
