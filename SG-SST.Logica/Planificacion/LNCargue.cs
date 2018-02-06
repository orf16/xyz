using OfficeOpenXml;
using SG_SST.Audotoria;
using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.Interfaces.Ausentismo;
using SG_SST.Interfaces.Empresas;
using SG_SST.InterfazManager.Ausentismo;
using SG_SST.InterfazManager.Empresas;
using SG_SST.Logica.Ausentismo;
using SG_SST.Logica.Empresas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Enumeraciones;



namespace SG_SST.Logica.Planificacion
{
    public class LNCargue
    {
        private static IEmpresa em = IMEmpresa.Empresa();
        private static IDepartamento dpto = IMAusentismo.Departamento();
        private static IContingencia cg = IMAusentismo.Contingencia();
        private static IDiagnostico dg = IMAusentismo.Diagnostico();
        private static IMunicipio mp = IMAusentismo.Municipio();
        private static IAusencia aus = IMAusentismo.Ausencia();

        public byte[] DescargarPlantillaCargue(string Nit)
        {
            ExcelPackage excel = new ExcelPackage();

            excel.Workbook.Worksheets.Add("Plantilla de cargue");

            ExcelWorksheet hoja1 = excel.Workbook.Worksheets[1];

            hoja1.Cells["A1"].Value = "Consecutivo";
            hoja1.Cells["B1"].Value = "ConsecutivoPadre";
            hoja1.Cells["C1"].Value = "Cod_Tipo_Documento";
            hoja1.Cells["D1"].Value = "Nit_Empresa";
            hoja1.Cells["E1"].Value = "Cod_Departamento";
            hoja1.Cells["F1"].Value = "Cod_Minicipio";
            hoja1.Cells["G1"].Value = "Cod_Sede";
            hoja1.Cells["H1"].Value = "Cod_Tipo_Documento";
            hoja1.Cells["I1"].Value = "Num_Documento";
            hoja1.Cells["J1"].Value = "Nombre_Trabajador";
            hoja1.Cells["K1"].Value = "Edad";
            hoja1.Cells["L1"].Value = "Genero";
            hoja1.Cells["M1"].Value = "Cod_Ocupacion";
            hoja1.Cells["N1"].Value = "Nombre_EPS";
            hoja1.Cells["O1"].Value = "Tipo_Vinculacion";
            hoja1.Cells["P1"].Value = "Salario_Base";
            hoja1.Cells["Q1"].Value = "Cod_Contingencia";
            hoja1.Cells["R1"].Value = "Cod_Area";
            hoja1.Cells["S1"].Value = "Fecha_Inicio";
            hoja1.Cells["T1"].Value = "Fecha_Fin";
            hoja1.Cells["U1"].Value = "Cod_Diagnostico";
            hoja1.Cells["V1"].Value = "Factor_Prestacional";
            hoja1.Cells["W1"].Value = "Observacion";

            int col = 1;
            foreach (var cel in hoja1.Cells["A1:W1"])
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

            //Cargar datos de hoja dos tipo documento
            excel.Workbook.Worksheets.Add("TipoDocumento");
            ExcelWorksheet hoja2 = excel.Workbook.Worksheets[2];

            List<EDTipoDocumento> tiposDocumento = em.ObtenerTiposDocumento();
            int nunInicial = 1;
            foreach (var tipoDocumento in tiposDocumento)
            {
                hoja2.Cells[string.Format("A{0}", nunInicial)].Value = tipoDocumento.Id_Tipo_Documento;
                hoja2.Cells[string.Format("B{0}", nunInicial)].Value = tipoDocumento.Descripcion;
                nunInicial++;
            }

            //Cargar datos de hoja tres con los departamentos
            excel.Workbook.Worksheets.Add("Departamentos");
            ExcelWorksheet hoja3 = excel.Workbook.Worksheets[3];

            List<SG_SST.EntidadesDominio.Ausentismo.EDDepartamento> departamentos = dpto.ObtenerDepartamento().ToList();
            nunInicial = 1;
            foreach (var dpt in departamentos)
            {
                hoja3.Cells[string.Format("A{0}", nunInicial)].Value = dpt.IdDepartamento;
                hoja3.Cells[string.Format("B{0}", nunInicial)].Value = dpt.Nombre;
                nunInicial++;
            }

            //Cargar datos de hoja tres con los departamentos
            excel.Workbook.Worksheets.Add("Minicipios");
            ExcelWorksheet hoja4 = excel.Workbook.Worksheets[4];

            List<SG_SST.EntidadesDominio.Ausentismo.EDMunicipio> miicipios = mp.ObtenerMunicipiosConDetps();
            nunInicial = 1;
            foreach (var m in miicipios)
            {
                hoja4.Cells[string.Format("A{0}", nunInicial)].Value = m.IdMunicipio;
                hoja4.Cells[string.Format("B{0}", nunInicial)].Value = m.Nombre;
                hoja4.Cells[string.Format("C{0}", nunInicial)].Value = m.NombreDepartamento;
                nunInicial++;
            }

            //Cargar datos de hoja cuatro con los sedes de la empresa
            excel.Workbook.Worksheets.Add("Sedes");
            ExcelWorksheet hoja5 = excel.Workbook.Worksheets[5];

            List<EDSede> sedes = em.ObtenernerSedesPorEmpresa(Nit);
            nunInicial = 1;
            foreach (var sede in sedes)
            {
                hoja5.Cells[string.Format("A{0}", nunInicial)].Value = sede.IdSede;
                hoja5.Cells[string.Format("B{0}", nunInicial)].Value = sede.NombreSede;
                nunInicial++;
            }


            ////Cargar datos de hoja cinco con los Empresas Asociadas
            //excel.Workbook.Worksheets.Add("Empresa Asociada");
            //ExcelWorksheet hoja5 = excel.Workbook.Worksheets[5];

            //List<EDEmpresa_Usuaria> EmpresaAsociadas = em.ObtenerEmpresasUsuariasPorEmpresa(Nit);
            //nunInicial = 1;
            //foreach (var asociada in EmpresaAsociadas)
            //{
            //    hoja5.Cells[string.Format("A{0}", nunInicial)].Value = asociada.IdEmpresaUsuaria;
            //    hoja5.Cells[string.Format("B{0}", nunInicial)].Value = asociada.RazonSocial;
            //    nunInicial++;
            //}

            //Cargar datos de hoja seis Genero del trabajador
            excel.Workbook.Worksheets.Add("Genero");
            ExcelWorksheet hoja6 = excel.Workbook.Worksheets[6];
            hoja6.Cells["A1"].Value = "M";
            hoja6.Cells["A2"].Value = "F";


            //Cargar datos de hoja siete con las ocupaciones
            excel.Workbook.Worksheets.Add("Ocuapciones");
            ExcelWorksheet hoja7 = excel.Workbook.Worksheets[7];

            List<EDOcupacion> Ocupaciones = em.ObtenerOpucaciones();
            nunInicial = 1;
            foreach (var ocupacion in Ocupaciones)
            {
                hoja7.Cells[string.Format("A{0}", nunInicial)].Value = ocupacion.Id_Ocupacion;
                hoja7.Cells[string.Format("B{0}", nunInicial)].Value = ocupacion.Descripcion;
                nunInicial++;
            }

            //Cargar datos de hoja ocho tipo vinculacion
            excel.Workbook.Worksheets.Add("Tipo viculacion");
            ExcelWorksheet hoja8 = excel.Workbook.Worksheets[8];
            hoja8.Cells["A1"].Value = "Dependiente";
            hoja8.Cells["A2"].Value = "Independiente";

            //Cargar datos de hoja nueve con las contigencias
            excel.Workbook.Worksheets.Add("Contigencias");
            ExcelWorksheet hoja9 = excel.Workbook.Worksheets[9];

            List<EDContingencia> Contigencias = cg.ObtenerContingencia().ToList();
            nunInicial = 1;
            foreach (var cng in Contigencias)
            {
                hoja9.Cells[string.Format("A{0}", nunInicial)].Value = cng.IdContingencia;
                hoja9.Cells[string.Format("B{0}", nunInicial)].Value = cng.Detalle;
                nunInicial++;
            }

            //Cargar datos de hoja diez con los proceso de la empresa o areas
            excel.Workbook.Worksheets.Add("Procesos o Areas");
            ExcelWorksheet hoja10 = excel.Workbook.Worksheets[10];

            List<EDProceso> Procesos = em.ObtenerProcesosPorEmpres(Nit);
            nunInicial = 1;
            foreach (var p in Procesos)
            {
                hoja10.Cells[string.Format("A{0}", nunInicial)].Value = p.Id_Proceso;
                hoja10.Cells[string.Format("B{0}", nunInicial)].Value = p.Descripcion;
                nunInicial++;
            }

            //Cargar datos de hoja once con los diagnosticos
            excel.Workbook.Worksheets.Add("Diagnosticos");
            ExcelWorksheet hoja11 = excel.Workbook.Worksheets[11];

            List<EDDiagnostico> Diagnosticos = dg.ObtenerDiagnostico().ToList();
            nunInicial = 1;
            foreach (var diag in Diagnosticos)
            {
                hoja11.Cells[string.Format("A{0}", nunInicial)].Value = diag.Codigo;
                hoja11.Cells[string.Format("B{0}", nunInicial)].Value = diag.Descripcion;
                nunInicial++;
            }

            return excel.GetAsByteArray();
        }

        public EDCargueMasivo CargarPlantillaCargueAusentismo(EDCargueMasivo cargue)
        {
            RegistraLog registraLog = new RegistraLog();
            EDCargueMasivo edCargue = new EDCargueMasivo();
            try
            {
                edCargue = CargarArchivoAusencias(cargue);
            }
            catch (Exception ex)
            {
                registraLog.RegistrarError(typeof(LNCargue), string.Format("Error en el método CargarPlantillaCargueAusentismo  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                edCargue.Message = "El proceso de cargue fallo: la estructura del archivo no es valida";
                return edCargue;
            }

            return edCargue;
        }

        /// <summary>
        /// Carga una hoja de excel y retorna un datatable con la información de la misma
        /// </summary>
        /// <param name="rutaArchivo"></param>
        /// <param name="nombreHoja"></param>
        /// <returns></returns>
        public EDCargueMasivo CargarArchivoAusencias(EDCargueMasivo cargue)
        {
            RegistraLog registraLog = new RegistraLog();
            EDCargueMasivo edCargue = new EDCargueMasivo();
            try
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(cargue.path)))
                {
                    var sheet = package.Workbook.Worksheets[1];
                    bool validaEstruc = ValidarNombreColumnas(sheet);
                    if (validaEstruc)
                        edCargue = ProcesarCargue(sheet, cargue);
                    else
                        edCargue.Message = "El cargue fallo: Los nombres de las columnas de la plantilla fueron modificados.";

                    return edCargue;
                }
            }
            catch (Exception ex)
            {
                registraLog.RegistrarError(typeof(LNCargue), string.Format("Error en el método CargarArchivoAusencias {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                edCargue.Message = "El proceso de cargue fallo: La estructura del archivo no es valida";
                return edCargue;
            }
        }

        private EDCargueMasivo ProcesarCargue(ExcelWorksheet sheet, EDCargueMasivo cargue)
        {
            EDCargueMasivo edCargue = new EDCargueMasivo();
            List<EDAusencia> Ausencias = new List<EDAusencia>();
            LNAusencia use = new LNAusencia();
            bool tieneDatos = false;
            bool result = false;

            int consecutivo;
            int conscutivopadre;
            int CodTipoDocumento;
            string Nit_Empresa;
            int Cod_Departamento;
            int Cod_Municipio;
            int Cod_Sede;
            int Cod_Tipo_Documento;
            string Num_Documento;
            string Nombre_Trabajador;
            int Edad;
            string Genero;
            int Cod_Ocupacion;
            string Nombre_EPS;
            string Tipo_Vinculacion;
            int Salario_Base;
            int Cod_Contingencia;
            int Cod_Area;
            DateTime Fecha_Inicio;
            DateTime Fecha_Fin;
            int Cod_Diagnostico;
            decimal Factor_Prestacional;
            string Observacion;

            int DiasLaborables = 5;
            int diasCalculados = 0;

            try
            {
                var dt = new DataTable();
                var rowCnt = sheet.Dimension.End.Row;
                LNAusencia lnAusencia = new LNAusencia();
               

                //Verificamos los nombres de las columnas
                // Loop through Columns
                for (var fila = 2; fila <= rowCnt; fila++)
                {
                    EDAusencia Ausencia = new EDAusencia();

                    if (sheet.Cells[fila, 1].Value == null || sheet.Cells[fila, 2].Value == null || sheet.Cells[fila, 3].Value == null
                || sheet.Cells[fila, 4].Value == null || sheet.Cells[fila, 5].Value == null || sheet.Cells[fila, 6].Value == null || sheet.Cells[fila, 7].Value == null
                || sheet.Cells[fila, 8].Value == null || sheet.Cells[fila, 9].Value == null || sheet.Cells[fila, 10].Value == null || sheet.Cells[fila, 11].Value == null
                || sheet.Cells[fila, 12].Value == null || sheet.Cells[fila, 13].Value == null || sheet.Cells[fila, 14].Value == null
                || sheet.Cells[fila, 15].Value == null || sheet.Cells[fila, 16].Value == null || sheet.Cells[fila, 17].Value == null
                || sheet.Cells[fila, 18].Value == null || sheet.Cells[fila, 19].Value == null || sheet.Cells[fila, 20].Value == null
                || sheet.Cells[fila, 21].Value == null || sheet.Cells[fila, 22].Value == null)
                    {
                        edCargue.Message = "Existen campos en blanco por favor revise la fila " + fila + "; es obligatorio diligenciar todas las columnas excepto observaciones.";
                        return edCargue;
                    }
                    try { consecutivo = int.Parse(sheet.Cells[fila, 1].Value.ToString()); }
                    catch
                    {
                        edCargue.Message = "El valor del campo consecutivo en la fila " + fila + ", tiene que ser un número entero.";
                        return edCargue;
                    }
                    try { conscutivopadre = int.Parse(sheet.Cells[fila, 2].Value.ToString()); }
                    catch
                    {
                        edCargue.Message = "El valor del campo consecutivopadre en la fila " + fila + ", tiene que ser un número entero.";
                        return edCargue;
                    }
                    if (conscutivopadre > 0 && fila == 2)
                    {
                        edCargue.Message = "El primer resgistro no puede ser un registro que indica prorroga, debe tener un registro padre que indica la ausencia inicial.";
                        return edCargue;
                    }
                    try
                    {
                        CodTipoDocumento = VerificarTipoDocumento(int.Parse(sheet.Cells[fila, 3].Value.ToString()));
                        if (CodTipoDocumento < 1)
                        {
                            edCargue.Message = "No se encontró el Cod_Tipo_Documento ingresado en la fila " + fila + ", por favor verifique con la lista en la hoja Documentos. ";
                            return edCargue;
                        }
                    }
                    catch
                    {
                        edCargue.Message = "El valor del campo Cod_Tipo_Documento en la fila " + fila + ", tiene que ser un número entero tomado de lista en la hoja Documentos.";
                        return edCargue;
                    }

                    Nit_Empresa = sheet.Cells[fila, 4].Value.ToString();
                    if(fila < 3)
                        DiasLaborables = lnAusencia.ObtenerDiasLaborablesEmpresa(Nit_Empresa);
                    try
                    {
                        Cod_Departamento = VerificarDepartamento(int.Parse(sheet.Cells[fila, 5].Value.ToString()));
                        if (Cod_Departamento < 1)
                        {
                            edCargue.Message = "No se encontró el Cod_Departamento ingresado en la fila " + fila + ", por favor verifique con la lista en la hoja Departamentos. ";
                            return edCargue;
                        }
                    }
                    catch
                    {
                        edCargue.Message = "El valor del campo Cod_Departamento en la fila " + fila + ", tiene que ser un número entero tomado de lista en la hoja Departamentos.";
                        return edCargue;
                    }
                    try
                    {
                        Cod_Municipio = VerificarMunicipio(int.Parse(sheet.Cells[fila, 6].Value.ToString()), Cod_Departamento);
                        if (Cod_Municipio < 1)
                        {
                            edCargue.Message = "No se encontró el Cod_Municipio de la fila " + fila + " asociado al Cod_Departamento ingresado, por favor verifique con la lista en la hoja Municipios. ";
                            return edCargue;
                        }
                    }
                    catch
                    {
                        edCargue.Message = "El valor del campo Cod_Municipio en la fila " + fila + ", tiene que ser un número entero tomado de lista en la hoja Municipios.";
                        return edCargue;
                    }
                    try
                    {
                        Cod_Sede = VerificarSede(int.Parse(sheet.Cells[fila, 7].Value.ToString()), Nit_Empresa);
                        if (Cod_Sede < 1)
                        {
                            edCargue.Message = "No se encontró el Cod_Sede de la fila " + fila + " asociada al Nit ingresado, por favor verifique con la lista en la hoja Sedes. ";
                            return edCargue;
                        }
                    }
                    catch
                    { 
                        edCargue.Message = "El valor del campo Cod_Sede en la fila " + fila + ", tiene que ser un número entero tomado de lista en la hoja Sedes.";
                        return edCargue;
                    }
                    try
                    {
                        Cod_Tipo_Documento = VerificarTipoDocumento(int.Parse(sheet.Cells[fila, 8].Value.ToString()));
                        if (Cod_Tipo_Documento < 1)
                        {
                            edCargue.Message = "No se encontró el Cod_Tipo_Documento ingresado en la fila " + fila + ", por favor verifique con la lista en la hoja Documentos. ";
                            return edCargue;
                        }
                    }
                    catch
                    {
                        edCargue.Message = "El valor del campo Cod_Tipo_Documento en la fila" + fila + ", tiene que ser un número entero tomado de lista en la hoja Documentos.";
                        return edCargue;
                    }

                    Num_Documento = sheet.Cells[fila, 9].Value.ToString();
                    Nombre_Trabajador = sheet.Cells[fila, 10].Value.ToString();
                    try { Edad = int.Parse(sheet.Cells[fila, 11].Value.ToString()); }
                    catch
                    {
                        edCargue.Message = "El valor del campo Edad en la fila " + fila + ", tiene que ser un número entero.";
                        return edCargue;
                    }

                    if (sheet.Cells[fila, 12].Value.ToString().ToUpper().Equals("F") || sheet.Cells[fila, 12].Value.ToString().ToUpper().Equals("M"))
                        Genero = sheet.Cells[fila, 12].Value.ToString().ToUpper();
                    else
                    {
                        edCargue.Message = "El valor ingresado en Genero fila " + fila + ", no es un valor de genero valido,  por favor ingrese F o M. ";
                        return edCargue;
                    }
                    try
                    {
                        Cod_Ocupacion = VerificarOcupacion(int.Parse(sheet.Cells[fila, 13].Value.ToString()));
                        if (Cod_Ocupacion < 1)
                        {
                            edCargue.Message = "No se encontró el Cod_Ocupacion ingresado en la fila " + fila + ", por favor verifique con la lista en la hoja Ocupaciones. ";
                            return edCargue;
                        }
                    }
                    catch
                    {
                        edCargue.Message = "El valor del campo Cod_Ocupacion en la fila " + fila + ", tiene que ser un número entero tomado de lista en la hoja Ocupaciones.";
                        return edCargue;
                    }

                    Nombre_EPS = sheet.Cells[fila, 14].Value.ToString();
                    Tipo_Vinculacion = sheet.Cells[fila, 15].Value.ToString();
                    try { Salario_Base = int.Parse(sheet.Cells[fila, 16].Value.ToString()); }
                    catch
                    {
                        edCargue.Message = "El valor del campo Salario_Base en la fila " + fila + ", tiene que ser un número entero sin puntos.";
                        return edCargue;
                    }
                    try
                    {
                        Cod_Contingencia = VerificarContingencia(int.Parse(sheet.Cells[fila, 17].Value.ToString()));
                        if (Cod_Contingencia < 1)
                        {
                            edCargue.Message = "No se encontró el Cod_Contingencia ingresado en la fila " + fila + ", por favor verifique con la lista en la hoja Contingencias. ";
                            return edCargue;
                        }
                    }
                    catch
                    {
                        edCargue.Message = "El valor del campo Cod_Contingencia en la fila " + fila + ", tiene que ser un número entero tomado de lista en la hoja Contingencias.";
                        return edCargue;
                    }
                    try
                    {
                        Cod_Area = VerificarProceso(int.Parse(sheet.Cells[fila, 18].Value.ToString()), Nit_Empresa);
                        if (Cod_Area < 1)
                        {
                            edCargue.Message = "No se encontró el Cod_Area de la fila " + fila + " asociado al Nit ingresado, por favor verifique con la lista en la hoja Areas. ";
                            return edCargue;
                        }
                    }
                    catch
                    {
                        edCargue.Message = "El valor del campo Cod_Area en la fila " + fila + ", tiene que ser un número entero tomado de lista en la hoja Areas.";
                        return edCargue;
                    }
                    try { Fecha_Inicio = Convert.ToDateTime(sheet.Cells[fila, 19].Value, CultureInfo.InvariantCulture); }
                    catch
                    {
                        edCargue.Message = "El valor del campo Fecha_Inicio en la fila " + fila + ", no es una fecha valida o no tiene el formato DD/MM/YYYY.";
                        return edCargue;
                    }
                    try { Fecha_Fin = Convert.ToDateTime(sheet.Cells[fila, 20].Value, CultureInfo.InvariantCulture); }
                    catch
                    {
                        edCargue.Message = "El valor del campo Fecha_Fin en la fila " + fila + ", no es una fecha valida o no tiene el formato DD/MM/YYYY.";
                        return edCargue;
                    }
                    try
                    {
                        if (Cod_Contingencia == (int)EnumAusentismo.Contingencias.AccidenteTrabajo || Cod_Contingencia == (int)EnumAusentismo.Contingencias.EnfermedadGeneral || Cod_Contingencia == (int)EnumAusentismo.Contingencias.EnfermedadLaboral)
                        {
                            Cod_Diagnostico = VerificarDiagnostico(sheet.Cells[fila, 21].Value.ToString());
                            if (Cod_Diagnostico < 1)
                            {
                                edCargue.Message = "No se encontró el Cod_Diagnostico ingresado en la fila " + fila + ", por favor verifique con la lista en la hoja Diagnosticos. ";
                                return edCargue;
                            }
                        }
                        else
                            Cod_Diagnostico = 0;
                    }
                    catch
                    {
                        edCargue.Message = "El valor del campo Cod_Diagnostico en la fila " + fila + ", debe ser alfanumerico tomado de lista en la hoja Diagnosticos.";
                        return edCargue;
                    }
                    try { Factor_Prestacional = Convert.ToDecimal(sheet.Cells[fila, 22].Value, CultureInfo.InvariantCulture); }
                    catch
                    {
                        edCargue.Message = "El valor del campo Factor_Prestacional en la fila " + fila + ", no es valido.";
                        return edCargue;
                    }

                    if(Fecha_Inicio > Fecha_Fin)
                    {
                        edCargue.Message = "La fecha de inicio es mayor que la fecha fin en la fila " + fila + ".";
                        return edCargue;
                    }

                    Observacion = string.Empty;
                    if (sheet.Cells[fila, 23].Value != null)
                        Observacion = sheet.Cells[fila, 23].Value.ToString();

                    if(Cod_Contingencia == (int)EnumAusentismo.Contingencias.LicenciaPaternidad)
                    {
                        diasCalculados = lnAusencia.CalcularDiasLaborales(Fecha_Inicio, Fecha_Fin, DiasLaborables, Cod_Contingencia);
                        if(diasCalculados != 8)
                        {
                            edCargue.Message = "La licencia de paternidad debe ser de 8 dias habiles, por favor verifique las fechas de inicio y fin en la fila " + fila;
                            return edCargue;
                        }
                    }
                    else if (Cod_Contingencia == (int)EnumAusentismo.Contingencias.LicenciaLuto)
                    {
                        diasCalculados = lnAusencia.CalcularDiasLaborales(Fecha_Inicio, Fecha_Fin, DiasLaborables, Cod_Contingencia);
                        if (diasCalculados != 5)
                        {
                            edCargue.Message = "La licencia de luto debe ser de 5 dias habiles, por favor verifique las fechas de inicio y fin en la fila " + fila;
                            return edCargue;
                        }
                    }
                    else if (Cod_Contingencia == (int)EnumAusentismo.Contingencias.LicenciaMaternidad)
                    {
                        diasCalculados = lnAusencia.CalcularDiasLaborales(Fecha_Inicio, Fecha_Fin, DiasLaborables, Cod_Contingencia);
                        if (diasCalculados != 126)
                        {
                            edCargue.Message = "La licencia de maternidad debe ser de 126 dias calendario, por favor verifique las fechas de inicio y fin en la fila " + fila;
                            return edCargue;
                        }
                    }
                    else if (Cod_Contingencia == (int) EnumAusentismo.Contingencias.PermisoPorHorasDia)
                    {
                        diasCalculados = lnAusencia.CalcularDiasLaborales(Fecha_Inicio, Fecha_Fin, DiasLaborables, Cod_Contingencia);
                        if (diasCalculados > 1)
                        {
                            edCargue.Message = "El permiso por dias no debe superar 1 dia, por favor verifique que las fechas de inicio y fin sean iguales en la fila " + fila;
                            return edCargue;
                        }
                    }
                    else
                        diasCalculados = lnAusencia.CalcularDiasLaborales(Fecha_Inicio, Fecha_Fin, DiasLaborables, Cod_Contingencia);

                   // Ausencia.IdAusencia = consecutivo;
                    Ausencia.consecutivoPadre = conscutivopadre;
                    Ausencia.Documento = Num_Documento;
                    Ausencia.IdEmpresa = Nit_Empresa;
                    Ausencia.IdEmpresaUsuaria = cargue.Id_Empresa_Usuaria;
                    Ausencia.idDepartamento = Cod_Departamento;
                    Ausencia.idMunicipio = Cod_Municipio;
                    Ausencia.IdContingencia = Cod_Contingencia;
                    Ausencia.IdDiagnostico = Cod_Diagnostico;                    
                    Ausencia.IdSede = Cod_Sede;
                    Ausencia.IdProceso = Cod_Area;
                    Ausencia.FechaInicio = Fecha_Inicio;
                    Ausencia.FechaFin = Fecha_Fin;
                    Ausencia.DiasAusencia = diasCalculados;
                    Ausencia.Costo = ((Salario_Base / 30) * Ausencia.DiasAusencia) * Factor_Prestacional;
                    Ausencia.FactorPrestacional = Factor_Prestacional;
                    Ausencia.Observaciones = Observacion;
                    Ausencia.IdOcupacion = Cod_Ocupacion;
                    Ausencia.Sexo = Genero;
                    Ausencia.Edad = Edad;
                    Ausencia.Eps = Nombre_EPS;
                    Ausencia.TipoVinculacion = Tipo_Vinculacion;
                    Ausencia.NombrePersona = Nombre_Trabajador;

                    Ausencias.Add(Ausencia);
                    tieneDatos = true;
                }
                if (tieneDatos)
                {
                    return aus.InsertarAusenciasCargueMasivo(Ausencias);
                    
                }
                else
                    edCargue.Message = "El proceso de cargue fallo, El archivo no contiene información valida";
            }
            catch (Exception ex)
            {
                RegistraLog registraLog = new RegistraLog();
                registraLog.RegistrarError(typeof(LNCargue), string.Format("Error en el método WorksheetToDataTable {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                edCargue.Message = "El proceso de cargue fallo: La estructura del archivo no es valida";
                return edCargue;
            }
            return edCargue;
        }

        private int VerificarDiagnostico(string Cod_Diagnostico)
        {            
            var diagActual = dg.ObtenerGiagnosticoPorCodigo(Cod_Diagnostico);
            if (diagActual != null)
                return diagActual.IdDiagnostico;
            else
                return 0;
        }

        private int VerificarProceso(int idProceso, string nit)
        {
            return em.ValidarProceso(idProceso, nit);
        }

        private int VerificarContingencia(int idContigencia)
        {
            return cg.ValidarContingencia(idContigencia);
        }

        private int VerificarOcupacion(int idOcupacion)
        {
            return em.ValidarOcupacion(idOcupacion); 
        }

        private int VerificarSede(int idSede, string nit_Empresa)
        {
            return em.ValidarSede(idSede, nit_Empresa);
        }

        private int VerificarTipoDocumento(int idTipoDoc)
        {
            return em.ValidarTipoDocumento(idTipoDoc);
        }

        private int VerificarMunicipio(int idMunicipio, int cod_Departamento)
        {
            return mp.ValidarMunicipio(idMunicipio, cod_Departamento);
        }

        private int VerificarDepartamento(int idDepartamento)
        {
            return dpto.ValidarDepartamento(idDepartamento);
        }

        private bool ValidarNombreColumnas(ExcelWorksheet sheet)
        {
            if (sheet.Cells[1, 1].Value.Equals("Consecutivo") && sheet.Cells[1, 2].Value.Equals("ConsecutivoPadre") && sheet.Cells[1, 3].Value.Equals("Cod_Tipo_Documento")
                && sheet.Cells[1, 4].Value.Equals("Nit_Empresa") && sheet.Cells[1, 5].Value.Equals("Cod_Departamento") && sheet.Cells[1, 6].Value.Equals("Cod_Minicipio") && sheet.Cells[1, 7].Value.Equals("Cod_Sede")
                && sheet.Cells[1, 8].Value.Equals("Cod_Tipo_Documento") && sheet.Cells[1, 9].Value.Equals("Num_Documento") && sheet.Cells[1, 10].Value.Equals("Nombre_Trabajador") && sheet.Cells[1, 11].Value.Equals("Edad")
                && sheet.Cells[1, 12].Value.Equals("Genero") && sheet.Cells[1, 13].Value.Equals("Cod_Ocupacion") && sheet.Cells[1, 14].Value.Equals("Nombre_EPS")
                && sheet.Cells[1, 15].Value.Equals("Tipo_Vinculacion") && sheet.Cells[1, 16].Value.Equals("Salario_Base") && sheet.Cells[1, 17].Value.Equals("Cod_Contingencia")
                && sheet.Cells[1, 18].Value.Equals("Cod_Area") && sheet.Cells[1, 19].Value.Equals("Fecha_Inicio") && sheet.Cells[1, 20].Value.Equals("Fecha_Fin")
                && sheet.Cells[1, 21].Value.Equals("Cod_Diagnostico") && sheet.Cells[1, 22].Value.Equals("Factor_Prestacional") && sheet.Cells[1, 23].Value.Equals("Observacion"))
                return true;
            else
                return false;
        }
    }
}
