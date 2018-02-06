using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Participacion
{
    public class ActaConvivenciaQuejasVM
    {
        public int PK_Id_Queja { get; set; }
        public int Consecutivo_Queja { get; set; }
        public int Consecutivo_Caso { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }
        public string NombreRefiereSituacion { get; set; }
        public string AspectosNoResueltos { get; set; }
        public string Compromisos { get; set; }
        public int? PK_Id_Acta { get; set; }
        public int? IdSede { get; set; }
        public int Pk_Id_Responsable { get; set; }
        public int Numero_Documento { get; set; }
        public string Nombre { get; set; }
        public int Pk_Id_AccionQueja { get; set; }
        public string AccionARealizar { get; set; }
        public List<ResponsablesQuejasVM> ResponsablesQuejas { get; set; }
        public List<AccionesActaQuejasVM> AccionesActaQuejas { get; set; }
    }

    public class ResponsablesQuejasVM
    {
        public int Pk_Id_Responsable { get; set; }
        public int Numero_Documento { get; set; }
        public string Nombre { get; set; }
        public int Fk_Id_Queja { get; set; }
        public int? PK_Id_Acta { get; set; }
    }
    public class AccionesActaQuejasVM
    {
        public int Pk_Id_AccionQueja { get; set; }
        public string AccionARealizar { get; set; }
        public int Fk_Id_Queja { get; set; }
        public int? PK_Id_Acta { get; set; }
    }

}