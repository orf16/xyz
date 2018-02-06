using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Empresas
{
    public interface IIncidente
    {
        List<EDIncidente> ConsultarIncidentes(EDIncidente_Modelo_Consulta parametros);

        EDIncidente_Listas_Basicas ObtenerListasBasicas(string nitEmpresa);

        EDIncidente ObtenerNuevoIncidente(string identificacionEmpresa, string identificacionUsuario);

        EDIncidente GuardarIncidente(EDIncidente incidente);
        EDIncidente ConsultarIncidente(EDIncidente_Modelo_Consulta parametros);
    }
}
