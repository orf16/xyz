
namespace SG_SST.Services.Empresas.Services
{
    using SG_SST.Models.Empresas;
    using SG_SST.Repositories.Empresas.IRepositories;
    using SG_SST.Repositories.Empresas.Repositories;
    using SG_SST.Services.Empresas.IServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class ProcesoServicios : IProcesoServicios
    {
        IProcesoRepositorio procesoRepositorio;

        public ProcesoServicios()
        {
            procesoRepositorio = new ProcesoRepositorio();
        }

        public List<Proceso> ObtenerProcesosPrincipales(int Pk_Empresa)
        {
            return procesoRepositorio.ObtenerProcesosPrincipales(Pk_Empresa);
        }

        public List<Proceso> ObtenerSubProcesos(int Pk_ProcesoPrincipal)
        {
            return procesoRepositorio.ObtenerSubProcesos(Pk_ProcesoPrincipal);
        }

        public Proceso ObtenerProceso(int Pk_Proceso) 
        {
            return procesoRepositorio.ObtenerProceso(Pk_Proceso);
        
        }
    }
}