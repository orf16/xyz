using SG_SST.Services.Organizacion.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SG_SST.Repositories.Organizacion.IRepositories;
using SG_SST.Repositories.Organizacion.Repositories;

using SG_SST.Models.Organizacion;

namespace SG_SST.Services.Organizacion.Services
{
    public class TipoRecursoServices:ITiporecursoServices
    {

        ITiporecursoRepositorio TiporecursoRepositorio;

        public TipoRecursoServices()
        {
            TiporecursoRepositorio = new TiporecursoRepositorio();

        }

        public bool GuardarTipoRecurso(TipoRecurso tiporecurso)
        
        {
            return TiporecursoRepositorio.GuardarTipoRecurso(tiporecurso);
            
        }

        public List<TipoRecurso> ObtenerTipoRecurso()
        {

            return TiporecursoRepositorio.ObtenerTipoRecurso();
        }

    }
}