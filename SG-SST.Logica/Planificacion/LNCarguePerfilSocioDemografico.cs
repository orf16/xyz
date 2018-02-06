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

    public class LNCarguePerfilSocioDemografico
    {
        private static IEmpresa em = IMEmpresa.Empresa();
        private static IClasificacionPeligros cp = IMIdentificacionPeligros.ClasificacionPeligros();
        private static IPeligro zona = IMPeligro.Peligro();
        private static IPerfilSocioDemografico per = IMPerfilSocioDemografico.PerfilSocioDemografico();
        private static IDepartamento dpto = IMAusentismo.Departamento();
        private static IMunicipio mp = IMAusentismo.Municipio();
        private SG_SSTContext db = new SG_SSTContext();
        public int total;
        public string nitEmpresa;
        string primerNombre, segundoNombre, primerApellido, segundoAPellido, tipoDocumento, direccion;
        int edad, meses, anyos, dias;
        DateTime fechaNacimiento;


        public EDCarguePerfil CargarPlantillaCarguePlanificacion(EDCarguePerfil cargueP)
        {
            nitEmpresa = cargueP.NitEmpresa;
            RegistraLog registraLog = new RegistraLog();
            EDCarguePerfil edCargue = new EDCarguePerfil();
            try
            {
                edCargue = CargarArchivoPlanificacion(cargueP);
            }
            catch (Exception ex)
            {
                registraLog.RegistrarError(typeof(LNCargue), string.Format("Error en el método CargarPlantillaCargueAusentismo  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
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
        public EDCarguePerfil CargarArchivoPlanificacion(EDCarguePerfil cargue)
        {
            RegistraLog registraLog = new RegistraLog();
            EDCarguePerfil edCargue = new EDCarguePerfil();
            try
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(cargue.path)))
                {
                    var sheet = package.Workbook.Worksheets["PlantillaPerfilSocioDemografico"];
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
            List<EDPerfilSocioDemografico> perfiles = new List<EDPerfilSocioDemografico>();

            string mensaje = "";
            string nFilaD = "";
            string nFilaS = "";
            string nFilaBla = "";
            string nfilaRep = "";
            string nfilaP = "";
            string mensajeEspBlancos = "Existen campos en blanco, es obligatorio diligenciar todas las columnas hasta la columna W, la columna R(Cod Proceso) no es obligatoria , revisar la(s) fila(s).";
            string mensajeDocumento = "Revisar la columna A(Número Identificación) ya que el documento no pertenece a la empresa,revisar la(s) fila(s)";
            string mensajeSede = "Revisar la  Columna  s(CodSede) ya que la sede no pertenece a la empresa, revisar la(s) fila(s)";
            string mensajeRepetidos = "No se pudo realizar el cargue, debido a que algunos perfiles a cargar ya existen, revisar la(s) fila(s)";
            string mensajeProceso = "Revisar la  Columna  R(Cod Proceso) ya que el proceso no pertenece a la empresa, revisar la(s) fila(s)";
            string mensajeRepetidosPlantilla="";
           
            try
            {
                var dt = new DataTable();
                var rowCnt = 0;

                rowCnt = sheet.Dimension.End.Row;


                bool noBlancos = true;
                bool esValidoPerfil = true;
                
                bool resu = false;
                bool documentoValido = false;
                //Verificamos los nombres de las columnas
                // Loop through Columns
                List<EDProceso> procesos = per.ObtenerProcesoEmpresa(cargue.NitEmpresa);
                List<EDSede> sedes = em.ObtenernerSedesPorEmpresa(cargue.NitEmpresa);
                for (var fila = 2; fila <= rowCnt; fila++)
                {

                    
                    string numeroIdentificacion;
            
                    //List<Proceso> procesos = procesoServicios.ObtenerProcesosPrincipales(cargue.NitEmpresa);
                    List<EDCondicionesRiesgoPerfil> condincionesRiesgo = new List<EDCondicionesRiesgoPerfil>();
                    EDCondicionesRiesgoPerfil condicionRiesgo = new EDCondicionesRiesgoPerfil();
                    EDPerfilSocioDemografico perfil = new EDPerfilSocioDemografico();

                    bool companeroPer = false;
                    bool tieneHijos = false;
                    //g string NumeroIdentificacion = sheet.Cells[fila, 1].Value.ToString();
                    if (sheet.Cells[fila, 1].Value != null)
                    {


                        if (sheet.Cells[fila, 1].Value == null || sheet.Cells[fila, 2].Value == null || sheet.Cells[fila, 3].Value == null
                       || sheet.Cells[fila, 4].Value == null || sheet.Cells[fila, 5].Value == null || sheet.Cells[fila, 6].Value == null
                       || sheet.Cells[fila, 7].Value == null || sheet.Cells[fila, 8].Value == null || sheet.Cells[fila, 9].Value == null
                       || sheet.Cells[fila, 10].Value == null || sheet.Cells[fila, 11].Value == null || sheet.Cells[fila, 12].Value == null
                       || sheet.Cells[fila, 13].Value == null || sheet.Cells[fila, 14].Value == null || sheet.Cells[fila, 15].Value == null
                       || sheet.Cells[fila, 16].Value == null || sheet.Cells[fila, 17].Value == null || sheet.Cells[fila, 19].Value == null
                       || sheet.Cells[fila, 20].Value == null || sheet.Cells[fila, 21].Value == null || sheet.Cells[fila, 22].Value == null
                       || sheet.Cells[fila, 23].Value == null)
                        {
                            nFilaBla += " " + fila;
                            esValidoPerfil = false;
                            noBlancos = false;

                        }

                        if (noBlancos)
                        {

                           int? codProceso=null;
                            numeroIdentificacion = sheet.Cells[fila, 1].Value.ToString();
                            string gradoEscolaridad = sheet.Cells[fila, 2].Value.ToString();
                            string ingresos = sheet.Cells[fila, 3].Value.ToString();
                            int municipioResidencia = int.Parse(sheet.Cells[fila, 44].Value.ToString());
                            string companeroPermanente = sheet.Cells[fila, 6].Value.ToString();
                            if (companeroPermanente.Equals("SI"))
                            {
                                companeroPer = true;
                            }
                            else
                            {
                                companeroPer = false;
                            }

                            string hijos = sheet.Cells[fila, 7].Value.ToString();

                            if (hijos.Equals("SI"))
                            {
                                tieneHijos = true;
                            }
                            else
                            {
                                tieneHijos = false;
                            }
                            int estratoSocioEconomico = int.Parse(sheet.Cells[fila, 8].Value.ToString());
                            int estadoCivil = int.Parse(sheet.Cells[fila, 45].Value.ToString());
                            int etnia = int.Parse(sheet.Cells[fila, 46].Value.ToString());
                            string sexo = sheet.Cells[fila, 11].Value.ToString();
                            int vinculacionLaboral = int.Parse(sheet.Cells[fila, 47].Value.ToString());
                            string turnoTrabajo = sheet.Cells[fila, 13].Value.ToString();
                            DateTime fechaUltimoCargo = Convert.ToDateTime(sheet.Cells[fila, 14].Value.ToString());
                            string caracteristicasFisicas = sheet.Cells[fila, 15].Value.ToString();
                            string caracteristicasPsicologicas = sheet.Cells[fila, 16].Value.ToString();
                            string evaluacionMedicaRequerida = sheet.Cells[fila, 17].Value.ToString();
                           
                            
                        if (sheet.Cells[fila, 18].Value != null)
                        {

                           codProceso =int.Parse(sheet.Cells[fila, 18].Value.ToString());
                        }


                        var Otro = "Otro";

                            int sede = int.Parse(sheet.Cells[fila, 19].Value.ToString());
                            string zona = sheet.Cells[fila, 20].Value.ToString();
                            int clasificacionPeligroA = int.Parse(sheet.Cells[fila, 49].Value.ToString());
                            
                    
                            string tiempoExposicionA = sheet.Cells[fila, 23].Value.ToString();
                            if (clasificacionPeligroA == 46)
                            {
                                condicionRiesgo.Otro = Otro;
                            }
                           


                            condicionRiesgo.FK_Clasificacion_De_Peligro = clasificacionPeligroA;
                            condicionRiesgo.tiempoExpos = tiempoExposicionA;
                            condincionesRiesgo.Add(condicionRiesgo);

                            EDCondicionesRiesgoPerfil condicionRiesgo2 = new EDCondicionesRiesgoPerfil();

                            if (sheet.Cells[fila, 25].Value != null && sheet.Cells[fila, 26].Value != null)
                            {

                                int clasificacionPeligroB = int.Parse(sheet.Cells[fila, 50].Value.ToString());
                                if(clasificacionPeligroB==46)
                                {
                                    condicionRiesgo2.Otro = Otro;
                                }
                                string tiempoExposicionB = sheet.Cells[fila, 26].Value.ToString();
                                condicionRiesgo2.FK_Clasificacion_De_Peligro = clasificacionPeligroB;
                                condicionRiesgo2.tiempoExpos = tiempoExposicionB;
                                condincionesRiesgo.Add(condicionRiesgo2);

                            }
                            EDCondicionesRiesgoPerfil condicionRiesgo3 = new EDCondicionesRiesgoPerfil();

                            if (sheet.Cells[fila, 28].Value != null && sheet.Cells[fila, 29].Value != null)
                            {

                           
                                int clasificacionPeligroC = int.Parse(sheet.Cells[fila, 51].Value.ToString());
                                if (clasificacionPeligroC == 46)
                                {
                                    condicionRiesgo3.Otro = Otro;
                                }
                                string tiempoExposicionC = sheet.Cells[fila, 29].Value.ToString();
                                condicionRiesgo3.FK_Clasificacion_De_Peligro = clasificacionPeligroC;
                                condicionRiesgo3.tiempoExpos = tiempoExposicionC;
                                condincionesRiesgo.Add(condicionRiesgo3);

                            }



                            documentoValido = ValidarDocumento(numeroIdentificacion,cargue);

                            if (documentoValido == false)
                            {
                                nFilaD += " " + fila;

                                esValidoPerfil = false;
                            }


                            //registros repetidos
                            var perfilesdb = db.Tbl_PerfilSocioDemograficoPlanificacion.Where(e => e.PK_Numero_Documento_Empl.Equals(numeroIdentificacion)).FirstOrDefault();


                            if (perfilesdb != null)
                            {
                                nfilaRep += " " + fila;
                                esValidoPerfil = false;
                            }


                            if (!sedes.Exists(s => s.IdSede == sede))
                            {
                                nFilaS += " " + fila;

                                esValidoPerfil = false;
                            }

                            if(codProceso>0)
                            {
                                if (!procesos.Exists(p => p.Id_Proceso == codProceso))
                                {
                                    nfilaP += " " + fila;

                                    esValidoPerfil = false;

                                }

                            }


                            if (esValidoPerfil)
                            {
                                perfil.condicionesRiesgo = condincionesRiesgo;
                                perfil.PK_Numero_Documento_Empl = numeroIdentificacion;
                                perfil.GradoEscolaridad = gradoEscolaridad;

                                perfil.Ingresos = ingresos;
                                perfil.Fk_Id_Municipio = municipioResidencia;
                                perfil.Conyuge = companeroPer;
                                perfil.Hijos = tieneHijos;
                                perfil.FK_Estrato = estratoSocioEconomico;
                                perfil.FK_Estado_Civil = estadoCivil;
                                perfil.FK_Etnia = etnia;
                                perfil.Sexo = sexo;
                                perfil.FK_VinculacionLaboral = vinculacionLaboral;
                                perfil.TurnoTrabajo = turnoTrabajo;
                                perfil.FechaIngresoUltimoCargo = fechaUltimoCargo;
                                perfil.caracteristicasFisicas = caracteristicasFisicas;
                                perfil.caracteristicasPsicologicas = caracteristicasPsicologicas;
                                perfil.evaluacionesMedicasRequeridas = evaluacionMedicaRequerida;
                                perfil.Pk_Id_Sede = sede;
                                perfil.Procesos =codProceso;
                                perfil.ZonaLugar = zona;
                                perfil.nitEmpresa = cargue.NitEmpresa;
                                perfiles.Add(perfil);
                            }
                        }

                    }
                        else{
                            break;
                        }
                }

         
                //Listo aca va mensajes y resto


                string[] documento = new string[perfiles.Count()];

                for (int i = 0; i < perfiles.Count(); i++)
                {
                    documento[i] = Convert.ToString(perfiles[i].PK_Numero_Documento_Empl);
                }

                bool resultado = documento.Distinct().Count() == documento.Length;
                
                if(resultado==false)
                {
                    mensajeRepetidosPlantilla = "Revisar la Columna A(Número Identificación) ya que existen registros repetidos" + "\n";
                    esValidoPerfil = false;
                }
                if (nfilaRep.Equals(""))
                {
                    mensajeRepetidos = "";
                }
                else
                {
                    mensajeRepetidos += nfilaRep + "\n";

                }

                if (nFilaBla.Equals(""))
                {
                    mensajeEspBlancos = "";
                }
                else
                {
                    mensajeEspBlancos += nFilaBla + "\n";
                }


                if (nFilaD.Equals(""))
                {
                    mensajeDocumento = "";
                }
                else
                {
                    mensajeDocumento += nFilaD + "\n";
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

                mensaje = mensajeRepetidosPlantilla+" "+ mensajeRepetidos + " " + mensajeEspBlancos + " " + mensajeDocumento + " " + mensajeSede + mensajeProceso + " ";

         
                if (esValidoPerfil == false)
                {
                    edCargue.Message = mensaje;
                    return edCargue;
                }


                if (esValidoPerfil == true)
                {
                    resu = per.InsertarCargueMasivoPerfil(perfiles);
                }
                else
                {
                    edCargue.Message = "No se puede realizar la carga,revise e intente de nuevo";
                    return edCargue;
                }

                if (resu)
                {
                    if (esValidoPerfil)
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

        public static int CalcularEdad(DateTime fechaNacimiento)
        {
            // Obtiene la fecha actual:
            DateTime fechaActual = DateTime.Today;

            // Comprueba que la se haya introducido una fecha válida; si
            // la fecha de nacimiento es mayor a la fecha actual se muestra mensaje
            // de advertencia:
            if (fechaNacimiento > fechaActual)
            {
                Console.WriteLine("La fecha de nacimiento es mayor que la actual.");
                return -1;
            }
            else
            {
                int edad = fechaActual.Year - fechaNacimiento.Year;

                // Comprueba que el mes de la fecha de nacimiento es mayor
                // que el mes de la fecha actual:
                if (fechaNacimiento.Month > fechaActual.Month)
                {
                    --edad;
                }

                return edad;
            }
        }



        public bool ValidarDocumento(string documento, EDCarguePerfil cargue)
        {
         


            try
            {
                var cliente = new RestSharp.RestClient(cargue.rutaServicio);
                var request = new RestRequest("wssst/afiliadoEmpresaActivo?", RestSharp.Method.GET);
         

               request.Parameters.Clear();
                request.AddParameter("tpEm", cargue.SiglaTipoDocumentoEmpresa);
                request.AddParameter("docEm", cargue.NitEmpresa);
                request.AddParameter("tpAfiliado", "cc");
                request.AddParameter("docAfiliado", documento);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");



                //se omite la validación de certificado de SSL
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                IRestResponse<List<EDPerfilSocioDemograficoWS>> response = cliente.Execute<List<EDPerfilSocioDemograficoWS>>(request);
                var result = response.Content;
                var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EDPerfilSocioDemograficoWS>>(result);

                var nitEmpresaU = "";
                nitEmpresaU = respuesta[0].documentoEmp;
                if (nitEmpresaU.Equals(nitEmpresa))
                {

                    

                    return true;


                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void calcularCamposEdad(DateTime fechaActual, DateTime fechaNacimiento)
        {
            //Fecha Actual
          
            int diaA = int.Parse(fechaActual.ToString("dd"));
            int mesA = int.Parse(fechaActual.ToString("MM"));
            int anyoA = int.Parse(fechaActual.ToString("yyyy"));
            // Fecha Nacimiento

            int diaN = fechaNacimiento.Day;
            int mesN = fechaNacimiento.Month;
            int anyoN = fechaNacimiento.Year;
           
            //Años- mes-dia de fecha nacimiento
         
            
            // Edad
            edad = anyoA - anyoN;
            if (edad != 0)
            {

                if (mesA < mesN)
                {
                    edad--;
                }
                if ((mesN == mesA) && (diaA < diaN))
                {
                    edad--;
                }
            }

        }

        public string calcularGrupoEtario(int edad)
        {
            string grupoEtario = "";

            if (edad < 18)
            {

                grupoEtario = "Menor de Edad";

            }
            else if (edad >= 18 && edad < 36)
            {

                grupoEtario = "18 a 35 años";

            }
            else if (edad >= 36 && edad < 51)
            {
                grupoEtario = "36 a 50 años";


            }
            else if (edad >= 51 && edad < 65)
            {
                grupoEtario = "51 a 64 años";

            }
            else
            {
                grupoEtario = "Mayores a 65 años";

            }
            return grupoEtario;

        }

        private bool ValidarNombreColumnas(ExcelWorksheet sheet)
        {
            if (sheet.Cells[1, 1].Value.Equals("Número Identificación") && sheet.Cells[1, 2].Value.Equals("Grado De Escolaridad")
                && sheet.Cells[1, 3].Value.Equals("Ingresos") && sheet.Cells[1, 4].Value.Equals("Departamento de Residencia") && sheet.Cells[1, 5].Value.Equals("Municipio de Residencia") && sheet.Cells[1, 6].Value.Equals("Compañero Permanente")
                && sheet.Cells[1, 7].Value.Equals("Hijos") && sheet.Cells[1, 8].Value.Equals("Estrato Socio Economico") && sheet.Cells[1, 9].Value.Equals("Estado Civil") && sheet.Cells[1, 10].Value.Equals("Etnia")
                && sheet.Cells[1, 11].Value.Equals("Sexo") && sheet.Cells[1, 12].Value.Equals("Vinculación Laboral") && sheet.Cells[1, 13].Value.Equals("Turno De Trabajo")


                && sheet.Cells[1, 14].Value.Equals("FechaUltimoCargo(DD/MM/YYYY)") && sheet.Cells[1, 15].Value.Equals("Características Físicas para el desempeño del cargo") && sheet.Cells[1, 16].Value.Equals("Características Psicológicas para el desempeño del cargo")
                && sheet.Cells[1, 17].Value.Equals("Evaluación médica requerida para el desempeño del cargo") && sheet.Cells[1, 18].Value.Equals("Cod Proceso") && sheet.Cells[1, 19].Value.Equals("Cod Sede") && sheet.Cells[1, 20].Value.Equals("Zona/Lugar") && sheet.Cells[1, 21].Value.Equals("Clasificación Peligro A") && sheet.Cells[1, 22].Value.Equals("Descripción Peligro A")


                && sheet.Cells[1, 23].Value.Equals("Tiempo de Exposición  en  Meses A") && sheet.Cells[1, 24].Value.Equals("Clasificación Peligro B") && sheet.Cells[1, 25].Value.Equals("Descripción Peligro B")
                && sheet.Cells[1, 26].Value.Equals("Tiempo de Exposición  en  Meses B") && sheet.Cells[1, 27].Value.Equals("Clasificación Peligro C") && sheet.Cells[1, 28].Value.Equals("Descripción Peligro C") && sheet.Cells[1, 29].Value.Equals("Tiempo de Exposición  en  Meses C") 

                )
                return true;
            else
                return false;
        }
    }


}
