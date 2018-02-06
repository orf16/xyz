using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Planificacion
{
    public interface IEvaluacionInicial
    {
        EDEmpresaEvaluar ConsultaEmpresaEvaluar(EDEmpresaEvaluar Empresa);
        List<EDAspecto> ConsultarAspectosPorEmpresa(EDEmpresaEvaluar Empresa);
        EDEmpresaEvaluar CrearEmpresaEvaluar(EDEmpresaEvaluar Empresa);
        EDAspecto CrearAspecto(EDAspecto Aspecto);
        int CrearEmpresaAspecto(int IdEmpresaEvaluar, int IdAspecto);
        bool CrearCalificacionInicialEmpresa(int IdAspectoEmpresa, decimal valor);
        bool EliminarAspectosEvalEmpresa(EDEmpresaEvaluar Empresa);
        bool EliminarAspecto(EDAspecto Aspecto);
        decimal ObtenerResultadoEvalInivical(EDEmpresaEvaluar Empresa);
        List<EDAspecto> ObtenerAspectosBase();
    }
}
