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
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Planificacion;
using SG_SST.InterfazManager.Planificacion;
using SG_SST.EntidadesDominio.Empleado;
using SG_SST.Models;
using RestSharp;
using System.Configuration;
using System.Web;
using System.Net;
using SG_SST.Models.Empresas;

namespace SG_SST.Logica.Planificacion
{

    public class LNCargueDx
    {
        private static IEmpresa em = IMEmpresa.Empresa();
        private static IClasificacionPeligros cp = IMIdentificacionPeligros.ClasificacionPeligros();
        private static IPeligro zona = IMPeligro.Peligro();
        private static IDxGralCondicionesDeSalud dia = IMDxGralCondicionesDeSalud.DxGralCondicionesDeSalud();
        private static IDepartamento dpto = IMAusentismo.Departamento();
        private static IMunicipio mp = IMAusentismo.Municipio();
        private SG_SSTContext db = new SG_SSTContext();

        private static IPerfilSocioDemografico per = IMPerfilSocioDemografico.PerfilSocioDemografico();

        public EDCarguePerfil CargarPlantillaDx(EDCarguePerfil cargueP)
        {
         
            RegistraLog registraLog = new RegistraLog();
            EDCarguePerfil edCargue = new EDCarguePerfil();
            try
            {
                edCargue = CargarArchivoDx(cargueP);
            }
            catch (Exception ex)
            {
                registraLog.RegistrarError(typeof(LNCargue), string.Format("Error en el método Cargar Plantilla Dx  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                edCargue.Message = "El cargue fallo: la estructura del archivo no es valida";
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
        public EDCarguePerfil CargarArchivoDx(EDCarguePerfil cargue)
        {
            RegistraLog registraLog = new RegistraLog();
            EDCarguePerfil edCargue = new EDCarguePerfil();
            try
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(cargue.path)))
                {
                    var sheet = package.Workbook.Worksheets["Diagnostico"];
                    // 

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
                edCargue.Message = "El proceso  fallo: La estructura del archivo no es valida";
                return edCargue;
            }
        }



        private EDCarguePerfil ProcesarCargue(ExcelWorksheet sheet, EDCarguePerfil cargue)
        {
            EDCarguePerfil edCargue = new EDCarguePerfil();
           List<EDDxSalud> diagnosticos = new List<EDDxSalud>();
            bool resu = false;
            bool esValidoDx = true;
            bool noBlancos = true;
            string nFilaBla = "";
            string nFilaS = "";
            string nfilaP = "";
            string nfilaT = "";
            string nFilaPV = "";

            string nfilaV = "";
            string nFilaFr = "";
            string nFilaNit = "";
            string nFilaRango = "";
            string nFilaCodDiag = "";
            string mensajePlantillaVacia = "La plantilla ingresada no tiene datos";
            string mensajeTamano = "El número de trabajadores no puede ser superior al total de trabajadores de la zona o lugar, revisar la(s) fila(s):";
            string mensajeRangoFecha = "La fecha final no puede ser menor que la fecha inicial, revisar la(s) fila(s):";
            string mensajeNitEmpresa = "El Nit ingresado no es válido, revisar la(s) fila(s):";
            string mensajesFechas = "Las fechas final e inicial no pueden ser mayores a la fecha actual, revisar la(s) fila(s):";
            string mensajeVigencia = "La vigencia seleccionada no puede ser superior al año actual, revisar la(s) fila(s):";
            string mensajeSede = "Revisar la  Columna  E(Código de la Sede) ya que la sede no pertenece a la empresa, revisar la(s) fila(s):";
            string mensajeEspBlancos = "Existen campos en blanco, es obligatorio diligenciar  por lo menos un registro de cada grupo, los otros campos son obligatorios exceptuado G (Código del Proceso) , revisar la(s) fila(s):";
            string mensajeProceso = "Revisar la  Columna  G (Código del Proceso) ya que el proceso no pertenece a la empresa, revisar la(s) fila(s):";
            string mensajeCodDiagnostico = "El código de diagnóstico ingresado no es válido, revisar la(s) fila(s):";
           
            
            string mensaje = "";
           
            
            try
            {
                var dt = new DataTable();
                var rowCnt = 0;

                rowCnt = sheet.Dimension.End.Row;


              
                //Verificamos los nombres de las columnas
                // Loop through Columns
                List<EDProceso> procesos = per.ObtenerProcesoEmpresa(cargue.NitEmpresa);
                List<EDSede> sedes = em.ObtenernerSedesPorEmpresa(cargue.NitEmpresa);
                for (var fila = 2; fila <= rowCnt; fila++)
                {

                    List<EDClasificacionPeligroDx> peligros = new List<EDClasificacionPeligroDx>();
                    List<EDSintomatologiaDx> sintomologias = new List<EDSintomatologiaDx>();
                    List<EDPruebasClinicasDx> pruebasClinicas = new List<EDPruebasClinicasDx>();
                    List<EDPruebasPClinicasDx> pruebasParaClinicas = new List<EDPruebasPClinicasDx>();
                    List<EDDiagnosticoCie10Dx> diagnosticosCie = new List<EDDiagnosticoCie10Dx>();

                    EDDxSalud diagnostico = new EDDxSalud();
                    
                    //g string NumeroIdentificacion = sheet.Cells[fila, 1].Value.ToString();
                    if (sheet.Cells[fila, 1].Value != null)
                    {


                        if (sheet.Cells[fila, 1].Value == null || sheet.Cells[fila, 2].Value == null || sheet.Cells[fila, 3].Value == null
                       || sheet.Cells[fila, 4].Value == null || sheet.Cells[fila, 5].Value == null || sheet.Cells[fila, 6].Value == null
                       || sheet.Cells[fila, 8].Value == null || sheet.Cells[fila, 9].Value == null
                       || sheet.Cells[fila, 10].Value == null

                       || sheet.Cells[fila, 19].Value == null || sheet.Cells[fila, 20].Value == null
                       || sheet.Cells[fila, 29].Value == null || sheet.Cells[fila, 30].Value == null
                       || sheet.Cells[fila, 39].Value == null || sheet.Cells[fila, 40].Value == null
                       || sheet.Cells[fila, 49].Value == null || sheet.Cells[fila, 50].Value == null || sheet.Cells[fila, 51].Value == null
                       || sheet.Cells[fila, 64].Value == null || sheet.Cells[fila, 65].Value == null || sheet.Cells[fila, 66].Value == null
                         
                       )
                        {
                            nFilaBla += " " + fila;
                            esValidoDx = false;
                            noBlancos = false;

                        }

                        if (noBlancos)
                        {

                            int? codProceso = null;

                            DateTime Fecha_Inicial_Dx = Convert.ToDateTime(sheet.Cells[fila, 2].Value.ToString());
                          
                            DateTime Fecha_Final_Dx = Convert.ToDateTime(sheet.Cells[fila, 3].Value.ToString());

                            DateTime FechaDx = DateTime.Now;
                            var vigencia = int.Parse(sheet.Cells[fila, 4].Value.ToString());
                            var anyoActual = DateTime.Now.Year;
                            var vigencia2=0;
                            if(vigencia<=anyoActual)
                            {
                               vigencia2 = int.Parse(sheet.Cells[fila, 4].Value.ToString());

                            }
                            else
                            {
                                nfilaV+= " " + fila;
                                esValidoDx = false;
                            }

                            if(Fecha_Inicial_Dx>DateTime.Now ||Fecha_Final_Dx>DateTime.Now   )
                            {
                                nFilaFr += " " + fila;
                                esValidoDx = false;
                            }


                            if (Fecha_Inicial_Dx > Fecha_Final_Dx )
                            {
                                nFilaRango += " " + fila;
                                esValidoDx = false;
                            }
                            
                            var sede = int.Parse(sheet.Cells[fila, 5].Value.ToString());
                            var zona = sheet.Cells[fila, 6].Value.ToString();
                        
                            var totalTraZonaLugar = int.Parse(sheet.Cells[fila, 8].Value.ToString());
                            var responsableInformacion = sheet.Cells[fila, 64].Value.ToString();
                            var Profesion_Responsable = sheet.Cells[fila, 65].Value.ToString();
                            var Tarjeta_Profesional = sheet.Cells[fila, 66].Value.ToString();
                            var nitEmpresa = sheet.Cells[fila, 1].Value.ToString();

                            if (sheet.Cells[fila, 7].Value != null)
                            {

                                codProceso = int.Parse(sheet.Cells[fila, 7].Value.ToString());
                            }

                            if(!nitEmpresa.Equals(cargue.NitEmpresa))
                            {
                                nFilaNit += " " + fila;
                                esValidoDx = false;
                            }
                            
                            EDClasificacionPeligroDx clasificacinPeligroA = new EDClasificacionPeligroDx();

                            if (sheet.Cells[fila, 9].Value != null && sheet.Cells[fila, 10].Value != null)
                            {

                                clasificacinPeligroA.idClasifiacionPeligro = int.Parse(sheet.Cells[fila, 186].Value.ToString());
                                peligros.Add(clasificacinPeligroA);
                            }

                            EDClasificacionPeligroDx clasificacinPeligroB = new EDClasificacionPeligroDx();

                            if (sheet.Cells[fila, 11].Value != null && sheet.Cells[fila, 12].Value != null)
                            {

                                clasificacinPeligroB.idClasifiacionPeligro = int.Parse(sheet.Cells[fila, 187].Value.ToString());
                                peligros.Add(clasificacinPeligroB);
                            }

                            EDClasificacionPeligroDx clasificacinPeligroC = new EDClasificacionPeligroDx();

                            if (sheet.Cells[fila, 13].Value != null && sheet.Cells[fila, 14].Value != null)
                            {

                                clasificacinPeligroC.idClasifiacionPeligro = int.Parse(sheet.Cells[fila, 188].Value.ToString());
                                peligros.Add(clasificacinPeligroC);
                            }



                            EDClasificacionPeligroDx clasificacinPeligroD = new EDClasificacionPeligroDx();

                            if (sheet.Cells[fila, 15].Value != null && sheet.Cells[fila, 16].Value != null)
                            {

                                clasificacinPeligroD.idClasifiacionPeligro = int.Parse(sheet.Cells[fila, 189].Value.ToString());
                                peligros.Add(clasificacinPeligroD);
                            }

                            EDClasificacionPeligroDx clasificacinPeligroE = new EDClasificacionPeligroDx();

                            if (sheet.Cells[fila, 17].Value != null && sheet.Cells[fila, 18].Value != null)
                            {

                                clasificacinPeligroE.idClasifiacionPeligro = int.Parse(sheet.Cells[fila, 190].Value.ToString());
                                peligros.Add(clasificacinPeligroE);
                            }

                            EDSintomatologiaDx sintomologiaA = new EDSintomatologiaDx();

                            if (sheet.Cells[fila, 19].Value != null && sheet.Cells[fila,20].Value != null)
                            {
                                sintomologiaA.Trabajadores_Sintomatologia = int.Parse(sheet.Cells[fila, 20].Value.ToString());
                                sintomologiaA.Sintomatologia = sheet.Cells[fila, 19].Value.ToString();
                                if (sintomologiaA.Trabajadores_Sintomatologia <= totalTraZonaLugar)
                                {
                                    sintomologias.Add(sintomologiaA);
                                }
                                else
                                {
                                    nfilaT += " " + fila;
                                    esValidoDx = false;
                                }

                               
                            }

                            EDSintomatologiaDx sintomologiaB = new EDSintomatologiaDx();

                            if (sheet.Cells[fila, 21].Value != null && sheet.Cells[fila, 22].Value != null)
                            {

                                sintomologiaB.Sintomatologia = sheet.Cells[fila, 21].Value.ToString();
                                sintomologiaB.Trabajadores_Sintomatologia = int.Parse(sheet.Cells[fila, 22].Value.ToString());

                                if (sintomologiaB.Trabajadores_Sintomatologia <= totalTraZonaLugar)
                                {
                                    sintomologias.Add(sintomologiaB);
                                }
                                else
                                {
                                    nfilaT += " " + fila;
                                    esValidoDx = false;
                                }

                            }

                            EDSintomatologiaDx sintomologiaC = new EDSintomatologiaDx();

                            if (sheet.Cells[fila, 23].Value != null && sheet.Cells[fila, 24].Value != null)
                            {

                                sintomologiaC.Sintomatologia = sheet.Cells[fila, 23].Value.ToString();
                                sintomologiaC.Trabajadores_Sintomatologia = int.Parse(sheet.Cells[fila, 24].Value.ToString());


                                if (sintomologiaC.Trabajadores_Sintomatologia <= totalTraZonaLugar)
                                {
                                    sintomologias.Add(sintomologiaC);
                                }
                                else
                                {
                                    nfilaT += " " + fila;
                                    esValidoDx = false;
                                }
                               
                            }


                            EDSintomatologiaDx sintomologiaD = new EDSintomatologiaDx();

                            if (sheet.Cells[fila, 25].Value != null && sheet.Cells[fila, 26].Value != null)
                            {
                                sintomologiaD.Sintomatologia = sheet.Cells[fila, 25].Value.ToString();
                                sintomologiaD.Trabajadores_Sintomatologia = int.Parse(sheet.Cells[fila, 26].Value.ToString());
                                if (sintomologiaD.Trabajadores_Sintomatologia <= totalTraZonaLugar)
                                {
                                    sintomologias.Add(sintomologiaD);
                                }
                                else
                                {
                                    nfilaT += " " + fila;
                                    esValidoDx = false;
                                }
                                
                            }

                            EDSintomatologiaDx sintomologiaE = new EDSintomatologiaDx();

                            if (sheet.Cells[fila, 27].Value != null && sheet.Cells[fila, 28].Value != null)
                            {

                                sintomologiaE.Sintomatologia = sheet.Cells[fila, 27].Value.ToString();
                                sintomologiaE.Trabajadores_Sintomatologia = int.Parse(sheet.Cells[fila, 28].Value.ToString());
                                if (sintomologiaE.Trabajadores_Sintomatologia <= totalTraZonaLugar)
                                {
                                    sintomologias.Add(sintomologiaE);
                                }
                                else
                                {
                                    nfilaT += " " + fila;
                                    esValidoDx = false;
                                }
                                
                            }


                            EDPruebasClinicasDx pruebaClinicaA = new EDPruebasClinicasDx();
                            if (sheet.Cells[fila, 29].Value != null && sheet.Cells[fila, 30].Value != null)
                            {

                                pruebaClinicaA.Prueba_Clinica = sheet.Cells[fila, 29].Value.ToString();
                                pruebaClinicaA.Trabajadores_Con_Prueba = int.Parse(sheet.Cells[fila, 30].Value.ToString());
                                if (pruebaClinicaA.Trabajadores_Con_Prueba <= totalTraZonaLugar)
                                {
                                    pruebasClinicas.Add(pruebaClinicaA);
                                }
                                else
                                {
                                    nfilaT += " " + fila;
                                    esValidoDx = false;
                                }
                                
                            }


                            EDPruebasClinicasDx pruebaClinicaB = new EDPruebasClinicasDx();
                            if (sheet.Cells[fila, 31].Value != null && sheet.Cells[fila, 32].Value != null)
                            {

                                pruebaClinicaB.Prueba_Clinica = sheet.Cells[fila, 31].Value.ToString();
                                pruebaClinicaB.Trabajadores_Con_Prueba = int.Parse(sheet.Cells[fila, 32].Value.ToString());


                               if (pruebaClinicaB.Trabajadores_Con_Prueba <= totalTraZonaLugar)
                                {
                                    pruebasClinicas.Add(pruebaClinicaB);
                                }
                                else
                                {
                                    nfilaT += " " + fila;
                                    esValidoDx = false;
                                }
                                
                                
                            }

                            EDPruebasClinicasDx pruebaClinicaC = new EDPruebasClinicasDx();
                            if (sheet.Cells[fila, 33].Value != null && sheet.Cells[fila, 34].Value != null)
                            {

                                pruebaClinicaC.Prueba_Clinica = sheet.Cells[fila, 33].Value.ToString();
                                pruebaClinicaC.Trabajadores_Con_Prueba = int.Parse(sheet.Cells[fila, 34].Value.ToString());
                                if (pruebaClinicaC.Trabajadores_Con_Prueba <= totalTraZonaLugar)
                                {
                                    pruebasClinicas.Add(pruebaClinicaC);
                                }
                                else
                                {
                                    nfilaT += " " + fila;
                                    esValidoDx = false;
                                }
                                
                                
                            }

                            EDPruebasClinicasDx pruebaClinicaD = new EDPruebasClinicasDx();
                            if (sheet.Cells[fila, 35].Value != null && sheet.Cells[fila, 36].Value != null)
                            {

                                pruebaClinicaD.Prueba_Clinica = sheet.Cells[fila, 35].Value.ToString();
                                pruebaClinicaD.Trabajadores_Con_Prueba = int.Parse(sheet.Cells[fila, 36].Value.ToString());
                                if (pruebaClinicaD.Trabajadores_Con_Prueba <= totalTraZonaLugar)
                                {
                                    pruebasClinicas.Add(pruebaClinicaD);
                                }
                                else
                                {
                                    nfilaT += " " + fila;
                                    esValidoDx = false;
                                }
                               
                            }

                            EDPruebasClinicasDx pruebaClinicaE = new EDPruebasClinicasDx();
                            if (sheet.Cells[fila, 37].Value != null && sheet.Cells[fila, 38].Value != null)
                            {

                                pruebaClinicaE.Prueba_Clinica = sheet.Cells[fila, 37].Value.ToString();
                                pruebaClinicaE.Trabajadores_Con_Prueba = int.Parse(sheet.Cells[fila, 38].Value.ToString());
                                if (pruebaClinicaE.Trabajadores_Con_Prueba <= totalTraZonaLugar)
                                {
                                    pruebasClinicas.Add(pruebaClinicaE);
                                }
                                else
                                {
                                    nfilaT += " " + fila;
                                    esValidoDx = false;
                                }
                                
                            }




                            EDPruebasPClinicasDx pruebaParaClinicaA = new EDPruebasPClinicasDx();
                           
                            if (sheet.Cells[fila, 39].Value != null && sheet.Cells[fila, 40].Value != null)
                            {

                                pruebaParaClinicaA.Prueba_P_Clinica = sheet.Cells[fila, 39].Value.ToString();
                                pruebaParaClinicaA.Trabajadores_Con_Prueba_P = int.Parse(sheet.Cells[fila, 40].Value.ToString());
                                if (pruebaParaClinicaA.Trabajadores_Con_Prueba_P <= totalTraZonaLugar)
                                {

                                    pruebasParaClinicas.Add(pruebaParaClinicaA);
                                }
                                else
                                {
                                    nfilaT += " " + fila;
                                    esValidoDx = false;
                                }
                                
                            }

                            EDPruebasPClinicasDx pruebaParaClinicaB = new EDPruebasPClinicasDx();

                            if (sheet.Cells[fila, 41].Value != null && sheet.Cells[fila, 42].Value != null)
                            {

                                pruebaParaClinicaB.Prueba_P_Clinica = sheet.Cells[fila, 41].Value.ToString();
                                pruebaParaClinicaB.Trabajadores_Con_Prueba_P = int.Parse(sheet.Cells[fila, 42].Value.ToString());
                                if (pruebaParaClinicaB.Trabajadores_Con_Prueba_P <= totalTraZonaLugar)
                                {

                                    pruebasParaClinicas.Add(pruebaParaClinicaB);
                                }
                                else
                                {
                                    nfilaT += " " + fila;
                                    esValidoDx = false;
                                }
                            }

                            EDPruebasPClinicasDx pruebaParaClinicaC = new EDPruebasPClinicasDx();

                            if (sheet.Cells[fila, 43].Value != null && sheet.Cells[fila, 44].Value != null)
                            {

                                pruebaParaClinicaC.Prueba_P_Clinica = sheet.Cells[fila, 43].Value.ToString();
                                pruebaParaClinicaC.Trabajadores_Con_Prueba_P = int.Parse(sheet.Cells[fila, 44].Value.ToString());
                                if (pruebaParaClinicaC.Trabajadores_Con_Prueba_P <= totalTraZonaLugar)
                                {

                                    pruebasParaClinicas.Add(pruebaParaClinicaC);
                                }
                                else
                                {
                                    nfilaT += " " + fila;
                                    esValidoDx = false;
                                }
                            }


                            EDPruebasPClinicasDx pruebaParaClinicaD = new EDPruebasPClinicasDx();

                            if (sheet.Cells[fila, 45].Value != null && sheet.Cells[fila, 46].Value != null)
                            {

                                pruebaParaClinicaD.Prueba_P_Clinica = sheet.Cells[fila, 45].Value.ToString();
                                pruebaParaClinicaD.Trabajadores_Con_Prueba_P = int.Parse(sheet.Cells[fila, 46].Value.ToString());
                                if (pruebaParaClinicaD.Trabajadores_Con_Prueba_P <= totalTraZonaLugar)
                                {

                                    pruebasParaClinicas.Add(pruebaParaClinicaD);
                                }
                                else
                                {
                                    nfilaT += " " + fila;
                                    esValidoDx = false;
                                }
                            }

                            EDPruebasPClinicasDx pruebaParaClinicaE = new EDPruebasPClinicasDx();

                            if (sheet.Cells[fila, 47].Value != null && sheet.Cells[fila, 48].Value != null)
                            {

                                pruebaParaClinicaE.Prueba_P_Clinica = sheet.Cells[fila, 47].Value.ToString();
                                pruebaParaClinicaE.Trabajadores_Con_Prueba_P = int.Parse(sheet.Cells[fila, 48].Value.ToString());
                                pruebaParaClinicaD.Trabajadores_Con_Prueba_P = int.Parse(sheet.Cells[fila, 46].Value.ToString());
                                if (pruebaParaClinicaE.Trabajadores_Con_Prueba_P <= totalTraZonaLugar)
                                {

                                    pruebasParaClinicas.Add(pruebaParaClinicaE);
                                }
                                else
                                {
                                    nfilaT += " " + fila;
                                    esValidoDx = false;
                                }
                            }

                            EDDiagnosticoCie10Dx diagnosticoCIE100A = new EDDiagnosticoCie10Dx();
                           
                            if (sheet.Cells[fila, 49].Value != null && sheet.Cells[fila, 51].Value != null)
                            {


                                var valDiagnostic = sheet.Cells[fila, 50].Value.ToString();
                                if (!valDiagnostic.Equals("NO ES VÁLIDO"))
                                {

                                    diagnosticoCIE100A.IdDiagnostico = int.Parse(sheet.Cells[fila, 191].Value.ToString());
                                    diagnosticoCIE100A.NumeroTrabajadoresConDiagnostico = int.Parse(sheet.Cells[fila, 51].Value.ToString());
                                    if (diagnosticoCIE100A.NumeroTrabajadoresConDiagnostico <= totalTraZonaLugar)
                                    {

                                        diagnosticosCie.Add(diagnosticoCIE100A);
                                    }
                                    else
                                    {
                                        nfilaT += " " + fila;
                                        esValidoDx = false;
                                    }
                                   
                                }
                                else
                                {
                                    nFilaCodDiag += " " + fila;
                                    esValidoDx = false;
                                }
                          
                            }

                            EDDiagnosticoCie10Dx diagnosticoCIE100B = new EDDiagnosticoCie10Dx();

                            if (sheet.Cells[fila, 52].Value != null && sheet.Cells[fila, 54].Value != null)
                            {


                                var valDiagnostic = sheet.Cells[fila, 53].Value.ToString();
                                if (!valDiagnostic.Equals("NO ES VÁLIDO"))
                                {

                                    diagnosticoCIE100B.IdDiagnostico = int.Parse(sheet.Cells[fila, 192].Value.ToString());
                                    diagnosticoCIE100B.NumeroTrabajadoresConDiagnostico = int.Parse(sheet.Cells[fila, 54].Value.ToString());

                                    if (diagnosticoCIE100B.NumeroTrabajadoresConDiagnostico <= totalTraZonaLugar)
                                    {

                                        diagnosticosCie.Add(diagnosticoCIE100B);
                                    }
                                    else
                                    {
                                        nfilaT += " " + fila;
                                        esValidoDx = false;
                                    }
                                }
                                else
                                {
                                    nFilaCodDiag += " " + fila;
                                    esValidoDx = false;
                                }

                            }


                            EDDiagnosticoCie10Dx diagnosticoCIE100C = new EDDiagnosticoCie10Dx();

                            if (sheet.Cells[fila, 55].Value != null && sheet.Cells[fila, 57].Value != null)
                            {


                                var valDiagnostic = sheet.Cells[fila, 56].Value.ToString();
                                if (!valDiagnostic.Equals("NO ES VÁLIDO"))
                                {

                                    diagnosticoCIE100C.IdDiagnostico = int.Parse(sheet.Cells[fila, 193].Value.ToString());
                                    diagnosticoCIE100C.NumeroTrabajadoresConDiagnostico = int.Parse(sheet.Cells[fila, 57].Value.ToString());

                                    if (diagnosticoCIE100C.NumeroTrabajadoresConDiagnostico <= totalTraZonaLugar)
                                    {

                                        diagnosticosCie.Add(diagnosticoCIE100C);
                                    }
                                    else
                                    {
                                        nfilaT += " " + fila;
                                        esValidoDx = false;
                                    }
                                }
                                else
                                {
                                    nFilaCodDiag += " " + fila;
                                    esValidoDx = false;
                                }

                            }

                            EDDiagnosticoCie10Dx diagnosticoCIE100D = new EDDiagnosticoCie10Dx();

                            if (sheet.Cells[fila, 58].Value != null && sheet.Cells[fila, 60].Value != null)
                            {


                                var valDiagnostic = sheet.Cells[fila, 59].Value.ToString();
                                if (!valDiagnostic.Equals("NO ES VÁLIDO"))
                                {

                                    diagnosticoCIE100D.IdDiagnostico = int.Parse(sheet.Cells[fila, 194].Value.ToString());
                                    diagnosticoCIE100D.NumeroTrabajadoresConDiagnostico = int.Parse(sheet.Cells[fila, 60].Value.ToString());
                                    if (diagnosticoCIE100D.NumeroTrabajadoresConDiagnostico <= totalTraZonaLugar)
                                    {

                                        diagnosticosCie.Add(diagnosticoCIE100D);
                                    }
                                    else
                                    {
                                        nfilaT += " " + fila;
                                        esValidoDx = false;
                                    }
                                }
                                else
                                {
                                    nFilaCodDiag += " " + fila;
                                    esValidoDx = false;
                                }
                            }

                            EDDiagnosticoCie10Dx diagnosticoCIE100E = new EDDiagnosticoCie10Dx();

                            if (sheet.Cells[fila, 61].Value != null && sheet.Cells[fila, 63].Value != null)
                            {


                                var valDiagnostic = sheet.Cells[fila, 62].Value.ToString();
                                if (!valDiagnostic.Equals("NO ES VÁLIDO"))
                                {

                                    diagnosticoCIE100E.IdDiagnostico = int.Parse(sheet.Cells[fila, 195].Value.ToString());
                                    diagnosticoCIE100E.NumeroTrabajadoresConDiagnostico = int.Parse(sheet.Cells[fila, 63].Value.ToString());

                                    if (diagnosticoCIE100E.NumeroTrabajadoresConDiagnostico <= totalTraZonaLugar)
                                    {

                                        diagnosticosCie.Add(diagnosticoCIE100E);
                                    }
                                    else
                                    {
                                        nfilaT += " " + fila;
                                        esValidoDx = false;
                                    }
                                }
                                else
                                {
                                    nFilaCodDiag += " " + fila;
                                    esValidoDx = false;
                                }
                            }
                        

                            if (!sedes.Exists(s => s.IdSede == sede))
                            {
                                nFilaS += " " + fila;

                                esValidoDx = false;
                            }


                            if (codProceso > 0)
                            {
                                if (!procesos.Exists(p => p.Id_Proceso == codProceso))
                                {
                                    nfilaP += " " + fila;

                                    esValidoDx = false;

                                }

                            }


                            if (esValidoDx)
                            {
                                diagnostico.nitEmpresa = nitEmpresa;
                                diagnostico.ZonaLugar = zona;
                                diagnostico.Fecha_Inicial_Dx = Fecha_Inicial_Dx;
                                diagnostico.Fecha_Final_Dx = Fecha_Final_Dx;
                                diagnostico.vigencia = vigencia2;
                                diagnostico.Pk_Id_Sede = sede;
                                diagnostico.Procesos = codProceso;
                                diagnostico.NumeroTrabajadoresLugar = totalTraZonaLugar;
                                diagnostico.Responsable_informacion = responsableInformacion;
                                diagnostico.Profesion_Responsable = Profesion_Responsable;
                                diagnostico.Tarjeta_Profesional = Tarjeta_Profesional;
                                diagnostico.EDClasificacionPeligroDx = peligros;
                                diagnostico.EDSintomatologiaDx = sintomologias;
                                diagnostico.EDPruebasClinicasDx = pruebasClinicas;
                                diagnostico.EDPruebasPClinicasDx = pruebasParaClinicas;
                                diagnostico.EDDiagnosticoCie10Dx = diagnosticosCie;
                                diagnostico.FechaCreacionDiagnostico = FechaDx;
                                diagnosticos.Add(diagnostico);

                            }
                        }

                    }
                    else
                    {
                        if(fila==2)
                        {
                            nFilaPV += "" + fila;
                            esValidoDx = false;
                        }
                        break;
                    }
                }


                //Listo aca va mensajes y resto


                if (nFilaPV.Equals(""))
                {
                    mensajePlantillaVacia = "";
                }
                else
                {
                    mensajePlantillaVacia +=  "\n";
                }
               

                if (nFilaBla.Equals(""))
                {
                    mensajeEspBlancos = "";
                }
                else
                {
                    mensajeEspBlancos += nFilaBla + "\n";
                }



                if (nFilaS.Equals(""))
                {
                    mensajeSede = "";
                }
                else
                {
                    mensajeSede += nFilaS + "\n";
                }

                if (nfilaP.Equals(""))
                {
                    mensajeProceso = "";
                }
                else
                {
                    mensajeProceso += nfilaP + "\n";
                }

                if (nfilaV.Equals(""))
                {
                    mensajeVigencia = "";
                }
                else
                {
                    mensajeVigencia += nfilaV + "\n";
                }


                if (nfilaT.Equals(""))
                {
                    mensajeTamano = "";
                }
                else
                {
                    mensajeTamano += nfilaT + "\n";
                }

                if (nFilaRango.Equals(""))
                {
                    mensajeRangoFecha = "";
                }
                else
                {
                    mensajeRangoFecha += nFilaRango + "\n";
                }


                if (nFilaRango.Equals(""))
                {
                    mensajeRangoFecha = "";
                }
                else
                {
                    mensajeRangoFecha += nFilaRango + "\n";
                }

                if (nFilaFr.Equals(""))
                {
                    mensajesFechas = "";
                }
                else
                {
                    mensajesFechas += nFilaFr + "\n";
                }

                if (nFilaNit.Equals(""))
                {
                    mensajeNitEmpresa = "";
                }
                else
                {
                    mensajeNitEmpresa += nFilaNit + "\n";
                }

                if (nFilaCodDiag.Equals(""))
                {
                    mensajeCodDiagnostico = "";
                }
                else
                {
                    mensajeCodDiagnostico += nFilaCodDiag + "\n";
                }



                mensaje = mensajePlantillaVacia + " " + mensajeEspBlancos + " " + mensajeSede + mensajeProceso + " " + mensajeVigencia + " " + mensajeTamano + " " + mensajeRangoFecha + " " + mensajesFechas + " " + mensajeNitEmpresa + " " + mensajeCodDiagnostico + " ";


                if (esValidoDx == false)
                {
                    edCargue.Message = mensaje;
                    return edCargue;
                }


                if (esValidoDx == true)
                {
                    resu = dia.InsertarCargueMasivoDx(diagnosticos);
                }
                else
                {
                    edCargue.Message = "No se puede realizar la carga,revise e intente de nuevo";
                    return edCargue;
                }

                if (resu)
                {
                    if (esValidoDx)
                    {
                        edCargue.Message = "OK";
                    }
                    else
                    {
                        edCargue.Message = mensaje;
                    }
                }
                else
                {
                    edCargue.Message = "El proceso de cargue falló";
                }

            }

            catch (Exception ex)
            {
                RegistraLog registraLog = new RegistraLog();
                registraLog.RegistrarError(typeof(LNCarguePerfilSocioDemografico), string.Format("Error en el método WorksheetToDataTable {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                edCargue.Message = "El proceso falló: revise la información y por favor intentelo de nuevo.";
                return edCargue;
            }

            return edCargue;
        }

       



        private bool ValidarNombreColumnas(ExcelWorksheet sheet)
        {
            if (sheet.Cells[1, 1].Value.Equals("Nit de La empresa") && sheet.Cells[1, 2].Value.Equals("Fecha inicial del período de Evaluación Médica Ocupacional/Autoreporte CS/PVE")
                && sheet.Cells[1, 3].Value.Equals("Fecha final del período de Evaluación Médica Ocupacional/Autoreporte CS/PVE") && sheet.Cells[1, 4].Value.Equals("Vigencia del Diagnóstico") && sheet.Cells[1, 5].Value.Equals("Código de la Sede") && sheet.Cells[1, 6].Value.Equals("Zona o Lugar")
                && sheet.Cells[1, 7].Value.Equals("Código del Proceso") && sheet.Cells[1, 8].Value.Equals("Total de Trabajadores de la zona o lugar")


                && sheet.Cells[1, 18].Value.Equals("Descripción del Peligro E")
                && sheet.Cells[1, 28].Value.Equals("Número de trabajadores con Sintomatología E")
                && sheet.Cells[1, 38].Value.Equals("Número de trabajadores con Anormalidad en Pruebas Clínicas E")
                && sheet.Cells[1, 48].Value.Equals("Número de trabajadores con Anormalidad en Pruebas Para-Clínicas E")
                && sheet.Cells[1, 63].Value.Equals("Número de trabajadores con el Diagnóstico CIE10 E")
                && sheet.Cells[1, 64].Value.Equals("Responsable de la Información")
                && sheet.Cells[1, 65].Value.Equals("Profesión del responsable de la información")
                && sheet.Cells[1, 66].Value.Equals("Tarjeta profesional del responsable de la Información")


               
                )
                return true;
            else
                return false;
        }
    }


}