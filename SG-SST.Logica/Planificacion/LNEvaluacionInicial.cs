using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Planificacion;
using SG_SST.InterfazManager.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Planificacion
{
    public class LNEvaluacionInicial
    {
        private static IEvaluacionInicial ev = IMEvaluaccion.EmpresaEvaluar();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Empresa"></param>
        /// <returns></returns>
        public EDEmpresaEvaluar CrearEmpresaEvaluar(EDEmpresaEvaluar Empresa)
        {
            bool resultEliminar = false;
            bool resultCrear = false;

            //Verifica la existencia de la empresa
            EDEmpresaEvaluar _empresa = ev.ConsultaEmpresaEvaluar(Empresa);
            if (_empresa.IdEmpresaEvaluar > 0)
            {
                resultEliminar = ev.EliminarAspectosEvalEmpresa(_empresa);
                if (resultEliminar)
                    resultCrear = CrearAspectos(_empresa);
            }
            else
            {
                _empresa = ev.CrearEmpresaEvaluar(Empresa);
                if (_empresa != null)
                    resultCrear = CrearAspectos(_empresa);
            }
            _empresa.CalificacionFinal = ev.ObtenerResultadoEvalInivical(_empresa);
            return _empresa;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        //public EDEmpresaEvaluar ConsultarEvaluacionInicialPorEmpresa(string NitEmpresa)
        //{
        //    EDEmpresaEvaluar empresaEvaluar = new EDEmpresaEvaluar();
        //    empresaEvaluar.Nit = NitEmpresa;
        //    empresaEvaluar = ev.ConsultaEmpresaEvaluar(empresaEvaluar);
        //    empresaEvaluar.Aspectos = ev.ConsultarAspectosPorEmpresa(empresaEvaluar);
        //    empresaEvaluar.CalificacionFinal = ev.ObtenerResultadoEvalInivical(empresaEvaluar);
        //    return empresaEvaluar;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_empresa"></param>
        /// <returns></returns>
        private bool CrearAspectos(EDEmpresaEvaluar _empresa)
        {
            bool result = false;
            decimal ValorValorizacion = 0;
            foreach (EDAspecto EDAsp in _empresa.Aspectos)
            {
                EDAspecto _edasp = ev.CrearAspecto(EDAsp);
                if (_edasp != null)
                {
                    int idEmpresaAspecto = ev.CrearEmpresaAspecto(_empresa.IdEmpresaEvaluar, _edasp.IdAspecto);
                    if (idEmpresaAspecto > 0)
                    {
                        ValorValorizacion = CalcularValorValorizacion(EDAsp.IdValorizacion, _empresa.Aspectos);
                        bool resultInsert = ev.CrearCalificacionInicialEmpresa(idEmpresaAspecto, ValorValorizacion);
                        if (!resultInsert)
                        {
                            ev.ConsultaEmpresaEvaluar(_empresa);
                            result = false;
                            break;
                        }
                        else
                            result = true;
                    }
                    else
                    {
                        ev.EliminarAspecto(_edasp);
                        ev.ConsultaEmpresaEvaluar(_empresa);
                        result = false;
                        break;
                    }
                }
                else
                {
                    ev.ConsultaEmpresaEvaluar(_empresa);
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idValorizacion"></param>
        /// <param name="aspectos"></param>
        /// <returns></returns>
        private decimal CalcularValorValorizacion(int idValorizacion, List<EDAspecto> aspectos)
        {
            decimal valor = 0;
            switch (idValorizacion)
            {
                case 1:
                    valor = 100 / Convert.ToDecimal(aspectos.Count);
                    break;
                case 2:
                    valor = 0;
                    break;
                case 3:
                    valor = (100 / Convert.ToDecimal(aspectos.Count)) / 2; ;
                    break;
                default:
                    valor = 0;
                    break;
            }
            return valor;
        }

        public List<EDAspecto> ObtenerAspectosBase()
        {
            return ev.ObtenerAspectosBase();           
        }
    }
}
