
namespace SG_SST.Services.General.Services
{
    using SG_SST.Models.Empresas;
    using SG_SST.Repositories.General.IRepositories;
    using SG_SST.Repositories.General.Repositories;
    using SG_SST.Services.General.IServices;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    public class RecursosServicios : IRecursosServicios
    {
        IRecursosRepositorio recursosRepositorio;
        public RecursosServicios()
        {
            recursosRepositorio = new RecursosRepositorio();
        }

        internal SG_SST.Services.General.IServices.IRecursosServicios IRecursosServicios
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public SG_SST.Repositories.General.Repositories.RecursosRepositorio RecursosRepositorio
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<Municipio> ObtenetMunicipios(int PK_Departamento)
        {
            return recursosRepositorio.ObtenetMunicipios(PK_Departamento);
        }

        public List<Departamento> ObtenerDepartamentos()
        {
            return recursosRepositorio.ObtenerDepartamentos();
        }

        public MemoryStream ExportarAExcel<Exportar>(List<Exportar> datosAExportar)
        {
            
            return recursosRepositorio.ExportarAExcel(datosAExportar);
        }

        public List<SelectListItem> ObtenerPeriodosAnios(int anioInicial, int anioFinal)
        {
            return recursosRepositorio.ObtenerPeriodosAnios(anioInicial, anioFinal);
        }

    }
}