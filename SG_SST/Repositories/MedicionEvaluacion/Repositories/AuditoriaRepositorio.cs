using SG_SST.Repositories.MedicionEvaluacion.IRepositories;
using SG_SST.Repositories.Empresas.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SG_SST.Models.MedicionEvaluacion;
using SG_SST.Models.Empresas;
using SG_SST.Models.Organizacion;
using SG_SST.Models.Empleado;
using SG_SST.Models;
using System;
using SG_SST.EntidadesDominio.MedicionEvaluacion;
using System.Drawing;
using System.IO;
using System.Data.Entity;
using SG_SST.Repositories.Empresas.Repositories;

namespace SG_SST.Repositories.MedicionEvaluacion.Repositories
{
    public class AuditoriaRepositorio : IAuditoriaRepositorio
    {
        private IProcesoRepositorio ProcesoRepositorio = new ProcesoRepositorio();

        
    }
}