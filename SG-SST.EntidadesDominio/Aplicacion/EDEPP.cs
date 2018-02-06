using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using System.Collections.Generic;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDEPP
    {
        public int Pk_Id_EPP { get; set; }
        public string NombreEPP { get; set; }
        public string ParteCuerpo { get; set; }
        public string EspecificacionTecnica { get; set; }
        public string Uso { get; set; }
        public string Mantenimiento { get; set; }
        public string VidaUtil { get; set; }
        public string Reposicion { get; set; }
        public string DisposicionFinal { get; set; }
        public string ArchivoImagen { get; set; }
        public string ArchivoImagen_download { get; set; }
        public string RutaAbsolutaImagen { get; set; }
        public string RutaImage { get; set; }
        public string NombreArchivo { get; set; }
        public string NombreArchivo_download { get; set; }
        public string RutaArchivo { get; set; }
        public int Fk_Id_Clasificacion_De_Peligro { get; set; }
        public string Clasificacion_De_Peligro { get; set; }
        public int Fk_Id_Empresa { get; set; }
        public string Cantidad { get; set; }
        public List<EDEPPCargo> Cargos { get; set; }
        public List<EDEPPSuministroEPP> EPPSuministroEPPs { get; set; }

    }
}
