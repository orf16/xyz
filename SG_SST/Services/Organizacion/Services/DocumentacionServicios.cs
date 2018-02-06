using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SG_SST.Repositories.Organizacion.Repositories;
using SG_SST.Repositories.Organizacion.IRepositories;
using SG_SST.Services.Organizacion.IServices;

using SG_SST.Models.Organizacion;

namespace SG_SST.Services.Organizacion.Services
{
    public class DocumentacionServicios : IDocumentacionServicios
    {
        DocumentacionRepositorio dr;

        public DocumentacionServicios()
        {
            dr = new DocumentacionRepositorio();//instancia  de politica repositorio quien trae los valores de las consultas en la BD        
        }

        public bool GrabarDocumentacion(Documentacion_Organizacion DocumentacionOrg)
        {
            return dr.GrabarDocumentacion(DocumentacionOrg);
        }


        public bool Eliminar_DocumentacionArchivo(int IdArchivo)
        {           
            return dr.Eliminar_DocumentacionArchivo(IdArchivo);
            
        }









    }
}