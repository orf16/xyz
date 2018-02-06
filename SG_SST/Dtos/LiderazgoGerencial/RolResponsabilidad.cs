using System;
using SG_SST.Models.Empresas;
using SG_SST.Models.LiderazgoGerencial;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Dtos.LiderazgoGerencial
{
    public class RolResponsabilidad
    {
        public List<Rol> RolesResponsabilidad
        {
            get;
            set;
        }
        public List<ObligacionesArl> ObligacionesArlRol
        {
            get;
            set;
        }
        public  List<ObligacionesEmpleadores> ObligacionesEmpleadoresRol
        {
            get;
            set;
        }
    }
}
