using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Empleado;



namespace SG_SST.Repositories.Planificacion.IRepositories
{
    interface IPerfilSocioDemograficoRepositorio
    {
        bool GrabarPerfilsocioDemoGrafico(tblEmpleado perfilsoc);


    }
}
