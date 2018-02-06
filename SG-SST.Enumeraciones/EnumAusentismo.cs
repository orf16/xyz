using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Enumeraciones
{
    public class EnumAusentismo
    {
        public enum Contingencias
        {
            EnfermedadGeneral = 1,
            EnfermedadLaboral = 2,
            AccidenteTrabajo = 3,
            LicenciaMaternidad = 4,
            LicenciaPaternidad = 5,
            LicenciaLuto = 7,
            PermisoPorHorasDia = 9
        }
    }
}
