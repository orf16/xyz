using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.Aplicacion;

namespace SG_SST.Interfaces.Aplicacion
{
    public interface IGestionDelCambio
    {
       
        EDGestionDelCambio GuardarGestionDelCambio(EDGestionDelCambio GestionCambio);
         bool EliminarGestionDelCambio(int idgestiondelcambio);


    }
}
