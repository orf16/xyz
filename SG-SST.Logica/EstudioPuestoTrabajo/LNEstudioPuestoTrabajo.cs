using SG_SST.EntidadesDominio.EstudioPuestoTrabajo;
using SG_SST.Interfaces.EstudioPuestoTrabajo;
using SG_SST.InterfazManager.EstudioPuestoTrabajo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.EstudioPuestoTrabajo
{
    public class LNEstudioPuestoTrabajo
    {
        private static IEstudioPuestoTrabajo objestudiopt = IMEstudioPuestoTrabajo.EstudioPT();

        public EDEstudioPuestoTrabajo GuardarEstudio(EDEstudioPuestoTrabajo estudio)
        {
            int resultado = objestudiopt.GuardarEstudio(estudio);
            if (resultado > 0)
                estudio.IdEstudioPuestoTrabajo = resultado;
            else
                estudio.IdEstudioPuestoTrabajo = -1;
            
            return estudio;
        }

        public List<EDSeguimientoEstudioPuestoTrabajo> ConsultarSeguimientoEstudio(int IdEstudioPT)
        {
            List<EDSeguimientoEstudioPuestoTrabajo> listaSeguimiento = objestudiopt.ConsultarSeguimientoEstudio(IdEstudioPT);
            return listaSeguimiento;
        }

        public List<EDEstudioPuestoTrabajo> ConsultarEstudioPTXNumIden(string NumeroIdentificacion)
        {
            List<EDEstudioPuestoTrabajo> listaEstudioPT = objestudiopt.ConsultarEstudioPTXNumIden(NumeroIdentificacion);
            return listaEstudioPT;
        }

        public List<EDEstudioPuestoTrabajo> BuscarCargo(string prefijo)
        {
            List<EDEstudioPuestoTrabajo> listaEstudioPT = objestudiopt.BuscarCargo(prefijo);
            return listaEstudioPT;
        }

        public List<EDEstudioPuestoTrabajo> ConsultarEstudioPTXCargo(string Cargo)
        {
            List<EDEstudioPuestoTrabajo> listaEstudioPT = objestudiopt.ConsultarEstudioPTXCargo(Cargo);
            return listaEstudioPT;
        }

        public List<EDArchivoEstudioPuestoTrabajo> ConsultarArchivosEstudio(int IdEstudioPT)
        {
            List<EDArchivoEstudioPuestoTrabajo> listaArchivos = objestudiopt.ConsultarArchivosEstudio(IdEstudioPT);
            return listaArchivos;
        }
    }
}
