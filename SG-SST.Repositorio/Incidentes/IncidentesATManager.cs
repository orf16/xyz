using SG_SST.EntidadesDominio.Incidentes;
using SG_SST.Interfaces.Incidentes;
using SG_SST.Models;
using SG_SST.Models.Incidentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Incidentes
{
    public class IncidentesATManager : IIncidentesAT
    {

        public EDIncidentesAT GuardarIncidentesAT(EDIncidentesAT incidentesat) 
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        IncidentesAT incidentes = new IncidentesAT()
                        {
                          
                        };

                        //context..Add(empre);
                        //context.SaveChanges();
                        //Transaction.Commit();
                        //empresa.Id_Empresa = empre.Pk_Id_Empresa;

                    }
                    catch (Exception e)
                    {
                        Transaction.Rollback();
                        return incidentesat;
                    }
                }
            }

            return incidentesat;

        }


    }
}
