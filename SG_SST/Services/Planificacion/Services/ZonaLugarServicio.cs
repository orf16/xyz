using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Services.Planificacion.Services
{
      using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using SG_SST.Repositories.Planificacion.Repositories;
    using SG_SST.Services.Planificacion.IServices;
    using System.Collections.Generic;

    public class ZonaLugarServicio: IZonaLugar
    {
         IConsultarZonasRepositorio clasesDeZonas;
        public ZonaLugarServicio()
        {
            clasesDeZonas = new ZonaLugarRepositorio();
        }

        internal SG_SST.Services.Planificacion.IServices.IZonaLugar IzonaLugar
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public List<Peligro> ConsultarZonasLugares(int idEmpresa)
        {
            return clasesDeZonas.ConsultarZonasLugares(idEmpresa);
         
        }
    }
    }
