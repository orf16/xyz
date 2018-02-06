﻿using SG_SST.EntidadesDominio.Ausentismo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Ausentismo
{
    public interface IContingencia
    {
        IEnumerable<EDContingencia> ObtenerContingencia();
        List<EDContingencia> BuscarContingencia(string prefijo);

        int ValidarContingencia(int idContigencia);
    }
}
