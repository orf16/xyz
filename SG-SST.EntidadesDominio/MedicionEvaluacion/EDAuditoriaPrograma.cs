using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.MedicionEvaluacion
{
    public class EDAuditoriaPrograma
    {
        public int Pk_Id_Programa { get; set; }
        public string Titulo { get; set; }
        public string Objetivo { get; set; }
        public string Alcance { get; set; }
        public string Metodologia { get; set; }
        public string Competencia { get; set; }
        public string Recursos { get; set; }
        public DateTime Fecha_Programacion { get; set; }
        public int Año { get; set; }
        public string  Periodicidad { get; set; }

        //Campos de Responsable
        public string NombreArchivoRes { get; set; }
        public string RutaArchivoRes { get; set; }
        public string Nombre_Responsable { get; set; }
        public string Numero_Id_Responsable { get; set; }       
        public string FirmaScrImageRes { set; get; }

        //Campos de Copasst
        public string NombreArchivoCopasst { get; set; }
        public string RutaArchivoPres { get; set; }
        public string Nombre_Copasst { get; set; }
        public string Numero_Id_Copasst { get; set; }      
        public string FirmaScrImagePres { set; get; }

        //FK Empresa
        public int Fk_Id_Empresa { get; set; }
        //FK Sede
        public string SedeAuditoria { get; set; }
        public int Fk_Id_Sede { get; set; }
    }
}
