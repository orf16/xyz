using SG_SST.Models.Organizacion;
using SG_SST.Repositories.Organizacion.IRepositories;
using SG_SST.Repositories.Organizacion.Repositories;
using SG_SST.Services.Organizacion.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Services.Organizacion.Services
{
    public class RecursoSedesServices:IRecursosSedesServices
    {
        IRecursosSedesRepositorio recursoporsede;


        public RecursoSedesServices()
        {

            recursoporsede = new RecursosSedesRepositorio();
        }

        public List<RecursoporSede> ObtenerRecursoSede(int Pk_Sede, int Periodo) 
        {
            return recursoporsede.ObtenerRecursoSede(Pk_Sede, Periodo);
        }
        
    }
}