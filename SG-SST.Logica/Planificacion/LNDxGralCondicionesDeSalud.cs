
namespace SG_SST.Logica.Planificacion
{
    using OfficeOpenXml;
    using SG_SST.EntidadesDominio.Planificacion;
    using SG_SST.Interfaces.Planificacion;
    using SG_SST.InterfazManager.Planificacion;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class LNDxGralCondicionesDeSalud
    {
        private static IDxGralCondicionesDeSalud DxGralCondicionesDeSalud = IMDxGralCondicionesDeSalud.DxGralCondicionesDeSalud();
        public bool GuardarDocDXSalud(EDDocDxSalud documento)
        {
            return DxGralCondicionesDeSalud.GuardarDocDXSalud(documento);
        }

        public List<EDDocDxSalud> ObtenerDocsDXSalud(int idEmpresa)
        {
            return DxGralCondicionesDeSalud.ObtenerDocsDXSalud(idEmpresa);
        }

        public bool EliminarDocDxSalud(int idDocDx)
        {
            return DxGralCondicionesDeSalud.EliminarDocDxSalud(idDocDx);
        }

        public EDDocDxSalud ObtenerDocDXSalud(int idDocDx)
        {
            return DxGralCondicionesDeSalud.ObtenerDocDXSalud(idDocDx);
        }

        public EDDxSalud GuardarDxSalud(EDDxSalud Diagnostico)
        {
            return DxGralCondicionesDeSalud.GuardarDxSalud(Diagnostico);
        }

        public List<EDDxSalud> ObtenerDiagnosticosPorsedeAnio(int idEmpresa)
        {
            return DxGralCondicionesDeSalud.ObtenerDiagnosticosPorsedeAnio(idEmpresa);
        }

        public List<EDDxSalud> ObtenerHistoricoDxDeSedePorAnio(int idDxSalud)
        {
            return DxGralCondicionesDeSalud.ObtenerHistoricoDxDeSedePorAnio(idDxSalud);
        }

        public byte[] DescargarHistoricoSedeAnio(int idDxSalud)
        {
            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Diagnostico");
            ExcelWorksheet hoja1 = excel.Workbook.Worksheets[1];

            hoja1.Cells["A1"].Value = "Nit de La empresa";
            hoja1.Cells["B1"].Value = "Razón Social";
            hoja1.Cells["C1"].Value = "Fecha inicial del período de Evaluación Médica Ocupacional/Autoreporte CS/PVE";
            hoja1.Cells["D1"].Value = "Fecha final del período de Evaluación Médica Ocupacional/Autoreporte CS/PVE";
            hoja1.Cells["E1"].Value = "Vigencia del Diagnóstico";
            hoja1.Cells["F1"].Value = "Sede";
            hoja1.Cells["G1"].Value = "Municipio de la sede";
            hoja1.Cells["H1"].Value = "Departamento de la sede";
            hoja1.Cells["I1"].Value = "Zona o Lugar";
            hoja1.Cells["J1"].Value = "Proceso";
            hoja1.Cells["K1"].Value = "Total de Trabajadores de la zona o lugar";

            hoja1.Cells["L1"].Value = "Clasificación del Peligro";
            hoja1.Cells["M1"].Value = "Descripción del Peligro";

            hoja1.Cells["N1"].Value = "Sintomatología";
            hoja1.Cells["O1"].Value = "Nº Trabajadores con la sintomatología";
            hoja1.Cells["P1"].Value = "% Anormalidad";

            hoja1.Cells["Q1"].Value = "Prueba Clínica";
            hoja1.Cells["R1"].Value = "Nº Trabajadores con prueba clínica anormal";
            hoja1.Cells["S1"].Value = "% Anormalidad";

            hoja1.Cells["T1"].Value = "Prueba P_Clínica";
            hoja1.Cells["U1"].Value = "Nº Trabajadores con prueba P_clínica anormal";
            hoja1.Cells["V1"].Value = "% Anormalidad";

            hoja1.Cells["W1"].Value = "Diagnóstico CIE10";
            hoja1.Cells["X1"].Value = "Nº Trabajadores con prueba clínica anormal";
            hoja1.Cells["Y1"].Value = "% Anormalidad";

            hoja1.Cells["Z1"].Value = "Responsable de la Información";
            hoja1.Cells["AA1"].Value = "Profesión del responsable de la información";
            hoja1.Cells["AB1"].Value = "Tarjeta profesional del responsable de la Información";


            int col = 1;
            foreach (var cel in hoja1.Cells["A1:AB1"])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                cel.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                cel.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                cel.Style.Font.Bold = true;
                cel.Style.WrapText = true;
                hoja1.Column(col).Width = 25;
                col++;


            }
            List<EDDxSalud> historicosSedeAnio = DxGralCondicionesDeSalud.ObtenerHistoricoDxDeSedePorAnio(idDxSalud);
            EDDxSalud eddxsalud = historicosSedeAnio.FirstOrDefault();

            hoja1.Cells[string.Format("A{0}", 2)].Value = eddxsalud.nitEmpresa;
            hoja1.Cells[string.Format("B{0}", 2)].Value = eddxsalud.nombreEmpresa;
            hoja1.Cells[string.Format("C{0}", 2)].Value = eddxsalud.Fecha_Inicial_Dx.ToString(("dd-MM-yyyy"));
            hoja1.Cells[string.Format("D{0}", 2)].Value = eddxsalud.Fecha_Final_Dx.ToString(("dd-MM-yyyy"));
            hoja1.Cells[string.Format("E{0}", 2)].Value = eddxsalud.vigencia;
            hoja1.Cells[string.Format("F{0}", 2)].Value = eddxsalud.NombreSede;
            hoja1.Cells[string.Format("G{0}", 2)].Value = eddxsalud.NombreMunicipio;
            hoja1.Cells[string.Format("H{0}", 2)].Value = eddxsalud.NombreDepartamento;
            hoja1.Cells[string.Format("I{0}", 2)].Value = eddxsalud.ZonaLugar;
            hoja1.Cells[string.Format("J{0}", 2)].Value = eddxsalud.nombreProceso;
            hoja1.Cells[string.Format("K{0}", 2)].Value = eddxsalud.NumeroTrabajadoresLugar;

            hoja1.Cells[string.Format("Z{0}", 2)].Value = eddxsalud.Responsable_informacion;
            hoja1.Cells[string.Format("AA{0}", 2)].Value = eddxsalud.Profesion_Responsable;
            hoja1.Cells[string.Format("AB{0}", 2)].Value = eddxsalud.Tarjeta_Profesional;

            int nunInicial = 2;
            foreach (var dx in eddxsalud.EDClasificacionPeligroDx)
            {
                hoja1.Cells[string.Format("L{0}", nunInicial)].Value = dx.nombreTipoPeligro;
                hoja1.Cells[string.Format("M{0}", nunInicial)].Value = dx.nombreDescripcion;
                nunInicial++;
            }
            nunInicial = 2;
            foreach (var dx in eddxsalud.EDSintomatologiaDx)
            {

                hoja1.Cells[string.Format("N{0}", nunInicial)].Value = dx.Sintomatologia;
                hoja1.Cells[string.Format("O{0}", nunInicial)].Value = dx.Trabajadores_Sintomatologia;
                hoja1.Cells[string.Format("P{0}", nunInicial)].Value = dx.porcentajeSintomatologia;
                nunInicial++;
            }
            nunInicial = 2;
            foreach (var dx in eddxsalud.EDPruebasClinicasDx)
            {
                hoja1.Cells[string.Format("Q{0}", nunInicial)].Value = dx.Prueba_Clinica;
                hoja1.Cells[string.Format("R{0}", nunInicial)].Value = dx.Trabajadores_Con_Prueba;
                hoja1.Cells[string.Format("S{0}", nunInicial)].Value = dx.porcentajePrueba;
                nunInicial++;
            }
            nunInicial = 2;
            foreach (var dx in eddxsalud.EDPruebasPClinicasDx)
            {
                hoja1.Cells[string.Format("T{0}", nunInicial)].Value = dx.Prueba_P_Clinica;
                hoja1.Cells[string.Format("U{0}", nunInicial)].Value = dx.Trabajadores_Con_Prueba_P;
                hoja1.Cells[string.Format("V{0}", nunInicial)].Value = dx.porcentajePruebaP;
                nunInicial++;
            }
            nunInicial = 2;
            foreach (var dx in eddxsalud.EDDiagnosticoCie10Dx)
            {
                hoja1.Cells[string.Format("W{0}", nunInicial)].Value = dx.NombreDiagnosticoCIE10;
                hoja1.Cells[string.Format("X{0}", nunInicial)].Value = dx.NumeroTrabajadoresConDiagnostico;
                hoja1.Cells[string.Format("Y{0}", nunInicial)].Value = dx.porcentajeDiagnostico;
                nunInicial++;
            }
            return excel.GetAsByteArray();
        }

        public byte[] ObtenerReporte(int idEmpresa)
        {
            ExcelPackage excel = new ExcelPackage();
            excel.Workbook.Worksheets.Add("Historico diagnostico");
            ExcelWorksheet hoja1 = excel.Workbook.Worksheets[1];

            hoja1.Cells["A1"].Value = "Nit de La empresa";
            hoja1.Cells["B1"].Value = "Razón Social";
            hoja1.Cells["C1"].Value = "Fecha inicial del período de Evaluación Médica Ocupacional/Autoreporte CS/PVE";
            hoja1.Cells["D1"].Value = "Fecha final del período de Evaluación Médica Ocupacional/Autoreporte CS/PVE";
            hoja1.Cells["E1"].Value = "Vigencia del Diagnóstico";
            hoja1.Cells["F1"].Value = "Sede";
            hoja1.Cells["G1"].Value = "Municipio de la sede";
            hoja1.Cells["H1"].Value = "Departamento de la sede";
            hoja1.Cells["I1"].Value = "Zona o Lugar";
            hoja1.Cells["J1"].Value = "Proceso";
            hoja1.Cells["K1"].Value = "Total de Trabajadores de la zona o lugar";

            hoja1.Cells["L1"].Value = "Clasificación del Peligro";
            hoja1.Cells["M1"].Value = "Descripción del Peligro";

            hoja1.Cells["N1"].Value = "Sintomatología";
            hoja1.Cells["O1"].Value = "Nº Trabajadores con la sintomatología";
            hoja1.Cells["P1"].Value = "% Anormalidad";

            hoja1.Cells["Q1"].Value = "Prueba Clínica";
            hoja1.Cells["R1"].Value = "Nº Trabajadores con prueba clínica anormal";
            hoja1.Cells["S1"].Value = "% Anormalidad";

            hoja1.Cells["T1"].Value = "Prueba P_Clínica";
            hoja1.Cells["U1"].Value = "Nº Trabajadores con prueba P_clínica anormal";
            hoja1.Cells["V1"].Value = "% Anormalidad";

            hoja1.Cells["W1"].Value = "Diagnóstico CIE10";
            hoja1.Cells["X1"].Value = "Nº Trabajadores con prueba clínica anormal";
            hoja1.Cells["Y1"].Value = "% Anormalidad";

            hoja1.Cells["Z1"].Value = "Responsable de la Información";
            hoja1.Cells["AA1"].Value = "Profesión del responsable de la información";
            hoja1.Cells["AB1"].Value = "Tarjeta profesional del responsable de la Información";


            int col = 1;
            foreach (var cel in hoja1.Cells["A1:AB1"])
            {
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                cel.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                cel.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                cel.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                cel.Style.Font.Bold = true;
                cel.Style.WrapText = true;
                hoja1.Column(col).Width = 25;
                col++;
            }
            List<EDDxSalud> historicosSedeAnio = DxGralCondicionesDeSalud.ObtenerReporte(idEmpresa);
            int nunInicial = 0;
            
            int numeroMayor = 2;
            foreach (EDDxSalud eddxsalud in historicosSedeAnio)
            {
                nunInicial = numeroMayor;
                hoja1.Cells[string.Format("A{0}", nunInicial)].Value = eddxsalud.nitEmpresa;
                hoja1.Cells[string.Format("B{0}", nunInicial)].Value = eddxsalud.nombreEmpresa;
                hoja1.Cells[string.Format("C{0}", nunInicial)].Value = eddxsalud.Fecha_Inicial_Dx.ToString(("dd-MM-yyyy"));
                hoja1.Cells[string.Format("D{0}", nunInicial)].Value = eddxsalud.Fecha_Final_Dx.ToString(("dd-MM-yyyy"));
                hoja1.Cells[string.Format("E{0}", nunInicial)].Value = eddxsalud.vigencia;
                hoja1.Cells[string.Format("F{0}", nunInicial)].Value = eddxsalud.NombreSede;
                hoja1.Cells[string.Format("G{0}", nunInicial)].Value = eddxsalud.NombreMunicipio;
                hoja1.Cells[string.Format("H{0}", nunInicial)].Value = eddxsalud.NombreDepartamento;
                hoja1.Cells[string.Format("I{0}", nunInicial)].Value = eddxsalud.ZonaLugar;
                hoja1.Cells[string.Format("J{0}", nunInicial)].Value = eddxsalud.nombreProceso;
                hoja1.Cells[string.Format("K{0}", nunInicial)].Value = eddxsalud.NumeroTrabajadoresLugar;

                hoja1.Cells[string.Format("Z{0}", nunInicial)].Value = eddxsalud.Responsable_informacion;
                hoja1.Cells[string.Format("AA{0}", nunInicial)].Value = eddxsalud.Profesion_Responsable;
                hoja1.Cells[string.Format("AB{0}", nunInicial)].Value = eddxsalud.Tarjeta_Profesional;

                int numInicialClass = nunInicial;
                foreach (var dx in eddxsalud.EDClasificacionPeligroDx)
                {
                    hoja1.Cells[string.Format("L{0}", numInicialClass)].Value = dx.nombreTipoPeligro;
                    hoja1.Cells[string.Format("M{0}", numInicialClass)].Value = dx.nombreDescripcion;                   
                    numInicialClass++;
                }
                numeroMayor = numInicialClass;
                int numInicialSin = nunInicial;
                foreach (var dx in eddxsalud.EDSintomatologiaDx)
                {
                    hoja1.Cells[string.Format("N{0}", numInicialSin)].Value = dx.Sintomatologia;
                    hoja1.Cells[string.Format("O{0}", numInicialSin)].Value = dx.Trabajadores_Sintomatologia;
                    hoja1.Cells[string.Format("P{0}", numInicialSin)].Value = dx.porcentajeSintomatologia;
                    numInicialSin++;
                }
                if (numeroMayor < numInicialSin)
                    numeroMayor = numInicialSin;
                int numInicialPrue = nunInicial;
                foreach (var dx in eddxsalud.EDPruebasClinicasDx)
                {
                    hoja1.Cells[string.Format("Q{0}", numInicialPrue)].Value = dx.Prueba_Clinica;
                    hoja1.Cells[string.Format("R{0}", numInicialPrue)].Value = dx.Trabajadores_Con_Prueba;
                    hoja1.Cells[string.Format("S{0}", numInicialPrue)].Value = dx.porcentajePrueba;
                    numInicialPrue++;
                }
                if (numeroMayor < numInicialPrue)
                    numeroMayor = numInicialPrue;
                int numInicialPrueP = nunInicial;
                foreach (var dx in eddxsalud.EDPruebasPClinicasDx)
                {
                    hoja1.Cells[string.Format("T{0}", numInicialPrueP)].Value = dx.Prueba_P_Clinica;
                    hoja1.Cells[string.Format("U{0}", numInicialPrueP)].Value = dx.Trabajadores_Con_Prueba_P;
                    hoja1.Cells[string.Format("V{0}", numInicialPrueP)].Value = dx.porcentajePruebaP;
                    numInicialPrueP++;
                }
                if (numeroMayor < numInicialPrueP)
                    numeroMayor = numInicialPrueP;
                int numInicialCIie = nunInicial;
                foreach (var dx in eddxsalud.EDDiagnosticoCie10Dx)
                {
                    hoja1.Cells[string.Format("W{0}", numInicialCIie)].Value = dx.NombreDiagnosticoCIE10;
                    hoja1.Cells[string.Format("X{0}", numInicialCIie)].Value = dx.NumeroTrabajadoresConDiagnostico;
                    hoja1.Cells[string.Format("Y{0}", numInicialCIie)].Value = dx.porcentajeDiagnostico;
                    numInicialCIie++;
                }
                if (numeroMayor < numInicialCIie)
                    numeroMayor = numInicialCIie;
            }
            return excel.GetAsByteArray();
        }

        public bool EliminarDxSalud(int idDx)
        {
            return DxGralCondicionesDeSalud.EliminarDxSalud(idDx);
        }

        public List<EDDxSalud> BuscarDiagnosticosPorsedeAnio(int idEmpresa, int strZonaLugar) 
        {
            return DxGralCondicionesDeSalud.BuscarDiagnosticosPorsedeAnio(idEmpresa, strZonaLugar);
        }

    }
}
