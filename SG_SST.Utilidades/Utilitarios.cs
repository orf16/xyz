using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SG_SST.Utilidades
{
    public static class Utilitarios
    {
        public static string ObtenerStrMes(int intMes)

        {
            switch (intMes)
            {
                case 1: return "ENERO";
                case 2: return "FEBRERO";
                case 3: return "MARZO";
                case 4: return "ABRIL";
                case 5: return "MAYO";
                case 6: return "JUNIO";
                case 7: return "JULIO";
                case 8: return "AGOSTO";
                case 9: return "SEPTIEMBRE";
                case 10: return "OCTUBRE";
                case 11: return "NOVIEMBRE";
                case 12: return "DICIEMBRE";
                default: return "";
            }
        }

        public static string ObtenerValorConformato(string valor)
        {
            try
            {
                return Regex.Match(valor, @"(([0-9]?(\.|\,)[1-9])*0*[1-9]*)*").Value;
            }
            catch
            {
                return valor;
            }
        }
    }
}
