using SG_SST.Interfaces.EstudioPuestoTrabajo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.EstudioPuestoTrabajo;
using SG_SST.Models;
using SG_SST.Audotoria;
using SG_SST.Models.Empresas;

namespace SG_SST.Repositorio.EstudioPuestoTrabajo
{
    public class EstudioPuestoTrabajoManager : IEstudioPuestoTrabajo
    {
        public int GuardarEstudio(EDEstudioPuestoTrabajo estudioPT)
        {
            try
            {
                //var result = false;
                int estID = -1;
                using (var context = new SG_SSTContext())
                {
                    var nuevoEstudio = new SG_SST.Models.Empresas.EstudioPuestoTrabajo();
                    nuevoEstudio.Numero_Identificacion = estudioPT.NumeroIdentificacion;
                    nuevoEstudio.Trabajador_Primer_Apellido = estudioPT.Apellido1;
                    nuevoEstudio.Trabajador_Segundo_Apellido = estudioPT.Apellido2;
                    nuevoEstudio.Trabajador_Primer_Nombre = estudioPT.Nombre1;
                    nuevoEstudio.Trabajador_Segundo_Nombre = estudioPT.Nombre2;
                    nuevoEstudio.Cargo_Empleado = estudioPT.Cargo;
                    nuevoEstudio.FK_Id_Sede = estudioPT.IdSede;
                    nuevoEstudio.FK_Id_Proceso = estudioPT.IdProceso;
                    nuevoEstudio.FK_Id_Diagnostico = estudioPT.IdDiagnostico;
                    nuevoEstudio.FK_Id_ObjetivoAnalisis = estudioPT.IdObjetivoAnalisis;
                    nuevoEstudio.FK_Id_Tipo_Analisis_Puesto_Trabajo = estudioPT.IdTipoAnalisis;
                    nuevoEstudio.FechaAnalisis = estudioPT.FechaAnalisis;
                    context.Tbl_EstudioPuestoTrabajo.Add(nuevoEstudio);
                    context.SaveChanges();
                    //result = true;
                    estID = nuevoEstudio.Pk_Id_EstudioPuestoTrabajo;
                    return estID;
                    //var log = new RegistraLog();
                    //log.RegistrarError(typeof(EstudioPuestoTrabajoManager), string.Format("Guardado la ausencias: {0}, {1}", DateTime.Now, nuevaAusencia.FechaInicio.ToString(), nuevaAusencia.Fecha_Fin.ToString()), new Exception());

                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                //log.RegistrarError(typeof(AusenciaManager), string.Format("Erorr en el guardado de ausencias: {0}, {1}. Error: {2}", DateTime.Now, ausencia.FechaInicio.ToString(), ausencia.FechaFin.ToString(), ex.StackTrace), ex);
                return -1;
            }
        }

        public List<EDSeguimientoEstudioPuestoTrabajo> ConsultarSeguimientoEstudio(int IdEstudioPT)
        {
            List<EDSeguimientoEstudioPuestoTrabajo> ListaEDSeguimiento = new List<EDSeguimientoEstudioPuestoTrabajo>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                SG_SST.Models.Empresas.EstudioPuestoTrabajo estudio = (from s in db.Tbl_EstudioPuestoTrabajo.Include("SeguimientoEstudioPuestoTrabajo")
                                                                       where s.Pk_Id_EstudioPuestoTrabajo == IdEstudioPT
                                                                       select s).FirstOrDefault();

                if (estudio != null)
                {
                    foreach (var item in estudio.SeguimientoEstudioPuestoTrabajo)
                    {
                        EDSeguimientoEstudioPuestoTrabajo EDSeguimiento = new EDSeguimientoEstudioPuestoTrabajo();
                        EDSeguimiento.Actividad = item.Actividad;
                        EDSeguimiento.Fecha = item.Fecha;
                        EDSeguimiento.Responsable = item.Responsable;

                        ListaEDSeguimiento.Add(EDSeguimiento);
                    }
                        
                }
            }
            
            return ListaEDSeguimiento;

        }

        public List<EDArchivoEstudioPuestoTrabajo> ConsultarArchivosEstudio(int IdEstudioPT)
        {
            List<EDArchivoEstudioPuestoTrabajo> ListaEDArchivos = new List<EDArchivoEstudioPuestoTrabajo>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                SG_SST.Models.Empresas.EstudioPuestoTrabajo estudio = (from s in db.Tbl_EstudioPuestoTrabajo.Include("ArchivosEstudioPuestoTrabajo")
                                                                       where s.Pk_Id_EstudioPuestoTrabajo == IdEstudioPT
                                                                       select s).FirstOrDefault();

                if (estudio != null)
                {
                    foreach (var item in estudio.ArchivosEstudioPuestoTrabajo)
                    {
                        EDArchivoEstudioPuestoTrabajo EDArchivo = new EDArchivoEstudioPuestoTrabajo();
                        EDArchivo.NombreArchivo = item.NombreArchivo;
                        EDArchivo.RutaArchivo = item.Ruta;
                        EDArchivo.IdEstudioPuestoTrabajo = item.PK_Id_Archivo_Estudio_Puesto_Trabajo;

                        ListaEDArchivos.Add(EDArchivo);
                    }

                }
            }

            return ListaEDArchivos;

        }

        public List<EDEstudioPuestoTrabajo> ConsultarEstudioPTXNumIden(string NumeroIdentificacion)
        {
            List<EDEstudioPuestoTrabajo> ListaEstudioPT = new List<EDEstudioPuestoTrabajo>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var listaestudio = (from s in db.Tbl_EstudioPuestoTrabajo
                                    join diag in db.Tbl_Diagnosticos on s.FK_Id_Diagnostico equals diag.PK_Id_Diagnostico
                                    where s.Numero_Identificacion == NumeroIdentificacion
                                    select new { s.Numero_Identificacion, s.Trabajador_Primer_Apellido
                                    ,s.Trabajador_Segundo_Apellido,s.Trabajador_Primer_Nombre
                                    ,s.Trabajador_Segundo_Nombre,s.Cargo_Empleado,s.FK_Id_Sede,s.FK_Id_Proceso
                                    ,s.FK_Id_Diagnostico,s.FK_Id_ObjetivoAnalisis,s.FK_Id_Tipo_Analisis_Puesto_Trabajo
                                    ,s.FechaAnalisis,s.Pk_Id_EstudioPuestoTrabajo, diag.Descripcion }).ToList();

                if (listaestudio != null)
                {
                    foreach (var item in listaestudio)
                    {
                        EDEstudioPuestoTrabajo EDEstudio = new EDEstudioPuestoTrabajo();
                        EDEstudio.NumeroIdentificacion = item.Numero_Identificacion;
                        EDEstudio.Apellido1 = item.Trabajador_Primer_Apellido;
                        EDEstudio.Apellido2 = item.Trabajador_Segundo_Apellido;
                        EDEstudio.Nombre1 = item.Trabajador_Primer_Nombre;
                        EDEstudio.Nombre2 = item.Trabajador_Segundo_Nombre;
                        EDEstudio.Cargo = item.Cargo_Empleado;
                        EDEstudio.IdSede = item.FK_Id_Sede;
                        EDEstudio.IdProceso = item.FK_Id_Proceso;
                        EDEstudio.IdDiagnostico = item.FK_Id_Diagnostico;
                        EDEstudio.Diagnostico = item.Descripcion;
                        EDEstudio.IdObjetivoAnalisis = item.FK_Id_ObjetivoAnalisis;
                        EDEstudio.IdTipoAnalisis = item.FK_Id_Tipo_Analisis_Puesto_Trabajo;
                        EDEstudio.FechaAnalisis = item.FechaAnalisis;
                        EDEstudio.IdEstudioPuestoTrabajo = item.Pk_Id_EstudioPuestoTrabajo;

                        ListaEstudioPT.Add(EDEstudio);
                    }

                }
            }
            return ListaEstudioPT;
        }

        /// <summary>
        /// Busca un cargo por el criterio de búsqueda.
        /// </summary>
        /// <param name="prefijo"></param>
        /// <returns></returns>
        public List<EDEstudioPuestoTrabajo> BuscarCargo(string prefijo)
        {
            List<EDEstudioPuestoTrabajo> ListaEstudioPT = new List<EDEstudioPuestoTrabajo>();
            var context = new SG_SSTContext();
            List<SG_SST.Models.Empresas.EstudioPuestoTrabajo> allCargos = context.Tbl_EstudioPuestoTrabajo
                .Where(d => d.Cargo_Empleado.Contains(prefijo)).ToList();

            List<SG_SST.Models.Empresas.EstudioPuestoTrabajo> distinctCargos = allCargos
                .GroupBy(p => p.Cargo_Empleado)
                .Select(g => g.First()).ToList();

            if (distinctCargos != null)
            {
                foreach (var item in distinctCargos)
                {
                    EDEstudioPuestoTrabajo EDEstudio = new EDEstudioPuestoTrabajo();
                    EDEstudio.Cargo = item.Cargo_Empleado;
                    EDEstudio.IdEstudioPuestoTrabajo = item.Pk_Id_EstudioPuestoTrabajo;

                    ListaEstudioPT.Add(EDEstudio);
                }

            }
            return ListaEstudioPT;
   
        }

        public List<EDEstudioPuestoTrabajo> ConsultarEstudioPTXCargo(string Cargo)
        {
            List<EDEstudioPuestoTrabajo> ListaEstudioPT = new List<EDEstudioPuestoTrabajo>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                List<SG_SST.Models.Empresas.EstudioPuestoTrabajo> listaestudio = (from s in db.Tbl_EstudioPuestoTrabajo
                                                                                  where s.Cargo_Empleado == Cargo
                                                                                  select s).ToList();

                if (listaestudio != null)
                {
                    foreach (var item in listaestudio)
                    {
                        EDEstudioPuestoTrabajo EDEstudio = new EDEstudioPuestoTrabajo();
                        EDEstudio.NumeroIdentificacion = item.Numero_Identificacion;
                        EDEstudio.Apellido1 = item.Trabajador_Primer_Apellido;
                        EDEstudio.Apellido2 = item.Trabajador_Segundo_Apellido;
                        EDEstudio.Nombre1 = item.Trabajador_Primer_Nombre;
                        EDEstudio.Nombre2 = item.Trabajador_Segundo_Nombre;
                        EDEstudio.Cargo = item.Cargo_Empleado;
                        EDEstudio.IdSede = item.FK_Id_Sede;
                        EDEstudio.IdProceso = item.FK_Id_Proceso;
                        EDEstudio.IdDiagnostico = item.FK_Id_Diagnostico;
                        EDEstudio.IdObjetivoAnalisis = item.FK_Id_ObjetivoAnalisis;
                        EDEstudio.IdTipoAnalisis = item.FK_Id_Tipo_Analisis_Puesto_Trabajo;
                        EDEstudio.FechaAnalisis = item.FechaAnalisis;
                        EDEstudio.IdEstudioPuestoTrabajo = item.Pk_Id_EstudioPuestoTrabajo;

                        ListaEstudioPT.Add(EDEstudio);
                    }

                }
            }
            return ListaEstudioPT;
        }
    }
}
