using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.InterfazManager.Empresas;
using SG_SST.Interfaces.Empresas;
using SG_SST.Models.Empresas;
using System.Data;
using SG_SST.Interfaces.Usuarios;
using SG_SST.InterfazManager.Usuarios;
using System.Globalization;

namespace SG_SST.Logica.Empresas
{
    public class LNRelacionesLaborales
    {
        private static IRelacionesLaborales em = IMEmpresa.RelacionesLaborales();
        public List<EDTipos> lstEstadosEmpleado = new List<EDTipos>();
        public List<EDTipos> lstTiposCotizante = new List<EDTipos>();
        public List<EDTipos> lstRazonesSociales = new List<EDTipos>();
        public List<EDTipos> lstTipoInconsistencia = new List<EDTipos>();
        public List<EDTipos> lstTipoOcupaciones = new List<EDTipos>();

        private static IUsuario us = IMUsuario.UsuarioSesion();

        public string validacion_linea;

        public bool GrabarArchivoRelacionesLaborales(List<EDRelacionesLaborales> lstRelLabTer, out string mensajes_validaciones)
        {
            bool rta = false;
            bool errorEnValidacionXLinea = false;
            validacion_linea = "";
            int linea = 1;
            string nit = "";
            string documento = "";
            int tipoTercero = -1;
            int tipoDocumento = -1;
            int tipoOcupacion = -1;
            string mensaje = "", mensaje_1 = "", mensaje_2 = "", mensaje_3 = "";
            System.Data.DataTable dtEmpresa = em.EmpresaTercero();
            System.Data.DataTable dtEmpleado = em.EmpleadoTercero();
            em.cargarTablas();
            foreach (var valRL in lstRelLabTer)
            {
                linea++;
                nit = "";
                documento = "";
                tipoTercero = em.ValidacionTipoTercero(valRL.TipoTercero);
                if (tipoTercero == -1)
                {
                    validacion_linea = "Fila: " + linea.ToString() + ", El campo (Tipo Tercero) está en blanco o el valor no es valido";
                    errorEnValidacionXLinea = true;
                    break;
                }

                if (!em.ValidacionCampoNumerico(valRL.NitEmpresa, out nit))
                {
                    validacion_linea = "Fila: " + linea.ToString() + ", El campo (Nit Empresa)  está en blanco o el valor no es valido";
                    errorEnValidacionXLinea = true;
                    break;
                }

                if (!em.ValidacionCampoCadena(valRL.RazonSocial))
                {
                    validacion_linea = "Fila: " + linea.ToString() + ", El campo (Razón Social)  está en blanco o el valor no es valido";
                    errorEnValidacionXLinea = true;
                    break;
                }

                tipoDocumento = em.ValidacionTipoDocumento(valRL.TipoDocumento);
                if (tipoDocumento == -1)
                {
                    validacion_linea = "Fila: " + linea.ToString() + ", El campo (Tipo Documento)  está en blanco o el valor no es valido";
                    errorEnValidacionXLinea = true;
                    break;
                }


                if (!em.ValidacionCampoNumerico(valRL.NumeroDocumento, out documento))
                {
                    validacion_linea = "Fila: " + linea.ToString() + ", El campo (Número Documento)  está en blanco o el valor no es valido";
                    errorEnValidacionXLinea = true;
                    break;
                }
                if (!em.ValidacionCampoCadena(valRL.Nombre1))
                {
                    validacion_linea = "Fila: " + linea.ToString() + ", Campo (Primer Nombre)  está en blanco o el valor no es valido";
                    errorEnValidacionXLinea = true;
                    break;
                }
                if (!em.ValidacionCampoCadena(valRL.Apellido1))
                {
                    validacion_linea = "Fila: " + linea.ToString() + ", El campo (Primer Apellido)  está en blanco o el valor no es valido";
                    errorEnValidacionXLinea = true;
                    break;
                }

                if (!ValidarFecha(valRL.FechaNacimiento))
                {
                    validacion_linea = "Fila: " + linea.ToString() + ", El campo (Fecha Nacimiento)  está en blanco o no cumple con el siguiente formato dd/mm/yyyy";
                    errorEnValidacionXLinea = true;
                    break;
                }

                try
                {
                    tipoOcupacion = em.ValidacionTipoOcupacion(int.Parse(valRL.Ocupacion));
                }
                catch (Exception e)
                {
                    validacion_linea = "Fila: " + linea.ToString() + ", El campo (Tipo Ocupacion)  está en blanco o el valor no es valido";
                    errorEnValidacionXLinea = true;
                    break;
                }
                if (tipoOcupacion == -1)
                {
                    validacion_linea = "Fila: " + linea.ToString() + ", El campo (Tipo Ocupacion)  está en blanco o el valor no es valido";
                    errorEnValidacionXLinea = true;
                    break;
                }

                if (!em.ValidacionCampoCadena(valRL.Cargo))
                {
                    validacion_linea = "Fila: " + linea.ToString() + ", Campo (Cargo)  está en blanco o el valor no es valido";
                    errorEnValidacionXLinea = true;
                    break;
                }


                if (!em.ValidacionEmail(valRL.Email))
                {
                    validacion_linea = "Fila: " + linea.ToString() + ", El campo (Email)  está en blanco o no tiene la estructura correcta";
                    errorEnValidacionXLinea = true;
                    break;
                }


                if (validacion_linea.Length == 0)//no se encontro errores en los valores
                {
                    //valida que no exista la empresa en la tabla temporal
                    if (!dtEmpresa.Rows.Cast<DataRow>().Where(u => u.Field<string>("PK_Nit_Empresa") == nit).Any())
                    {
                        if (!em.ValidaExisteEmpresa(nit))
                            dtEmpresa.Rows.Add(valRL.RazonSocial, nit);
                        else
                            mensaje_3 = "En la base de datos ya existe NIT(" + nit + ")";

                    }

                    //valida que no exista empleado y empresa previamente cargado
                    if (!dtEmpleado.Rows.Cast<DataRow>().Where(u => u.Field<string>("FK_EmpresaTercero") == nit && u.Field<string>("Numero_Documento_Empl") == valRL.NumeroDocumento).Any())
                    {
                        //validar q no exista la empresa para crearla
                        if (!em.ValidaExisteEmpresaEmpleado(nit, valRL.NumeroDocumento))
                            dtEmpleado.Rows.Add(null, tipoDocumento, valRL.Nombre1, valRL.Nombre2, valRL.Apellido1, valRL.Apellido2,
                                valRL.FechaNacimiento, valRL.Email, valRL.Ocupacion, valRL.Cargo, valRL.Email, nit, tipoTercero, valRL.NumeroDocumento, valRL.idEmpresa);
                        else
                            mensaje_2 = "En la base de datos ya existe NIT(" + nit + ") - Numero de Documento Empleado (" + valRL.NumeroDocumento + ")";
                    }
                    else
                    {
                        mensaje_1 = "Se encontraron registros NIT(" + nit + ") - Numero de Documento Empleado (" + valRL.NumeroDocumento + ") repetidos";
                    }

                }
            }
            if (!errorEnValidacionXLinea)
            {
                if (dtEmpresa.Rows.Count == 0 && dtEmpleado.Rows.Count == 0)
                {
                    mensaje = mensaje_1 + mensaje_2 + mensaje_3 + ", Archivo no procesado!!!";
                    rta = false;
                }
                else if (em.GrabarArchivoRelacionesLaborales(dtEmpresa, dtEmpleado))
                {
                    mensaje = mensaje_1 + mensaje_2 + mensaje_3 + ", Archivo guardado con Exito !!!";
                    rta = true;
                }
                else
                {
                    rta = false;
                    mensaje = "Error al guardar la informacion de la empresa y empleado tercero. verifique";
                }
            }
            else
            {
                mensaje = validacion_linea;
                rta = false;
            }

            mensajes_validaciones = mensaje;
            return rta;
        }

        private bool ValidarFecha(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return false;           
            
            string[] fecha = valor.Split('/');
            try
            {
                if (fecha.Length > 0)
                {
                    if (int.Parse(fecha[0]) > 0 && int.Parse(fecha[0]) <= 31)
                        if (int.Parse(fecha[1]) > 0 && int.Parse(fecha[1]) <= 12)
                            if (int.Parse(fecha[2].Substring(0,4)) > 1800)
                                return true;
                            else
                                return false;
                        else
                            return false;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }

        public List<EDEmpleadoRelLab> ListarRelacionesLabTerceros(string estado, string tipoCotizante, string DocumentoEmpresa, int pageIndex, int pageSize, out int pageCount)
        {
            return em.ListarEmpleadoRelLab(estado, tipoCotizante, DocumentoEmpresa, pageIndex, pageSize, out pageCount);

        }


        public List<EDEmpleadoTercero> ListarRelacionesLaborales(string razonSocialnit, string tipoTercero, string DocumentoEmpresa, int pageIndex, int pageSize, out int pageCount)
        {
            return em.ListarTerceroRelLab(razonSocialnit, tipoTercero, DocumentoEmpresa, pageIndex, pageSize, out pageCount);

        }

        public List<EDTipos> DevuelveEstadosEmpleados()
        {
            return em.ObtenerEstadosEmpleados();
        }

        public List<EDTipos> DevuelveTiposCotizantes()
        {
            return em.ObtenerTiposCotizantes();
        }

        public List<EDTipos> DevuelveTiposTerceros()
        {
            return em.ObtenerTiposTerceros();
        }

        public DataTable DescargaArchivoExcelEmpleado(string nit, string estado, string tipoCotizante)
        {
            return em.DescargaArchivoExcelEmpleado(nit, estado, tipoCotizante);
        }

        public DataTable DescargaArchivoExcelTerceroRelLab(string nit, string razonSocialNit, string tipoTercero)
        {
            return em.DescargaArchivoExcelTerceroRelLab(nit, razonSocialNit, tipoTercero);
        }

        public DataTable TablaTerceroRelLab()
        {
            return em.TablaTerceroRelLab();
        }

        public DataTable TablaEmpleados()
        {
            return em.TablaEmpleados();
        }

        public List<EDTiposS> DevuelveRazonesSocialesdeTerceros(string NitEmpresaLogueada)
        {
            return em.DevuelveRazonesSocialesdeTerceros(NitEmpresaLogueada);
        }

        public List<EDTipos> ObtenerTiposInconsistencias()
        {

            return em.ObtenerTiposInconsistencias();
        }

        public EDNotificarInconsistencia GrabarNotificarInconsistenciaLaboral(EDNotificarInconsistencia notIncon)
        {
            return em.GrabarNotificacionInconsistenciaLaborales(notIncon);
        }

        public List<EDTiposS> DevuelveCorreoGerente(string razonSocialnit)
        {
            return em.DevuelveCorreoGerente(razonSocialnit);
        }
    }
}
