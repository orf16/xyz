using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Reportes
{
    public  class RecursoParametros
    {
        private static string reportPath;

        public static string ReportPath
        {
            get { return reportPath; }
            set { reportPath = value; }
        }

        private static string nombreReporte = "";

        public static string NombreReporte
        {
            get { return nombreReporte; }
            set { nombreReporte = value; }
        }
        private static string reporte = "";

        public static string Reporte
        {
            get { return reporte; }
            set { reporte = value; }
        }

        private static string nitempresa = "";
        public static string NitEmpresa
        {
            get { return nitempresa; }
            set { nitempresa = value; }
        }

        private static int anio ;
        public static int Anio
        {
            get { return anio; }
            set { anio = value; }
        }

        private static int? origen = null;
        public static int? Origen
        {
            get { return origen; }
            set { origen = value; }
        }

        private static int? empresausuaria = null;
        public static int? EmpresaUsuaria
        {
            get { return empresausuaria; }
            set { empresausuaria = value; }
        }

        private static int? sede = null;
        public static int? Sede
        {
            get { return sede; }
            set { sede = value; }
        }

        private static int sedeInd;
        public static int SedeInd
        {
            get { return sedeInd; }
            set { sedeInd = value; }
        }
        private  static int? departamento = null;
        public static int? Departamento
        {
            get { return departamento; }
            set { departamento = value; }
        }
        private static string constanteK = "";
        public static string ConstanteK
        {
            get { return constanteK; }
            set { constanteK = value; }
        }
        private static int contigencia;

        public static int Contigencia
        {
            get { return contigencia; }
            set { contigencia = value; }
        }

        private static string contigenciaTexto = "";
        public static string ContigenciaTexto
        {
            get { return contigenciaTexto; }
            set { contigenciaTexto = value; }
        }

        private static string sedeTexto = "";
        public static string SedeTexto
        {
            get { return sedeTexto; }
            set { sedeTexto = value; }
        }

        private static int? tipoReporte=null;
        public static int? TipoReporte
        {
            get { return tipoReporte; }
            set { tipoReporte = value; }
        }

        private static string estado = "";

        public static string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        private static int anioComparacion;
        public static int AnioComparacion
        {
            get { return anioComparacion; }
            set { anioComparacion = value; }
        }

        private static int? proceso = null;
        public static int? Proceso
        {
            get { return proceso; }
            set { proceso = value; }
        }

        private static string procesoTexto = "";

        public static string ProcesoTexto
        {
            get { return procesoTexto; }
            set { procesoTexto = value; }
        }
   }
}