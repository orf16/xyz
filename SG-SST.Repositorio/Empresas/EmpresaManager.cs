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
using SG_SST.Models.LiderazgoGerencial;
using System.Data.Entity;


namespace SG_SST.Repositorio.Empresas
{
    public class EmpresaManager : IEmpresa
    {

        public List<EDDepartamento> ObtenerDepartamentos()
        {
             List<EDDepartamento> Departamentos = new List<EDDepartamento>();
             using (SG_SSTContext context = new SG_SSTContext())
             {
                 using (var tx = context.Database.BeginTransaction())
                 
                 {
                     RegistraLog registraLog = new RegistraLog();
                     try
                     {
                      Departamentos = context.Tbl_Departamento.Select(td => new EDDepartamento
                     {
                        Pk_Id_Departamento = td.Pk_Id_Departamento,
                        Codigo_Departamento= td.Codigo_Departamento,
                        Nombre_Departamento=td.Nombre_Departamento,
                     }).ToList();              
                     }
                     catch(Exception ex)
                     {
                         registraLog.RegistrarError(typeof(EmpresaManager), string.Format("Error al registrar el Plan de Accion  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                         tx.Rollback();
                         return null;

                     }

                 }

             }
             return Departamentos;

        }

        public List<EDMunicipio> ObtenerMunicipiosporDepartamento(int id)
        {
            List<EDMunicipio> Municipios = new List<EDMunicipio>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var tx = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        Municipios = (from mun in context.Tbl_Municipio
                                      where mun.Fk_Nombre_Departamento == id
                                      select new EDMunicipio
                                      {
                                          IdMunicipio=mun.Pk_Id_Municipio,
                                          NombreMunicipio=mun.Nombre_Municipio,
                                          CodigoMunicipio=mun.Codigo_Municipio,
                                      }).ToList(); 
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(EmpresaManager), string.Format("Error al registrar el Plan de Accion  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        tx.Rollback();
                        return null;

                    }

                }

            }
            return Municipios;

        }



        public List<EDSede> ObtenernerSedesPorEmpresa(string Nit)
        {
            List<EDSede> Sedes = new List<EDSede>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Sedes = (from s in context.Tbl_Sede
                         join e in context.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                         join sdm in context.Tbl_SedeMunicipio on s.Pk_Id_Sede equals sdm.Fk_id_Sede
                         join mp in context.Tbl_Municipio on sdm.Fk_Id_Municipio equals mp.Pk_Id_Municipio
                         join dept in context.Tbl_Departamento on mp.Fk_Nombre_Departamento equals dept.Pk_Id_Departamento
                         where e.Nit_Empresa == Nit
                         select new EDSede
                         {
                             DireccionSede = s.Direccion_Sede,
                             IdEmpresa = e.Pk_Id_Empresa,
                             IdSede = s.Pk_Id_Sede,
                             NombreSede = s.Nombre_Sede,
                             Sector = s.Sector,
                             IdMunicipio = mp.Pk_Id_Municipio,
                             NombreMunici = mp.Nombre_Municipio,
                             IdDepto = dept.Pk_Id_Departamento,
                             NombreDepto = dept.Nombre_Departamento
                         }).ToList();
            }
            return Sedes;
        }

        public EDSede ObtenernerSedesPorEmpresa(int IdSede)
        {
            EDSede Sedes = new EDSede();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Sedes = (from s in context.Tbl_Sede
                         join e in context.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                         join sdm in context.Tbl_SedeMunicipio on s.Pk_Id_Sede equals sdm.Fk_id_Sede
                         join mp in context.Tbl_Municipio on sdm.Fk_Id_Municipio equals mp.Pk_Id_Municipio
                         join dept in context.Tbl_Departamento on mp.Fk_Nombre_Departamento equals dept.Pk_Id_Departamento
                         where s.Pk_Id_Sede == IdSede
                         select new EDSede
                         {
                             DireccionSede = s.Direccion_Sede,
                             IdEmpresa = e.Pk_Id_Empresa,
                             IdSede = s.Pk_Id_Sede,
                             NombreSede = s.Nombre_Sede,
                             Sector = s.Sector,
                             IdMunicipio = mp.Pk_Id_Municipio,
                             NombreMunici = mp.Nombre_Municipio,
                             IdDepto = dept.Pk_Id_Departamento,
                             NombreDepto = dept.Nombre_Departamento
                         }).FirstOrDefault();
            }
            return Sedes;
        }

        public List<EDCIIU> ObtenerActividadesEconomicas()
        {
            throw new NotImplementedException();
        }


        public void GuardarRolesParaNuevaEmpresa()
        {
            try
            {
                using (SG_SSTContext context = new SG_SSTContext())
                {
                    using (var tx = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var roles = (from rol in context.Tbl_Rol
                                         join rc in context.Tbl_Responsabilidades_Por_Rol on rol.Pk_Id_Rol equals rc.Fk_Id_Rol
                                         join rcr in context.Tbl_Rendicion_Cuenta_Por_Rol on rol.Pk_Id_Rol equals rcr.Fk_Id_Rol
                                         group rol by new { rol.Descripcion } into grpRol
                                         select new
                                         {
                                             NombreRol = grpRol.Key.Descripcion,
                                         }).ToList();
                            if (roles != null && roles.Count > 0)
                            {
                                foreach (var rol in roles)
                                {
                                    var rolRespoRenCuent = context.Tbl_Rol.Where(r => r.Descripcion.Equals(rol.NombreRol)).FirstOrDefault();
                                    var responsRol = (from rsp in context.Tbl_Responsabilidades
                                                      join rspr in context.Tbl_Responsabilidades_Por_Rol on rsp.Pk_Id_Responsabilidades equals rspr.Fk_Id_Responsabilidades
                                                      where rspr.Fk_Id_Rol == rolRespoRenCuent.Pk_Id_Rol
                                                      select rsp).ToList();

                                    var rendCuentasRol = (from rc in context.Tbl_RendicionDeCuentas
                                                          join rcr in context.Tbl_Rendicion_Cuenta_Por_Rol on rc.Pk_Id_RendicionDeCuentas equals rcr.Fk_Id_RendicionDeCuentas
                                                          where rcr.Fk_Id_Rol == rolRespoRenCuent.Pk_Id_Rol
                                                          select rc).ToList();
                                    var nuevoRol = new Rol()
                                    {
                                        Descripcion = rol.NombreRol,
                                        Fk_Id_Empresa = null
                                    };
                                    context.Tbl_Rol.Add(nuevoRol);
                                    context.SaveChanges();
                                    List<Responsabilidades> respons = new List<Responsabilidades>();
                                    foreach (var rpr in responsRol)
                                    {
                                        var resp = new Responsabilidades();
                                        resp.ResponsabilidadesPorRoles = new List<ResponsabilidadesPorRol>();
                                        ResponsabilidadesPorRol rxrol = new ResponsabilidadesPorRol();
                                        resp.Descripcion = rpr.Descripcion;
                                        rxrol.Rol = nuevoRol;
                                        resp.ResponsabilidadesPorRoles.Add(rxrol);
                                        respons.Add(resp);
                                    }
                                    context.Tbl_Responsabilidades.AddRange(respons);
                                    List<RendicionDeCuentas> rendic = new List<RendicionDeCuentas>();
                                    foreach (var rcpr in rendCuentasRol)
                                    {
                                        RendicionDeCuentas rend = new RendicionDeCuentas();
                                        rend.RendicionDeCuentasPorRoles = new List<RendicionDeCuentasPorRol>();
                                        RendicionDeCuentasPorRol rdxrol = new RendicionDeCuentasPorRol();
                                        rend.Descripcion = rcpr.Descripcion;
                                        rdxrol.Rol = nuevoRol;
                                        rend.RendicionDeCuentasPorRoles.Add(rdxrol);
                                        rendic.Add(rend);
                                    }
                                    context.Tbl_RendicionDeCuentas.AddRange(rendic);
                                }
                                tx.Commit();
                            }
                        }
                        catch (Exception ex)
                        {
                            tx.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        public EDEmpresas GuardarLogoEmpresa(EDEmpresas logo)
        {
            var Logo = logo.Id_Empresa;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    RegistraLog registraLog = new RegistraLog();
                    try
                    {
                        Empresa edit = new Empresa();
                        edit = (from ar in context.Tbl_Empresa
                                where ar.Pk_Id_Empresa == Logo
                                select ar).FirstOrDefault();
                        if(edit!=null & edit.Logo_Empresa!=null)
                        {
                            edit.Logo_Empresa = logo.NombreArchivo;
                            context.Entry(edit).State = EntityState.Modified;  
                        }
                        else
                        {
                            edit.Logo_Empresa = logo.NombreArchivo;
                            //context.Tbl_Empresa.Add(edit.Logo_Empresa);
                        }
                        context.SaveChanges();
                        Transaction.Commit();
                        return logo;
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(EmpresaManager), string.Format("Error al registrar la Modificacion de la Condicion Insegura  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;
                    }
                }
            }
        }

        public EDEmpresas ObtenerLogoEmpresa(string nitempresa)
        {
            EDEmpresas logoempresa = new EDEmpresas();
            //var EditFres = null;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {

                    RegistraLog registraLog = new RegistraLog();

                    try
                    {
                         logoempresa = (from e in context.Tbl_Empresa
                                        where e.Nit_Empresa == nitempresa
                                        select new EDEmpresas{
                                            NombreArchivo=e.Logo_Empresa,
                                        }).FirstOrDefault();
                    }
                    catch (Exception ex)
                    {
                        registraLog.RegistrarError(typeof(EmpresaManager), string.Format("Error al obtener la información  {0}: {1}", DateTime.Now, ex.StackTrace), ex);
                        Transaction.Rollback();
                        return null;

                    }
                }
                return logoempresa;
            }

        }

        /// <summary>
        /// Registra la información de una nueva empresa, sus roles, sede principal,
        /// responsabilidades y rendición de cuentas
        /// </summary>
        /// <param name="empresa"></param>
        /// <returns></returns>
        public EDEmpresas GuardarEmpresaYSusRelaciones(EDEmpresas empresa)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var Transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //se registra en base de datos la nueva empresa
                        Empresa empre = new Empresa()
                        {
                            Pk_Id_Empresa = empresa.Id_Empresa,
                            Nit_Empresa = empresa.Nit_Empresa,
                            Tipo_Documento = empresa.Tipo_Documento,
                            Identificacion_Representante = empresa.Identificacion_Representante,
                            Razon_Social = empresa.Razon_Social,
                            Direccion = empresa.Direccion,
                            Telefono = empresa.Telefono,
                            Fax = empresa.Fax,
                            Riesgo = empresa.Riesgo,
                            Total_Empleados = empresa.Total_Empleados,
                            ID_Seccional = empresa.IdSeccional,
                            ID_Sector_Economico = empresa.IdSectorEconomico,
                            Email = empresa.Email == null ? "" : empresa.Email.ToLower(),
                            Sitio_Web = empresa.SitioWeb,
                            Codigo_Actividad = empresa.Codigo_Actividad,
                            Fecha_Vigencia_Actual = empresa.Fecha_Vigencia_Actual,
                            Flg_Estado = empresa.Flg_Estado,
                            Zona = empresa.Zona,
                            Descripcion_Actividad = empresa.Descripcion_Actividad
                        };
                        context.Tbl_Empresa.Add(empre);
                        context.SaveChanges();
                        //se obtienen los roles base asociados a la empresa.
                        var listaRoles = context.Tbl_RolesBase.Select(r => r).ToList();
                        //var nombreParametro = Enumeraciones.EnumAdministracionUsuarios.ParametrosSistemaPorNombre.RolesBaseEmpresa;
                        //var rolesEmpresa = context.Tbl_ParametrosSistema.Where(ps => ps.NombreParametro.Equals(nombreParametro)).Select(ps => ps.Valor).FirstOrDefault();
                        //se obtienen los roles base que deben estar asociados a una nueva empresa
                        //var roles = (from rol in context.Tbl_Rol
                        //             join rc in context.Tbl_Responsabilidades_Por_Rol on rol.Pk_Id_Rol equals rc.Fk_Id_Rol
                        //             join rcr in context.Tbl_Rendicion_Cuenta_Por_Rol on rol.Pk_Id_Rol equals rcr.Fk_Id_Rol
                        //             group rol by new { rol.Descripcion } into grpRol
                        //             select new
                        //             {
                        //                 NombreRol = grpRol.Key.Descripcion,
                        //             }).ToList();
                        if (listaRoles != null && listaRoles.Count > 0)
                        {
                            //para cada rol base obtenido se registran las responsabilidades
                            //y las rendiciones de cuentas asociadas a cada rol
                            foreach (var rol in listaRoles)
                            {
                                //var rolRespoRenCuent = context.Tbl_Rol.Where(r => r.Descripcion.Equals(rol)).FirstOrDefault();
                                //se obtiene el listado de responsabilidades asociadas a cada rol
                                var responsRol = (from rsp in context.Tbl_ResponsabilidadesBase
                                                  join rspr in context.Tbl_Roles_Por_ResponsabilidadesBase on rsp.Pk_Id_ResponsabilidadesBase equals rspr.Fk_Id_ResponsabilidadesBase
                                                  where rspr.Fk_Id_RolesBase == rol.Pk_Id_RolesBase
                                                  select rsp).ToList();
                                //se obtiene el listado de rendición de cuentas asociadas a cada rol
                                var rendCuentasRol = (from rc in context.Tbl_RendicionDeCuentasBase
                                                      join rcr in context.Tbl_Roles_Por_RendicionDeCuentasBase on rc.Pk_Id_RendicionDeCuentasBase equals rcr.Fk_Id_RendicionDeCuentasBase
                                                      where rcr.Fk_Id_RolesBase == rol.Pk_Id_RolesBase
                                                      select rc).ToList();
                                var nuevoRol = new Rol()
                                {
                                    Descripcion = rol.Descripcion,
                                    Fk_Id_Empresa = empre.Pk_Id_Empresa
                                };
                                context.Tbl_Rol.Add(nuevoRol);
                                context.SaveChanges();
                                List<Responsabilidades> respons = new List<Responsabilidades>();
                                foreach (var rpr in responsRol)
                                {
                                    var resp = new Responsabilidades();
                                    resp.ResponsabilidadesPorRoles = new List<ResponsabilidadesPorRol>();
                                    ResponsabilidadesPorRol rxrol = new ResponsabilidadesPorRol();
                                    resp.Descripcion = rpr.Descripcion;
                                    rxrol.Rol = nuevoRol;
                                    resp.ResponsabilidadesPorRoles.Add(rxrol);
                                    respons.Add(resp);
                                }
                                context.Tbl_Responsabilidades.AddRange(respons);
                                List<RendicionDeCuentas> rendic = new List<RendicionDeCuentas>();
                                foreach (var rcpr in rendCuentasRol)
                                {
                                    RendicionDeCuentas rend = new RendicionDeCuentas();
                                    rend.RendicionDeCuentasPorRoles = new List<RendicionDeCuentasPorRol>();
                                    RendicionDeCuentasPorRol rdxrol = new RendicionDeCuentasPorRol();
                                    rend.Descripcion = rcpr.Descripcion;
                                    rdxrol.Rol = nuevoRol;
                                    rend.RendicionDeCuentasPorRoles.Add(rdxrol);
                                    rendic.Add(rend);
                                }
                                context.Tbl_RendicionDeCuentas.AddRange(rendic);
                            }
                            context.SaveChanges();
                        }
                        //se registra en base de datos la nueva sede principal
                        Sede nuevaSede = new Sede()
                        {
                            Fk_Id_Empresa = empre.Pk_Id_Empresa,
                            Nombre_Sede = "Principal",
                            Direccion_Sede = empre.Direccion,
                            Sector = "Urbano"
                        };
                        context.Tbl_Sede.Add(nuevaSede);
                        context.SaveChanges();
                        //se consulta y se guarda la relación de la sede principal con el municipio
                        var codMunicipio = empresa.Municipio.CodigoMunicipio;
                        var codDepartamento = empresa.Departamento.Codigo_Departamento;
                        var idMunicipio = (from m in context.Tbl_Municipio
                                           join d in context.Tbl_Departamento on m.Fk_Nombre_Departamento equals d.Pk_Id_Departamento
                                           where m.Codigo_Municipio.Equals(codMunicipio)
                                           && d.Codigo_Departamento.Equals(codDepartamento)
                                           select m.Pk_Id_Municipio).FirstOrDefault();
                        SedeMunicipio nuevaSedeMun = new SedeMunicipio()
                        {
                            Fk_id_Sede = nuevaSede.Pk_Id_Sede,
                            Fk_Id_Municipio = idMunicipio
                        };
                        context.Tbl_SedeMunicipio.Add(nuevaSedeMun);
                        context.SaveChanges();
                        //Actualiza la empresa con sus roles para que queden asociados
                        //context.Tbl_Rol.Where(r => r.Fk_Id_Empresa.Equals(null)).ToList().ForEach(r => r.Fk_Id_Empresa = empre.Pk_Id_Empresa);
                        context.SaveChanges();
                        Transaction.Commit();
                        empresa.Id_Empresa = empre.Pk_Id_Empresa;
                    }
                    catch (Exception e)
                    {
                        Transaction.Rollback();
                        return empresa;
                    }
                }
            }
            return empresa;
        }

        public void GuardarSedePrincipal(EDSede sede)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var tx = context.Database.BeginTransaction())
                {
                    try
                    {
                        Sede nuevaSede = new Sede()
                        {
                            Fk_Id_Empresa = sede.IdEmpresa,
                            Nombre_Sede = "Principal",
                            Direccion_Sede = sede.DireccionSede,
                            Sector = "Urbano"
                        };
                        context.Tbl_Sede.Add(nuevaSede);
                        context.SaveChanges();
                        sede.IdSede = nuevaSede.Pk_Id_Sede;
                        var idMunicipio = context.Tbl_Municipio.Where(m => m.Codigo_Municipio == sede.IdMunicipio.ToString()).Select(e => e.Pk_Id_Municipio).FirstOrDefault();
                        
                        SedeMunicipio nuevaSedeMun = new SedeMunicipio()
                        {
                            Fk_id_Sede = sede.IdSede,
                            Fk_Id_Municipio = int.Parse(idMunicipio.ToString ())
                        };
                        context.Tbl_SedeMunicipio.Add(nuevaSedeMun);
                        context.SaveChanges();
                        tx.Commit();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                    }
                }
            }
        }
        public List<EDEmpresa_Usuaria> ObtenerEmpresasUsuariasPorEmpresa(string Nit)
        {
            List<EDEmpresa_Usuaria> EmpresasUsuarias = new List<EDEmpresa_Usuaria>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                EmpresasUsuarias = contex.Tbl_Empresas_Usuarias.Where(eu => eu.Documento_Empresa.Trim().Equals(Nit.Trim()))
                    .Select(eu => new EDEmpresa_Usuaria
                    {
                        IdEmpresaUsuaria = eu.PK_Id_Empresa_Usuaria,
                        RazonSocial = eu.Razon_Social,
                        DT = new System.Data.DataTable()
                    }).ToList();
            }
            return EmpresasUsuarias;
        }

        public EDEmpresaEvaluar ObtenerDatosEmpresaEvaluar(string Nit, string Responsable)
        {
            EDEmpresaEvaluar empresa = new EDEmpresaEvaluar();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                empresa = (from e in context.Tbl_Empresa
                           join ci in context.Tbl_CIU on e.Codigo_Actividad equals ci.Codigo_CIU
                           join u in context.Tbl_Usuario on e.Pk_Id_Empresa equals u.Fk_Id_Empresa
                           join ur in context.Tbl_UsuarioRol on u.Pk_Id_Usuario equals ur.Fk_Id_Usuario
                           join r in context.Tbl_Rol on ur.Fk_Id_Rol equals r.Pk_Id_Rol
                           where e.Nit_Empresa.Equals(Nit) && r.Descripcion.ToLower().Trim().Equals(Responsable.ToLower())
                           select new EDEmpresaEvaluar
                           {
                               Nit = e.Nit_Empresa,
                               RazonSocial = e.Razon_Social,
                               CodActividadEconomica = e.Codigo_Actividad,
                               ActividadEconomica = ci.Descripcion,
                               ResponsableSGSST = u.Nombre_Usuario
                           }).FirstOrDefault();
            }

            return empresa;
        }

        public List<EDTipoDocumento> ObtenerTiposDocumento()
        {
            List<EDTipoDocumento> tiposDocumento = new List<EDTipoDocumento>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                tiposDocumento = context.Tbl_TipoDocumentos.Select(td => new EDTipoDocumento
                {
                    Id_Tipo_Documento = td.PK_IDTipo_Documento,
                    Descripcion = td.Descripcion
                }).ToList();
            }

            return tiposDocumento;
        }

        public List<EDOcupacion> ObtenerOpucaciones()
        {
            List<EDOcupacion> ocupaciones = new List<EDOcupacion>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                ocupaciones = context.Tbl_Ocupacion.Select(o => new EDOcupacion
                {
                    Id_Ocupacion = o.PK_Ocupacion,
                    Descripcion = o.Descripcion_Ocupacion
                }).ToList();
            }

            return ocupaciones;
        }



        public List<EDProceso> ObtenerProcesosPorEmpresaprnivel(string Nit)
        {
            List<EDProceso> Procesos = new List<EDProceso>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Procesos = (from e in context.Tbl_Empresa
                            join ep in context.Tbl_ProcesoEmpresa on e.Pk_Id_Empresa equals ep.Fk_Id_Empresa
                            join p in context.Tbl_Procesos on ep.Fk_Id_Proceso equals p.Pk_Id_Proceso
                            where e.Nit_Empresa.Trim().Equals(Nit.Trim()) & p.Fk_Id_Proceso==null
                            select new EDProceso
                            {
                                Id_Proceso = p.Pk_Id_Proceso,
                                Descripcion = p.Descripcion_Proceso,
                                Id_Proceso_Padre = p.Fk_Id_Proceso
                            }).ToList();

            }

            return Procesos;

        }




        public List<EDProceso> ObtenerProcesosPorEmpres(string Nit)
        {
            List<EDProceso> Procesos = new List<EDProceso>();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                Procesos = (from e in context.Tbl_Empresa
                            join ep in context.Tbl_ProcesoEmpresa on e.Pk_Id_Empresa equals ep.Fk_Id_Empresa
                            join p in context.Tbl_Procesos on ep.Fk_Id_Proceso equals p.Pk_Id_Proceso
                            where e.Nit_Empresa.Trim().Equals(Nit.Trim())
                            select new EDProceso
                            {
                                Id_Proceso = p.Pk_Id_Proceso,
                                Descripcion = p.Descripcion_Proceso,
                                Id_Proceso_Padre = p.Fk_Id_Proceso
                            }).ToList();

            }

            return Procesos;
        }

        /// <summary>
        /// Retorna el listado de empresas activas
        /// </summary>
        /// <param name="Nit"></param>
        /// <returns></returns>
        public List<EDEmpresas> ObtenerEmpresasRegistradas()
        {
            List<EDEmpresas> empresas = new List<EDEmpresas>();
            try
            {

                using (var context = new SG_SSTContext())
                {
                    empresas = context.Tbl_Empresa.Select(e => new EDEmpresas()
                    {
                        Id_Empresa = e.Pk_Id_Empresa,
                        Razon_Social = e.Razon_Social,
                        Email = e.Email,
                        Nit_Empresa = e.Nit_Empresa,
                        Codigo_Actividad = e.Codigo_Actividad,
                        Flg_Estado = e.Flg_Estado,
                        Telefono = e.Telefono.Value
                    }).Where(e => e.Flg_Estado.ToUpper().Equals("ACTIVA")).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;//Se debe configurar para que se registe el Log
            }
            return empresas;
        }

        public int ValidarProceso(int idProceso, string nit)
        {
            int result = 0;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var Proceso = (from e in context.Tbl_Empresa
                               join ep in context.Tbl_ProcesoEmpresa on e.Pk_Id_Empresa equals ep.Fk_Id_Empresa
                               join p in context.Tbl_Procesos on ep.Fk_Id_Proceso equals p.Pk_Id_Proceso
                               where e.Nit_Empresa.Trim().Equals(nit.Trim()) && p.Pk_Id_Proceso == idProceso
                               select p).FirstOrDefault();
                if (Proceso != null)
                    result = Proceso.Pk_Id_Proceso;
            }
            return result;
        }

        public int ValidarOcupacion(int idOcupacion)
        {
            int result = 0;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var ocup = context.Tbl_Ocupacion.Where(o => o.PK_Ocupacion == idOcupacion).Select(o => o).FirstOrDefault();
                if (ocup != null)
                    result = ocup.PK_Ocupacion;
            }
            return result;
        }

        public int ValidarSede(int idSede, string nit)
        {
            int result = 0;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var Sede = (from s in context.Tbl_Sede
                            join e in context.Tbl_Empresa on s.Fk_Id_Empresa equals e.Pk_Id_Empresa
                            where s.Pk_Id_Sede == idSede && e.Nit_Empresa.Equals(nit.Trim())
                            select s).FirstOrDefault();
                if (Sede != null)
                    result = Sede.Pk_Id_Sede;
            }
            return result;
        }

        public int ValidarTipoDocumento(int idTipoDoc)
        {
            int result = 0;
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var tipoDoc = context.Tbl_TipoDocumentos.Where(td => td.PK_IDTipo_Documento == idTipoDoc).Select(td => td).FirstOrDefault();
                if (tipoDoc != null)
                    result = tipoDoc.PK_IDTipo_Documento;
            }
            return result;
        }
    }
}
