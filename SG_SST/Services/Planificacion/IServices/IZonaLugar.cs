using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Services.Planificacion.IServices
{
    using SG_SST.Models.Planificacion;
    using System.Collections.Generic;
    interface IZonaLugar
    {
        List<Peligro> ConsultarZonasLugares(int idEmpresa);
    }
}