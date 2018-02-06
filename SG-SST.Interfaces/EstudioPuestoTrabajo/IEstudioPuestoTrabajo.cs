using SG_SST.EntidadesDominio.EstudioPuestoTrabajo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Interfaces.EstudioPuestoTrabajo
{
    public interface IEstudioPuestoTrabajo
    {
        int GuardarEstudio(EDEstudioPuestoTrabajo estudioPT);
        List<EDSeguimientoEstudioPuestoTrabajo> ConsultarSeguimientoEstudio(int IdEstudioPT);
        List<EDEstudioPuestoTrabajo> ConsultarEstudioPTXNumIden(string NumeroIdentificacion);
        List<EDEstudioPuestoTrabajo> BuscarCargo(string prefijo);
        List<EDEstudioPuestoTrabajo> ConsultarEstudioPTXCargo(string Cargo);
        List<EDArchivoEstudioPuestoTrabajo> ConsultarArchivosEstudio(int IdEstudioPT);

    }
}
