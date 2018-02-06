


namespace SG_SST.Repositories.Planificacion.Repositories
{
    using SG_SST.Models;
    using SG_SST.Models.Planificacion;
    using SG_SST.Repositories.Planificacion.IRepositories;
    using System;
    using System.Linq;
    using System.Data;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using System.IO;
    using SG_SST.EntidadesDominio.Planificacion;




    public class RequisitosLegalesOtrosRepositorio : IRequisitosLegalesOtrosRepositorio
    {
        #region "Atributos"
        private string strError;
        public static int PKMatrizreqlegalea;


        #endregion

        #region "Métodos Públicos - Consultas en BD"

        SG_SSTContext dbReqLeg;

        public static int pk_ActividadEconomica;

        public RequisitosLegalesOtrosRepositorio()
        {
            dbReqLeg = new SG_SSTContext();
        }

        public bool valorvariable(int mivalor)
        {

            if (mivalor == 1)
            {

            PKMatrizreqlegalea = 0;
                }
            return true;
        
        }





        public bool GrabarRequisitosLegalesOtros(RequisitosLegalesOtros objreq)
        {
            try
            {
                dbReqLeg.Tbl_Requisitos_legales_Otros.Add(objreq);
                dbReqLeg.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        public bool ModficarRequisitosLegalesOtros(RequisitosLegalesOtros objreq)
        {
            try
            {
                dbReqLeg.Entry(objreq).State = EntityState.Modified;
                dbReqLeg.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }



        public RequisitosLegalesOtros BuscarRequisitosLegalesOtros(int PK_RequisitosLegales)
        {
            RequisitosLegalesOtros ObregLeg = dbReqLeg.Tbl_Requisitos_legales_Otros.Where(g => g.PK_RequisitosLegalesOtros == PK_RequisitosLegales).FirstOrDefault();

            return ObregLeg;
        }



        /*
        /// <summary>
        /// Consulta para obtener un listado de los datos en Requisitos legales otros-para exportar a Excel
        /// </summary>
        /// <param name="Pk_Sede"></param>
        /// <returns></returns>
        public List<RequisitosLegalesOtros> ObtenerRequisitosLegalesExcel_Repositorio(int ID_Empresa)
        {

            List<RequisitosLegalesOtros> Objlist_RequisitosLegales = new List<RequisitosLegalesOtros>();


            Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_legales_Otros.Where(s => s.FK_Empresa == ID_Empresa).ToList();



            //List<RequisitosLegalesOtros> Objlist_RequisitosLeg = dbReqLeg.Tbl_Requisitos_legales_Otros

            //.Include(a=> a.Tbl_Cumplimiento_Evaluacion.Descripcion_Cumplimiento_Evaluacion)
            //.Include(p => p.Tbl_Estado_RequisitoslegalesOtros.Descripcion_Estado_RequisitoslegalesOtros)

            //.Include(b=> b.FechaPublicacion)
            //.Include(c=> c.Ente)
            /*
            //.Include(d=> d.Articulo)
            //.Include(e=> e.Descripcion)
            //.Include(f=> f.Modificacion)
            .Include(g=> g.Sugerencias)
            .Include(h=> h.PartesInteresadas)
            .Include(i=> i.Clase_De_Peligro)
            .Include(j=> j.Peligro)
            .Include(k=> k.Aspectos)
            .Include(l=> l.Impactos)
            .Include(m=> m.Evidencia_Cumplimiento)
            .Include(n=> n.FK_Cumplimiento_Evaluacion)
            .Include(o=> o.Hallazgo)
            .Include(p=> p.FK_Estado_RequisitoslegalesOtros)
            .Include(q=> q.Responsable)
            .Include(r=> r.Fecha_Seguimiento_Control)
            .Include(s=> s.Fecha_Actualizacion)
             * 
             */
            //.Where(rs => rs.PK_RequisitosLegalesOtros == Pk_RequisitosLegales).ToList();
            //return (Objlist_RequisitosLegales);
        //}

    


        public List<RequisitosLegalesOtros> Busqueda_RequisitosLegales_Peligro(string strPeligroBusqueda, int PK_Empresa)
        {
            List<RequisitosLegalesOtros> Objlist_RequisitosLegales = new List<RequisitosLegalesOtros>();

            Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_legales_Otros.Where(s => s.Peligro.Contains(strPeligroBusqueda)).ToList();
            return (Objlist_RequisitosLegales);

        }



        public List<RequisitosLegalesPosipedia> Busqueda_PorActividadEconomica(int FK_Actividad_Economica, int PK_Empresa)
        {
            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = new List<RequisitosLegalesPosipedia>();
            Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(s => s.FK_Actividad_Economica == FK_Actividad_Economica).ToList();

            pk_ActividadEconomica = FK_Actividad_Economica;

            return Objlist_RequisitosLegales;
        }




        public bool CrearMatriz(MatrizRequisitosLegales OBJMatriz)
        {
            try
            {
                dbReqLeg.Tbl_Matriz_RequisitosLegales.Add(OBJMatriz);                
                dbReqLeg.SaveChanges();
                PKMatrizreqlegalea = OBJMatriz.PK_MatrizRequisitosLegales;
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }


        /// <summary>
        /// SE GUARDAN LOS REQUISITOS SELECCIONADOS PARA CREAR LA MATRIZ
        /// </summary>
        /// <param name="OtrasInteracciones"></param>
        /// <returns></returns>
        public bool GuardarRequisitos_Seleccionados(RequisitosLegalesOtros objreq, int fkmatriz)
        {

            try
            {

                if (PKMatrizreqlegalea != 0) { 



                dbReqLeg.Tbl_Requisitos_legales_Otros.Add(objreq);
                dbReqLeg.SaveChanges();

                //consulta para saber cual es la FK con la que quedo almacenado el requisito en base de datos

                RequisitosLegalesOtros objmat = new RequisitosLegalesOtros();              


                Requisitos_Matriz objm = new Requisitos_Matriz();


                MatrizRequisitosLegales objmatr = new MatrizRequisitosLegales();

                //se consulta el PK y se almacena en la tabla resquisitosmatriz con su correspondiente matriz

                //int requisito;

                objm.FK_RequisitosLegalesOtros = objreq.PK_RequisitosLegalesOtros; // SE TIENE LA PK ALMACENADA DEL REGISTRO
                objm.FK_MatrizRequisitosLegales = PKMatrizreqlegalea;// SE ALMACENA EL NOMBRE DE LA MATRIZ

                RelacionarRequisitosMatriz(objm);
                objm = null;
                //PKMatrizreqlegalea = 0;
                return true;
                }
                else
                {
                    dbReqLeg.Tbl_Requisitos_legales_Otros.Add(objreq);
                    dbReqLeg.SaveChanges();

                    //consulta para saber cual es la FK con la que quedo almacenado el requisito en base de datos

                    RequisitosLegalesOtros objmat = new RequisitosLegalesOtros();


                    Requisitos_Matriz objm = new Requisitos_Matriz();


                    MatrizRequisitosLegales objmatr = new MatrizRequisitosLegales();

                    //se consulta el PK y se almacena en la tabla resquisitosmatriz con su correspondiente matriz

                    //int requisito;

                    objm.FK_RequisitosLegalesOtros = objreq.PK_RequisitosLegalesOtros; // SE TIENE LA PK ALMACENADA DEL REGISTRO
                    objm.FK_MatrizRequisitosLegales = fkmatriz;// SE ALMACENA EL NOMBRE DE LA MATRIZ

                    RelacionarRequisitosMatriz(objm);
                    objm = null;
                    //PKMatrizreqlegalea = 0;
                    return true;
                







                }





            }
            catch (Exception)
            {

                return false;
            }

        }


        public bool RelacionarRequisitosMatriz(Requisitos_Matriz objm)
        {

            try
            {
                //Requisitos_Matriz obj = new Requisitos_Matriz();
                //obj.FK_RequisitosLegalesOtros = objmatr.PK_MatrizRequisitosLegales;
                //obj.FK_MatrizRequisitosLegales = objmatr.NombreMatriz;

                dbReqLeg.Tbl_Requisitos_Matriz.Add(objm);
                dbReqLeg.SaveChanges();
                
           

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }



        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormafechapublicacion(string TipoNorma, DateTime? fechapublicacion, int? FK_ActividadEconomica)
        {
            //List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq)).ToList();

            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.FK_Actividad_Economica == FK_ActividadEconomica).ToList();

            return Objlist_RequisitosLegales;

        }

        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormaEnteReq(string TipoNorma, string EnteReq, int FK_ActividadEconomica)
        {
            //List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq)).ToList();

            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.Ente.Contains(EnteReq) && p.FK_Actividad_Economica == FK_ActividadEconomica).ToList();

            return Objlist_RequisitosLegales;

        }

        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormaDescripcionReq(string TipoNorma, string DescripcionReq, int FK_ActividadEconomica)
        {
            //List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq)).ToList();

            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.Descripcion.Contains(DescripcionReq) && p.FK_Actividad_Economica == FK_ActividadEconomica).ToList();

            return Objlist_RequisitosLegales;

        }


        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalfechapublicacionEnteReq(DateTime fechapublicacion, string EnteReq, int FK_ActividadEconomica)
        {
            //List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq)).ToList();

            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.FK_Actividad_Economica == FK_ActividadEconomica).ToList();

            return Objlist_RequisitosLegales;

        }

        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalEnteReqDescripcionReq(string EnteReq, string DescripcionReq, int FK_ActividadEconomica)
        {
            //List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq)).ToList();

            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq) && p.FK_Actividad_Economica == FK_ActividadEconomica).ToList();

            return Objlist_RequisitosLegales;

        }



        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalfechapublicacionDescripcionReq(DateTime fechapublicacion, string DescripcionReq, int FK_ActividadEconomica)
        {
            //List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq)).ToList();

            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Descripcion.Contains(DescripcionReq) && p.FechaPublicacion == fechapublicacion && p.FK_Actividad_Economica == FK_ActividadEconomica).ToList();

            return Objlist_RequisitosLegales;

        }



        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegal(string TipoNorma, int FK_ActividadEconomica)
        {
            //List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq)).ToList();

            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FK_Actividad_Economica == FK_ActividadEconomica).ToList();

            return Objlist_RequisitosLegales;

        }


        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalfechapublicacion(DateTime fechapublicacion, int FK_ActividadEconomica)
        {
            //List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq)).ToList();

            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.FechaPublicacion == fechapublicacion && p.FK_Actividad_Economica == FK_ActividadEconomica).ToList();

            return Objlist_RequisitosLegales;

        }

        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalEnteReq(string EnteReq, int FK_ActividadEconomica)
        {
            //List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq)).ToList();

            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Ente.Contains(EnteReq) && p.FK_Actividad_Economica == FK_ActividadEconomica).ToList();

            return Objlist_RequisitosLegales;

        }


        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalDescripcionReq(string DescripcionReq, int FK_ActividadEconomica)
        {
            //List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq)).ToList();

            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Descripcion.Contains(DescripcionReq) && p.FK_Actividad_Economica == FK_ActividadEconomica).ToList();

            return Objlist_RequisitosLegales;

        }

        //veriicar
        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormafechapublicacionEnteReq(string TipoNorma, DateTime fechapublicacion, string EnteReq, int FK_ActividadEconomica)
        {
            //List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq)).ToList();

            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.FK_Actividad_Economica == FK_ActividadEconomica).ToList();

            return Objlist_RequisitosLegales;

        }



        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormaEnteReqDescripcionReq(string TipoNorma, string EnteReq, string DescripcionReq, int FK_ActividadEconomica)
        {
            //List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq)).ToList();

            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq) && p.FK_Actividad_Economica == FK_ActividadEconomica).ToList();

            return Objlist_RequisitosLegales;

        }


        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormafechapublicacionDescripcionReq(string TipoNorma, DateTime fechapublicacion, string DescripcionReq, int FK_ActividadEconomica)
        {
            //List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq)).ToList();

            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Descripcion.Contains(DescripcionReq) && p.FK_Actividad_Economica == FK_ActividadEconomica).ToList();
           
            return Objlist_RequisitosLegales;

        }




        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalfechapublicacionEnteReqDescripcionReq(DateTime fechapublicacion, string EnteReq, string DescripcionReq, int FK_ActividadEconomica)
        {
            //List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq)).ToList();

            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq) && p.FK_Actividad_Economica == FK_ActividadEconomica).ToList();

            return Objlist_RequisitosLegales;

        }



        public List<RequisitosLegalesPosipedia> BusquedaRequisitoLegalTipoNormafechapublicacionEnteReqDescripcionReq(string TipoNorma, DateTime fechapublicacion, string EnteReq, string DescripcionReq, int FK_ActividadEconomica)
        {
            //List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq)).ToList();

            List<RequisitosLegalesPosipedia> Objlist_RequisitosLegales = dbReqLeg.Tbl_Requisitos_Legales_Posipedia.Where(p => p.Tipo_Norma.Contains(TipoNorma) && p.FechaPublicacion == fechapublicacion && p.Ente.Contains(EnteReq) && p.Descripcion.Contains(DescripcionReq) && p.FK_Actividad_Economica == FK_ActividadEconomica).ToList();

            return Objlist_RequisitosLegales;

        }


        //se agrega nuevo o nuevos requisitos legales a la matriz creada
    
           public bool AgregarNuevo_RequisitoMatriz(int FKMatrizRequisitosLegales,int fkRequisito)
           {
            
        



               return true;

           }






           /// <summary>
           /// SE GUARDAN LOS NUEVOS REQUISITOS SELECCIONADOS EN LA MATRIZ SELECCIONADA
           /// </summary>
           /// <param name="OtrasInteracciones"></param>
           /// <returns></returns>
           public bool GuardarNuevoRequisitos_SeleccionadosMatriz(RequisitosLegalesOtros objreq,  int PKMatriz)
           {

               try
               {
                   dbReqLeg.Tbl_Requisitos_legales_Otros.Add(objreq);//SE GUARDA EL REQUISITO EN LA TABLA Tbl_Requisitos_legales_Otros
                   dbReqLeg.SaveChanges();

                   //consulta para saber cual es la FK con la que quedo almacenado el requisito en base de datos

                   RequisitosLegalesOtros objmat = new RequisitosLegalesOtros();

                   RequisitosLegalesOtros objmatreqle = dbReqLeg.Tbl_Requisitos_legales_Otros.Where(g =>g.Numero_Norma == objreq.Numero_Norma).FirstOrDefault();
                   //MatrizRequisitosLegales objmatreq = dbReqLeg.Tbl_Matriz_RequisitosLegales.Where(g => g.NombreMatriz == varnombrematyriz).FirstOrDefault();



                   Requisitos_Matriz objm = new Requisitos_Matriz();


                   MatrizRequisitosLegales objmatr = new MatrizRequisitosLegales();

                   //se consulta el PK y se almacena en la tabla resquisitosmatriz con su correspondiente matriz

                   int requisito;

                   objm.FK_RequisitosLegalesOtros = objreq.PK_RequisitosLegalesOtros; // SE TIENE LA PK ALMACENADA DEL REGISTRO
                   objm.FK_MatrizRequisitosLegales = PKMatriz;// SE ALMACENA EL NOMBRE DE LA MATRIZ

                   RelacionarRequisitosMatriz(objm);


                   return true;
               }
               catch (Exception)
               {

                   return false;
               }

           }

       
        //Metodo para modificar los requisitos legales de la matriz seleccionada
           public bool ModificarRequisitosLegalesOtros_Matriz(RequisitosLegalesOtros objreq)
           {
               try
               {
                   dbReqLeg.Entry(objreq).State = EntityState.Modified;
                   dbReqLeg.SaveChanges();                       
               

                   return true;
               }
               catch (Exception ex)
               {
                   strError = ex.Message;
                   return false;
               }
           }



           public bool EliminarMatrices(int PK_Matriz)
           {
               try
               {
                   //Requisitos_Matriz ObjMat = dbReqLeg.Tbl_Requisitos_Matriz.Where(g => g.FK_MatrizRequisitosLegales == PK_Matriz).FirstOrDefault();              
                   
                   MatrizRequisitosLegales objmatelim = dbReqLeg.Tbl_Matriz_RequisitosLegales.Where(g => g.PK_MatrizRequisitosLegales == PK_Matriz).FirstOrDefault();

                   //List<RequisitosLegalesOtros> objregleg = dbReqLeg.Tbl_Requisitos_legales_Otros.Where(g => g.PK_RequisitosLegalesOtros == ObjMat.FK_RequisitosLegalesOtros).ToList();


                   //dbReqLeg.Tbl_Requisitos_Matriz.Remove(ObjMat);
                   dbReqLeg.Tbl_Matriz_RequisitosLegales.Remove(objmatelim);
                   //dbReqLeg.Tbl_Requisitos_legales_Otros.Remove(objregleg);





                   dbReqLeg.SaveChanges();
                   return true;
               }
               catch (Exception ex)
               {
                   strError = ex.Message;
                   return false;
               }
           }




        /// <summary>
        /// metodo para guardar requisito creado por el formulario -  para agregar a la matriz
        /// </summary>
        /// <param name="objreq"></param>
        /// <returns></returns>
           public bool GrabarRequisitosLegalesOtros_Formulario(RequisitosLegalesOtros objreqformulario, int pk_matriz)
           {
               try
               {
                   RequisitosLegalesOtros objreqleg = new RequisitosLegalesOtros();              

                   //objreq.FK_Empresa = usuarioActual.IdEmpresa;
                   objreqformulario.Evidencia_Cumplimiento = "";
                   objreqformulario.FK_Cumplimiento_Evaluacion = 4;
                   objreqformulario.Hallazgo = "";
                   objreqformulario.FK_Estado_RequisitoslegalesOtros = 4;
                   objreqformulario.Responsable = "";
                   objreqformulario.Fecha_Seguimiento_Control = DateTime.Now;
                   objreqformulario.Fecha_Actualizacion = DateTime.Now;
                   //objreq.FK_Empresa = usuarioActual.IdEmpresa;
                   //objreqformulario.FK_Actividad_Economica = pk_ActividadEconomica;

                   objreqleg = dbReqLeg.Tbl_Requisitos_legales_Otros.Add(objreqformulario);// se guarda el requisito legal en Tbl_Requisitos_legales_Otros
                   dbReqLeg.SaveChanges();


                   Requisitos_Matriz objreqmat = new Requisitos_Matriz();

                   objreqmat.FK_RequisitosLegalesOtros = objreqleg.PK_RequisitosLegalesOtros;
                   objreqmat.FK_MatrizRequisitosLegales = pk_matriz;

                   //dbReqLeg.Tbl_Requisitos_Matriz.Add(objreqmat);// se guarda el registro en la tabla Tbl_Requisitos_Matriz (contiene el requisito creado desde el formulario)
                   //dbReqLeg.SaveChanges();

                   GrabarMatriz_ReuisitoNuevo_Formulaio(objreqmat);

                   pk_ActividadEconomica = 0;/*****variable estatica para agregar la pkactividadeconomica*****/

                   return true;
               }
               catch (Exception ex)
               {
                   strError = ex.Message;
                   return false;
               }
           }







           public bool GrabarMatriz_ReuisitoNuevo_Formulaio(Requisitos_Matriz objreqmat)
           {
               try
               {
                   dbReqLeg.Tbl_Requisitos_Matriz.Add(objreqmat);// se guarda el registro en la tabla Tbl_Requisitos_Matriz (contiene el requisito creado desde el formulario)
                   dbReqLeg.SaveChanges();

                   return true;
               }
               catch (Exception ex)
               {
                   strError = ex.Message;
                   return false;
               }
           }



           /// <summary>
           /// metodo para eliminar los requisitos legales de la matriz 
           /// </summary>
           /// <param name=""></param>
           /// <returns></returns>
           public bool Eliminar_ReqLegalesOtros(int PK_RequisitosLegales)
           {
               try
               {
                   RequisitosLegalesOtros ObregLeg = dbReqLeg.Tbl_Requisitos_legales_Otros.Where(g => g.PK_RequisitosLegalesOtros == PK_RequisitosLegales).FirstOrDefault();

                   Requisitos_Matriz objreqmat = dbReqLeg.Tbl_Requisitos_Matriz.Where(g => g.FK_RequisitosLegalesOtros == PK_RequisitosLegales).FirstOrDefault();

                   dbReqLeg.Tbl_Requisitos_legales_Otros.Remove(ObregLeg);

                   dbReqLeg.Tbl_Requisitos_Matriz.Remove(objreqmat);

                   dbReqLeg.SaveChanges();
                   return true;
               }
               catch (Exception ex)
               {
                   strError = ex.Message;
                   return false;
               }
           }






           public bool Busqueda_Matricesduplicado(string NombreMatriz, int PK_Empresa)
           {
               MatrizRequisitosLegales Objlist_Mat = new MatrizRequisitosLegales();
               Objlist_Mat = dbReqLeg.Tbl_Matriz_RequisitosLegales.Where(s => s.FK_Empresa == PK_Empresa && s.NombreMatriz == NombreMatriz).FirstOrDefault();

               if (Objlist_Mat != null)
               {//si ya existe esa matriz

                   return true;//ya existe esa matriz
               }
               else
               {
                   return false;////no existe esa matriz
               }
           }




        #endregion
























    }
}
