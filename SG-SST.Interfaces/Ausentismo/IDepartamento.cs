using SG_SST.EntidadesDominio.Ausentismo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Ausentismo
{
    public interface IDepartamento
    {
        IEnumerable<EDDepartamento> ObtenerDepartamento();
        int ValidarDepartamento(int idDepartamento);
    }
}
