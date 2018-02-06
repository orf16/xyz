using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Interfaces.Ausentismo;
using SG_SST.InterfazManager.Ausentismo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Ausentismo
{
    public class LNConfiguracion
    {
        private static IConfiguracion configuracionMg = IMAusentismo.Configuracion();
        private static IAusencia ausenMg = IMAusentismo.Ausencia();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuracion"></param>
        /// <returns></returns>
        public bool GuardarConfiguracion(EDConfiguracion configuracion)
        {
            var resultado = configuracionMg.GuardarConfiguracion(configuracion);
            return resultado;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mes"></param>
        /// <returns></returns>
        /// 
        public decimal AusenciasPorMes(string ano, int mes, int idEmpresa)
        {
            //Basados en el ano y mes construimos las fechas para obtener los ausentimos que iniciaron y finalizadron 
            //en el mimso mes
            List<EDCalculoNHA> ValoresNHA = new List<EDCalculoNHA>();
            decimal HorasDelPeriodo = 0;
           
            DateTime fechaInicial = Convert.ToDateTime(string.Format("{0}/{1}/{2}", ano, mes, 1));
            DateTime fechafin = Convert.ToDateTime(string.Format("{0}/{1}/{2}", fechaInicial.Year, fechaInicial.Month, fechaInicial.AddMonths(1).AddDays(-1).Day));

            int diasHablides = ausenMg.ObtenerDiasLaborablesEmpresa(idEmpresa.ToString());
            if (diasHablides == 1)
                diasHablides = 5;
            else if (diasHablides == 2)
                diasHablides = 6;


            var AusenciasMismoPedido = configuracionMg.ObtieneAusenciasDelMismoPerido(fechaInicial, fechafin, idEmpresa);

            LLenaListaConTrabajadoresYDias(ValoresNHA, AusenciasMismoPedido, true);

            var AusenciasAnteriorPeriodo = configuracionMg.ObtieneAusenciasDesdePeridoAterior(fechaInicial, fechafin, idEmpresa);
            DateTime _fIni = Convert.ToDateTime(string.Format("{0}/{1}/{2}", fechafin.Year, fechafin.Month, 1));
            LLenaListaConTrabajadoresYDiasCrucePeriodos(_fIni, ValoresNHA, AusenciasAnteriorPeriodo, diasHablides, true);

            var AusenciasSiguientePeriodo = configuracionMg.ObtieneAusenciasPeridoConFinMesSiguiente(fechaInicial, fechafin, idEmpresa);
            DateTime _fFin = Convert.ToDateTime(string.Format("{0}/{1}/{2}", fechaInicial.Year, fechaInicial.Month, fechaInicial.AddMonths(1).AddDays(-1).Day));
            LLenaListaConTrabajadoresYDiasCrucePeriodos(_fFin, ValoresNHA, AusenciasSiguientePeriodo, diasHablides, false);

            var AusenciasCrucePeriodos = configuracionMg.ObtieneAusenciasPeriodosAtrasYAdelante (fechaInicial, fechafin, idEmpresa);

            LLenaListaConTrabajadoresYDias(ValoresNHA, AusenciasCrucePeriodos, false);

            foreach (var valHNA in ValoresNHA)
            {
                var horas = valHNA.Dias * 8;
                if (horas > 192)
                    HorasDelPeriodo = HorasDelPeriodo + 192;
                else
                    HorasDelPeriodo = HorasDelPeriodo + horas;
            }


            return HorasDelPeriodo;
        }

        private static void LLenaListaConTrabajadoresYDiasCrucePeriodos(DateTime fechaRef, List<EDCalculoNHA> ValoresNHA, List<EDAusencia> AusenciasAnteriorPeriodo, int diasHablides, bool esMesAtras)
        {
            int dias = 0;
            foreach (var item in AusenciasAnteriorPeriodo)
            {
                LNAusencia use = new LNAusencia();
                if (esMesAtras)
                    dias = use.CalcularDiasLaborales(fechaRef, item.FechaFin, diasHablides, item.IdContingencia);
                else
                    dias = use.CalcularDiasLaborales(item.FechaInicio, fechaRef, diasHablides, item.IdContingencia);

                var va = ValoresNHA.Where(v => v.Documento.Equals(item.Documento)).Select(v => v).FirstOrDefault();
                if (va != null)
                    va.Dias = va.Dias + dias;
                else
                    ValoresNHA.Add(new EDCalculoNHA
                    {
                        Documento = item.Documento,
                        Dias = dias
                    });
            }
        }

        private static void LLenaListaConTrabajadoresYDias(List<EDCalculoNHA> ValoresNHA, List<EDAusencia> Ausencias, bool esMismoPeriodo)
        {
            
                foreach (var item in Ausencias)
                {
                    var va = ValoresNHA.Where(v => v.Documento.Equals(item.Documento)).Select(v => v).FirstOrDefault();
                    if (esMismoPeriodo)
                    {
                        if (va != null)
                            va.Dias = va.Dias + item.DiasAusencia;
                        else
                            ValoresNHA.Add(new EDCalculoNHA
                            {
                                Documento = item.Documento,
                                Dias = item.DiasAusencia
                            });
                    }
                    else
                    {
                        if (va != null)
                            va.Dias = va.Dias + 30; //Se pone 30 por que es el maximo de dias que puede tener un tabajador mensual
                        else
                            ValoresNHA.Add(new EDCalculoNHA
                            {
                                Documento = item.Documento,
                                Dias = 30
                            });
                    }
                }            
        }

        public List<EDConfiguracion> ObtenerConfiguraciones (string NitEmpresa, int ano)
        {
            return configuracionMg.ObtenerConfiguracionesEmpresa(NitEmpresa, ano);
        }

        public bool EliminarConfiguracion(string NitEmpresa, int idConfigurcion)
        {
            return configuracionMg.EliminarConfiguracion(NitEmpresa, idConfigurcion);
        }
    }
}
