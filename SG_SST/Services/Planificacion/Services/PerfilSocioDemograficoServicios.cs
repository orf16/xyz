using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SG_SST.Models.Planificacion;
using SG_SST.Repositories.Planificacion.IRepositories;
using SG_SST.Repositories.Planificacion.Repositories;
using SG_SST.Services.Planificacion.IServices;
using SG_SST.Models.Empleado;



namespace SG_SST.Services.Planificacion.Services
{
    public class PerfilSocioDemograficoServicios : IPerfilSocioDemograficoServicios
    {          

        /*
        IPerfilSocioDemograficoRepositorio perfilSocioDemograficoRepositorio;
          public PerfilSocioDemograficoServicios()
        {
            perfilSocioDemograficoRepositorio  = new PerfilSocioDemograficoRepositorio();
        }
        
        */

        PerfilSocioDemograficoRepositorio objPerfilSocioDemografico;

        public PerfilSocioDemograficoServicios() {
            objPerfilSocioDemografico = new PerfilSocioDemograficoRepositorio();
        
        }


        public bool GrabarPerfilSocioDemografico(tblEmpleado perfilsoc) {

            return objPerfilSocioDemografico.GrabarPerfilsocioDemoGrafico(perfilsoc);
        
        }



    }
}