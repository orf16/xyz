using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SG_SST.Models;
using SG_SST.Models.Organizacion;
using SG_SST.Models.Empresas;
using SG_SST.Repositories.Organizacion.IRepositories;
using System.Data.Entity;
using System.Web.Mvc;
using System.Configuration;
using RestSharp;
using System.Net;
using SG_SST.Dtos.Organizacion;


namespace SG_SST.Repositories.Organizacion.Repositories
{
    public class CompetenciaRepositorio : ICompetenciaRepositorio
    {

        private SG_SSTContext db;

        public CompetenciaRepositorio()
        {
            db = new SG_SSTContext();
        }


        /// <summary>
        /// Metodo para guardar o editar  las competencias
        /// </summary>
        /// <param name="Fk_Id_Rol", name="Fk_Id_Cargo", name="idEmpleados", name="Id_TematicaPos", name="editar"></param>
        /// <returns></returns>
        public bool GrabarCompetencia(List<int> Fk_Id_Rol, List<int> Fk_Id_Cargo,
         string idEmpleados, List<Tematica> Id_TematicaPos, int editar, int SessionEmp)
        {

            Cargo Car = new Cargo();
            Tematica Tem = new Tematica();
            Rol Ro = new Rol();
            List<Tematica> Tematic = new List<Tematica>();
            List<Cargo> Carg = new List<Cargo>();
            List<Rol> ListRo = new List<Rol>();
            List<EmpleadoTematica> ListEmpTematica = new List<EmpleadoTematica>();
            List<EmpleadoPorTematica> ListEmpPorTematica = new List<EmpleadoPorTematica>();
            List<CargoPorRol> CargoPorRolList = new List<CargoPorRol>();
            List<RolPorTematica> RolPorTematicaList = new List<RolPorTematica>();
            List<TematicaPorEmpresa> TematicaPorEmpresaList = new List<TematicaPorEmpresa>();
            try
            {
                foreach (var CarU in Fk_Id_Cargo)
                {
                    Car = db.Tbl_Cargo.Where(u => u.Pk_Id_Cargo == CarU).FirstOrDefault();
                    Carg.Add(Car);
                }
                foreach (var IdRol in Fk_Id_Rol)
                {
                    Ro = db.Tbl_Rol.Where(u => u.Pk_Id_Rol == IdRol).FirstOrDefault();
                    ListRo.Add(Ro);
                }
                if (idEmpleados != null && idEmpleados != "")
                {
                    string[] id = idEmpleados.Split(',');
                    foreach (var idEmp in id)
                    {
                        string[] identificacion = idEmp.Split('&');
                        int idInt = Convert.ToInt32(identificacion[0]);
                        EmpleadoTematica empleadoTematica = db.Tbl_Empleado_Tematica.Where(e => e.Numero_Documento == idInt).FirstOrDefault();
                        if (empleadoTematica == null)
                        {
                            empleadoTematica = new EmpleadoTematica();
                            empleadoTematica.Numero_Documento = idInt;
                            empleadoTematica.Nombre_Empleado = identificacion[1] + " " + identificacion[2];
                            empleadoTematica.Apellidos_Empleado = identificacion[3] + " " + identificacion[4];
                            empleadoTematica.Cargo_Empleado = identificacion[5];
                            empleadoTematica.Email_Persona = identificacion[6];
                            ListEmpTematica.Add(empleadoTematica);
                        }
                    }

                }
                db.Tbl_Empleado_Tematica.AddRange(ListEmpTematica);
                db.SaveChanges();

                foreach (var rols in ListRo)
                {
                    foreach (var capt in Carg)
                    {
                        CargoPorRol CargPorRol = new CargoPorRol();
                        CargPorRol.Fk_Id_Cargo = capt.Pk_Id_Cargo;
                        CargPorRol.Fk_Id_Rol = rols.Pk_Id_Rol;
                        CargoPorRolList.Add(CargPorRol);
                    }
                    foreach (var tematica in Id_TematicaPos)
                    {
                        RolPorTematica RolPorTematica = new RolPorTematica();
                        RolPorTematica.Fk_Id_Tematica = tematica.Id_Tematica;
                        RolPorTematica.Fk_Id_Rol = rols.Pk_Id_Rol;
                        RolPorTematicaList.Add(RolPorTematica);
                    }
                    if (idEmpleados != null && idEmpleados != "")
                    {
                        string[] id = idEmpleados.Split(',');
                        foreach (var idEmp in id)
                        {
                            EmpleadoPorTematica empleadoPorTematica = new EmpleadoPorTematica();
                            string[] identificacion = idEmp.Split('&');
                            int idInt = Convert.ToInt32(identificacion[0]);
                            EmpleadoTematica empleadoTematica = db.Tbl_Empleado_Tematica.Where(e => e.Numero_Documento == idInt).FirstOrDefault();
                            empleadoPorTematica.Fk_Id_Tematica = empleadoTematica.Pk_Id_EmpleadoTematica;
                            empleadoPorTematica.Fk_Id_Rol = rols.Pk_Id_Rol;
                            ListEmpPorTematica.Add(empleadoPorTematica);
                        }
                    }
                }

                foreach (var tpE in Id_TematicaPos)
                {
                    TematicaPorEmpresa TematicaPorEmpresa = new TematicaPorEmpresa();
                    TematicaPorEmpresa.Fk_Id_Tematica = tpE.Id_Tematica;
                    TematicaPorEmpresa.Fk_Id_Empresa = SessionEmp;
                    TematicaPorEmpresaList.Add(TematicaPorEmpresa);
                }
                /*Si necesitamos editar, eliminamos las competencias para luego reasignarlas*/
                if (editar != 0)
                {
                    List<RolPorTematica> RolPorTematica = db.Tbl_Rol_Por_Tematica.Where(s => s.Fk_Id_Rol == editar).ToList();
                    List<CargoPorRol> CargoPorRol = db.Tbl_Cargo_Por_Rol.Where(t => t.Fk_Id_Rol == editar).ToList();
                    List<EmpleadoPorTematica> EmpleadoPorTematica = db.Tbl_Empleado_Por_Tematica.Where(t => t.Fk_Id_Rol == editar).ToList();
                    db.Tbl_Rol_Por_Tematica.RemoveRange(RolPorTematica);
                    db.Tbl_Cargo_Por_Rol.RemoveRange(CargoPorRol);
                    db.Tbl_Empleado_Por_Tematica.RemoveRange(EmpleadoPorTematica);
                }
                db.Tbl_Cargo_Por_Rol.AddRange(CargoPorRolList);
                db.Tbl_Rol_Por_Tematica.AddRange(RolPorTematicaList);
                db.Tbl_Tematica_Por_Empresa.AddRange(TematicaPorEmpresaList);
                db.Tbl_Empleado_Por_Tematica.AddRange(ListEmpPorTematica);
                db.SaveChanges();
                return true;
               
            }

                 catch (Exception)
            {
                return false;
                throw;
            }

        }

        /// <summary>
        /// Método para obtener los Roles por tematica de acuerdo al id del rol ingresado
        /// </summary>
        /// <param name="rol", name="SessionEmp"></param>
        /// <returns>List<Rol></returns>
        public List<Rol> ObtenerRol(int rol, int SessionEmp)
        {

            List<Rol> Roles = db.Tbl_Rol.Include(p => p.RolPorTematica)
               .Where(r => r.Pk_Id_Rol == rol && r.Fk_Id_Empresa == SessionEmp).ToList();
            return Roles;
        }
        /// <summary>
        /// Método para obtener los Roles por tematica de acuerdo al id del rol ingresado
        /// </summary>
        /// <param name="rol"></param>
        /// <returns>List<RolPorTematica></returns>
        public List<RolPorTematica> ObtenerRolPorTematicaPorRol(int rol)
        {

            List<RolPorTematica> RolPorTematicaList = db.Tbl_Rol_Por_Tematica.Where(s => s.Fk_Id_Rol == rol).ToList();
            return RolPorTematicaList;
        }

        /// <summary>
        /// Método para obtener los cargos por rol de acuerdo al id del rol ingresado
        /// </summary>
        /// <param name="rol"></param>
        /// <returns>List<CargoPorRol></returns>
        public List<CargoPorRol> ObtenerCargoPorRolPorRol(int rol)
        {

            List<CargoPorRol> CargoPorRolList = db.Tbl_Cargo_Por_Rol.Where(t => t.Fk_Id_Rol == rol).ToList();
            return CargoPorRolList;
        }
        /// <summary>
        /// Método para obtener los cargos por rol de acuerdo al id del cargo ingresado
        /// </summary>
        /// <param name="cargo"></param>
        /// <returns>List<CargoPorRol></returns>
        public List<CargoPorRol> ObteneCargoPorRolPorCargo(int cargo)
        {

            List<CargoPorRol> CargoPorRolList = db.Tbl_Cargo_Por_Rol.Where(t => t.Fk_Id_Cargo == cargo).ToList();
            return CargoPorRolList;
        }
        /// <summary>
        /// Método para obtener Rol de acuerdo a los ids del cargo y rol ingresados
        /// </summary>
        /// <param name="cargo", name="SessionEmp"></param>
        /// <returns>List<Rol></returns>
        public List<Rol> ObtenerRolPorCargo(int cargo, int SessionEmp)
        {

            var Rol = (from c in db.Tbl_Rol
                       join rp in db.Tbl_Cargo_Por_Rol on c.Pk_Id_Rol equals rp.Fk_Id_Rol
                       where rp.Fk_Id_Cargo == cargo && c.Fk_Id_Empresa == SessionEmp
                       select c
                       );
            List<Rol> Roles = Rol.ToList();
            return Roles;
        }
        /// <summary>
        /// Método para obtener el roles por tematica de acuerdo al id del cargo ingresado
        /// </summary>
        /// <param name="cargo"></param>
        /// <returns>List<RolPorTematica></returns>
        public List<RolPorTematica> ObtenerRolPorTematicaPorCargo(int cargo)
        {

            var RolTem = (from c in db.Tbl_Rol_Por_Tematica
                          join rp in db.Tbl_Cargo_Por_Rol on c.Fk_Id_Rol equals rp.Fk_Id_Rol
                          where rp.Fk_Id_Cargo == cargo
                          select c
                  );
            List<RolPorTematica> RolPorTematicaList = RolTem.ToList();
            return RolPorTematicaList;
        }
        /// <summary>
        /// Método para obtener cargos por rol de acuerdo a los ids del cargo y rol ingresados
        /// </summary>
        /// <param name="rol", name="cargo"></param>
        /// <returns>List<CargoPorRol></returns>
        public List<CargoPorRol> ObteneCargoPorRolPorRolCargo(int rol, int cargo)
        {

            List<CargoPorRol> CargoPorRolList = db.Tbl_Cargo_Por_Rol.Where(t => t.Fk_Id_Cargo == cargo && t.Fk_Id_Rol == rol).ToList();
            return CargoPorRolList;
        }
        /// <summary>
        /// Método para obtener Rol de acuerdo a los ids del cargo y rol ingresados
        /// </summary>
        /// <param name="rol", name="cargo"></param>
        /// <returns>List<Rol></returns>
        public List<Rol> ObtenerRolPorRolCargo(int rol, int cargo)
        {
            var Rol = (from c in db.Tbl_Rol
                       join rp in db.Tbl_Cargo_Por_Rol on c.Pk_Id_Rol equals rp.Fk_Id_Rol
                       where (rp.Fk_Id_Cargo == cargo && rp.Fk_Id_Rol == rol)
                       select c
                         );
            List<Rol> Roles = Rol.ToList();
            return Roles;
        }

        /// <summary>
        /// Método para obtener roles por tematica por rol y cargo ingresados
        /// </summary>
        /// <param name="rol", name="cargo"></param>
        /// <returns>List<RolPorTematica></returns>
        public List<RolPorTematica> ObtenerRolPorTematicaPorRolCargo(int rol, int cargo)
        {


            var RolTem = (from c in db.Tbl_Rol_Por_Tematica
                          join rp in db.Tbl_Cargo_Por_Rol on c.Fk_Id_Rol equals rp.Fk_Id_Rol
                          where (rp.Fk_Id_Cargo == cargo && rp.Fk_Id_Rol == rol)

                          select c
             );
            List<RolPorTematica> RolPorTematicaList = RolTem.ToList();
            return RolPorTematicaList;
        }
        /// <summary>
        /// Método para obtener roles por tematica por el id de la tematica ingresado
        /// </summary>
        /// <param name="idbusqueda", name="SessionEmp"></param>
        /// <returns>List<RolPorTematica></returns>

        public List<RolPorTematica> ObtenerRolPorTematicaPorTematica(int idbusqueda, int SessionEmp)
        {
            List<RolPorTematica> RolPorTematicaList = db.Tbl_Rol_Por_Tematica.Include(p => p.Rol.CargoPorRol).Where(r => r.Fk_Id_Tematica == idbusqueda && r.Rol.Fk_Id_Empresa == SessionEmp).ToList();
            return RolPorTematicaList;
        }
        /// <summary>
        /// Método para obtener Roles por tematica por los ids de la tematica y rol ingresados
        /// </summary>
        /// <param name="idbusqueda", name="rol"></param>
        /// <returns>List<RolPorTematica></returns>
        public List<RolPorTematica> ObtenerRolPorTematicaPorTematicaRol(int idbusqueda, int rol)
        {
            List<RolPorTematica> RolPorTematicaList = db.Tbl_Rol_Por_Tematica.Include(p => p.Rol.CargoPorRol).Where(r => r.Fk_Id_Tematica == idbusqueda && r.Fk_Id_Rol == rol).ToList();
            return RolPorTematicaList;
        }
        /// <summary>
        /// Método para obtener Roles por tematica por los ids de la tematica y cargo ingresados
        /// </summary>
        /// <param name="idbusqueda", name="cargo", name="SessionEmp"></param>
        /// <returns>List<RolPorTematica></returns>
        public List<RolPorTematica> ObtenerRolPorTematicaPorTemaCargo(int idbusqueda, int cargo, int SessionEmp)
        {

            try
            {
                var RolTem = (from c in db.Tbl_Rol_Por_Tematica
                              where c.Fk_Id_Tematica == idbusqueda
                              join rp in db.Tbl_Cargo_Por_Rol on c.Fk_Id_Rol equals rp.Fk_Id_Rol
                              where rp.Fk_Id_Cargo == cargo && rp.Rol.Fk_Id_Empresa == SessionEmp
                              select c
                 );

                List<RolPorTematica> RolPorTematicaList = RolTem.ToList();
                return RolPorTematicaList;
            }

             catch (Exception)
             {
                 return null;
                 throw;
             }
        }
        /// <summary>
        /// Método para obtener Roles 
        /// </summary>
        /// <param name="SessionEmp"></param>
        /// <returns>List<Rol></returns>
        public List<Rol> ObtenerRoles (int SessionEmp)
        {
            try
            {
                var Rol = db.Tbl_Rol.Where(x => x.Fk_Id_Empresa == SessionEmp);
                List<Rol> Roles = Rol.ToList();
                return Roles;
            }

            catch (Exception)
            {
                return null;
                throw;
            }
        }

        /// <summary>
        /// Método para obtener roles libres para asignar competencias
        /// </summary>
        /// <param name="SessionEmp"></param>
        /// <returns>List<Rol></returns>
        public List<Rol> ObtenerRolesLibres(int SessionEmp)
        {
            try
            {
                var Rol = db.Tbl_Rol.Where(x => x.Fk_Id_Empresa == SessionEmp && x.CargoPorRol.Count == 0);
                List<Rol> Roles = Rol.ToList();
                return Roles;
            }

            catch (Exception)
            {
                return null;
                throw;
            }
        }
        /// <summary>
        /// Método para obtener Roles libres para asignar competencias
        /// </summary>
        /// <param name="rol", name="SessionEmp"></param>
        /// <returns>SelectList</returns>
        public SelectList ObtenerRolesLibresEditar(int rol, int SessionEmp)
        {
            try
            {
                SelectList RolSel = new SelectList(from rolls in db.Tbl_Rol
                                        .Where(x => (x.Fk_Id_Empresa == SessionEmp && x.CargoPorRol.Count == 0) || x.Pk_Id_Rol == rol)
                                                   select new
                                                   {
                                                       Value = rolls.Pk_Id_Rol,
                                                       Text = rolls.Descripcion
                                                   },
                               "Value", "Text", rol);
                return RolSel;
            }

            catch (Exception)
            {
                return null;
                throw;
            }
        }
        /// <summary>
        /// Método para obtener cargos
        /// </summary>
        /// <param</param>
        /// <returns>List<Cargo></returns>
        public List<Cargo> ObtenerCargos(string NitEmpresa)
        {
             try
            {
                var cliente = new RestSharp.RestClient(ConfigurationManager.AppSettings["Url"]);
                var request = new RestRequest(ConfigurationManager.AppSettings["consultaAfiliados"], RestSharp.Method.GET);
                request.RequestFormat = DataFormat.Xml;
                request.Parameters.Clear();
                request.AddParameter("tpEm", "NI");
                request.AddParameter("docEm", NitEmpresa);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");

                //se omite la validación de certificado de SSL
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                IRestResponse<List<EmpleadosWSDTO>> response = cliente.Execute<List<EmpleadosWSDTO>>(request);
                var result = response.Content;
                var respuesta = Newtonsoft.Json.JsonConvert.DeserializeObject<List<EmpleadosWSDTO>>(result);
                var cargosSiarp = (from c in respuesta
                                      select c.cargo).Distinct();
                List<Cargo> Cargos= new List<Cargo>();
                List<Cargo> CargosParaAdicion = new List<Cargo>();
                foreach (var cargo in cargosSiarp)
                {
                    Cargo cargoExist = db.Tbl_Cargo.Where(x => x.Nombre_Cargo.Equals(cargo)).FirstOrDefault();
                    if (cargoExist==null) {

                        cargoExist = new Cargo();
                        cargoExist.Nombre_Cargo = cargo;
                        db.Tbl_Cargo.Add(cargoExist);
                        db.SaveChanges();                      
                    }                    
                    Cargos.Add(db.Tbl_Cargo.Where(x => x.Nombre_Cargo.Equals(cargo)).FirstOrDefault());                    
                }                 
                return Cargos.OrderBy(x=> x.Nombre_Cargo).ToList();
            }

             catch (Exception)
             {
                 return null;
                 throw;
             }
        }
        /// <summary>
        /// Método para obtener los cargos  seleccionados de acuerdo al id del rol ingresado
        /// </summary>
        /// <param name="rol"></param>
        /// <returns>List<int></returns>
        public List<int> ObtenerCargosSeleccionados(int rol)
        {
            try
            {
                List<int> Cargos =(from r in db.Tbl_Cargo_Por_Rol where r.Fk_Id_Rol == rol select r.Fk_Id_Cargo).ToList();
                return Cargos;
            }

            catch (Exception)
            {
                return null;
                throw;
            }
        }
        /// <summary>
        /// Método para guardar una tematica tipo empresa
        /// </summary>
        /// <param name="Tematica", name="SessionEmp"></param>
        /// <returns>List<Tematica></returns>
        public List<Tematica> GuardarTematicaE(Tematica tematic, int SessionEmp)
        {
            try
            {
                tematic.SesionEmpresa=SessionEmp;
                db.Tbl_Tematica.Add(tematic);
                db.SaveChanges();
                List<Tematica> tematicas = db.Tbl_Tematica.Where(p => p.TipoTematica == 2 && p.SesionEmpresa == SessionEmp).ToList();
                return tematicas;

            }

            catch (Exception)
            {   
                return null;
                throw;
            }
        }
        /// <summary>
        /// Método para obtener las tematicas ingresadas por posipedia
        /// </summary>
        /// <param></param>
        /// <returns>List<Tematica></returns>
        public List<Tematica> ObtenerTematicaPosipedia()
        {
            try
            {
                List<Tematica> tematicas = db.Tbl_Tematica.Where(z => z.TipoTematica == 1).ToList();
                return tematicas;

            }

            catch (Exception)
            {
                return null;
                throw;
            }
        }

        /// <summary>
        /// Método para obtener las tematicas  posipedia seleccionadas para el id rol ingresado
        /// </summary>
        /// <param name="rol"></param>
        /// <returns>List<Tematica></returns>
        public List<Tematica> ObtenerTematicaPosipediaSeleccionadas(int rol)
        {
            try
            {  List<Tematica> tematicas = (from c in db.Tbl_Tematica
                                            join rp in db.Tbl_Rol_Por_Tematica on c.Id_Tematica equals rp.Fk_Id_Tematica
                                            where (c.TipoTematica != 2 && rp.Fk_Id_Rol == rol)
                                            select c
                            ).ToList();
                return tematicas;

            }

            catch (Exception)
            {
                return null;
                throw;
            }
        }
        /// <summary>
        /// Método para obtener las tematicas  empresa de acuerdo al id empresa del usuario en sesión
        /// </summary>
        /// <param name="SessionEmp"></param>
        /// <returns>List<Tematica></returns>
        public List<Tematica> ObtenerTematicaEmpresa(int SessionEmp)
        {
            try
            {
                List<Tematica> tematicas = db.Tbl_Tematica.Where(z => z.TipoTematica == 2 && z.SesionEmpresa == SessionEmp).ToList();
                return tematicas;

            }

            catch (Exception)
            {
                return null;
                throw;
            }
        }

        /// <summary>
        /// Método para obtener las tematicas  empresa seleccionadas para el id rol ingresado
        /// </summary>
        /// <param name="rol"></param>
        /// <returns>List<Tematica></returns>
        public List<Tematica> ObtenerTematicaEmpresaSeleccionadas(int rol)
        {
            try
            {
                List<Tematica> tematicas = (from c in db.Tbl_Tematica
                                            join rp in db.Tbl_Rol_Por_Tematica on c.Id_Tematica equals rp.Fk_Id_Tematica
                                            where (c.TipoTematica == 2 && rp.Fk_Id_Rol == rol)
                                            select c
                ).ToList();
                return tematicas;

            }

            catch (Exception)
            {
                return null;
                throw;
            }
        }
        /// <summary>
        /// Método para obtener las tematicas posipedia de acuerdo al texto ingresado en el buscador
        /// </summary>
        /// <param name="Busqueda"></param>
        /// <returns>List<Tematica></returns>
        public List<Tematica> ObtenerTematica(string Busqueda)
        {
            try
            {
                List<Tematica> tematicas = db.Tbl_Tematica.Where(p => p.Tematicas.Contains(Busqueda) && p.TipoTematica == 1).ToList();
                return tematicas;

            }

            catch (Exception)
            {
                return null;
                throw;
            }
        }
        /// <summary>
        /// Método para obtener las tematicas empresa de acuerdo al texto ingresado en el buscador
        /// </summary>
        /// <param name="Busqueda", name="SessionEmp"></param>
        /// <returns>List<Tematica></returns>
        public List<Tematica> ObtenerTematicaEmp(string Busqueda, int SessionEmp)
        {
            try
            {
                List<Tematica> tematicas = db.Tbl_Tematica.Where(p => p.Tematicas.Contains(Busqueda) && p.TipoTematica == 2 && p.SesionEmpresa == SessionEmp).ToList();
                return tematicas;

            }

            catch (Exception)
            {
                return null;
                throw;
            }
        }
        /// <summary>
        /// Método para obtener las tematicas posipedia de acuerdo al texto ingresado en el buscador
        /// </summary>
        /// <param name="prefijo", name="SessionEmp"></param>
        /// <returns>List<Tematica></returns>
        public List<Tematica> ObtenerCompetenciaPorTematica(string prefijo, int SessionEmp)
        {
            try
            {
                List<Tematica> tematicas = db.Tbl_Tematica.Where(c => c.Tematicas.StartsWith(prefijo) && (c.SesionEmpresa == null || c.SesionEmpresa == SessionEmp)).ToList();
                return tematicas;

            }

            catch (Exception)
            {
                return null;
                throw;
            }
        }

        /// <summary>
        /// Método para obtener los empleados que se encuentran asociados al rol
        /// </summary>
        /// <param name="rol"></param>
        /// <returns>List<EmpleadoPorTematica></returns>
        public List<EmpleadoPorTematica> ObtenerEmpleadoPorRol(int rol)
        {
            try
            {
                List<EmpleadoPorTematica> EmpleadoPorTematica = db.Tbl_Empleado_Por_Tematica.Where(t => t.Fk_Id_Rol == rol).ToList();
                return EmpleadoPorTematica;

            }

            catch (Exception)
            {
                return null;
                throw;
            }
        }



        /// <summary>
        /// Método para eliminar las competencias asignadas al id del rol ingresado
        /// </summary>
        /// <param name="rol"></param>
        /// <returns>bool</returns>
        public bool EliminarCompetencia(int rol)
        {
            try
            {
                List<RolPorTematica> RolPorTematicaList = db.Tbl_Rol_Por_Tematica.Where(s => s.Fk_Id_Rol == rol).ToList();
                List<CargoPorRol> CargoPorRolList = db.Tbl_Cargo_Por_Rol.Where(t => t.Fk_Id_Rol == rol).ToList();
                List<EmpleadoPorTematica> EmpleadoPorTematica = db.Tbl_Empleado_Por_Tematica.Where(t => t.Fk_Id_Rol == rol).ToList();
                db.Tbl_Rol_Por_Tematica.RemoveRange(RolPorTematicaList);
                db.Tbl_Cargo_Por_Rol.RemoveRange(CargoPorRolList);
                db.Tbl_Empleado_Por_Tematica.RemoveRange(EmpleadoPorTematica);
                db.SaveChanges();
                return true;

            }

            catch (Exception)
            {
                return false;
                throw;
            }
        }



    }
}