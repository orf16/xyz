using System;
using SG_SST.Models.Empresas;
using SG_SST.Models.LiderazgoGerencial;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Dtos.LiderazgoGerencial
{
    public class ActaRoles
    {
        public Rol RolesResponsabilidadResponsable
        {
            get;
            set;
        }
        public Rol RolesResponsabilidadRepresentante
        {
            get;
            set;
        }
    }
}