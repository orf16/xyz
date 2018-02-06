
namespace SG_SST.Services.LiderazgoGerencial.Services
{
    using SG_SST.Models.LiderazgoGerencial;
    using SG_SST.Models.Empresas;
    using SG_SST.Repositories.LiderazgoGerencial.IRepositories;
    using SG_SST.Repositories.LiderazgoGerencial.Repositories;
    using SG_SST.Services.LiderazgoGerencial.Iservices;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class RolPorResponsabilidadServicio : IRolPorResponsabilidadServicio
    {
       
    
        IRolPorResponsabilidadRepositorio rolPorResponsabilidadRepositorio;
        public RolPorResponsabilidadServicio()
        {
            rolPorResponsabilidadRepositorio  = new RolPorResponsabilidadRepositorio();
        }

        public bool GuardarRolYResponsabilidades(Rol rol, List<Responsabilidades> responsabilidad, List<RendicionDeCuentas> rendicion, int Pk_Id_Empresa)
        {
            return rolPorResponsabilidadRepositorio.GuardarRolYResponsabilidades(rol, responsabilidad, rendicion, Pk_Id_Empresa);
        }

        public bool EditarRolYResponsabilidades(Rol rol, List<Responsabilidades> responsabilidad, List<RendicionDeCuentas> rendicionDeCuenta, int[] responsaEliminadas, int[] rendiciEliminadas, int Pk_Id_Empresa)
        {
            return rolPorResponsabilidadRepositorio.EditarRolYResponsabilidades(rol, responsabilidad, rendicionDeCuenta, responsaEliminadas, rendiciEliminadas, Pk_Id_Empresa);
        }

        public bool EliminarRolYResponsabilidades(int id)
        {
            return rolPorResponsabilidadRepositorio.EliminarRolYResponsabilidades(id);
        }

        public bool CrearRolYResponsabilidadesPreestablecidos(int id)
        {
            return rolPorResponsabilidadRepositorio.CrearRolYResponsabilidadesPreestablecidos(id);
        }

        public List<Rol> RolesPorEmpresa(int Pk_Id_Empresa)
        {
            return rolPorResponsabilidadRepositorio.RolesPorEmpresa(Pk_Id_Empresa);
        }

        public List<ObligacionesArl> GetObligacionesArl()
        {
            return rolPorResponsabilidadRepositorio.GetObligacionesArl();
        }

        public List<ObligacionesEmpleadores> GetObligacionesEmpleadores()
        {
            return rolPorResponsabilidadRepositorio.GetObligacionesEmpleadores();
        }


    }
}