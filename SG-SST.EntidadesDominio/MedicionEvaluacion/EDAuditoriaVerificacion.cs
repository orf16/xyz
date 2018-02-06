using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.MedicionEvaluacion
{
    public class EDAuditoriaVerificacion
    {    
        public int Pk_Id_Lista_Verificacion { get; set; }
        public string Pregunta { get; set; }
        public string Requisito { get; set; }
        public string Hallazgo { get; set; }
        public string Tipo_Hallazgo { get; set; }
        public int Fk_Id_Auditoria { get; set; }

        public string Compromiso { get; set; }
        public string Responsable { get; set; }
        public DateTime FechaCierre { get; set; }

        public List<EDAuditoriaVerificacion> ListaVerficiacionLista { get; set; }


    }
}
