
namespace SG_SST.Services.Empresas.Services
{
    using SG_SST.Models;
    using SG_SST.Models.Empresas;
    using SG_SST.Repositories.Empresas.IRepositories;
    using SG_SST.Repositories.Empresas.Repositories;
    using SG_SST.Services.Empresas.IServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class SedeServicios : ISedeServicios
    {
        ISedeRepositorio sedeRepositorio;

        public SedeServicios()
        {
            sedeRepositorio = new SedeRepositorio();
        }

        public SedeServicios(SG_SSTContext db)
        {
            sedeRepositorio = new SedeRepositorio(db);
        }

        public SedeMunicipio ObtenerSedePorMunicipio(int Pk_Sede) 
        {
            return sedeRepositorio.ObtenerSedePorMunicipio(Pk_Sede);
        }

        public List<Sede> SedesPorEmpresa(int Pk_Id_Empresa) 
        {
            return sedeRepositorio.SedesPorEmpresa(Pk_Id_Empresa);
        }

        public List<SedeMunicipio> SedesMunicipioPorEmpresa(int Pk_Id_Empresa) 
        {
            return sedeRepositorio.SedesMunicipioPorEmpresa(Pk_Id_Empresa);
        }
    }
}