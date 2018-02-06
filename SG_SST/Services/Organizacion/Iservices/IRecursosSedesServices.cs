using SG_SST.Models.Organizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Services.Organizacion.Iservices
{
    interface IRecursosSedesServices
    {

        /// <summary>
        /// Definicion del metodo que me retorna los recursos asignados por sedes.
        /// </summary>
        /// <returns></returns>
        List<RecursoporSede> ObtenerRecursoSede(int Pk_Sede, int Periodo);
    }
}
