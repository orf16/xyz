using OfficeOpenXml;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Empresas;
using SG_SST.InterfazManager.Empresas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Empresas
{
    public class LNIncidente
    {
        private static IIncidente ii = IMIncidente.Incidente();

        /// <summary>
        /// Realiza una consulta de incidentes.
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public List<EDIncidente> ConsultarIncidentes(EDIncidente_Modelo_Consulta parametros)
        {
            return ii.ConsultarIncidentes(parametros);
        }

        public EDIncidente ConsultarIncidente(EDIncidente_Modelo_Consulta parametros)
        {
            return ii.ConsultarIncidente(parametros);
        }

        public byte[] ObtenerReporteIncidentesExcel(EDIncidente_Modelo_Consulta parametros)
        {
            var incidentes = ii.ConsultarIncidentes(parametros);


            ExcelPackage excel = new ExcelPackage();

            excel.Workbook.Worksheets.Add("Incidentes");

            ExcelWorksheet hoja = excel.Workbook.Worksheets[1];

            hoja.Cells["A1:AY1"].Merge = true;
            hoja.Cells["A1"].Value = "REPORTE DE INCIDENTES ";
            hoja.Cells["A1"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            hoja.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            hoja.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            hoja.Cells["A1"].Style.Font.Bold = true;
            hoja.Cells["A1"].Style.WrapText = true;

            hoja.Cells["A2"].Value = "NOMBRE DE LA ACTIVIDAD ECONÓMICA (SEDE PRINCIPAL)";
            hoja.Cells["A2"].Style.Font.Bold = true;
            hoja.Cells["B2"].Value = "CÓDIGO";
            hoja.Cells["B2"].Style.Font.Bold = true;
            hoja.Cells["C2"].Value = "NOMBRE O RAZÓN SOCIAL";
            hoja.Cells["C2"].Style.Font.Bold = true;
            hoja.Cells["D2"].Value = "TIPO DE IDENTIFICACIÓN";
            hoja.Cells["D2"].Style.Font.Bold = true;
            hoja.Cells["E2"].Value = "Nº (número de identificación)";
            hoja.Cells["E2"].Style.Font.Bold = true;
            hoja.Cells["F2"].Value = "DIRECCIÓN";
            hoja.Cells["F2"].Style.Font.Bold = true;
            hoja.Cells["G2"].Value = "TELÉFONO";
            hoja.Cells["G2"].Style.Font.Bold = true;
            hoja.Cells["H2"].Value = "CORREO ELECTRÓNICO (MAIL)";
            hoja.Cells["H2"].Style.Font.Bold = true;
            hoja.Cells["I2"].Value = "DEPARTAMENTO";
            hoja.Cells["I2"].Style.Font.Bold = true;
            hoja.Cells["J2"].Value = "MUNICIPIO";
            hoja.Cells["J2"].Style.Font.Bold = true;
            hoja.Cells["K2"].Value = "ZONA";
            hoja.Cells["K2"].Style.Font.Bold = true;
            hoja.Cells["L2"].Value = "SEDE";
            hoja.Cells["L2"].Style.Font.Bold = true;
            hoja.Cells["M2"].Value = "DIRECCIÓN";
            hoja.Cells["M2"].Style.Font.Bold = true;
            hoja.Cells["N2"].Value = "TELÉFONO";
            hoja.Cells["N2"].Style.Font.Bold = true;
            hoja.Cells["O2"].Value = "DEPARTAMENTO";
            hoja.Cells["O2"].Style.Font.Bold = true;
            hoja.Cells["P2"].Value = "MUNICIPIO";
            hoja.Cells["P2"].Style.Font.Bold = true;
            hoja.Cells["Q2"].Value = "ZONA";
            hoja.Cells["Q2"].Style.Font.Bold = true;
            hoja.Cells["R2"].Value = "TIPO DE VINCULACIÓN LABORAL";
            hoja.Cells["R2"].Style.Font.Bold = true;
            hoja.Cells["S2"].Value = "TIPO DE IDENTIFICACIÓN";
            hoja.Cells["S2"].Style.Font.Bold = true;
            hoja.Cells["T2"].Value = "Nº (número de identificación)";
            hoja.Cells["T2"].Style.Font.Bold = true;
            hoja.Cells["U2"].Value = "PRIMER APELLIDO";
            hoja.Cells["U2"].Style.Font.Bold = true;
            hoja.Cells["V2"].Value = "SEGUNDO APELLIDO";
            hoja.Cells["V2"].Style.Font.Bold = true;
            hoja.Cells["W2"].Value = "PRIMER NOMBRE";
            hoja.Cells["W2"].Style.Font.Bold = true;
            hoja.Cells["X2"].Value = "SEGUNDO NOMBRE";
            hoja.Cells["X2"].Style.Font.Bold = true;   
            hoja.Cells["Y2"].Value = "FECHA DE NACIMIENTO";
            hoja.Cells["Y2"].Style.Font.Bold = true;
            hoja.Cells["Z2"].Value = "GÉNERO";
            hoja.Cells["Z2"].Style.Font.Bold = true;
            hoja.Cells["AA2"].Value = "DIRECCIÓN";
            hoja.Cells["AA2"].Style.Font.Bold = true;
            hoja.Cells["AB2"].Value = "TELÉFONO";
            hoja.Cells["AB2"].Style.Font.Bold = true;
            hoja.Cells["AC2"].Value = "DEPARTAMENTO";
            hoja.Cells["AC2"].Style.Font.Bold = true;
            hoja.Cells["AD2"].Value = "MUNICIPIO";
            hoja.Cells["AD2"].Style.Font.Bold = true;
            hoja.Cells["AE2"].Value = "ZONA";
            hoja.Cells["AE2"].Style.Font.Bold = true;
            hoja.Cells["AF2"].Value = "OCUPACIÓN HABITUAL";
            hoja.Cells["AF2"].Style.Font.Bold = true;
            hoja.Cells["AG2"].Value = "FECHA DE INGRESO A LA EMPRESA";
            hoja.Cells["AG2"].Style.Font.Bold = true;
            hoja.Cells["AH2"].Value = "JORNADA DE TRABAJO HABITUAL";
            hoja.Cells["AH2"].Style.Font.Bold = true;
            hoja.Cells["AI2"].Value = "FECHA DEL INCIDENTE";
            hoja.Cells["AI2"].Style.Font.Bold = true;
            hoja.Cells["AJ2"].Value = "HORA DEL INCIDENTE";
            hoja.Cells["AJ2"].Style.Font.Bold = true;
            hoja.Cells["AK2"].Value = "DÍA DE LA SEMANA EN EL QUE OCURRIÓ EL INCIDENTE";
            hoja.Cells["AK2"].Style.Font.Bold = true;
            hoja.Cells["AL2"].Value = "JORNADA EN QUE SUCEDE";
            hoja.Cells["AL2"].Style.Font.Bold = true;
            hoja.Cells["AM2"].Value = "¿ESTABA REALIZANDO SU LABOR HABITUAL?";
            hoja.Cells["AM2"].Style.Font.Bold = true;
            hoja.Cells["AN2"].Value = "¿CUAL?";
            hoja.Cells["AN2"].Style.Font.Bold = true;
            hoja.Cells["AO2"].Value = "TOTAL TIEMPO LABORADO PREVIO AL INCIDENTE";
            hoja.Cells["AO2"].Style.Font.Bold = true;
            hoja.Cells["AP2"].Value = "TIPO DE INCIDENTE";
            hoja.Cells["AP2"].Style.Font.Bold = true;
            hoja.Cells["AQ2"].Value = "DEPARTAMENTO DEL INCIDENTE";
            hoja.Cells["AQ2"].Style.Font.Bold = true;
            hoja.Cells["AR2"].Value = "MUNICIPIO DEL INCIDENTE";
            hoja.Cells["AR2"].Style.Font.Bold = true;
            hoja.Cells["AS2"].Value = "ZONA";
            hoja.Cells["AS2"].Style.Font.Bold = true;
            hoja.Cells["AT2"].Value = "LUGAR DONDE OCURRIÓ EL INCIDENTE";
            hoja.Cells["AT2"].Style.Font.Bold = true;
            hoja.Cells["AU2"].Value = "INDIQUE CUAL SITIO";
            hoja.Cells["AU2"].Style.Font.Bold = true;
            hoja.Cells["AV2"].Value = "OTRO (Especifique)";
            hoja.Cells["AV2"].Style.Font.Bold = true;
            hoja.Cells["AW2"].Value = "POSIBLE CONSECUENCIA";
            hoja.Cells["AW2"].Style.Font.Bold = true;
            hoja.Cells["AX2"].Value = "DESCRIPCIÓN DEL INCIDENTE";
            hoja.Cells["AX2"].Style.Font.Bold = true;
            hoja.Cells["AY2"].Value = "FECHA DE DILIGENCIAMIENTO DEL INCIDENTE";
            hoja.Cells["AY2"].Style.Font.Bold = true;

            int nunInicial = 3;

            foreach (var dato in incidentes)
            {
                hoja.Cells[string.Format("A{0}", nunInicial)].Value = dato.General_actividad_economica;
                hoja.Cells[string.Format("A{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("B{0}", nunInicial)].Value = dato.General_codigo;
                hoja.Cells[string.Format("B{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("C{0}", nunInicial)].Value = dato.General_razon_social;
                hoja.Cells[string.Format("C{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("D{0}", nunInicial)].Value = dato.General_tipo_documento.Sigla;
                hoja.Cells[string.Format("D{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("E{0}", nunInicial)].Value = dato.General_numero_identificación;
                hoja.Cells[string.Format("E{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("F{0}", nunInicial)].Value = dato.General_sede_principal_direccion;
                hoja.Cells[string.Format("F{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("G{0}", nunInicial)].Value = dato.General_sede_principal_telefono;
                hoja.Cells[string.Format("G{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("H{0}", nunInicial)].Value = dato.General_correo_electronico;
                hoja.Cells[string.Format("H{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("I{0}", nunInicial)].Value = dato.General_sede_principal_departamento;
                hoja.Cells[string.Format("I{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("J{0}", nunInicial)].Value = dato.General_sede_principal_municipio;
                hoja.Cells[string.Format("J{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("K{0}", nunInicial)].Value = dato.General_sede_principal_zona.Equals("U") ? "Urbano" : "Rural";

                hoja.Cells[string.Format("K{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("L{0}", nunInicial)].Value = dato.General_sede.NombreSede == null ? "" : dato.General_sede.NombreSede;
                hoja.Cells[string.Format("L{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("M{0}", nunInicial)].Value = dato.General_sede.DireccionSede == null ? "" : dato.General_sede.DireccionSede;
                hoja.Cells[string.Format("M{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("N{0}", nunInicial)].Value = dato.General_sede.Telefono == null ? "" : dato.General_sede.Telefono;
                hoja.Cells[string.Format("N{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("O{0}", nunInicial)].Value = dato.General_sede.NombreDepto == null ? "" : dato.General_sede.NombreDepto;
                hoja.Cells[string.Format("O{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("P{0}", nunInicial)].Value = dato.General_sede.NombreMunici == null ? "" : dato.General_sede.NombreMunici;
                hoja.Cells[string.Format("P{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("Q{0}", nunInicial)].Value = dato.General_sede.Sector == null ? "" : dato.General_sede.Sector;
                hoja.Cells[string.Format("Q{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("R{0}", nunInicial)].Value = dato.Persona_vinculacion_laboral.Descripcion_VinculacionLaboral;
                hoja.Cells[string.Format("R{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("S{0}", nunInicial)].Value = dato.Persona_tipo_documento.Sigla;
                hoja.Cells[string.Format("S{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("T{0}", nunInicial)].Value = dato.Persona_numero_identificacion;
                hoja.Cells[string.Format("T{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("U{0}", nunInicial)].Value = dato.Persona_primer_apellido;
                hoja.Cells[string.Format("U{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("V{0}", nunInicial)].Value = dato.Persona_segundo_apellido;
                hoja.Cells[string.Format("V{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("W{0}", nunInicial)].Value = dato.Persona_primer_nombre;
                hoja.Cells[string.Format("W{0}", nunInicial)].Style.WrapText = true;               
                hoja.Cells[string.Format("X{0}", nunInicial)].Value = dato.Persona_segundo_nombre;
                hoja.Cells[string.Format("X{0}", nunInicial)].Style.WrapText = true;                          
                hoja.Cells[string.Format("Y{0}", nunInicial)].Value = string.Format("{0}/{1}/{2}", dato.Persona_fecha_nacimiento.Day, dato.Persona_fecha_nacimiento.Month, dato.Persona_fecha_nacimiento.Year);
                hoja.Cells[string.Format("Y{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("Z{0}", nunInicial)].Value = dato.Persona_genero.Equals("F") ? "Femenino" : "Masculino";
                hoja.Cells[string.Format("Z{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AA{0}", nunInicial)].Value = dato.Persona_direccion;
                hoja.Cells[string.Format("AA{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AB{0}", nunInicial)].Value = dato.Persona_telefono;
                hoja.Cells[string.Format("AB{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AC{0}", nunInicial)].Value = dato.Persona_departamento;
                hoja.Cells[string.Format("AC{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AD{0}", nunInicial)].Value = dato.Persona_municipio;
                hoja.Cells[string.Format("AD{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AE{0}", nunInicial)].Value = dato.Persona_zona.Descripcion_ZonaLugar.Equals("U") ? "Urbano" : "Rural";
                hoja.Cells[string.Format("AE{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AF{0}", nunInicial)].Value = dato.Persona_ocupacion_habitual;
                hoja.Cells[string.Format("AF{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AG{0}", nunInicial)].Value = string.Format("{0}/{1}/{2}", dato.Persona_fecha_ingreso_empresa.Day, dato.Persona_fecha_ingreso_empresa.Month, dato.Persona_fecha_ingreso_empresa.Year);
                hoja.Cells[string.Format("AG{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AH{0}", nunInicial)].Value = dato.Persona_tipo_jornada.Nombre_Jornada;
                hoja.Cells[string.Format("AH{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AI{0}", nunInicial)].Value = string.Format("{0}/{1}/{2}", dato.Incidente_fecha.Day, dato.Incidente_fecha.Month, dato.Incidente_fecha.Year);
                hoja.Cells[string.Format("AI{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AJ{0}", nunInicial)].Value = string.Format("{0}:{1}", dato.Incidente_fecha.Hour < 10 ? "0" + dato.Incidente_fecha.Hour.ToString() : dato.Incidente_fecha.Hour.ToString(), dato.Incidente_fecha.Minute < 10 ? "0" + dato.Incidente_fecha.Minute.ToString() : dato.Incidente_fecha.Minute.ToString ());
                hoja.Cells[string.Format("AJ{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AK{0}", nunInicial)].Value = obtenerdia(dato.Incidente_dia_semana);
                hoja.Cells[string.Format("AK{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AL{0}", nunInicial)].Value = dato.Incidente_jornada_normal ? "Normal" : "Extra";
                hoja.Cells[string.Format("AL{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AM{0}", nunInicial)].Value = dato.Incidente_realizaba_labor_habitual ? "SI" : "NO";
                hoja.Cells[string.Format("AM{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AN{0}", nunInicial)].Value = dato.Incidente_nombre_labor;
                hoja.Cells[string.Format("AN{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AO{0}", nunInicial)].Value = dato.Incidente_tiempo_previo_al_incidente_HHMM;
                hoja.Cells[string.Format("AO{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AP{0}", nunInicial)].Value = dato.Incidente_tipo_incidente.Nombre_Incidente;
                hoja.Cells[string.Format("AP{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AQ{0}", nunInicial)].Value = dato.Incidente_departamento.Nombre_Departamento;
                hoja.Cells[string.Format("AQ{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AR{0}", nunInicial)].Value = dato.Incidente_municipio.NombreMunicipio;
                hoja.Cells[string.Format("AR{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AS{0}", nunInicial)].Value = dato.Incidente_zona_incidente.Descripcion_ZonaLugar.Equals("U") ? "Urbano" : "Rural";
                hoja.Cells[string.Format("AS{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AT{0}", nunInicial)].Value = dato.Incidente_ocurre_dentro_empresa ? "DENTRO DE LA EMPRESA" : "FUERA DE LA EMPRESA";
                hoja.Cells[string.Format("AT{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AU{0}", nunInicial)].Value = dato.Incidente_sitio_incidente.Nombre_Sitio;
                hoja.Cells[string.Format("AU{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AV{0}", nunInicial)].Value = dato.Incidente_sitio_incidente_otro;
                hoja.Cells[string.Format("AV{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AW{0}", nunInicial)].Value = dato.Incidente_consecuencia.Nombre_consecuencia;
                hoja.Cells[string.Format("AW{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AX{0}", nunInicial)].Value = dato.Incidente_descripcion;
                hoja.Cells[string.Format("AX{0}", nunInicial)].Style.WrapText = true;
                hoja.Cells[string.Format("AY{0}", nunInicial)].Value = string.Format("{0}/{1}/{2}", dato.Incidente_fecha_diligenciamiento.Day, dato.Incidente_fecha_diligenciamiento.Month, dato.Incidente_fecha_diligenciamiento.Year);
                hoja.Cells[string.Format("AY{0}", nunInicial)].Style.WrapText = true;
                nunInicial++;
            }

            for (int col = 1; col < 52; col++)
            {
                hoja.Column(col).Width = 50;
            }
            for (int fil = 3; fil < incidentes.Count + 2; fil++)
            {
                hoja.Row(fil).Height = 50;
            }

            hoja.Column(1).Width = 70;
            hoja.Column(48).Width = 100;
            
            foreach (var cel in hoja.Cells[string.Format("A1:AY{0}", incidentes.Count + 2)])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }
            
            //FileInfo fileInfo = new FileInfo(@"D:\Archivos\Incidente.xlsx");
            //excel.SaveAs(fileInfo);
            return excel.GetAsByteArray();

        }

        private string obtenerdia(string incidente_dia_semana)
        {
            switch(incidente_dia_semana.Trim().ToUpper())
            {
                case "LU": return "LUNES";
                case "MA": return "MARTES";
                case "MI": return "MIERCOLES";
                case "JU": return "JUEVES";
                case "VI": return "VIERNES";
                case "SÁ": return "SABADO";
                case "DO": return "DOMINGO";
                default: return "";               
            }
        }


        /// <summary>
        /// Retorna las listas básicas de datos para utilizar en el formulario.
        /// </summary>
        /// <returns></returns>
        public EDIncidente_Listas_Basicas ObtenerListasBasicas(string nitEmpresa)
        {
            return ii.ObtenerListasBasicas(nitEmpresa);
        }




        public EDIncidente GuardarIncidente(EDIncidente inidente)
        {
            return ii.GuardarIncidente(inidente);
        }




        /// <summary>
        /// Crea un modelo de incidente en blanco con base a la información del usuario indicado.
        /// </summary>
        /// <param name="identificacionUsuario"></param>
        /// <returns></returns>
        public EDIncidente ObtenerNuevoIncidente(string identificacionEmpresa, string identificacionUsuario)
        {
            return ii.ObtenerNuevoIncidente(identificacionEmpresa, identificacionUsuario);
        }

    }
}
