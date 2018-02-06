using SG_SST.EntidadesDominio.Empleado;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.MedicionEvaluacion;
using SG_SST.Interfaces.Empresas;
using SG_SST.Interfaces.MedicionEvaluacion;
using SG_SST.InterfazManager.Empresas;
using SG_SST.InterfazManager.MedicionEvaluacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace SG_SST.Logica.MedicionEvaluacion
{

    public class LNAcciones
    {
        private static IEmpresa em = IMEmpresa.Empresa();
        private static IAccion acc = IMAccion.Accion();
        /// <summary>
        /// Metodo para obtener los diferentes tipos de documentos posibles que hay en la aplicación
        /// </summary>
        public List<EDTipoDocumento> BuscarTipoDocumento()
        {
            List<EDTipoDocumento> tiposDocumento = em.ObtenerTiposDocumento();
            return tiposDocumento;
        }
        /// <summary>
        /// Metodo para obtener un número de ID que será presentado al usuario en la interfaz del módulo
        /// </summary>
        /// <param name="Pk_Id_Empresa">Llave de la empresa</param>
        /// <returns>Regresa un int para el vista NuevaAccion</returns>
        public int NuevoNumeroACP(int Pk_Id_Empresa)
        {
            int NuevoACP = 0;
            var resultado = acc.ListadeAccionesPorEmpresaID(Pk_Id_Empresa);
            bool probar = false;
            while (probar == false)
            {
                NuevoACP = NuevoACP + 1;
                probar = true;
                for (int i = 0; i < resultado.Count; i++)
                {
                    if (resultado[i].Id_Accion == NuevoACP)
                    {
                        probar = false;
                        break;
                    }
                }
            }
            return NuevoACP;
        }
        /// <summary>
        /// Metodo para obtener en los analisis que se representan como un árbol un nivel en el que se encuentra un nodo
        /// </summary>
        /// <param name="ListaAnalisis">Lista del Analisis</param>
        /// /// <param name="ParentId">Id del padre del Nodo a consultar</param>
        /// <returns>Regresa un int para el vista NuevaAccion</returns>
        public int NivelAnalisis(List<EDAnalisis> ListaAnalisis, int ParentId)
        {
            int Nivel = 0;
            bool probar = true;
            while (probar)
            {
                probar = false;
                var analisis = (from s in ListaAnalisis
                                where s.Id_Analisis == ParentId
                                select s).FirstOrDefault<EDAnalisis>();
                if (analisis != null)
                {
                    probar = true;
                    ParentId = analisis.Parent_Id;
                    Nivel = Nivel + 1;
                }
            }
            return Nivel;
        }
        public List<EDAccion> ListadeAccionesPorEmpresa(int Pk_Id_Empresa)
        {
            var ListaAcciones = acc.ListadeAccionesPorEmpresa(Pk_Id_Empresa);
            return ListaAcciones;
        }
        public int GuardarAccion(int Pk_Id_Empresa)
        {
            int IdAccionGuardada = acc.UltimoPkId(Pk_Id_Empresa).Pk_Id_Accion;
            return IdAccionGuardada;
        }
        public bool GuardarAccionbool(EDAccion EDAccion)
        {
            bool IdAccionGuardada = false;





            IdAccionGuardada = acc.GuardarAccionbool(EDAccion);
            return IdAccionGuardada;
        }
        public void GuardarHallazgo(EDHallazgo Hallazgo)
        {
            acc.GuardarHallazgo(Hallazgo);
        }
        public void GuardarAnalisis(EDAnalisis EDAnalisis)
        {
            try
            {
                acc.GuardarAnalisis(EDAnalisis);
            }
            catch (Exception)
            {

            }
        }
        public bool GuardarSeguimiento(EDSeguimiento EDSeguimiento)
        {
            bool ProbarGuardar = false;
            ProbarGuardar = acc.GuardarSeguimiento(EDSeguimiento);
            return ProbarGuardar;
        }
        public bool GuardarActividad(EDActividad EDActividad)
        {
            bool ProbarGuardar = false;
            ProbarGuardar = acc.GuardarActividad(EDActividad);
            return ProbarGuardar;
        }
        public bool GuardarArchivosAccion(EDArchivosAcciones EDArchivosAcciones)
        {
            bool ProbarGuardar = false;
            ProbarGuardar = acc.GuardarArchivosAccion(EDArchivosAcciones);
            return ProbarGuardar;
        }
        public bool EditarAccion(EDAccion Accion)
        {
            bool probar = acc.EditarAccion(Accion);
            return probar;
        }
        public bool EditarHallazgo(EDHallazgo Hallazgo)
        {
            bool probar = acc.EditarHallazgo(Hallazgo);
            return probar;
        }
        public bool EditarSeguimiento(EDSeguimiento Seguimiento)
        {
            bool probar = acc.EditarSeguimiento(Seguimiento);
            return probar;
        }
        public bool EditarActividad(EDActividad Actividad)
        {
            bool probar = acc.EditarActividad(Actividad);
            return probar;
        }
        public bool EditarArchivo(EDArchivosAcciones Archivo)
        {
            bool probar = acc.EditarArchivo(Archivo);
            return probar;
        }
        public void EliminarHallazgos(EDHallazgo EDHallazgo)
        {
            acc.EliminarHallazgos(EDHallazgo);
        }
        public bool EliminarArchivos(EDArchivosAcciones EDArchivosAcciones)
        {
            bool ProbarEliminar = false;
            ProbarEliminar = acc.EliminarArchivos(EDArchivosAcciones);
            return ProbarEliminar;
        }
        public int UltimoIdArchivo(int idAccion, int idEmpresa)
        {
            int UltimoId = 0;
            UltimoId = acc.UltimoIdArchivo(idAccion, idEmpresa);
            return UltimoId;
        }
        public void EliminarAnalisis(EDAnalisis EDAnalisis)
        {
            acc.EliminarAnalisis(EDAnalisis);
        }
        public List<EDAccion> ConsultarAcciones(string Id, string Nombre, string Estado, int idEmpresa, string Sede)
        {
            List<EDAccion> Test = acc.ConsultarAcciones(Id, Nombre, Estado, idEmpresa, Sede);
            return Test;
        }
        public EDAccion ConsultaAccion(int IdAccion, int idEmpresa)
        {
            EDAccion EDAccion = new EDAccion();
            EDAccion = acc.ConsultaAccion(IdAccion, idEmpresa);
            return EDAccion;
        }

        public void ConsultaAccionEstado(int idEmpresa)
        {
            acc.ConsultaAccionEstado(idEmpresa);
        }

        public EDActividad ConsultarActividad(int IdActividad)
        {
            EDActividad EDActividad = new EDActividad();
            EDActividad = acc.ConsultarActividad(IdActividad);
            return EDActividad;
        }

        public EDSeguimiento ConsultarSeguimiento(int IdSeguimiento)
        {
            EDSeguimiento EDSeguimiento = new EDSeguimiento();
            EDSeguimiento = acc.ConsultarSeguimiento(IdSeguimiento);
            return EDSeguimiento;
        }
        public List<EDAnalisis> ConsultarAnalisis(int Tipo, int IdAccion)
        {
            List<EDAnalisis> ListaEDAnalisis = new List<EDAnalisis>();
            ListaEDAnalisis = acc.ConsultarAnalisis(Tipo, IdAccion);
            return ListaEDAnalisis;
        }
        public List<EDHallazgo> ListaHallazgos(int IdAccion)
        {
            List<EDHallazgo> ListaEDHallazgos = new List<EDHallazgo>();
            ListaEDHallazgos = acc.ListaHallazgos(IdAccion);
            return ListaEDHallazgos;
        }
        public List<EDArchivosAcciones> ListaArchivos(int IdAccion)
        {

            List<EDArchivosAcciones> ListaEDArchivo = acc.ListaArchivos(IdAccion);

            return ListaEDArchivo;
        }
        public List<EDAnalisis> ListaAnalisis(int IdAccion)
        {
            List<EDAnalisis> ListaEDAnalisis = acc.ListaAnalisis(IdAccion);

            return ListaEDAnalisis;
        }
        public bool EliminarEncontrarAccion(int IdAccion, int IdEmpresa)
        {
            bool ProbarEliminar = false;
            ProbarEliminar = acc.EliminarEncontrarAccion(IdAccion, IdEmpresa);
            return ProbarEliminar;
        }
        public List<EDCargo> ListaCargos()
        {
            List<EDCargo> ListaCargos = acc.ListaCargos();
            return ListaCargos;
        }
        public List<EDAnalisis> ConsultaAnalisisEdicion(int IdAccion, int idEmpresa, short Tipo)
        {
            List<EDAnalisis> ListaAnalisis = acc.ConsultaAnalisisEdicion(IdAccion, idEmpresa, Tipo);
            return ListaAnalisis;
        }

        public bool GuardarCambiosAnalisis(List<EDAnalisis> EDAnalisisGuardar, List<EDAnalisis> EDAnalisisEditar, List<EDAnalisis> EDAnalisisEliminar, short Tipo, int IdAccion)
        {
            bool probarGuardarCambios = acc.GuardarCambiosAnalisis(EDAnalisisGuardar, EDAnalisisEditar, EDAnalisisEliminar, Tipo, IdAccion);
            return probarGuardarCambios;
        }

    }
}
