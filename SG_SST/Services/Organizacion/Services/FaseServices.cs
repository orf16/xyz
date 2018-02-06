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


    public class FaseServices:IFaseServices
    {
        IFaseRepositorio faseRepositorio;
       
        
        public FaseServices()
        {
            faseRepositorio = new FaseRepositorio();
           
        }

        public bool GuardarFase(Fase fase)
        {
                return faseRepositorio.GuardarFase(fase); 
                
        }

        public List<Fase> ObtenerFase()
        {
            return faseRepositorio.ObtenerFase();
        }

    }
    
}