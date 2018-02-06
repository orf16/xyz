using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.Planificacion;

namespace SG_SST.Interfaces.Planificacion
{
    public interface IClasificacionPeligros
    {
        List<EDClasificacionDePeligro> ObtenerPeligrosConTiposPeligros();
    }
}
