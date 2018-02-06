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
using SG_SST.Audotoria;
using System.Data.SqlClient;
using SG_SST.EntidadesDominio.Planificacion;
using System.Data.Entity.Core.Objects;

namespace SG_SST.Repositorio.Empresas
{
    public class IncidenteAppManager : IncidenteAPP
    {
        public EDIncidenteAPP GuardarIncidenteAPP(EDIncidenteAPP incidente)
        {
            RegistraLog registrarLog = new RegistraLog();

         
        try
         {
            using (var context = new SG_SSTContext())
            {

                using (var Transaction = context.Database.BeginTransaction())
                {

                    int idDepartamentoTrabajador;
                    int idMunicipioTrabajador;
                    int idDepartamento;
                    int idMunicipio;
                    int idUsuarioSistema;
                    EDBusquedaMunicipio muniSede = null;


                    muniSede = (from s in context.Tbl_Sede
                                join b in context.Tbl_SedeMunicipio on
                                s.Pk_Id_Sede equals b.Fk_id_Sede

                                join i in context.Tbl_Municipio on b.Fk_Id_Municipio equals i.Pk_Id_Municipio


                                join d in context.Tbl_Departamento on i.Fk_Nombre_Departamento equals d.Pk_Id_Departamento

                                where s.Pk_Id_Sede == incidente.FK_id_sede_general
                                select new EDBusquedaMunicipio()
                                {
                                    //IDMunicipio = i.Pk_Id_Municipio,
                                    DescripcionMunicipio = i.Nombre_Municipio,
                                    DescripcionDepartamento = d.Nombre_Departamento

                                }).FirstOrDefault();

                    //se busca la empresa para actualizar la informacion                    
                    var empresa = context.Tbl_Empresa.Where(e => e.Nit_Empresa.Equals(incidente.General_numero_identificacion.Trim())).Select(e => e).FirstOrDefault();
                    var idsedePrincipal = 0;
                    var idsedePrincipal1 = context.Tbl_Sede.Where(sd => sd.Fk_Id_Empresa == empresa.Pk_Id_Empresa && sd.Nombre_Sede.Equals("Principal")).Select(sdi => sdi.Pk_Id_Sede).FirstOrDefault();
                    if (idsedePrincipal1 > 0)
                        idsedePrincipal = idsedePrincipal1;
                    else
                        idsedePrincipal = context.Tbl_Sede.Where(sd => sd.Fk_Id_Empresa == empresa.Pk_Id_Empresa).Select(sdi => sdi.Pk_Id_Sede).ToList().Min();

                    var sedePrincipalMunicipio = (from sdm in context.Tbl_SedeMunicipio
                                                  where sdm.Fk_id_Sede == idsedePrincipal
                                                  select sdm).FirstOrDefault();

                    if (incidente.FK_id_sede_general == idsedePrincipal)
                    {
                        incidente.General_mismos_datos_sede_principal = true;
                        incidente.FK_id_sede_no_principal = 0;
                    }
                    else
                    {
                        incidente.General_mismos_datos_sede_principal = false;
                        incidente.FK_id_sede_no_principal = incidente.FK_id_sede_general;
                    }
                    incidente.FK_id_sede_general = idsedePrincipal;
    
                

                 
                    //Actualiza datos de la sede principal

                    context.SaveChanges();


                    idDepartamento = context.Tbl_Departamento.Where(e => e.Nombre_Departamento.Equals(muniSede.DescripcionDepartamento)).Select(e => e.Pk_Id_Departamento).FirstOrDefault();
                    idMunicipio = context.Tbl_Municipio.Where(m => m.Fk_Nombre_Departamento.Equals(idDepartamento) && m.Nombre_Municipio.Equals(muniSede.DescripcionMunicipio)).Select(e => e.Pk_Id_Municipio).FirstOrDefault();
                    
                    incidente.General_sede_principal_municipio_id = idMunicipio;
                    idUsuarioSistema = context.Tbl_UsuarioSistema.Where(e => e.Documento.Equals(incidente.Persona_numero_identificacion)).Select(e => e.Pk_Id_UsuarioSistema).FirstOrDefault();
                    incidente.FK_id_usuariosistema_persona = idUsuarioSistema;

                    idDepartamentoTrabajador = context.Tbl_Departamento.Where(d => d.Nombre_Departamento.Equals(incidente.departamentoTrabajador)).Select(d => d.Pk_Id_Departamento).FirstOrDefault();
                    idMunicipioTrabajador = context.Tbl_Municipio.Where(m => m.Fk_Nombre_Departamento.Equals(idDepartamentoTrabajador) && m.Nombre_Municipio.Equals(incidente.municipioTrabajador)).Select(e => e.Pk_Id_Municipio).FirstOrDefault();

                    incidente.Persona_departamento_id = idDepartamentoTrabajador;
                    incidente.Persona_municipio_id = idMunicipioTrabajador;

                    Incidente inct = new Incidente
                    {
                        FK_id_consecuencia_incidente = incidente.FK_id_consecuencia_incidente,
                        FK_id_departamento_incidente = incidente.FK_id_departamento_incidente,
                        FK_id_incidente_tipo_incidente = incidente.FK_id_incidente_tipo_incidente,
                        FK_id_jornada_habitual_persona = incidente.FK_id_jornada_habitual_persona,
                        FK_id_municipio_incidente = incidente.FK_id_municipio_incidente,
                        //FK_id_sede_general = incidente.FK_id_sede_general,
                      
                        //prueba
                        FK_id_sede_general = idsedePrincipal,
                        FK_id_sede_no_principal = incidente.FK_id_sede_no_principal,
                        FK_id_sitio_incidente = incidente.FK_id_sitio_incidente,
                        FK_id_tipo_documento_general = incidente.FK_id_tipo_documento_general,
                        FK_id_tipo_documento_persona = incidente.FK_id_tipo_documento_persona,
                        FK_id_usuariosistema_persona = incidente.FK_id_usuariosistema_persona,
                        FK_id_vinculacionlaboral_persona = incidente.FK_id_vinculacionlaboral_persona,
                        FK_id_zonalugar_incidente = incidente.FK_id_zonalugar_incidente,
                        FK_id_zonalugar_persona = incidente.FK_id_zonalugar_persona,
                        General_actividad_economica_id = incidente.General_actividad_economica_id,
                        General_mismos_datos_sede_principal = incidente.General_mismos_datos_sede_principal,
                        General_numero_identificación = incidente.General_numero_identificacion,
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
                        Dia_Semana_Incidente = incidente.Dia_Semana_Incidente
                    };

                    context.Tbl_Incidentes.Add(inct);
                    context.SaveChanges();
                    Transaction.Commit();
                    incidente.Pk_Id_Incidente = inct.Pk_Id_Incidente;


                }

                return incidente;
            }
         }
            catch (Exception ex)
            {
                incidente = new EDIncidenteAPP();
                registrarLog.RegistrarError(typeof(IncidenteAppManager), string.Format("Error en la insercion del formulario de reporte de incidente: {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                return incidente;
            }
        }
    }
  }




