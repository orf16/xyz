using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Models;
using SG_SST.Models.Planificacion;
using SG_SST.Interfaces.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Planificacion
{
    public class PeligroManager : IPeligro
    {
        public bool GuardarPeligro(EDPeligro edpeligro)
        {
            using (SG_SSTContext context = new SG_SSTContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Peligro peligro = new Peligro
                        {
                            //PK_Peligro = edpeligro.PK_Peligro,
                            Nombre_Del_Profesional = edpeligro.Nombre_Del_Profesional,
                            Numero_De_Documento = edpeligro.Numero_De_Documento,
                            Numero_De_Licencia_SST = edpeligro.Numero_De_Documento,
                            Fecha_De_Evaluacion = edpeligro.Fecha_De_Evaluacion,
                            Lugar = edpeligro.Lugar,
                            Actividad = edpeligro.Actividad,
                            Tarea = edpeligro.Tarea,
                            FLG_Rutinaria = edpeligro.FLG_Rutinaria,
                            Fuente_Generadora_De_Peligro = edpeligro.Fuente_Generadora_De_Peligro,
                            Otro = edpeligro.Otro,
                            Fuente = edpeligro.Fuente,
                            Medio = edpeligro.Medio,
                            Eliminacion = edpeligro.Eliminacion,
                            Sustitucion = edpeligro.Sustitucion,
                            Controles_De_Ingenieria = edpeligro.Controles_De_Ingenieria,
                            Controles_Administrativos = edpeligro.Controles_Administrativos,
                            Elementos_De_Proteccion = edpeligro.Elementos_De_Proteccion,
                            Accion_De_Prevencion = edpeligro.Accion_De_Prevencion,
                            FK_Clasificacion_De_Peligro = edpeligro.FK_Clasificacion_De_Peligro,
                            FK_Sede = edpeligro.FK_Sede,
                            FK_Proceso = edpeligro.FK_Proceso
                        };
                        peligro.ConsecuenciasPorPeligros = new List<ConsecuenciaPorPeligro>();
                        peligro.GTC45 = new List<GTC45>();
                        peligro.GTC45.Add(
                            new GTC45
                            {
                                FLG_Higienico = edpeligro.gtc45.FLG_Higienico,
                                FLG_Tipo_De_Calificacion = edpeligro.gtc45.FLG_Tipo_De_Calificacion,
                                Efectos_Posibles = edpeligro.gtc45.Efectos_Posibles,
                                Nivel_De_Probablidad = edpeligro.gtc45.Nivel_De_Probablidad,
                                Nivel_De_Riesgo = edpeligro.gtc45.Nivel_De_Riesgo,
                                Numero_De_Expuestos = edpeligro.gtc45.Numero_De_Expuestos,
                                Peor_Consecuencia = edpeligro.gtc45.Peor_Consecuencia,
                                FLG_Requisito_Legal = edpeligro.gtc45.FLG_Requisito_Legal,
                                FK_Nivel_De_Deficiencia = edpeligro.gtc45.FK_Nivel_De_Deficiencia,
                                FK_Nivel_De_Exposicion = edpeligro.gtc45.FK_Nivel_De_Exposicion,
                            });
                        peligro.ConsecuenciasPorPeligros.Add(
                            new ConsecuenciaPorPeligro
                                {
                                   // PK_Consecuencia_Por_Peligro = edpeligro.ConsecuenciasPorPeligro.PK_Consecuencia_Por_Peligro,
                                    FK_Consecuencia = edpeligro.ConsecuenciasPorPeligro.FK_Consecuencia
                                });


                        context.Tbl_Peligro.Add(peligro);
                        context.SaveChanges();
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public List<EDPeligro> ObtenernerZonasPorEmpresa(string Nit_Empresa)
        {
            List<EDPeligro> zonas = new List<EDPeligro>();
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                zonas = (from p in contex.Tbl_Peligro
                         join se in contex.Tbl_Sede  on p.FK_Sede equals se.Pk_Id_Sede 
                         join e in contex.Tbl_Empresa on se.Fk_Id_Empresa equals e.Pk_Id_Empresa
                         where e.Nit_Empresa.Equals(Nit_Empresa)
                         select new EDPeligro
                         {
                             Lugar=p.Lugar,
                         }).ToList();
            }
            return zonas;
        }
        public List<string> ObtenerLugaresPeligros(int id_Empresa) 
        {
            List<string> lugares = new List<string>(); 

            using (SG_SSTContext context = new SG_SSTContext())
            {
                lugares = (from p in context.Tbl_Peligro
                           where p.Sede.Fk_Id_Empresa == id_Empresa
                           select p.Lugar).ToList();
            }
            return lugares;
        }
        public List<EDClasificacionDePeligro> ObtenerClasificaciónPorSede(int IdSede)
        {
            List<EDClasificacionDePeligro> ListaClasPeligroED = new List<EDClasificacionDePeligro>();
            List<ClasificacionDePeligro> ListaClasPeligro = new List<ClasificacionDePeligro>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var pageObject = (from Emp in db.Tbl_Clasificacion_De_Peligro
                                  join usu in db.Tbl_Peligro on Emp.PK_Clasificacion_De_Peligro equals usu.FK_Clasificacion_De_Peligro
                                  where usu.FK_Sede == IdSede
                                  select Emp).ToList<ClasificacionDePeligro>();

                ListaClasPeligro = pageObject;
            }
            foreach (var item in ListaClasPeligro)
            {
                EDClasificacionDePeligro EDClasificacionDePeligros = new EDClasificacionDePeligro();
                EDClasificacionDePeligros.IdClasificacionDePeligro = item.PK_Clasificacion_De_Peligro;
                EDClasificacionDePeligros.DescripcionClaseDePeligro = item.Descripcion_Clase_De_Peligro;
                ListaClasPeligroED.Add(EDClasificacionDePeligros);
            }
            return ListaClasPeligroED;
        }
        public List<EDPeligro> ObtenerPeligrosPorClaseyEmpresa(int IdClase, int IdEmpresa)
        {
            List<EDPeligro> ListaPeligrosED = new List<EDPeligro>();
            List<Peligro> ListaPeligros = new List<Peligro>();

            using (var context = new SG_SSTContext())
            {
                var r = (from u in context.Tbl_Peligro
                         join ue in context.Tbl_Clasificacion_De_Peligro on u.FK_Clasificacion_De_Peligro equals ue.PK_Clasificacion_De_Peligro
                         join e in context.Tbl_Sede on u.FK_Sede equals e.Pk_Id_Sede
                         where ue.PK_Clasificacion_De_Peligro.Equals(IdClase) && e.Fk_Id_Empresa.Equals(IdEmpresa)
                         select u).ToList<Peligro>();

                if (r != null)
                {
                    ListaPeligros = r;
                }
            }

            foreach (var item in ListaPeligros)
            {
                EDPeligro EDPeligro = new EDPeligro();
                EDPeligro.PK_Peligro = item.PK_Peligro;
                EDPeligro.Nombre_Del_Profesional = item.Nombre_Del_Profesional;
                EDPeligro.Numero_De_Documento = item.Numero_De_Documento;
                EDPeligro.Numero_De_Licencia_SST = item.Numero_De_Licencia_SST;
                EDPeligro.Fecha_De_Evaluacion = item.Fecha_De_Evaluacion;
                EDPeligro.Lugar = item.Lugar;
                EDPeligro.Actividad = item.Actividad;
                EDPeligro.Tarea = item.Tarea;
                EDPeligro.FLG_Rutinaria = item.FLG_Rutinaria;
                EDPeligro.Fuente_Generadora_De_Peligro = item.Fuente_Generadora_De_Peligro;
                EDPeligro.Otro = item.Otro;
                EDPeligro.Fuente = item.Fuente;
                EDPeligro.Medio = item.Medio;
                EDPeligro.Eliminacion = item.Eliminacion;
                EDPeligro.Sustitucion = item.Sustitucion;
                EDPeligro.Controles_De_Ingenieria = item.Controles_De_Ingenieria;
                EDPeligro.Controles_Administrativos = item.Controles_Administrativos;
                EDPeligro.Elementos_De_Proteccion = item.Elementos_De_Proteccion;
                EDPeligro.Accion_De_Prevencion = item.Accion_De_Prevencion;
                EDPeligro.FK_Clasificacion_De_Peligro = item.FK_Clasificacion_De_Peligro;
                EDPeligro.FK_Sede = item.FK_Sede;
                EDPeligro.FK_Proceso = item.FK_Proceso;

                ListaPeligrosED.Add(EDPeligro);

            }



            return ListaPeligrosED;
        }
        public string ObtenerClasificación(int IdClasificacion)
        {
            string EDClasPeligro = null;
            ClasificacionDePeligro ClasificacionDePeligro = new ClasificacionDePeligro();
            using (SG_SSTContext context = new SG_SSTContext())
            {
                var pel = (from p in context.Tbl_Clasificacion_De_Peligro
                           where p.PK_Clasificacion_De_Peligro == IdClasificacion
                           select p).FirstOrDefault<ClasificacionDePeligro>();
                if (pel != null)
                {
                    ClasificacionDePeligro = pel;
                    EDClasPeligro = pel.Descripcion_Clase_De_Peligro;

                }
            }
            return EDClasPeligro;
        }
        public List<EDClasificacionDePeligro> ObtenerClasificaciónPorTipo(int IdTipoPeligro)
        {
            List<EDClasificacionDePeligro> ListaClasPeligroED = new List<EDClasificacionDePeligro>();
            List<ClasificacionDePeligro> ListaClasPeligro = new List<ClasificacionDePeligro>();
            using (SG_SSTContext db = new SG_SSTContext())
            {
                var Object = (from p in db.Tbl_Clasificacion_De_Peligro
                              where p.FK_Tipo_De_Peligro == IdTipoPeligro
                              select p).ToList<ClasificacionDePeligro>();

                ListaClasPeligro = Object;
            }
            foreach (var item in ListaClasPeligro)
            {
                EDClasificacionDePeligro EDClasificacionDePeligros = new EDClasificacionDePeligro();
                EDClasificacionDePeligros.IdClasificacionDePeligro = item.PK_Clasificacion_De_Peligro;
                EDClasificacionDePeligros.DescripcionClaseDePeligro = item.Descripcion_Clase_De_Peligro;
                ListaClasPeligroED.Add(EDClasificacionDePeligros);
            }
            return ListaClasPeligroED;
        }
        public EDPeligro ObtenerPeligrosPorId(int IdPeligro)
        {
            Peligro ListaPeligros = new Peligro();
            using (var db = new SG_SSTContext())
            {
                var r = (from p in db.Tbl_Peligro
                         where p.PK_Peligro == IdPeligro
                         select p).FirstOrDefault<Peligro>();

                if (r != null)
                {
                    ListaPeligros = r;
                }
            }
            EDPeligro EDPeligro = new EDPeligro();
            EDPeligro.PK_Peligro = ListaPeligros.PK_Peligro;
            EDPeligro.Nombre_Del_Profesional = ListaPeligros.Nombre_Del_Profesional;
            EDPeligro.Numero_De_Documento = ListaPeligros.Numero_De_Documento;
            EDPeligro.Numero_De_Licencia_SST = ListaPeligros.Numero_De_Licencia_SST;
            EDPeligro.Fecha_De_Evaluacion = ListaPeligros.Fecha_De_Evaluacion;
            EDPeligro.Lugar = ListaPeligros.Lugar;
            EDPeligro.Actividad = ListaPeligros.Actividad;
            EDPeligro.Tarea = ListaPeligros.Tarea;
            EDPeligro.FLG_Rutinaria = ListaPeligros.FLG_Rutinaria;
            EDPeligro.Fuente_Generadora_De_Peligro = ListaPeligros.Fuente_Generadora_De_Peligro;
            EDPeligro.Otro = ListaPeligros.Otro;
            EDPeligro.Fuente = ListaPeligros.Fuente;
            EDPeligro.Medio = ListaPeligros.Medio;
            EDPeligro.Eliminacion = ListaPeligros.Eliminacion;
            EDPeligro.Sustitucion = ListaPeligros.Sustitucion;
            EDPeligro.Controles_De_Ingenieria = ListaPeligros.Controles_De_Ingenieria;
            EDPeligro.Controles_Administrativos = ListaPeligros.Controles_Administrativos;
            EDPeligro.Elementos_De_Proteccion = ListaPeligros.Elementos_De_Proteccion;
            EDPeligro.Accion_De_Prevencion = ListaPeligros.Accion_De_Prevencion;
            EDPeligro.FK_Clasificacion_De_Peligro = ListaPeligros.FK_Clasificacion_De_Peligro;
            EDPeligro.FK_Sede = ListaPeligros.FK_Sede;
            EDPeligro.FK_Proceso = ListaPeligros.FK_Proceso;
            return EDPeligro;
        }
    }
}
