using SG_SST.Interfaces.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.Empresas;
using System.Data.Entity.SqlServer;
using SG_SST.Models;
using SG_SST.Models.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Audotoria;
using System.Data.SqlClient;
using SG_SST.EntidadesDominio.Usuario;
using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.EntidadesDominio.Empleado;
using System.Data.Entity.Core.Objects;

namespace SG_SST.Repositorio.Empresas
{
    public class IncidenteManager : IIncidente
    {

        /// <summary>
        /// Realiza una consulta sobre incidentes utilizando parámaetros.
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public List<EDIncidente> ConsultarIncidentes(EDIncidente_Modelo_Consulta parametros)
        {
            List<EDIncidente> incidentes = new List<EDIncidente>();
            try
            {
                using (SG_SSTContext context = new SG_SSTContext())
                {

                    // Se utilizan las siguientes variables para algunos datos que
                    // EF no puede traducir, como: AddDay, string.IsNullOrWhiteSpace
                    if (parametros.IncidenteFechaFinal != null)
                        parametros.IncidenteFechaFinal.Value.AddDays(1);
                    var HayPersonaNumeroIdentificacion
                        = !string.IsNullOrWhiteSpace(parametros.PersonaNumeroIdentificacion.Trim());
                    var HayNit
                        = !string.IsNullOrWhiteSpace(parametros.Nit_Empresa.Trim());
                    var idSedeNoPrincipal = 0;
                    var incidentetmp = context.Tbl_Incidentes.Where(i => i.FK_id_sede_no_principal == parametros.IncidenteSedeID.Value).Select(i => i).FirstOrDefault();
                    if (incidentetmp != null)
                    {
                        idSedeNoPrincipal = incidentetmp.FK_id_sede_no_principal;
                        parametros.IncidenteSedeID = incidentetmp.FK_id_sede_general;
                    }
                    int numPagina = 0;
                    if (parametros.numPagina > 0)
                        numPagina = (parametros.numPagina * parametros.cantidadPorPagina) - parametros.cantidadPorPagina + 1;

                    if (!parametros.ConsultarTodo)
                    {
                        incidentes = (from i in context.Tbl_Incidentes
                                      join c in context.Tbl_Incidente_Consecuencia on i.FK_id_consecuencia_incidente equals c.Pk_Id_Incidente_Consecuencia
                                      join sd in context.Tbl_Sede on i.FK_id_sede_general equals sd.Pk_Id_Sede
                                      join sdnp in context.Tbl_Sede on i.FK_id_sede_no_principal equals sdnp.Pk_Id_Sede into lefjoinsede
                                      from ljoinsede in lefjoinsede.DefaultIfEmpty()
                                      join ti in context.Tbl_Tipo_Incidente on i.FK_id_incidente_tipo_incidente equals ti.Pk_Id_Tipo_Incidente
                                      join li in context.Tbl_Sitio_Incidente on i.FK_id_sitio_incidente equals li.Pk_Id_Sitio_Incidente
                                      where (!parametros.IncidenteFechaInicial.HasValue || i.Incidente_fecha >= parametros.IncidenteFechaInicial.Value)
                                      && (!parametros.IncidenteFechaFinal.HasValue || i.Incidente_fecha <= parametros.IncidenteFechaFinal)
                                      && (!HayNit || i.General_numero_identificación.Equals(parametros.Nit_Empresa))
                                      && (!HayPersonaNumeroIdentificacion || i.Persona_numero_identificacion.Equals(parametros.PersonaNumeroIdentificacion))
                                      && (!parametros.IncidentePosibleConsecuenciaID.HasValue || i.FK_id_consecuencia_incidente == parametros.IncidentePosibleConsecuenciaID.Value)
                                      && (!parametros.IncidenteSedeID.HasValue || i.FK_id_sede_general == parametros.IncidenteSedeID.Value)
                                      && (i.FK_id_sede_no_principal == idSedeNoPrincipal)
                                      && (!parametros.IncidenteTipoIncidenteID.HasValue || i.FK_id_incidente_tipo_incidente == parametros.IncidenteTipoIncidenteID.Value)
                                      && (!parametros.IncidenteLugarIncidente.HasValue || i.Incidente_ocurre_dentro_empresa == parametros.IncidenteLugarIncidente.Value)
                                      && (!parametros.IncidenteSitioID.HasValue || i.FK_id_sitio_incidente == parametros.IncidenteSitioID.Value)
                                      select new EDIncidente
                                      {
                                          Pk_Id_Incidente = i.Pk_Id_Incidente,
                                          General_mismos_datos_sede_principal = i.General_mismos_datos_sede_principal,
                                          IncidenteFechaInicial = parametros.IncidenteFechaInicial.Value,
                                          IncidenteFechaFinal = parametros.IncidenteFechaFinal.Value,
                                          Persona_numero_identificacion = i.Persona_numero_identificacion,
                                          Incidente_consecuencia = new EDIncidenteConsecuencia() { Nombre_consecuencia = c.Nombre_consecuencia },
                                          General_sede = new EDSede() { NombreSede = ljoinsede.Nombre_Sede == null ? sd.Nombre_Sede : ljoinsede.Nombre_Sede },
                                          Incidente_ocurre_dentro_empresa = i.Incidente_ocurre_dentro_empresa,
                                          Incidente_tipo_incidente = new EDTipoIncidente() { Nombre_Incidente = ti.Nombre_Incidente },
                                          Incidente_sitio_incidente = new EDSitioIncidente() { Nombre_Sitio = li.Nombre_Sitio }
                                      }).OrderBy(x => x.Pk_Id_Incidente).Skip(numPagina).Take(parametros.cantidadPorPagina).ToList();

                        incidentes[0].cantidadRegistros = (from i in context.Tbl_Incidentes
                                                           join c in context.Tbl_Incidente_Consecuencia on i.FK_id_consecuencia_incidente equals c.Pk_Id_Incidente_Consecuencia
                                                           join sd in context.Tbl_Sede on i.FK_id_sede_general equals sd.Pk_Id_Sede
                                                           join sdnp in context.Tbl_Sede on i.FK_id_sede_no_principal equals sdnp.Pk_Id_Sede into lefjoinsede
                                                           from ljoinsede in lefjoinsede.DefaultIfEmpty()
                                                           join ti in context.Tbl_Tipo_Incidente on i.FK_id_incidente_tipo_incidente equals ti.Pk_Id_Tipo_Incidente
                                                           join li in context.Tbl_Sitio_Incidente on i.FK_id_sitio_incidente equals li.Pk_Id_Sitio_Incidente
                                                           where (!parametros.IncidenteFechaInicial.HasValue || i.Incidente_fecha >= parametros.IncidenteFechaInicial.Value)
                                                           && (!parametros.IncidenteFechaFinal.HasValue || i.Incidente_fecha <= parametros.IncidenteFechaFinal)
                                                           && (!HayNit || i.General_numero_identificación.Equals(parametros.Nit_Empresa))
                                                           && (!HayPersonaNumeroIdentificacion || i.Persona_numero_identificacion.Equals(parametros.PersonaNumeroIdentificacion))
                                                           && (!parametros.IncidentePosibleConsecuenciaID.HasValue || i.FK_id_consecuencia_incidente == parametros.IncidentePosibleConsecuenciaID.Value)
                                                           && (!parametros.IncidenteSedeID.HasValue || i.FK_id_sede_general == parametros.IncidenteSedeID.Value)
                                                           && (i.FK_id_sede_no_principal == idSedeNoPrincipal)
                                                           && (!parametros.IncidenteTipoIncidenteID.HasValue || i.FK_id_incidente_tipo_incidente == parametros.IncidenteTipoIncidenteID.Value)
                                                           && (!parametros.IncidenteLugarIncidente.HasValue || i.Incidente_ocurre_dentro_empresa == parametros.IncidenteLugarIncidente.Value)
                                                           && (!parametros.IncidenteSitioID.HasValue || i.FK_id_sitio_incidente == parametros.IncidenteSitioID.Value)
                                                           select new EDIncidente
                                                           {
                                                               Pk_Id_Incidente = i.Pk_Id_Incidente,
                                                               General_mismos_datos_sede_principal = i.General_mismos_datos_sede_principal,
                                                               IncidenteFechaInicial = parametros.IncidenteFechaInicial.Value,
                                                               IncidenteFechaFinal = parametros.IncidenteFechaFinal.Value,
                                                               Persona_numero_identificacion = i.Persona_numero_identificacion,
                                                               Incidente_consecuencia = new EDIncidenteConsecuencia() { Nombre_consecuencia = c.Nombre_consecuencia },
                                                               General_sede = new EDSede() { NombreSede = ljoinsede.Nombre_Sede == null ? sd.Nombre_Sede : ljoinsede.Nombre_Sede },
                                                               Incidente_ocurre_dentro_empresa = i.Incidente_ocurre_dentro_empresa,
                                                               Incidente_tipo_incidente = new EDTipoIncidente() { Nombre_Incidente = ti.Nombre_Incidente },
                                                               Incidente_sitio_incidente = new EDSitioIncidente() { Nombre_Sitio = li.Nombre_Sitio }
                                                           }).ToList().Count();

                    }
                    else
                    {
                        //var empresa = context.Tbl_Empresa.Where(e => e.Nit_Empresa.Equals(parametros.Nit_Empresa.Trim())).Select(e => e).FirstOrDefault();
                        //var sedePrincipal = context.Tbl_Sede.Where(sd => sd.Pk_Id_Sede == (context.Tbl_Sede.Where(sdi => sd.Fk_Id_Empresa == empresa.Pk_Id_Empresa).Select(sdi => sdi.Pk_Id_Sede).Min())).Select(sd => sd).FirstOrDefault();

                        incidentes = (from i in context.Tbl_Incidentes
                                      join ciu in context.Tbl_CIU on i.General_actividad_economica_id equals ciu.Codigo_CIU
                                      join e in context.Tbl_Empresa on i.General_numero_identificación equals e.Nit_Empresa
                                      join c in context.Tbl_Incidente_Consecuencia on i.FK_id_consecuencia_incidente equals c.Pk_Id_Incidente_Consecuencia
                                      join sdp in context.Tbl_Sede on i.FK_id_sede_general equals sdp.Pk_Id_Sede
                                      join sdpm in context.Tbl_SedeMunicipio on sdp.Pk_Id_Sede equals sdpm.Fk_id_Sede
                                      join msdp in context.Tbl_Municipio on sdpm.Fk_Id_Municipio equals msdp.Pk_Id_Municipio
                                      join dsdp in context.Tbl_Departamento on msdp.Fk_Nombre_Departamento equals dsdp.Pk_Id_Departamento
                                      join sd in context.Tbl_Sede on i.FK_id_sede_no_principal equals sd.Pk_Id_Sede into lefjoinsede
                                      from ljoinsede in lefjoinsede.DefaultIfEmpty()
                                      join sdm in context.Tbl_SedeMunicipio on ljoinsede.Pk_Id_Sede equals sdm.Fk_id_Sede into lefjoinsdm
                                      from ljoinsdm in lefjoinsdm.DefaultIfEmpty()
                                      join msd in context.Tbl_Municipio on ljoinsdm.Fk_Id_Municipio equals msd.Pk_Id_Municipio into lefjoinmsd
                                      from ljoinmsd in lefjoinmsd.DefaultIfEmpty()
                                      join dsd in context.Tbl_Departamento on ljoinmsd.Fk_Nombre_Departamento equals dsd.Pk_Id_Departamento into lefjoindsd
                                      from ljoindsd in lefjoindsd.DefaultIfEmpty()
                                      join zem in context.Tbl_ZonaLugar on i.General_sede_principal_zona_id equals zem.PK_ZonaLugar
                                      join vcl in context.Tbl_VinculacionLaboral on i.FK_id_vinculacionlaboral_persona equals vcl.PK_VinculacionLaboral
                                      join tip in context.Tbl_TipoDocumentos on i.FK_id_tipo_documento_persona equals tip.PK_IDTipo_Documento
                                      join mp in context.Tbl_Municipio on i.Persona_municipio_id equals mp.Pk_Id_Municipio
                                      join dp in context.Tbl_Departamento on i.Persona_departamento_id equals dp.Pk_Id_Departamento
                                      join zp in context.Tbl_ZonaLugar on i.FK_id_zonalugar_persona equals zp.PK_ZonaLugar
                                      join jh in context.Tbl_Tipo_Jornada on i.FK_id_jornada_habitual_persona equals jh.Pk_Id_Tipo_Jornada
                                      join ti in context.Tbl_Tipo_Incidente on i.FK_id_incidente_tipo_incidente equals ti.Pk_Id_Tipo_Incidente
                                      join di in context.Tbl_Departamento on i.FK_id_departamento_incidente equals di.Pk_Id_Departamento
                                      join mi in context.Tbl_Municipio on i.FK_id_municipio_incidente equals mi.Pk_Id_Municipio
                                      join zi in context.Tbl_ZonaLugar on i.FK_id_zonalugar_incidente equals zi.PK_ZonaLugar
                                      join li in context.Tbl_Sitio_Incidente on i.FK_id_sitio_incidente equals li.Pk_Id_Sitio_Incidente
                                      join cons in context.Tbl_Incidente_Consecuencia on i.FK_id_consecuencia_incidente equals cons.Pk_Id_Incidente_Consecuencia
                                      where (!parametros.IncidenteFechaInicial.HasValue || i.Incidente_fecha >= parametros.IncidenteFechaInicial.Value)
                                      && (!parametros.IncidenteFechaFinal.HasValue || i.Incidente_fecha <= parametros.IncidenteFechaFinal)
                                      && (!HayPersonaNumeroIdentificacion || i.Persona_numero_identificacion.Equals(parametros.PersonaNumeroIdentificacion))
                                      && (!HayNit || i.General_numero_identificación.Equals(parametros.Nit_Empresa))
                                      && (!parametros.IncidentePosibleConsecuenciaID.HasValue || i.FK_id_consecuencia_incidente == parametros.IncidentePosibleConsecuenciaID.Value)
                                      && (!parametros.IncidenteSedeID.HasValue || i.FK_id_sede_general == parametros.IncidenteSedeID.Value)
                                      && (i.FK_id_sede_no_principal == idSedeNoPrincipal)
                                      && (!parametros.IncidenteTipoIncidenteID.HasValue || i.FK_id_incidente_tipo_incidente == parametros.IncidenteTipoIncidenteID.Value)
                                      && (!parametros.IncidenteLugarIncidente.HasValue || i.Incidente_ocurre_dentro_empresa == parametros.IncidenteLugarIncidente.Value)
                                      && (!parametros.IncidenteSitioID.HasValue || i.FK_id_sitio_incidente == parametros.IncidenteSitioID.Value)
                                      select new EDIncidente
                                      {
                                          Pk_Id_Incidente = i.Pk_Id_Incidente,
                                          General_actividad_economica = ciu.Descripcion,
                                          General_codigo = ciu.Codigo_CIU.ToString(),
                                          General_razon_social = e.Razon_Social,
                                          General_tipo_documento = new EDTipoDocumento() { Sigla = e.Tipo_Documento },
                                          General_numero_identificación = i.General_numero_identificación,
                                          General_sede_principal_direccion = e.Direccion,
                                          General_sede_principal_telefono = e.Telefono.ToString(),
                                          General_correo_electronico = e.Email,
                                          General_sede_principal_departamento = dsdp.Nombre_Departamento,
                                          General_sede_principal_municipio = msdp.Nombre_Municipio,
                                          General_sede_principal_zona = zem.Descripcion_ZonaLugar,
                                          General_sede = new EDSede()
                                          {
                                              NombreSede = ljoinsede.Nombre_Sede,
                                              DireccionSede = ljoinsede.Direccion_Sede,
                                              Telefono = ljoinsede.Telefono,
                                              NombreDepto = ljoindsd.Nombre_Departamento,
                                              NombreMunici = ljoinmsd.Nombre_Municipio,
                                              Sector = ljoinsede.Sector
                                          },
                                          Persona_vinculacion_laboral = new EDVinculacionLaboral() { Descripcion_VinculacionLaboral = vcl.Descripcion_VinculacionLaboral },
                                          Persona_primer_apellido = i.Persona_primer_apellido,
                                          Persona_primer_nombre = i.Persona_primer_nombre,
                                          Persona_segundo_apellido = i.Persona_segundo_apellido,
                                          Persona_segundo_nombre = i.Persona_segundo_nombre,
                                          Persona_tipo_documento = new EDTipoDocumento() { Sigla = tip.Sigla },
                                          Persona_numero_identificacion = i.Persona_numero_identificacion,
                                          Persona_fecha_nacimiento = i.Persona_fecha_nacimiento,
                                          Persona_genero = i.Persona_genero,
                                          Persona_direccion = i.Persona_direccion,
                                          Persona_telefono = i.Persona_telefono,
                                          Persona_departamento = dp.Nombre_Departamento,
                                          Persona_municipio = mp.Nombre_Municipio,
                                          Persona_zona = new EDZonaLugar() { Descripcion_ZonaLugar = zp.Descripcion_ZonaLugar },
                                          Persona_ocupacion_habitual = i.Persona_ocupacion_habitual,
                                          Persona_fecha_ingreso_empresa = i.Persona_fecha_ingreso_empresa,
                                          Persona_tipo_jornada = new EDTipoJornada() { Nombre_Jornada = jh.Nombre_Jornada },
                                          Incidente_fecha = i.Incidente_fecha,
                                          Incidente_dia_semana = i.Dia_Semana_Incidente,
                                          Incidente_jornada_normal = i.Incidente_jornada_normal,
                                          Incidente_nombre_labor = i.Incidente_nombre_labor,
                                          Incidente_realizaba_labor_habitual = i.Incidente_realizaba_labor_habitual,
                                          Incidente_tiempo_previo_al_incidente = i.Incidente_tiempo_previo_al_incidente,
                                          Incidente_tiempo_previo_al_incidente_HHMM = i.Incidente_tiempo_previo_al_incidente.ToString(),
                                          Incidente_tipo_incidente = new EDTipoIncidente() { Nombre_Incidente = ti.Nombre_Incidente },
                                          Incidente_departamento = new EntidadesDominio.Empresas.EDDepartamento() { Nombre_Departamento = di.Nombre_Departamento },
                                          Incidente_municipio = new EntidadesDominio.Empresas.EDMunicipio() { NombreMunicipio = mi.Nombre_Municipio },
                                          Incidente_zona_incidente = new EDZonaLugar() { Descripcion_ZonaLugar = zi.Descripcion_ZonaLugar },
                                          Incidente_ocurre_dentro_empresa = i.Incidente_ocurre_dentro_empresa,
                                          Incidente_sitio_incidente = new EDSitioIncidente() { Nombre_Sitio = li.Nombre_Sitio },
                                          Incidente_sitio_incidente_otro = i.Incidente_sitio_incidente_otro,
                                          Incidente_consecuencia = new EDIncidenteConsecuencia() { Nombre_consecuencia = cons.Nombre_consecuencia },
                                          Incidente_descripcion = i.Incidente_descripcion,
                                          Incidente_fecha_diligenciamiento = i.Incidente_fecha_diligenciamiento
                                      }).Distinct().OrderBy(x => x.Incidente_fecha_diligenciamiento).ToList();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return incidentes;
        }

        /// <summary>
        /// Retorna las listas básicas de datos para utilizar en el formulario.
        /// </summary>
        /// <returns></returns>
        public EDIncidente_Listas_Basicas ObtenerListasBasicas(string nitEmpresa)
        {
            var Resultado = new EDIncidente_Listas_Basicas();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var Deptos =
                    context.Tbl_Departamento
                    .OrderBy(x => x.Nombre_Departamento.ToLower())
                    .ToArray();

                Resultado.Departamentos = Deptos.Select(x => x.ObtenerED()).ToList();
                Resultado.TiposDocumento = context.Tbl_TipoDocumentos.ToArray()
                    .Select(x => x.ObtenerED()).ToList();
                Resultado.Zonas = context.Tbl_ZonaLugar.ToArray()
                    .Select(x => x.ObtenerED()).ToList();
                Resultado.VinculacionLaboral = context.Tbl_VinculacionLaboral.ToArray()
                    .Select(x => x.ObtenerED()).ToList();
                Resultado.Jornadas = context.Tbl_Tipo_Jornada.ToArray()
                    .Select(x => x.ObtenerED()).ToList();
                Resultado.TiposIncidente = context.Tbl_Tipo_Incidente.ToArray()
                    .Select(x => x.ObtenerED()).ToList();
                Resultado.SitiosIncidente = context.Tbl_Sitio_Incidente.ToArray()
                    .Select(x => x.ObtenerED()).ToList();
                Resultado.IncidenteConsecuencias = context.Tbl_Incidente_Consecuencia.ToArray()
                    .Select(x => x.ObtenerED()).ToList();
                var CI = new System.Globalization.CultureInfo("es-CO");
                Resultado.DiasSemana = CI.DateTimeFormat.DayNames
                .Select(t => t.ToUpper().Substring(0, 2)).ToList();
            }

            return Resultado;
        }

        /// <summary>
        /// Crea un modelo de incidente en blanco con base a la información del usuario indicado.
        /// </summary>
        /// <param name="identificacionUsuario"></param>
        /// <returns></returns>
        public EDIncidente ObtenerNuevoIncidente(string identificacionEmpresa, string identificacionUsuario)
        {
            // Obtener la información básica del usuario.
            var Empleado = ConsultarEmpleado(identificacionEmpresa, identificacionUsuario);
            var Resultado = new EDIncidente();
            Resultado.Limpiar();

            if (Empleado != null)
            {
                var InfoDptoMcpio = ObtenerDptoMcpio(Empleado.FK_id_municipio);

                // Llenar con la información disponible del empleado.
                Resultado.FK_id_tipo_documento_persona = Empleado.FK_Tipo_Documento_Empl;
                Resultado.Persona_numero_identificacion = Empleado.Numero_Documento_Empl;
                Resultado.Persona_primer_nombre = Empleado.Nombre1;
                Resultado.Persona_segundo_nombre = Empleado.Nombre2;
                Resultado.Persona_primer_apellido = Empleado.Apellido1;
                Resultado.Persona_segundo_apellido = Empleado.Apellido2;
                Resultado.Persona_fecha_nacimiento = Empleado.FechaNacimiento;
                Resultado.Persona_genero = Empleado.Genero;
                Resultado.Persona_direccion = Empleado.Direccion;
                Resultado.Persona_telefono = Empleado.Telefono;
                Resultado.Persona_departamento_id = Empleado.FK_id_departamento;
                Resultado.Persona_departamento = InfoDptoMcpio.Item1;
                Resultado.Persona_municipio_id = Empleado.FK_id_municipio;
                Resultado.Persona_municipio = InfoDptoMcpio.Item2;
                Resultado.FK_id_zonalugar_persona = Empleado.FK_id_zona;
                Resultado.Persona_zona = Empleado.Zona;
                Resultado.Persona_ocupacion_habitual = Empleado.Ocupacion_habitual;
                Resultado.Persona_fecha_ingreso_empresa = Empleado.Fecha_ingreso_empresa;
                Resultado.FK_id_jornada_habitual_persona = Empleado.FK_id_jornada_habitual;
                Resultado.Persona_tipo_jornada = Empleado.Jornada_trabajo_habitual;
            }

            var Empresa = ConsultarEmpresa(identificacionEmpresa);
            if (Empresa != null)
            {
                // Llenar con la información disponible de la empresa.
                Resultado.General_numero_identificación = identificacionEmpresa;
                Resultado.General_sede_principal_direccion = Empresa.DireccionEmpresa;
                Resultado.General_correo_electronico = Empresa.EmailEmpresa;
                Resultado.General_actividad_economica = Empresa.ActEconoPrincipal;
                Resultado.General_razon_social = Empresa.RazonSocial;
            }

            return Resultado;
        }

        /// <summary>
        /// Retorna los nombres del departamento y el municipio, en ese orden.
        /// </summary>
        /// <param name="municipioId"></param>
        /// <returns></returns>
        public Tuple<string, string> ObtenerDptoMcpio(int municipioId)
        {
            using (var context = new SG_SSTContext())
            {
                var R = (from d in context.Tbl_Departamento
                         join m in context.Tbl_Municipio on d.Pk_Id_Departamento equals m.Fk_Nombre_Departamento
                         where m.Pk_Id_Municipio == municipioId
                         select new { Dpto = d.Nombre_Departamento, Mcpio = m.Nombre_Municipio })
                        .FirstOrDefault();
                if (R == null)
                    return new Tuple<string, string>("", "");
                else
                    return new Tuple<string, string>(R.Dpto, R.Mcpio);
            }
        }

        /// <summary>
        /// Consultar información básica de la empresa del usuario logueado.
        /// </summary>
        /// <param name="nit"></param>
        /// <returns></returns>
        public EDEmpresa ConsultarEmpresa(string nit)
        {
            using (var context = new SG_SSTContext())
            {
                var R = context.Tbl_Empresa
                    .Where(x => x.Nit_Empresa == nit)
                    .FirstOrDefault();

                if (R != null)
                {
                    return R.ObtenerED();
                }
                else
                    return new EDEmpresa();
            }
        }

        /// <summary>
        /// Retorna la información básica de un empleado.
        /// </summary>
        /// <param name="nit"></param>
        /// <param name="identificacionEmpleado"></param>
        /// <returns></returns>
        public EDEmpleadoTercero ConsultarEmpleado(string nit, string identificacionEmpleado)
        {
            EDEmpleadoTercero Resultado = null;
            nit = nit.Trim();

            using (var context = new SG_SSTContext())
            {
                var R = (from erl in context.Tbl_EmpleadoTercero
                         join emp in context.Tbl_EmpresaTercero on erl.FK_EmpresaTercero equals emp.PK_Nit_Empresa
                         join tt in context.Tbl_TipoTercero on erl.FKRelacionLaboralTercero equals tt.Pk_Id_TipoTercero
                         join te in context.Tbl_Empresa on erl.FK_Empresa equals te.Pk_Id_Empresa
                         join td in context.Tbl_TipoDocumentos on erl.FK_Tipo_Documento_Empl equals td.PK_IDTipo_Documento
                         where te.Nit_Empresa.Trim().Equals(nit)
                         && erl.Numero_Documento_Empl == identificacionEmpleado
                         select new EDEmpleadoTercero()
                         {
                             Apellido1 = erl.Apellido1,
                             Apellido2 = erl.Apellido2,
                             Cargo_Empl = erl.Cargo_Empl,
                             Direccion = erl.Direccion,
                             Email = erl.Email,
                             FechaNacimiento = erl.FechaNacimiento,
                             Fecha_ingreso_empresa = erl.Fecha_ingreso_empresa,
                             FK_id_departamento = erl.FK_id_departamento,
                             FK_id_jornada_habitual = erl.FK_id_jornada_habitual,
                             FK_id_municipio = erl.FK_id_municipio,
                             FK_id_zona = erl.FK_id_zona,
                             FK_Tipo_Documento_Empl = erl.FK_Tipo_Documento_Empl,
                             Genero = erl.Genero,
                             Nombre1 = erl.Nombre1,
                             Nombre2 = erl.Nombre2,
                             Numero_Documento_Empl = erl.Numero_Documento_Empl,
                             Ocupacion_Empl = erl.Ocupacion_Empl,
                             Ocupacion_habitual = erl.Ocupacion_habitual,
                             PK_Nit_Empresa = erl.FK_EmpresaTercero,
                             RazonSocial = emp.Razon_Social,
                             RelacionesLaboralesTercero = tt.Descripcion_Tipo_Tercero,
                             Telefono = erl.Telefono,
                             TipoDocumento = td.Descripcion
                         });
                Resultado = R.FirstOrDefault();
            }

            return Resultado;
        }

        public EDIncidente GuardarIncidente(EDIncidente incidente)
        {
            RegistraLog registrarLog = new RegistraLog();

            try
            {
                using (var context = new SG_SSTContext())
                {
                    int idSedeNoPrincipal = 0;
                    using (var Transaction = context.Database.BeginTransaction())
                    {
                        incidente.Incidente_fecha = Convert.ToDateTime(incidente.strIncidente_fecha);
                        //Primero se actualiza la informacion de la empresa y sede
                        //se busca la empresa para actualizar la informacion                    
                        var empresa = context.Tbl_Empresa.Where(e => e.Nit_Empresa.Equals(incidente.General_numero_identificación.Trim())).Select(e => e).FirstOrDefault();
                        var idsedePrincipal = 0;
                        var idsedePrincipal1 = context.Tbl_Sede.Where(sd => sd.Fk_Id_Empresa == empresa.Pk_Id_Empresa && sd.Nombre_Sede.Equals("Principal")).Select(sdi => sdi.Pk_Id_Sede).FirstOrDefault();
                        if (idsedePrincipal1 > 0)
                            idsedePrincipal = idsedePrincipal1;
                        else
                            idsedePrincipal = context.Tbl_Sede.Where(sd => sd.Fk_Id_Empresa == empresa.Pk_Id_Empresa).Select(sdi => sdi.Pk_Id_Sede).ToList().Min();

                        var sedePrincipalMunicipio = (from sdm in context.Tbl_SedeMunicipio
                                                      where sdm.Fk_id_Sede == idsedePrincipal
                                                      select sdm).FirstOrDefault();
                        incidente.FK_id_sede_general = idsedePrincipal;
                        if (!incidente.General_mismos_datos_sede_principal)
                        {
                            var sedeNoPrincipal = context.Tbl_Sede.Where(sd => sd.Pk_Id_Sede == incidente.General_sede.IdSede).Select(sd => sd).FirstOrDefault();
                            var sedeMunicipio = (from sdm in context.Tbl_SedeMunicipio
                                                 where sdm.Fk_id_Sede == sedeNoPrincipal.Pk_Id_Sede
                                                 select sdm).FirstOrDefault();
                            //Se actualiza los datos de la sede que no es principal
                            sedeNoPrincipal.Direccion_Sede = incidente.General_sede.DireccionSede;
                            sedeNoPrincipal.Telefono = incidente.General_sede.Telefono;
                            sedeMunicipio.Fk_Id_Municipio = incidente.General_sede.IdMunicipio;
                            sedeNoPrincipal.Sector = incidente.General_sede.Sector;
                            idSedeNoPrincipal = sedeNoPrincipal.Pk_Id_Sede;
                        }
                        //Actualiza dosto de la empresa
                        empresa.Codigo_Actividad = int.Parse(incidente.General_codigo);
                        empresa.Descripcion_Actividad = incidente.General_actividad_economica;
                        empresa.Telefono = int.Parse(incidente.General_sede_principal_telefono);
                        empresa.Direccion = incidente.General_sede_principal_direccion;
                        empresa.Email = incidente.General_correo_electronico;
                        //Actualiza datos de la sede principal

                        context.SaveChanges();

                        Incidente inct = new Incidente
                        {
                            FK_id_consecuencia_incidente = incidente.FK_id_consecuencia_incidente,
                            FK_id_departamento_incidente = incidente.FK_id_departamento_incidente,
                            FK_id_incidente_tipo_incidente = incidente.FK_id_incidente_tipo_incidente,
                            FK_id_jornada_habitual_persona = incidente.FK_id_jornada_habitual_persona,
                            FK_id_municipio_incidente = incidente.FK_id_municipio_incidente,
                            FK_id_sede_general = idsedePrincipal,
                            FK_id_sede_no_principal = idSedeNoPrincipal,
                            FK_id_sitio_incidente = incidente.FK_id_sitio_incidente,
                            FK_id_tipo_documento_general = incidente.FK_id_tipo_documento_general,
                            FK_id_tipo_documento_persona = incidente.FK_id_tipo_documento_persona,
                            FK_id_usuariosistema_persona = incidente.Persona_usuario_sistema.IdUsuarioSistema,
                            FK_id_vinculacionlaboral_persona = incidente.FK_id_vinculacionlaboral_persona,
                            FK_id_zonalugar_incidente = incidente.FK_id_zonalugar_incidente,
                            FK_id_zonalugar_persona = incidente.FK_id_zonalugar_persona,
                            General_actividad_economica_id = int.Parse(incidente.General_codigo),
                            General_mismos_datos_sede_principal = incidente.General_mismos_datos_sede_principal,
                            General_numero_identificación = incidente.General_numero_identificación,
                            General_sede_principal_municipio_id = incidente.General_sede_principal_municipio_id,
                            General_sede_principal_zona_id = incidente.General_sede_principal_zona_id,
                            Incidente_descripcion = incidente.Incidente_descripcion,
                            Incidente_fecha = incidente.Incidente_fecha,
                            Incidente_fecha_diligenciamiento = incidente.Incidente_fecha_diligenciamiento,
                            Incidente_jornada_normal = incidente.Incidente_jornada_normal,
                            Incidente_nombre_labor = incidente.Incidente_nombre_labor,
                            Incidente_ocurre_dentro_empresa = incidente.Incidente_ocurre_dentro_empresa,
                            Incidente_realizaba_labor_habitual = incidente.Incidente_realizaba_labor_habitual,
                            Incidente_sitio_incidente_otro = incidente.Incidente_sitio_incidente_otro,
                            Incidente_tiempo_previo_al_incidente = incidente.Incidente_tiempo_previo_al_incidente,
                            Persona_departamento_id = incidente.Persona_departamento_id,
                            Persona_direccion = incidente.Persona_direccion,
                            Persona_fecha_ingreso_empresa = incidente.Persona_fecha_ingreso_empresa,
                            Persona_fecha_nacimiento = incidente.Persona_fecha_nacimiento,
                            Persona_genero = incidente.Persona_genero,
                            Persona_municipio_id = incidente.Persona_municipio_id,
                            Persona_numero_identificacion = incidente.Persona_numero_identificacion,
                            Persona_ocupacion_habitual = incidente.Persona_ocupacion_habitual,
                            Persona_primer_apellido = incidente.Persona_primer_apellido,
                            Persona_primer_nombre = incidente.Persona_primer_nombre,
                            Persona_segundo_apellido = incidente.Persona_segundo_apellido,
                            Persona_segundo_nombre = incidente.Persona_segundo_nombre,
                            Persona_telefono = incidente.Persona_telefono,
                            Dia_Semana_Incidente = incidente.Incidente_dia_semana
                        };

                        context.Tbl_Incidentes.Add(inct);
                        context.SaveChanges();
                        Transaction.Commit();
                        incidente.Pk_Id_Incidente = inct.Pk_Id_Incidente;

                    }
                }
                return incidente;
            }
            catch (Exception ex)
            {
                incidente = new EDIncidente();
                registrarLog.RegistrarError(typeof(IncidenteManager), string.Format("Error en la insercion del formulario de reporte de incidente: {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                return incidente;
            }
        }

        public EDIncidente ConsultarIncidente(EDIncidente_Modelo_Consulta parametros)
        {
            EDIncidente incidente = new EDIncidente();
            using (var context = new SG_SSTContext())
            {
                //var empresa = context.Tbl_Empresa.Where(e => e.Nit_Empresa.Equals(parametros.Nit_Empresa.Trim())).Select(e => e).FirstOrDefault();
                //var sedePrincipal = context.Tbl_Sede.Where(sd => sd.Pk_Id_Sede == (context.Tbl_Sede.Where(sdi => sd.Fk_Id_Empresa == empresa.Pk_Id_Empresa).Select(sdi => sdi.Pk_Id_Sede).Min())).Select(sd => sd).FirstOrDefault();

                incidente = (from i in context.Tbl_Incidentes
                             join ciu in context.Tbl_CIU on i.General_actividad_economica_id equals ciu.Codigo_CIU
                             join e in context.Tbl_Empresa on i.General_numero_identificación equals e.Nit_Empresa
                             join c in context.Tbl_Incidente_Consecuencia on i.FK_id_consecuencia_incidente equals c.Pk_Id_Incidente_Consecuencia
                             join sdp in context.Tbl_Sede on i.FK_id_sede_general equals sdp.Pk_Id_Sede
                             join sdpm in context.Tbl_SedeMunicipio on sdp.Pk_Id_Sede equals sdpm.Fk_id_Sede
                             join msdp in context.Tbl_Municipio on sdpm.Fk_Id_Municipio equals msdp.Pk_Id_Municipio
                             join dsdp in context.Tbl_Departamento on msdp.Fk_Nombre_Departamento equals dsdp.Pk_Id_Departamento
                             join sd in context.Tbl_Sede on i.FK_id_sede_no_principal equals sd.Pk_Id_Sede into lefjoinsede
                             from ljoinsede in lefjoinsede.DefaultIfEmpty()
                             join sdm in context.Tbl_SedeMunicipio on ljoinsede.Pk_Id_Sede equals sdm.Fk_id_Sede into lefjoinsdm
                             from ljoinsdm in lefjoinsdm.DefaultIfEmpty()
                             join msd in context.Tbl_Municipio on ljoinsdm.Fk_Id_Municipio equals msd.Pk_Id_Municipio into lefjoinmsd
                             from ljoinmsd in lefjoinmsd.DefaultIfEmpty()
                             join dsd in context.Tbl_Departamento on ljoinmsd.Fk_Nombre_Departamento equals dsd.Pk_Id_Departamento into lefjoindsd
                             from ljoindsd in lefjoindsd.DefaultIfEmpty()
                             join zem in context.Tbl_ZonaLugar on i.General_sede_principal_zona_id equals zem.PK_ZonaLugar
                             join vcl in context.Tbl_VinculacionLaboral on i.FK_id_vinculacionlaboral_persona equals vcl.PK_VinculacionLaboral
                             join tip in context.Tbl_TipoDocumentos on i.FK_id_tipo_documento_persona equals tip.PK_IDTipo_Documento
                             join mp in context.Tbl_Municipio on i.Persona_municipio_id equals mp.Pk_Id_Municipio
                             join dp in context.Tbl_Departamento on i.Persona_departamento_id equals dp.Pk_Id_Departamento
                             join zp in context.Tbl_ZonaLugar on i.FK_id_zonalugar_persona equals zp.PK_ZonaLugar
                             join jh in context.Tbl_Tipo_Jornada on i.FK_id_jornada_habitual_persona equals jh.Pk_Id_Tipo_Jornada
                             join ti in context.Tbl_Tipo_Incidente on i.FK_id_incidente_tipo_incidente equals ti.Pk_Id_Tipo_Incidente
                             join di in context.Tbl_Departamento on i.FK_id_departamento_incidente equals di.Pk_Id_Departamento
                             join mi in context.Tbl_Municipio on i.FK_id_municipio_incidente equals mi.Pk_Id_Municipio
                             join zi in context.Tbl_ZonaLugar on i.FK_id_zonalugar_incidente equals zi.PK_ZonaLugar
                             join li in context.Tbl_Sitio_Incidente on i.FK_id_sitio_incidente equals li.Pk_Id_Sitio_Incidente
                             join cons in context.Tbl_Incidente_Consecuencia on i.FK_id_consecuencia_incidente equals cons.Pk_Id_Incidente_Consecuencia
                             where i.Pk_Id_Incidente == parametros.IncidenteID
                             select new EDIncidente
                             {
                                 Pk_Id_Incidente = i.Pk_Id_Incidente,
                                 General_sede_principal_departamento_id = dsdp.Pk_Id_Departamento,
                                 General_sede_principal_municipio_id = msdp.Pk_Id_Municipio,
                                 General_actividad_economica = ciu.Descripcion,
                                 General_mismos_datos_sede_principal = i.General_mismos_datos_sede_principal,
                                 General_codigo = ciu.Codigo_CIU.ToString(),
                                 General_razon_social = e.Razon_Social,
                                 General_tipo_documento = new EDTipoDocumento() { Id_Tipo_Documento = i.FK_id_tipo_documento_general, Sigla = e.Tipo_Documento },
                                 General_numero_identificación = i.General_numero_identificación,
                                 General_sede_principal_direccion = e.Direccion,
                                 General_sede_principal_telefono = e.Telefono.ToString(),
                                 General_correo_electronico = e.Email,
                                 General_sede_principal_departamento = dsdp.Nombre_Departamento,
                                 General_sede_principal_municipio = msdp.Nombre_Municipio,
                                 General_sede_principal_zona = zem.Descripcion_ZonaLugar,
                                 General_sede_principal_zona_id = i.General_sede_principal_zona_id,
                                 General_sede = new EDSede()
                                 {
                                     IdSede = ljoinsede.Pk_Id_Sede == null ? 0 : ljoinsede.Pk_Id_Sede,
                                     NombreSede = ljoinsede.Nombre_Sede,
                                     DireccionSede = ljoinsede.Direccion_Sede,
                                     Telefono = ljoinsede.Telefono,
                                     IdDepto = ljoindsd.Pk_Id_Departamento == null ? 0 : ljoindsd.Pk_Id_Departamento,
                                     NombreDepto = ljoindsd.Nombre_Departamento,
                                     NombreMunici = ljoinmsd.Nombre_Municipio,
                                     IdMunicipio = ljoinmsd.Pk_Id_Municipio == null ? 0 : ljoinmsd.Pk_Id_Municipio,
                                     Sector = ljoinsede.Sector
                                 },
                                 Persona_vinculacion_laboral = new EDVinculacionLaboral() { PK_VinculacionLaboral = i.FK_id_vinculacionlaboral_persona, Descripcion_VinculacionLaboral = vcl.Descripcion_VinculacionLaboral },
                                 Persona_primer_apellido = i.Persona_primer_apellido,
                                 Persona_primer_nombre = i.Persona_primer_nombre,
                                 Persona_segundo_apellido = i.Persona_segundo_apellido,
                                 Persona_segundo_nombre = i.Persona_segundo_nombre,
                                 Persona_tipo_documento = new EDTipoDocumento() { Id_Tipo_Documento = i.FK_id_tipo_documento_persona, Sigla = tip.Sigla },
                                 Persona_numero_identificacion = i.Persona_numero_identificacion,
                                 Persona_fecha_nacimiento = i.Persona_fecha_nacimiento,
                                 Persona_genero = i.Persona_genero,
                                 Persona_direccion = i.Persona_direccion,
                                 Persona_telefono = i.Persona_telefono,
                                 Persona_municipio_id = i.Persona_municipio_id,
                                 Persona_departamento_id = i.Persona_departamento_id,
                                 Persona_departamento = dp.Nombre_Departamento,
                                 Persona_municipio = mp.Nombre_Municipio,
                                 Persona_zona = new EDZonaLugar() { PK_ZonaLugar = i.FK_id_zonalugar_persona, Descripcion_ZonaLugar = zp.Descripcion_ZonaLugar },
                                 Persona_ocupacion_habitual = i.Persona_ocupacion_habitual,
                                 Persona_fecha_ingreso_empresa = i.Persona_fecha_ingreso_empresa,
                                 FK_id_jornada_habitual_persona = i.FK_id_jornada_habitual_persona,
                                 Persona_tipo_jornada = new EDTipoJornada() { Nombre_Jornada = jh.Nombre_Jornada },
                                 Incidente_fecha = i.Incidente_fecha,
                                 Incidente_dia_semana = i.Dia_Semana_Incidente,
                                 Incidente_jornada_normal = i.Incidente_jornada_normal,
                                 Incidente_realizaba_labor_habitual = i.Incidente_realizaba_labor_habitual,
                                 Incidente_nombre_labor = i.Incidente_nombre_labor,
                                 FK_id_incidente_tipo_incidente = i.FK_id_incidente_tipo_incidente,
                                 Incidente_tiempo_previo_al_incidente = i.Incidente_tiempo_previo_al_incidente,
                                 Incidente_tiempo_previo_al_incidente_HHMM = i.Incidente_tiempo_previo_al_incidente.ToString(),
                                 Incidente_sitio_incidente_otro = i.Incidente_sitio_incidente_otro,
                                 Incidente_tipo_incidente = new EDTipoIncidente() { Nombre_Incidente = ti.Nombre_Incidente },
                                 Incidente_departamento = new EntidadesDominio.Empresas.EDDepartamento() { Pk_Id_Departamento = i.FK_id_departamento_incidente, Nombre_Departamento = di.Nombre_Departamento },
                                 Incidente_municipio = new EntidadesDominio.Empresas.EDMunicipio() { IdMunicipio = i.FK_id_municipio_incidente, NombreMunicipio = mi.Nombre_Municipio },
                                 Incidente_zona_incidente = new EDZonaLugar() { PK_ZonaLugar = i.FK_id_zonalugar_incidente, Descripcion_ZonaLugar = zi.Descripcion_ZonaLugar },
                                 Incidente_ocurre_dentro_empresa = i.Incidente_ocurre_dentro_empresa,
                                 Incidente_sitio_incidente = new EDSitioIncidente() { Pk_Id_Sitio_Incidente = i.FK_id_sitio_incidente, Nombre_Sitio = li.Nombre_Sitio },
                                 Incidente_consecuencia = new EDIncidenteConsecuencia() { Pk_Id_Incidente_Consecuencia = i.FK_id_consecuencia_incidente, Nombre_consecuencia = cons.Nombre_consecuencia },
                                 Incidente_descripcion = i.Incidente_descripcion,
                                 Incidente_fecha_diligenciamiento = i.Incidente_fecha_diligenciamiento
                             }).FirstOrDefault();
            }

            return incidente;
        }
    }
}
