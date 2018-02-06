
namespace SG_SST.Services.Empresas.IServices
{
    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    interface IProcesoServicios
    {
        /// <summary>
        /// Defincion del metodo que me retorna los procesos principales
        /// </summary>
        /// <returns><Lista de procesos /returns>
        List<Proceso> ObtenerProcesosPrincipales(int Pk_Empresa);


        /// <summary>
        /// Defincion del metodo que me retorna los subprocesos por procesos
        /// </summary>
        /// <returns><Lista de procesos /returns>
        List<Proceso> ObtenerSubProcesos(int Pk_ProcesoPrincipal);

        /// <summary>
        /// Definición del metodo que retorna un proceso buscandolo por su clave primaria o id
        /// </summary>
        /// <param name="Pk_Proceso">clave primaria</param>
        /// <returns></returns>
        Proceso ObtenerProceso(int Pk_Proceso);
    }
}
