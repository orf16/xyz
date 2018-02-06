using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Models;
using SG_SST.Models.Planificacion;
using SG_SST.Models.Empleado;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Planificacion;
using SG_SST.Audotoria;
using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.EntidadesDominio.Empleado;
using System.Data;
using System.Data.Entity;
using SG_SST.EntidadesDominio.Empresas;

//using SG_SST.Utilidades.Traza;


namespace SG_SST.Repositorio.Planificacion
{
    
    public class PerfilSocioDemograficoManager : IPerfilSocioDemografico
    {
        

        public EDPerfilSocioDemografico GuardarPerfilSociodemografico(EDPerfilSocioDemografico perfilsoc)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
            
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        
                        PerfilSocioDemograficoPlanificacion perfilSocioDemografico = new PerfilSocioDemograficoPlanificacion()
                        {

                           IDEmpleado_PerfilSocioDemoGrafico = perfilsoc.IDEmpleado_PerfilSocioDemoGrafico,
                           //Tipo_Documento = perfilsoc.idtipodoc,
                            PK_Numero_Documento_Empl = perfilsoc.PK_Numero_Documento_Empl,
                            //Nombre1 = perfilsoc.Nombre1,
                            //Nombre2 = perfilsoc.Nombre2,
                            //Apellido1 = perfilsoc.Apellido1,
                            //Apellido2 = perfilsoc.Apellido2,
                            //Fk_Sede = perfilsoc.Pk_Id_Sede,
                            //FK_Clasificacion_De_Peligro = perfilsoc.FK_Clasificacion_De_Peligro,
                            //ZonaLugar = perfilsoc.ZonaLugar,
                            GradoEscolaridad = perfilsoc.GradoEscolaridad,
                            Ingresos = perfilsoc.Ingresos,
                            //OtroPeligro=perfilsoc.Otro,
                            FK_Ciudad = perfilsoc.Fk_Id_Municipio,
                            //Direccion = perfilsoc.Direccion,
                            Conyuge = perfilsoc.Conyuge,
                            Hijos = perfilsoc.Hijos,
                            FK_Estrato = perfilsoc.FK_Estrato,
                            FK_Estado_Civil = perfilsoc.FK_Estado_Civil,
                            //FK_Raza = perfilsoc.FK_Raza,
                            //FK_OcupacionPerfil=perfilsoc.PKOcupacionPerfil,
                            Sexo = perfilsoc.Sexo,
                            //GrupoEtarios = perfilsoc.GrupoEtarios,
                            FK_VinculacionLaboral = perfilsoc.FK_VinculacionLaboral,
                            TurnoTrabajo = perfilsoc.TurnoTrabajo,
                            //Cargo = perfilsoc.Cargo,
                            //FechaIngresoEmpresa = perfilsoc.fechaIngresoEmpresa,
                            FechaIngresoUltimoCargo = perfilsoc.FechaIngresoUltimoCargo,
                            //ocupacion=perfilsoc.OcupacionPerfil,
                            evaluacionesMedicasRequeridas=perfilsoc.evaluacionesMedicasRequeridas,
                            caracteristicasPsicologicas=perfilsoc.caracteristicasPsicologicas,
                            caracteristicasFisicas=perfilsoc.caracteristicasFisicas,
                            FK_Etnia=perfilsoc.FK_Etnia,
                            //Edad=perfilsoc.EdadPersona,
                            nitEmpresa=perfilsoc.nitEmpresa,
                            FK_Proceso=perfilsoc.Procesos,
                            Fk_Sede=perfilsoc.Pk_Id_Sede,
                            ZonaLugar=perfilsoc.ZonaLugar,


                        };
                      

                        context.Tbl_PerfilSocioDemograficoPlanificacion.Add(perfilSocioDemografico);
                        
                        context.SaveChanges();

                        List<Condiciones_Riesgo_Perfil> condiciones = new List<Condiciones_Riesgo_Perfil>();

                        foreach (var per in perfilsoc.condicionesRiesgo)
                        {

                            Condiciones_Riesgo_Perfil condicion = new Condiciones_Riesgo_Perfil();

                          
                            condicion.OtroPeligro = per.Otro;
                            
                            condicion.FK_Clasificacion_De_Peligro = per.FK_Clasificacion_De_Peligro;
                            condicion.tiempoExposicion = per.tiempoExpos;
                            condicion.FK_PerfilSocioDemografico = perfilSocioDemografico.IDEmpleado_PerfilSocioDemoGrafico;
                            condiciones.Add(condicion);

                             
                         
                        }

                        context.Tbl_Condiciones_Riesgo_Perfil.AddRange(condiciones);
                        context.SaveChanges();
                        Transaction.Commit();

                        return perfilsoc;

                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(PerfilSocioDemograficoManager), string.Format("Error al guardar el perfil SocioDemocratio en la  base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }

                }
            }

        }


        //Editar
        public EDPerfilSocioDemografico EditarPerfilSociodemografico(EDPerfilSocioDemografico perfilsoc)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {

                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {

                        var clasificacionPeligro = 0;
                        
                        PerfilSocioDemograficoPlanificacion perfilSocioDemografico = new PerfilSocioDemograficoPlanificacion()
                        {

                            IDEmpleado_PerfilSocioDemoGrafico = perfilsoc.IDEmpleado_PerfilSocioDemoGrafico,
                            //Tipo_Documento = perfilsoc.idtipodoc,
                            PK_Numero_Documento_Empl = perfilsoc.PK_Numero_Documento_Empl,
                            //Nombre1 = perfilsoc.Nombre1,
                            //Nombre2 = perfilsoc.Nombre2,
                            //Apellido1 = perfilsoc.Apellido1,
                            //Apellido2 = perfilsoc.Apellido2,
                            //Fk_Sede = perfilsoc.Pk_Id_Sede,
                            //FK_Clasificacion_De_Peligro = perfilsoc.FK_Clasificacion_De_Peligro,
                            //ZonaLugar = perfilsoc.ZonaLugar,
                            GradoEscolaridad = perfilsoc.GradoEscolaridad,
                            Ingresos = perfilsoc.Ingresos,
                            //OtroPeligro=perfilsoc.Otro,
                            FK_Ciudad = perfilsoc.Fk_Id_Municipio,
                            //Direccion = perfilsoc.Direccion,
                            Conyuge = perfilsoc.Conyuge,
                            Hijos = perfilsoc.Hijos,
                            FK_Estrato = perfilsoc.FK_Estrato,
                            FK_Estado_Civil = perfilsoc.FK_Estado_Civil,
                            //FK_Raza = perfilsoc.FK_Raza,
                            //FK_OcupacionPerfil=perfilsoc.PKOcupacionPerfil,
                            Sexo = perfilsoc.Sexo,
                            //GrupoEtarios = perfilsoc.GrupoEtarios,
                            FK_VinculacionLaboral = perfilsoc.FK_VinculacionLaboral,
                            TurnoTrabajo = perfilsoc.TurnoTrabajo,
                            //Cargo = perfilsoc.Cargo,
                            //FechaIngresoEmpresa = perfilsoc.fechaIngresoEmpresa,
                            FechaIngresoUltimoCargo = perfilsoc.FechaIngresoUltimoCargo,
                            //ocupacion=perfilsoc.OcupacionPerfil,
                            evaluacionesMedicasRequeridas = perfilsoc.evaluacionesMedicasRequeridas,
                            caracteristicasPsicologicas = perfilsoc.caracteristicasPsicologicas,
                            caracteristicasFisicas = perfilsoc.caracteristicasFisicas,
                            FK_Etnia = perfilsoc.FK_Etnia,
                            //Edad=perfilsoc.EdadPersona,
                            nitEmpresa = perfilsoc.nitEmpresa,
                            FK_Proceso = perfilsoc.Procesos,
                            Fk_Sede = perfilsoc.Pk_Id_Sede,
                            ZonaLugar = perfilsoc.ZonaLugar,


                        };
                        context.Entry(perfilSocioDemografico).State = EntityState.Modified;
                        context.SaveChanges();

                        List<Condiciones_Riesgo_Perfil> condiciones = new List<Condiciones_Riesgo_Perfil>();
                       
                        List<Condiciones_Riesgo_Perfil> condicionesGuardar = new List<Condiciones_Riesgo_Perfil>();

                        foreach (var per in perfilsoc.condicionesRiesgo)
                        {


                            if (per.FK_Clasificacion_De_Peligro == 0)
                            {
                                clasificacionPeligro = per.FK_Clasificacion_De_PeligroE;
                            }
                            else
                            {
                                clasificacionPeligro = per.FK_Clasificacion_De_Peligro;
                            }
                            Condiciones_Riesgo_Perfil condicion = new Condiciones_Riesgo_Perfil();



                            if (per.PKCondicionesRiesgoPerfil > 0)
                            {
                                condicion.OtroPeligro = per.Otro;
                                condicion.PK_Condiciones_Riesgo_Perfil = per.PKCondicionesRiesgoPerfil;
                                condicion.FK_Clasificacion_De_Peligro = clasificacionPeligro;
                                condicion.tiempoExposicion = per.tiempoExpos;
                                condicion.FK_PerfilSocioDemografico = perfilSocioDemografico.IDEmpleado_PerfilSocioDemoGrafico;
                                // condiciones.Add(condicion);
                                context.Entry(condicion).State = EntityState.Modified;

                                context.SaveChanges();

                            }
                            else
                            {
                                Condiciones_Riesgo_Perfil condicionG = new Condiciones_Riesgo_Perfil();


                                condicionG.OtroPeligro = per.Otro;

                                condicionG.FK_Clasificacion_De_Peligro = per.FK_Clasificacion_De_Peligro;
                                condicionG.tiempoExposicion = per.tiempoExpos;
                                condicionG.FK_PerfilSocioDemografico = perfilSocioDemografico.IDEmpleado_PerfilSocioDemoGrafico;
                                condicionesGuardar.Add(condicionG);


                            }
                        }


                        context.Tbl_Condiciones_Riesgo_Perfil.AddRange(condicionesGuardar);
                        context.SaveChanges();

                        Transaction.Commit();

                        return perfilsoc;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(PerfilSocioDemograficoManager), string.Format("Error al guardar el perfil SocioDemocratio en la  base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }

                }
            }

        }




        public bool InsertarCargueMasivoPerfil(List<EDPerfilSocioDemografico> perfiles)
        {
            
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (EDPerfilSocioDemografico perfil in perfiles)
                        {

                          

                            PerfilSocioDemograficoPlanificacion perfilSocioDemografico = new PerfilSocioDemograficoPlanificacion()
                   {

                       IDEmpleado_PerfilSocioDemoGrafico = perfil.IDEmpleado_PerfilSocioDemoGrafico,
                       PK_Numero_Documento_Empl = perfil.PK_Numero_Documento_Empl,
                      
                      
                       GradoEscolaridad = perfil.GradoEscolaridad,
                       Ingresos = perfil.Ingresos,
                     
                       FK_Ciudad = perfil.Fk_Id_Municipio,
                   
                       Conyuge = perfil.Conyuge,
                       Hijos = perfil.Hijos,
                       FK_Estrato = perfil.FK_Estrato,
                       FK_Estado_Civil = perfil.FK_Estado_Civil,
                     
                       Sexo = perfil.Sexo,
                       //GrupoEtarios = perfil.GrupoEtarios,
                       FK_VinculacionLaboral = perfil.FK_VinculacionLaboral,
                       TurnoTrabajo = perfil.TurnoTrabajo,
               
                       FechaIngresoUltimoCargo = perfil.FechaIngresoUltimoCargo,
                       
                       evaluacionesMedicasRequeridas = perfil.evaluacionesMedicasRequeridas,
                       caracteristicasPsicologicas = perfil.caracteristicasPsicologicas,
                       caracteristicasFisicas = perfil.caracteristicasFisicas,
                       FK_Etnia = perfil.FK_Etnia,
                       
                       nitEmpresa = perfil.nitEmpresa,

                     
                        FK_Proceso = perfil.Procesos,
                       
                    
                       Fk_Sede = perfil.Pk_Id_Sede,
                       ZonaLugar = perfil.ZonaLugar



                   };

                        
                        context.Tbl_PerfilSocioDemograficoPlanificacion.Add(perfilSocioDemografico);
                        context.SaveChanges();

                        List<Condiciones_Riesgo_Perfil> condiciones = new List<Condiciones_Riesgo_Perfil>();

                        foreach (var per in perfil.condicionesRiesgo)
                        {

                            Condiciones_Riesgo_Perfil condicion = new Condiciones_Riesgo_Perfil();


                            condicion.OtroPeligro = per.Otro;

                            condicion.FK_Clasificacion_De_Peligro = per.FK_Clasificacion_De_Peligro;
                            condicion.tiempoExposicion = per.tiempoExpos;
                            condicion.FK_PerfilSocioDemografico = perfilSocioDemografico.IDEmpleado_PerfilSocioDemoGrafico;
                            condiciones.Add(condicion);



                        }

                        context.Tbl_Condiciones_Riesgo_Perfil.AddRange(condiciones);
                        context.SaveChanges();
                      

                      
                        }
                        Transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        Transaction.Rollback();
                        return false;
                    }
                }
                throw new NotImplementedException();
            }

        }

        public List<EDGradoEscolaridad> ObtenerGradoEscolaridad()
        {

            List<EDGradoEscolaridad> grado = new List<EDGradoEscolaridad>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                grado = (from g in contex.Tbl_GradoEscolaridad

                         select new EDGradoEscolaridad
                         {
                             IdGradoEscolaridad = g.PK_GradoEscolaridad,
                             DescripcionGradoEscolaridad = g.Descripcion_GradoEscolaridad
                         }).ToList();
            }
            return grado;

        }



        public bool EliminarExpocionPeligro(int idExposicion)
        {

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        Condiciones_Riesgo_Perfil cond = context.Tbl_Condiciones_Riesgo_Perfil.Find(idExposicion);
                        context.Tbl_Condiciones_Riesgo_Perfil.Remove(cond);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(PerfilSocioDemograficoManager), string.Format("Error al eliminar la exposición  de la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool EliminarPerfilSocioDemografico(int idPerfil)
        {

            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        PerfilSocioDemograficoPlanificacion perfil = context.Tbl_PerfilSocioDemograficoPlanificacion.Find(idPerfil);
                        context.Tbl_PerfilSocioDemograficoPlanificacion.Remove(perfil);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(DxGralCondicionesDeSaludManager), string.Format("Error al eliminar el documento de dx en la base de datos  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }



        public List<EDOcupacionPerfil> BuscarOcupacion(string prefijo)
        {
            var context = new SG_SSTContext();

            return context.Tbl_Ocupaciones_De_Perfil.Where(d => d.grupoPrimario.Contains(prefijo) || d.codigo.Contains(prefijo)).Select(d => new EDOcupacionPerfil()
            {
                PKOcupacionPerfil=d.PK_OcupacionPerfil,
                grupoPrimario=d.grupoPrimario,
                codigo=d.codigo
          
            }).ToList();
        }

        public EDBusquedaMunicipio BuscarMunicipiosDeSede(int fk_sede)
        {


          EDBusquedaMunicipio busMun = new EDBusquedaMunicipio();


            using (var context = new SG_SSTContext())
            {
                busMun = (from s in context.Tbl_Sede
                          join b in context.Tbl_SedeMunicipio on
                          s.Pk_Id_Sede equals b.Fk_id_Sede

                          join i in context.Tbl_Municipio on b.Fk_Id_Municipio equals i.Pk_Id_Municipio


                          join d in context.Tbl_Departamento on i.Fk_Nombre_Departamento equals d.Pk_Id_Departamento

                          where s.Pk_Id_Sede == fk_sede
                          select new EDBusquedaMunicipio()
                          {
                              //IDMunicipio = i.Pk_Id_Municipio,
                              DescripcionMunicipio = i.Nombre_Municipio,
                              DescripcionDepartamento = d.Nombre_Departamento

                          }).FirstOrDefault();
                return busMun;
            }
        }


        public EDPerfilSocioDemografico obtenerPerfilesPorID(int id)
        {

            EDPerfilSocioDemografico per = new EDPerfilSocioDemografico();
            using (SG_SSTContext contex = new SG_SSTContext())
            {

                per = (from perfil in contex.Tbl_PerfilSocioDemograficoPlanificacion

                            where perfil.IDEmpleado_PerfilSocioDemoGrafico == id



                            select new EDPerfilSocioDemografico
                            {
                                IDEmpleado_PerfilSocioDemoGrafico=perfil.IDEmpleado_PerfilSocioDemoGrafico,
                                PK_Numero_Documento_Empl=perfil.PK_Numero_Documento_Empl,
                                GradoEscolaridad=perfil.GradoEscolaridad,
                                Ingresos=perfil.Ingresos,
                                Conyuge=perfil.Conyuge,
                                Hijos=perfil.Hijos,
                                estrato=perfil.Tbl_Estrato.Descripcion_Estrato,
                                estadoCivil=perfil.Tbl_Estado_Civil.Descripcion_EstadoCivil,
                                Sexo=perfil.Sexo,
                                vinculacionLabotal=perfil.Tbl_VinculacionLaboral.Descripcion_VinculacionLaboral,
                                TurnoTrabajo=perfil.TurnoTrabajo,
                                FechaIngresoUltimoCargo=perfil.FechaIngresoUltimoCargo,
                                municipio=perfil.municipios.Nombre_Municipio,
                                ZonaLugar=perfil.ZonaLugar,
                                nombreSede=perfil.Sede.Nombre_Sede,
                                etnia=perfil.Tbl_Etnia.Descripcion_Etnia,
                                caracteristicasFisicas=perfil.caracteristicasFisicas,
                                caracteristicasPsicologicas=perfil.caracteristicasPsicologicas,
                                evaluacionesMedicasRequeridas=perfil.evaluacionesMedicasRequeridas,
                                nombreProceso=perfil.Procesos.Descripcion_Proceso,
                                nitEmpresa=perfil.nitEmpresa,
                                
                               
                                Pk_Id_Sede=perfil.Fk_Sede,
                                FK_Estado_Civil=perfil.FK_Estado_Civil,
                                FK_Estrato=perfil.FK_Estrato,
                                Fk_Id_Municipio=perfil.FK_Ciudad,
                                FK_VinculacionLaboral=perfil.FK_VinculacionLaboral,
                                FK_Etnia=perfil.FK_Etnia,
                                Procesos=perfil.FK_Proceso,
                                Fk_Id_Departamento=perfil.municipios.Departamento.Pk_Id_Departamento,
                                departamento=perfil.municipios.Departamento.Nombre_Departamento,
                            }).FirstOrDefault();

                return per;

            }
        }

        public List<EDPerfilSocioDemografico> obtenerPerfilesPorEmpresa(string nitEmpresa)
        {

           List<EDPerfilSocioDemografico> per = new List<EDPerfilSocioDemografico>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {

                per = (from perfil in contex.Tbl_PerfilSocioDemograficoPlanificacion

                       where perfil.nitEmpresa == nitEmpresa



                       select new EDPerfilSocioDemografico
                       {
                           IDEmpleado_PerfilSocioDemoGrafico = perfil.IDEmpleado_PerfilSocioDemoGrafico,
                           PK_Numero_Documento_Empl = perfil.PK_Numero_Documento_Empl,
                           GradoEscolaridad = perfil.GradoEscolaridad,
                           Ingresos = perfil.Ingresos,
                           Conyuge = perfil.Conyuge,
                           Hijos = perfil.Hijos,
                           estrato = perfil.Tbl_Estrato.Descripcion_Estrato,
                           estadoCivil = perfil.Tbl_Estado_Civil.Descripcion_EstadoCivil,
                           Sexo = perfil.Sexo,
                           vinculacionLabotal = perfil.Tbl_VinculacionLaboral.Descripcion_VinculacionLaboral,
                           TurnoTrabajo = perfil.TurnoTrabajo,
                           FechaIngresoUltimoCargo = perfil.FechaIngresoUltimoCargo,
                           municipio = perfil.municipios.Nombre_Municipio,
                           ZonaLugar = perfil.ZonaLugar,
                           nombreSede = perfil.Sede.Nombre_Sede,
                           etnia = perfil.Tbl_Etnia.Descripcion_Etnia,
                           caracteristicasFisicas = perfil.caracteristicasFisicas,
                           caracteristicasPsicologicas = perfil.caracteristicasPsicologicas,
                           evaluacionesMedicasRequeridas = perfil.evaluacionesMedicasRequeridas,
                           nombreProceso = perfil.Procesos.Descripcion_Proceso,
                           nitEmpresa = perfil.nitEmpresa,


                           Pk_Id_Sede = perfil.Fk_Sede,
                           FK_Estado_Civil = perfil.FK_Estado_Civil,
                           FK_Estrato = perfil.FK_Estrato,
                           Fk_Id_Municipio = perfil.FK_Ciudad,
                           FK_VinculacionLaboral = perfil.FK_VinculacionLaboral,
                           FK_Etnia = perfil.FK_Etnia,
                           Procesos = perfil.FK_Proceso,
                           Fk_Id_Departamento = perfil.municipios.Departamento.Pk_Id_Departamento,
                           departamento = perfil.municipios.Departamento.Nombre_Departamento,
                       }).ToList();

                return per;

            }
        }

        public List<EDCondicionesRiesgoPerfil> ObtenerCondicionesRiesgoPorEmpresa(string nitEmpresa)
        {

            List<EDCondicionesRiesgoPerfil> condicionesRiesgo = new List<EDCondicionesRiesgoPerfil>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {

                condicionesRiesgo = (from condiciones in contex.Tbl_Condiciones_Riesgo_Perfil
                                     join perfil in contex.Tbl_PerfilSocioDemograficoPlanificacion
                                     on condiciones.FK_PerfilSocioDemografico equals perfil.IDEmpleado_PerfilSocioDemoGrafico
                                     where perfil.nitEmpresa==nitEmpresa



                                     select new EDCondicionesRiesgoPerfil
                                     {
                                         //ID_ActividadActosInseguros = activi.PK_ID_ActividadActosInseguros,
                                         //nombreActividad = activi.NombreActividad,
                                         //RespActividad = activi.ResponsableActividad,
                                         //FecEjecucion = activi.FechaEjecucion,
                                         //FKReportes = activi.FK_Id_Reportes,

                                         PKCondicionesRiesgoPerfil = condiciones.PK_Condiciones_Riesgo_Perfil,
                                         FK_Clasificacion_De_Peligro = condiciones.FK_Clasificacion_De_Peligro,
                                         OtroPeligro = condiciones.OtroPeligro,
                                         tiempoExpos = condiciones.tiempoExposicion,
                                         FKPerfilSocioDemografico = condiciones.FK_PerfilSocioDemografico,
                                         ClasificacionPeligro = condiciones.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro,
                                         FK_Tipo_De_Peligro = condiciones.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro,
                                     }).ToList();

                return condicionesRiesgo;

            }
        }


        public List<EDCondicionesRiesgoPerfil> ObtenerCondicionesRiesgoPerfilPorID(int id)
        {

            List<EDCondicionesRiesgoPerfil> condicionesRiesgo = new List<EDCondicionesRiesgoPerfil>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {

                condicionesRiesgo = (from condiciones in contex.Tbl_Condiciones_Riesgo_Perfil

                               where condiciones.FK_PerfilSocioDemografico == id



                               select new EDCondicionesRiesgoPerfil
                               {
                                   //ID_ActividadActosInseguros = activi.PK_ID_ActividadActosInseguros,
                                   //nombreActividad = activi.NombreActividad,
                                   //RespActividad = activi.ResponsableActividad,
                                   //FecEjecucion = activi.FechaEjecucion,
                                   //FKReportes = activi.FK_Id_Reportes,
                                  
                                   PKCondicionesRiesgoPerfil=condiciones.PK_Condiciones_Riesgo_Perfil,
                                   FK_Clasificacion_De_Peligro=condiciones.FK_Clasificacion_De_Peligro,
                                   OtroPeligro=condiciones.OtroPeligro,
                                   tiempoExpos=condiciones.tiempoExposicion,
                                   FKPerfilSocioDemografico=condiciones.FK_PerfilSocioDemografico,
                                   ClasificacionPeligro=condiciones.Tbl_Clasificacion_De_Peligro.Descripcion_Clase_De_Peligro,
                                   FK_Tipo_De_Peligro=condiciones.Tbl_Clasificacion_De_Peligro.FK_Tipo_De_Peligro,
                               }).ToList();

                return condicionesRiesgo;

            }
        }



        public List<EDProceso> ObtenerProcesoEmpresa(string nit)
        {

            List<EDProceso> procesos = new List<EDProceso>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                procesos = (from p in contex.Tbl_Procesos 
                            join pe in contex.Tbl_ProcesoEmpresa
                            on p.Pk_Id_Proceso equals pe.Fk_Id_Proceso
                            join e in contex.Tbl_Empresa 
                            on pe.Fk_Id_Empresa equals e.Pk_Id_Empresa
                            where e.Nit_Empresa==nit


                         select new EDProceso
                         {
                             Id_Proceso=p.Pk_Id_Proceso,
                             Descripcion=p.Descripcion_Proceso
                      
                         }).ToList();
            }
            return procesos;

        }

    }
}
