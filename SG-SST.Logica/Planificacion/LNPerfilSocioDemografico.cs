using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Planificacion;
using SG_SST.InterfazManager.Planificacion;
using SG_SST.Models;
using SG_SST.EntidadesDominio.Participacion;
using SG_SST.EntidadesDominio.Empresas;
using OfficeOpenXml;
namespace SG_SST.Logica.Planificacion
{
    public class LNPerfilSocioDemografico
    {
        private static IPerfilSocioDemografico em = IMPerfilSocioDemografico.PerfilSocioDemografico();
        private SG_SSTContext db = new SG_SSTContext();

        public EDPerfilSocioDemografico GuardarerfilSocioDemografico(EDPerfilSocioDemografico perfilsoc)
        {
            EDPerfilSocioDemografico mp = em.GuardarPerfilSociodemografico(perfilsoc);


            return mp;
        }

        public EDPerfilSocioDemografico EditarPerfilSocioDemografico(EDPerfilSocioDemografico perfilsoc)
        {


            return em.EditarPerfilSociodemografico(perfilsoc);
        }


        public bool documentoExiste(string documento)
        {

            var perfiles = db.Tbl_PerfilSocioDemograficoPlanificacion.Where(e => e.PK_Numero_Documento_Empl.Equals(documento)).FirstOrDefault();
            if (perfiles != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<EDOcupacionPerfil> AutoCompletarOcupacion(string prefijo)
        {
            var respuesta = em.BuscarOcupacion(prefijo);
            return respuesta;
        }
        public bool EliminarPerfilSocioDemografico(int idPerfil)
        {
            return em.EliminarPerfilSocioDemografico(idPerfil);

        }

        public EDPerfilSocioDemografico obtenerPerfilesPorID(int id)
        {
            return em.obtenerPerfilesPorID(id);
        }


        public EDBusquedaMunicipio BuscarMunicipiosDeSede(int fk_sede)
        {
            return em.BuscarMunicipiosDeSede(fk_sede);
        }

        public List<EDCondicionesRiesgoPerfil> ObtenerCondicionesRiesgoPerfilPorID(int id)
        {
            return em.ObtenerCondicionesRiesgoPerfilPorID(id);
        }

        public List<EDCondicionesRiesgoPerfil> ObtenerCondicionesRiesgoPorEmpresa(string nitEmpresa)
        {
            return em.ObtenerCondicionesRiesgoPorEmpresa(nitEmpresa);
        }

        public List<EDPerfilSocioDemografico> obtenerPerfilesPorEmpresa(string nitEmpresa)
        {
            return em.obtenerPerfilesPorEmpresa(nitEmpresa);
        }


        public string calcularGrupoEtareo(int edad)
        {
            string grupoEtareo = "";

            if (edad <= 18)
            {
                grupoEtareo = "Menores de 18 años a 25 años";
            }
            else if (edad >= 26 && edad <= 35)
            {
                grupoEtareo = "26 a 35 años";
            }
            else if (edad >= 36 && edad <= 45)
            {
                grupoEtareo = "36 a 45 años";
            }
            else if (edad >= 46 && edad <= 55)
            {
                grupoEtareo = "46 a 55 años";
            }
            else
            {
                grupoEtareo = "Mayores a los 55 Años";
            }
            return grupoEtareo;
        }


        public bool EliminarExpocionPeligro(int idExposicion)
        {
            return em.EliminarExpocionPeligro(idExposicion);
        }


        public byte[] ObtenerRepExcelPorPerfil(EDPerfilSocioDemografico perfil)
        {

            ExcelPackage excel = new ExcelPackage();

            excel.Workbook.Worksheets.Add("PerfilSociodemografico");
            ExcelWorksheet hoja1 = excel.Workbook.Worksheets[1];
            hoja1.Cells["A1"].Value = "Nit empresa";
            hoja1.Cells["B1"].Value = "Razón social de la empresa";
            hoja1.Cells["C1"].Value = "Documento del empleado";
            hoja1.Cells["D1"].Value = "Nombres";
            hoja1.Cells["E1"].Value = "Apellidos";
            hoja1.Cells["F1"].Value = "E.P.S";
            hoja1.Cells["G1"].Value = "A.F.P";
            hoja1.Cells["H1"].Value = "Sede";
            hoja1.Cells["I1"].Value = "Municipio de Sede";
            hoja1.Cells["J1"].Value = "Departamento de Sede";
            hoja1.Cells["K1"].Value = "Zona/lugar";
            hoja1.Cells["L1"].Value = "Proceso";
            hoja1.Cells["M1"].Value = "Grado de escolaridad";
            hoja1.Cells["N1"].Value = "Ingresos";
            hoja1.Cells["O1"].Value = "Departamento de residencia";
            hoja1.Cells["P1"].Value = "Municipio de residencia";
            hoja1.Cells["Q1"].Value = "Dirección";
            hoja1.Cells["R1"].Value = "Conyuge";
            hoja1.Cells["S1"].Value = "Hijos";
            hoja1.Cells["T1"].Value = "Estrato Socioeconómico";
            hoja1.Cells["U1"].Value = "Estado civil";
            hoja1.Cells["V1"].Value = "Etnia";
            hoja1.Cells["W1"].Value = "Ocupación CIUO";
            hoja1.Cells["X1"].Value = "Edad";
            hoja1.Cells["Y1"].Value = "Grupo Étareo";
            hoja1.Cells["Z1"].Value = "Sexo";
            hoja1.Cells["AA1"].Value = "Vinculación laboral";
            hoja1.Cells["AB1"].Value = "Turno de trabajo";
            hoja1.Cells["AC1"].Value = "Cargo";
            hoja1.Cells["AD1"].Value = "Fecha de ingreso a la empresa";
            hoja1.Cells["AE1"].Value = "Fecha de ingreso a último cargo";
            hoja1.Cells["AF1"].Value = "Años en el último cargo";
            hoja1.Cells["AG1"].Value = "Meses en el último cargo";
            hoja1.Cells["AH1"].Value = "Días en el último cargo";
            hoja1.Cells["AI1"].Value = "Características Físicas para el desempeño del cargo";
            hoja1.Cells["AJ1"].Value = "Características Psicológicas para el desempeño del cargo";
            hoja1.Cells["AK1"].Value = "Evaluación médica requerida para el desempeño del cargo";

            var conyuge = "";
            if (perfil.Conyuge)
            {
                conyuge = "SI";
            }
            else
            {
                conyuge = "NO";
            }


            var hijos = "";
            if (perfil.Hijos)
            {
                hijos = "SI";
            }
            else
            {
                hijos = "NO";
            }
            string nombres = perfil.Nombre1 + " " + perfil.Nombre2;

            string apellidos = perfil.Apellido1 + "  " + perfil.Apellido2;

            var grupoEtareo = calcularGrupoEtareo(perfil.EdadPersona);


            int col = 1;
            foreach (var cel in hoja1.Cells["A1:AK1"])
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
            int nunInicial = 2;
            var proceso = "";
            if (perfil.nombreProceso != null)
            {
                proceso = perfil.nombreProceso;
            }
            else
            {

                proceso = "NA";
            }
            hoja1.Cells[string.Format("A{0}", nunInicial)].Value = perfil.nitEmpresa;
            hoja1.Cells[string.Format("B{0}", nunInicial)].Value = perfil.razonSocialEmpresa;
            hoja1.Cells[string.Format("C{0}", nunInicial)].Value = perfil.PK_Numero_Documento_Empl;
            hoja1.Cells[string.Format("D{0}", nunInicial)].Value = nombres;
            hoja1.Cells[string.Format("E{0}", nunInicial)].Value = apellidos;
            hoja1.Cells[string.Format("F{0}", nunInicial)].Value = perfil.EPS;
            hoja1.Cells[string.Format("G{0}", nunInicial)].Value = perfil.AFP;
            hoja1.Cells[string.Format("H{0}", nunInicial)].Value = perfil.nombreSede;
            hoja1.Cells[string.Format("I{0}", nunInicial)].Value = perfil.ciudadSede;
            hoja1.Cells[string.Format("J{0}", nunInicial)].Value = perfil.departamentoSede;
            hoja1.Cells[string.Format("K{0}", nunInicial)].Value = perfil.ZonaLugar;
            hoja1.Cells[string.Format("L{0}", nunInicial)].Value = proceso;
            hoja1.Cells[string.Format("M{0}", nunInicial)].Value = perfil.GradoEscolaridad;
            hoja1.Cells[string.Format("N{0}", nunInicial)].Value = perfil.Ingresos;
            hoja1.Cells[string.Format("O{0}", nunInicial)].Value = perfil.departamento;
            hoja1.Cells[string.Format("P{0}", nunInicial)].Value = perfil.municipio;
            hoja1.Cells[string.Format("Q{0}", nunInicial)].Value = perfil.Direccion;


            hoja1.Cells[string.Format("R{0}", nunInicial)].Value = conyuge;
            hoja1.Cells[string.Format("S{0}", nunInicial)].Value = hijos;
            hoja1.Cells[string.Format("T{0}", nunInicial)].Value = perfil.FK_Estrato;
            hoja1.Cells[string.Format("U{0}", nunInicial)].Value = perfil.estadoCivil;
            hoja1.Cells[string.Format("V{0}", nunInicial)].Value = perfil.etnia;
            hoja1.Cells[string.Format("W{0}", nunInicial)].Value = perfil.OcupacionPerfil;
            hoja1.Cells[string.Format("X{0}", nunInicial)].Value = perfil.EdadPersona;
            hoja1.Cells[string.Format("Y{0}", nunInicial)].Value = grupoEtareo;
            hoja1.Cells[string.Format("Z{0}", nunInicial)].Value = perfil.Sexo;
            hoja1.Cells[string.Format("AA{0}", nunInicial)].Value = perfil.vinculacionLabotal;
            hoja1.Cells[string.Format("AB{0}", nunInicial)].Value = perfil.TurnoTrabajo;
            hoja1.Cells[string.Format("AC{0}", nunInicial)].Value = perfil.Cargo;
            hoja1.Cells[string.Format("AD{0}", nunInicial)].Value = perfil.fechaIngresoEmpresa.ToString("dd/MM/yyyy");
            hoja1.Cells[string.Format("AE{0}", nunInicial)].Value = perfil.FechaIngresoUltimoCargo.ToString("dd/MM/yyyy");
            hoja1.Cells[string.Format("AF{0}", nunInicial)].Value = perfil.anyos;
            hoja1.Cells[string.Format("AG{0}", nunInicial)].Value = perfil.mes;
            hoja1.Cells[string.Format("AH{0}", nunInicial)].Value = perfil.dia;
            hoja1.Cells[string.Format("AI{0}", nunInicial)].Value = perfil.caracteristicasFisicas;
            hoja1.Cells[string.Format("AJ{0}", nunInicial)].Value = perfil.caracteristicasPsicologicas;
            hoja1.Cells[string.Format("AK{0}", nunInicial)].Value = perfil.evaluacionesMedicasRequeridas;

            excel.Workbook.Worksheets.Add("ExposicionPeligro");
            ExcelWorksheet hoja2 = excel.Workbook.Worksheets["ExposicionPeligro"];



           // hoja1.Cells.AutoFitColumns();

            hoja2.Cells["A1"].Value = "Tipo de péligro";
            hoja2.Cells["B1"].Value = "Descripción de péligro";
            hoja2.Cells["C1"].Value = "Tiempo de Exposición  en  Meses";



            List<EDCondicionesRiesgoPerfil> condiciones = perfil.condicionesRiesgo;


            nunInicial = 2;

            foreach (var con in condiciones)
            {
                var clasificacionPeligro = "";

                if (con.descripcionPeligro.Equals("Otro"))
                {
                    clasificacionPeligro = con.OtroPeligro;

                }
                else
                {
                    clasificacionPeligro = con.ClasificacionPeligro;
                }
                hoja2.Cells[string.Format("A{0}", nunInicial)].Value = con.descripcionPeligro;
                hoja2.Cells[string.Format("B{0}", nunInicial)].Value = clasificacionPeligro;
                hoja2.Cells[string.Format("C{0}", nunInicial)].Value = con.tiempoExpos;



                nunInicial++;
            }
            hoja2.Cells.AutoFitColumns();

            foreach (var cel in hoja2.Cells["A1:AC1"])
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

            return excel.GetAsByteArray();

        }

        public byte[] ObtenerRepExcelPorEmpresa(List<EDPerfilSocioDemografico> perfiles, List<EDCondicionesRiesgoPerfil> condiciones)
        {

            ExcelPackage excel = new ExcelPackage();

            excel.Workbook.Worksheets.Add("PerfilSociodemografico");
            ExcelWorksheet hoja1 = excel.Workbook.Worksheets[1];
            hoja1.Cells["A1"].Value = "ID";
            hoja1.Cells["B1"].Value = "Nit empresa";
            hoja1.Cells["C1"].Value = "Razón Social de la empresa";
            hoja1.Cells["D1"].Value = "Documento del empleado";
            hoja1.Cells["E1"].Value = "Nombres";
            hoja1.Cells["F1"].Value = "Apellidos";
            hoja1.Cells["G1"].Value = "E.P.S";
            hoja1.Cells["H1"].Value = "A.F.P";
            hoja1.Cells["I1"].Value = "Sede";
            hoja1.Cells["J1"].Value = "Municipio de Sede";
            hoja1.Cells["K1"].Value = "Departamento de Sede";
            hoja1.Cells["L1"].Value = "Zona/lugar";
            hoja1.Cells["M1"].Value = "Proceso";
            hoja1.Cells["N1"].Value = "Grado de escolaridad";
            hoja1.Cells["O1"].Value = "Ingresos";
            hoja1.Cells["P1"].Value = "Departamento de residencia";
            hoja1.Cells["Q1"].Value = "Municipio de residencia";
            hoja1.Cells["R1"].Value = "Dirección";
            hoja1.Cells["S1"].Value = "Conyuge";
            hoja1.Cells["T1"].Value = "Hijos";
            hoja1.Cells["U1"].Value = "Estrato Socioeconómico";
            hoja1.Cells["V1"].Value = "Estado civil";
            hoja1.Cells["W1"].Value = "Etnia";
            hoja1.Cells["X1"].Value = "Ocupación CIUO";
            hoja1.Cells["Y1"].Value = "Edad";
            hoja1.Cells["Z1"].Value = "Grupo Étareo";
            hoja1.Cells["AA1"].Value = "Sexo";
            hoja1.Cells["AB1"].Value = "Vinculación laboral";
            hoja1.Cells["AC1"].Value = "Turno de trabajo";
            hoja1.Cells["AD1"].Value = "Cargo";
            hoja1.Cells["AE1"].Value = "Fecha de ingreso a la empresa";
            hoja1.Cells["AF1"].Value = "Fecha de ingreso a último cargo";
            hoja1.Cells["AG1"].Value = "Años en el último cargo";
            hoja1.Cells["AH1"].Value = "Meses en el último cargo";
            hoja1.Cells["AI1"].Value = "Días en el último cargo";
            hoja1.Cells["AJ1"].Value = "Características Físicas para el desempeño del cargo";
            hoja1.Cells["AK1"].Value = "Características Psicológicas para el desempeño del cargo";
            hoja1.Cells["AL1"].Value = "Evaluación médica requerida para el desempeño del cargo";





            int col = 1;
            foreach (var cel in hoja1.Cells["A1:AL1"])
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
            int nunInicial = 2;

            foreach (var perfil in perfiles)
            {

                string nombres = perfil.Nombre1 + " " + perfil.Nombre2;

                string apellidos = perfil.Apellido1 + "  " + perfil.Apellido2;
                var conyuge = "";
                if (perfil.Conyuge)
                {
                    conyuge = "SI";
                }
                else
                {
                    conyuge = "NO";
                }


                var hijos = "";
                if (perfil.Hijos)
                {
                    hijos = "SI";
                }
                else
                {
                    hijos = "NO";
                }
                var proceso = "";
                if (perfil.nombreProceso != null)
                {
                    proceso = perfil.nombreProceso;
                }
                else
                {

                    proceso = "NA";
                }

                var grupoEtareo = calcularGrupoEtareo(perfil.EdadPersona);
                hoja1.Cells[string.Format("A{0}", nunInicial)].Value = perfil.IDEmpleado_PerfilSocioDemoGrafico;
                hoja1.Cells[string.Format("B{0}", nunInicial)].Value = perfil.nitEmpresa;
                hoja1.Cells[string.Format("C{0}", nunInicial)].Value = perfil.razonSocialEmpresa;
                hoja1.Cells[string.Format("D{0}", nunInicial)].Value = perfil.PK_Numero_Documento_Empl;
                hoja1.Cells[string.Format("E{0}", nunInicial)].Value = nombres;
                hoja1.Cells[string.Format("F{0}", nunInicial)].Value = apellidos;
                hoja1.Cells[string.Format("G{0}", nunInicial)].Value = perfil.EPS;
                hoja1.Cells[string.Format("H{0}", nunInicial)].Value = perfil.AFP;
                hoja1.Cells[string.Format("I{0}", nunInicial)].Value = perfil.nombreSede;
                hoja1.Cells[string.Format("J{0}", nunInicial)].Value = perfil.ciudadSede;
                hoja1.Cells[string.Format("K{0}", nunInicial)].Value = perfil.departamentoSede;
                hoja1.Cells[string.Format("L{0}", nunInicial)].Value = perfil.ZonaLugar;
                hoja1.Cells[string.Format("M{0}", nunInicial)].Value = proceso;
                hoja1.Cells[string.Format("N{0}", nunInicial)].Value = perfil.GradoEscolaridad;
                hoja1.Cells[string.Format("O{0}", nunInicial)].Value = perfil.Ingresos;
                hoja1.Cells[string.Format("P{0}", nunInicial)].Value = perfil.departamento;
                hoja1.Cells[string.Format("Q{0}", nunInicial)].Value = perfil.municipio;
                hoja1.Cells[string.Format("R{0}", nunInicial)].Value = perfil.Direccion;


                hoja1.Cells[string.Format("S{0}", nunInicial)].Value = conyuge;
                hoja1.Cells[string.Format("T{0}", nunInicial)].Value = hijos;
                hoja1.Cells[string.Format("U{0}", nunInicial)].Value = perfil.estrato;
                hoja1.Cells[string.Format("V{0}", nunInicial)].Value = perfil.estadoCivil;
                hoja1.Cells[string.Format("W{0}", nunInicial)].Value = perfil.etnia;
                hoja1.Cells[string.Format("X{0}", nunInicial)].Value = perfil.OcupacionPerfil;
                hoja1.Cells[string.Format("Y{0}", nunInicial)].Value = perfil.EdadPersona;
                hoja1.Cells[string.Format("Z{0}", nunInicial)].Value = grupoEtareo;
                hoja1.Cells[string.Format("AA{0}", nunInicial)].Value = perfil.Sexo;
                hoja1.Cells[string.Format("AB{0}", nunInicial)].Value = perfil.vinculacionLabotal;
                hoja1.Cells[string.Format("AC{0}", nunInicial)].Value = perfil.TurnoTrabajo;
                hoja1.Cells[string.Format("AD{0}", nunInicial)].Value = perfil.Cargo;
                hoja1.Cells[string.Format("AE{0}", nunInicial)].Value = perfil.fechaIngresoEmpresa.ToString("dd/MM/yyyy");
                hoja1.Cells[string.Format("AF{0}", nunInicial)].Value = perfil.FechaIngresoUltimoCargo.ToString("dd/MM/yyyy");
                hoja1.Cells[string.Format("AG{0}", nunInicial)].Value = perfil.anyos;
                hoja1.Cells[string.Format("AH{0}", nunInicial)].Value = perfil.mes;
                hoja1.Cells[string.Format("AI{0}", nunInicial)].Value = perfil.dia;
                hoja1.Cells[string.Format("AJ{0}", nunInicial)].Value = perfil.caracteristicasFisicas;
                hoja1.Cells[string.Format("AK{0}", nunInicial)].Value = perfil.caracteristicasPsicologicas;
                hoja1.Cells[string.Format("AL{0}", nunInicial)].Value = perfil.evaluacionesMedicasRequeridas;

                nunInicial++;
            }
            excel.Workbook.Worksheets.Add("ExposicionPeligro");
            ExcelWorksheet hoja2 = excel.Workbook.Worksheets["ExposicionPeligro"];



           // hoja1.Cells.AutoFitColumns();

            hoja2.Cells["A1"].Value = "ID";
            hoja2.Cells["B1"].Value = "Tipo de péligro";
            hoja2.Cells["C1"].Value = "Descripción de péligro";
            hoja2.Cells["D1"].Value = "Tiempo de Exposición  en  Meses";


            //List<EDCondicionesRiesgoPerfil> condiciones = perfil.condicionesRiesgo;


            nunInicial = 2;

            foreach (var con in condiciones)
            {

                var clasificacionPeligro = "";

                if (con.descripcionPeligro.Equals("Otro"))
                {
                    clasificacionPeligro = con.OtroPeligro;

                }
                else
                {
                    clasificacionPeligro = con.ClasificacionPeligro;
                }

                hoja2.Cells[string.Format("A{0}", nunInicial)].Value = con.FKPerfilSocioDemografico;
                hoja2.Cells[string.Format("B{0}", nunInicial)].Value = con.descripcionPeligro;
                hoja2.Cells[string.Format("C{0}", nunInicial)].Value = clasificacionPeligro;
                hoja2.Cells[string.Format("D{0}", nunInicial)].Value = con.tiempoExpos;




                nunInicial++;
            }
            hoja2.Cells.AutoFitColumns();

            foreach (var cel in hoja2.Cells["A1:AD1"])
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

            return excel.GetAsByteArray();

        }

        public byte[] ObtenerReporteExcelProcesoYSede(EDProcesoSede informacionPlantilla)
        {
            ExcelPackage excel = new ExcelPackage();

            excel.Workbook.Worksheets.Add("Códigos plantilla perfil sociodemográfico");
            ExcelWorksheet hoja1 = excel.Workbook.Worksheets[1];

            hoja1.Cells["A1"].Value = "Código Sede";
            hoja1.Cells["B1"].Value = "Nombre Sede";



            hoja1.Cells["C1"].Value = "Código Proceso";
            hoja1.Cells["D1"].Value = "Nombre Proceso";



            int col = 1;


            int nunInicial = 2;
            foreach (var sedes in informacionPlantilla.sedes)
            {

                hoja1.Cells[string.Format("A{0}", nunInicial)].Value = sedes.IdSede;
                hoja1.Cells[string.Format("B{0}", nunInicial)].Value = sedes.NombreSede;

                nunInicial++;
            }

            foreach (var cel in hoja1.Cells["A1:D1"])
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

            nunInicial = 2;
            foreach (var proceso in informacionPlantilla.procesos)
            {

                hoja1.Cells[string.Format("C{0}", nunInicial)].Value = proceso.Id_Proceso;
                hoja1.Cells[string.Format("D{0}", nunInicial)].Value = proceso.Descripcion;


                nunInicial++;
            }




            hoja1.Cells.AutoFitColumns();
            return excel.GetAsByteArray();

        }

    }
}


