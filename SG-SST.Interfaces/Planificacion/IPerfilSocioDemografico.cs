using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.Empleado;
using SG_SST.EntidadesDominio.Empresas;

namespace SG_SST.Interfaces.Planificacion
{
    public interface IPerfilSocioDemografico
    {
        EDPerfilSocioDemografico GuardarPerfilSociodemografico(EDPerfilSocioDemografico objPerfil);

        EDPerfilSocioDemografico EditarPerfilSociodemografico(EDPerfilSocioDemografico objPerfil);


        bool InsertarCargueMasivoPerfil(List<EDPerfilSocioDemografico> perfiles);

        bool EliminarPerfilSocioDemografico (int idPerfil);
        List<EDGradoEscolaridad> ObtenerGradoEscolaridad();

        List<EDProceso> ObtenerProcesoEmpresa(string nit);

        EDPerfilSocioDemografico obtenerPerfilesPorID(int id);

        EDBusquedaMunicipio BuscarMunicipiosDeSede(int fk_sede);
        List<EDCondicionesRiesgoPerfil> ObtenerCondicionesRiesgoPerfilPorID(int id);
        List<EDOcupacionPerfil> BuscarOcupacion(string prefijo);

        List<EDCondicionesRiesgoPerfil> ObtenerCondicionesRiesgoPorEmpresa(string nitEmpresa);

        List<EDPerfilSocioDemografico> obtenerPerfilesPorEmpresa(string nitEmpresa);

        bool EliminarExpocionPeligro(int idExposicion);

    }


}
