using SG_SST.Interfaces.EstudioPuestoTrabajo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.EstudioPuestoTrabajo;
using SG_SST.Models;
using SG_SST.Audotoria;

namespace SG_SST.Repositorio.EstudioPuestoTrabajo
{
    public class ArchivoEstudioPuestoTrabajoManager : IArchivoEstudioPuestoTrabajo
    {
        public bool GuardarArchivo(EDArchivoEstudioPuestoTrabajo archivoPT)
        {
            try
            {
                var result = false;
                using (var context = new SG_SSTContext())
                {
                    var estudio = context.Tbl_EstudioPuestoTrabajo.Find(archivoPT.IdEstudioPuestoTrabajo);
                    var nuevoArchivo = new SG_SST.Models.Empresas.ArchivosEstudioPuestoTrabajo();
                    nuevoArchivo.NombreArchivo = archivoPT.NombreArchivo;
                    nuevoArchivo.Ruta = archivoPT.RutaArchivo;
                    estudio.ArchivosEstudioPuestoTrabajo = new List<Models.Empresas.ArchivosEstudioPuestoTrabajo>();
                    estudio.ArchivosEstudioPuestoTrabajo.Add(nuevoArchivo);
                    context.SaveChanges();
                    result = true;
                    return result;
                    //var log = new RegistraLog();
                    //log.RegistrarError(typeof(EstudioPuestoTrabajoManager), string.Format("Guardado la ausencias: {0}, {1}", DateTime.Now, nuevaAusencia.FechaInicio.ToString(), nuevaAusencia.Fecha_Fin.ToString()), new Exception());

                }
            }
            catch (Exception ex)
            {
                var log = new RegistraLog();
                //log.RegistrarError(typeof(AusenciaManager), string.Format("Erorr en el guardado de ausencias: {0}, {1}. Error: {2}", DateTime.Now, ausencia.FechaInicio.ToString(), ausencia.FechaFin.ToString(), ex.StackTrace), ex);
                return false;
            }
        }
    }
}
