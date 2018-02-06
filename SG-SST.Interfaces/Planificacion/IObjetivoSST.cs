using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.Planificacion
{
    public interface IObjetivoSST
    {
        List<EDObjetivoSST> ObtenerObjetivos(int IdEmpresa);
        List<EDObjetivoSST> GuardarObjetivo(EDObjetivoSST Objetivo);
        List<EDObjetivoSST> EliminarObjetivo(List<EDObjetivoSST> Objetivos);
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                