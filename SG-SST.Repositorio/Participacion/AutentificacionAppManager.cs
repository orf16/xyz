using SG_SST.Audotoria;
using SG_SST.EntidadesDominio.Participacion;
using SG_SST.Models;
using SG_SST.Models.Participacion;
using SG_SST.Interfaces.Participacion;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
namespace SG_SST.Repositorio.Participacion
{
    public class AutentificacionAppManager : IAuntentificarApp
    {
        public bool AutentificarAPP(string nitEmpresa, string documentoEmpleado)
        {

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        return true;
                    }
                    catch (Exception ex)
                    {
                      
                        return false;
                    }
                }
            }
        }
    }
}
