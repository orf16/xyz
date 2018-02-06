using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SG_SST.Models.Planificacion;
namespace SG_SST.Repositories.Planificacion.IRepositories
{
    interface IConsultarZonasRepositorio
    {
        List<Peligro> ConsultarZonasLugares(int idEmpresa);
    }
}