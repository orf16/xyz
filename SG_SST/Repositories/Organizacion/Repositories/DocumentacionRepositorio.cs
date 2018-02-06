using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SG_SST.Models.Organizacion;
using System.Data.Entity;
using System.Linq;
using SG_SST.Models;


namespace SG_SST.Repositories.Organizacion.Repositories
{
    public class DocumentacionRepositorio
    {
        SG_SSTContext dbDocumentacion;

        public DocumentacionRepositorio()
        {
            dbDocumentacion = new SG_SSTContext();
        }



        public bool GrabarDocumentacion(Documentacion_Organizacion DocumentacionOrg)
        {
            if (DocumentacionOrg.NombreArchivo_Documentacion != null)
            {
                dbDocumentacion.Tbl_Documentacion_Organizacion.Add(DocumentacionOrg);
                dbDocumentacion.SaveChanges();
                return true;
            }
            return false;
        }




        /// <summary>
        /// script para eliminar el archivo seleccionado en Módulo Organizacion-Documentacion
        /// </summary>
        /// <param name="IdArchivo"></param>
        /// <returns></returns>
        public bool Eliminar_DocumentacionArchivo(int IdArchivo){

            try
            {
                Documentacion_Organizacion modDocumentacion = dbDocumentacion.Tbl_Documentacion_Organizacion.Find(IdArchivo);
                dbDocumentacion.Tbl_Documentacion_Organizacion.Remove(modDocumentacion);
                dbDocumentacion.SaveChanges();
            

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