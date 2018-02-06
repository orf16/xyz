using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.MedicionEvaluacion
{
    public class EDAuditoriaInforme
    {
        // De Programa de Auditorias
        public EDAuditoriaPrograma Informe_EDAuditoriaPrograma { get; set; }
        // De Plan de Auditoria
        public EDAuditoria Informe_EDAuditoria { get; set; }
        // De Lista de Verificación
        public List<EDAuditoriaVerificacion> ListaVerficiacionInforme { get; set; }
        // De Lista de Verificación:Cuadro Compromiso
        public List<EDAuditoriaVerificacion> ListaCuadroCompromiso { get; set; }
        // De Actividades Auditoria
        public List<EDActividadAuditoria> ListaActividadesInforme { get; set; }
        public string NombreEmpresa { get; set; }
        public string NitEmpresa { get; set; }
        public string RutaPres { get; set; }
        public string RutaRes { get; set; }
        public string Conclusiones { get; set; }
        //Campos de Responsable proceso
        public string NombreArchivoRes { get; set; }
        public string RutaArchivoRes { get; set; }
        public string FirmaScrImageRes { get; set; }
        public string Nombre_Responsable { get; set; }
        public string Numero_Id_Responsable { get; set; }
        //Campos del Auditor
        public string NombreArchivoAuditor { get; set; }
        public string RutaArchivoAuditor { get; set; }
        public string FirmaScrImageAuditor { get; set; }
        public string Nombre_Auditor { get; set; }
        public string Numero_Id_Auditor { get; set; }

        public int Pk_Id_Auditoria { get; set; }

    }
}
