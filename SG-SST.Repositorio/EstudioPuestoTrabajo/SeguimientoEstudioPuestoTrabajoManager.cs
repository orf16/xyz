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
    public class SeguimientoEstudioPuestoTrabajoManager : ISeguimientoEstudioPuestoTrabajo
    {
        public bool GuardarSeguimiento(EDSeguimientoEstudioPuestoTrabajo seguimientoPT)
        {
            try
            {
                var result = false;
                using (var context = new SG_SSTContext())
                {
                    var estudio = context.Tbl_EstudioPuestoTrabajo.Find(seguimientoPT.IdEstudioPuestoTrabajo);
                    var nuevoSeguimiento = new SG_SST.Models.Empresas.SeguimientoEstudioPuestoTrabajo();
                    nuevoSeguimiento.Actividad = seguimientoPT.Actividad;
                    nuevoSeguimiento.Fecha = seguimientoPT.Fecha;
                    nuevoSeguimiento.Responsable = seguimientoPT.Responsable;
                    estudio.SeguimientoEstudioPuestoTrabajo = new List<SeguimientoEstudioPuestoTrabajo>();
                    estudio.SeguimientoEstudioPuestoTrabajo.Add(nuevoSeguimiento);
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
