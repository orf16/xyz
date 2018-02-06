using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SG_SST.Models.Politica;
using SG_SST.Repositories.Politica.Repositories;
using SG_SST.Repositories.Politica.IRepositories;
using SG_SST.Services.Politica.IServices;
using SG_SST.Models.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.EntidadesDominio.Usuario;
using SG_SST.Models.Revision;


namespace SG_SST.Services.Politica.Services
{
    public class PoliticaServicios : IpoliticaServicios
    {

        IPoliticaRepositorio gb;

        public PoliticaServicios()
        {
            gb = new PoliticaRepositorio();//instancia  de politica repositorio quien trae los valores de las consultas en la BD        
        }

        internal SG_SST.Services.Politica.IServices.IpoliticaServicios IpoliticaServicios
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public SG_SST.Repositories.Politica.Repositories.PoliticaRepositorio PoliticaRepositorio
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public bool GrabarPolitica(mPolitica politica)
        {
            ///toda la logica de grabar gobierno 
            return gb.GrabarPolitica(politica);
        }


        public bool GrabarPoliticaEmpresa(string politicae, int ID_Empresa, bool politicaFirma_Rep)
        {
            mPolitica objpolitica = gb.ObtenerPolitica(ID_Empresa);//obtiene la consulta del RepositorioGobierno
            if (objpolitica != null)
            {
                objpolitica.Firma_Rep = politicaFirma_Rep;
                objpolitica.strDescripcion_Politica = politicae;
                return gb.ModficarPolitica(objpolitica);
            }
            else
            {
                mPolitica gbo = new mPolitica();
                gbo.Firma_Rep = politicaFirma_Rep;
                gbo.strDescripcion_Politica = politicae;
                gbo.FK_Empresa = ID_Empresa;
                gb.GrabarPolitica(gbo);
                return true;
            }
        }



        public bool EliminarPolitica(int ID_Empresa)
        {
            return gb.EliminaPolitica(ID_Empresa);
        }



        public string ObtenerPolitica(int ID_Empresa)
        {
            return gb.ObtenerPoliticaEmpresa(ID_Empresa);
        }


        /// <summary>
        ///validar si existen datos en la tabala politica para permitir o no mostrar menu de crear y cargar politica
        /// </summary>
        public string ValidaExistePolitica(int ID_Empresa)
        {

            mPolitica objpolitica = gb.ObtenerPolitica(ID_Empresa);//obtiene la consulta del RepositorioGobierno
            if (objpolitica != null)//si existen datos 
            {
                return "texto";
            }
            else
            {
                return "texto";
            }
        }



        public bool GrabarOtrasInteracciones(Mod_OtrasInteracciones OtrasInteracciones)
        {
            ///toda la logica de grabar OtrasInteracciones 
            return gb.GrabarOtrasInteracciones(OtrasInteracciones);



        }

        public bool EliminarOtrasInteracciones(int id)
        {
            return gb.EliminarOtrasInteracciones(id);
        }


        /// <summary>
        /// Servicio que permite poner como documento privado el archivo subido - otras interacciones y directrices
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ModificarDocumentoPrivado(int id)
        {

            Mod_OtrasInteracciones obj = gb.ObtenerDocumentoPrivado(id);

            if (obj != null)
            {
                if (obj.TipoDocumento_Archivo == "Documento Privado")
                {
                    obj.TipoDocumento_Archivo = " ";
                    return gb.ModificaOtrasInteracciones(obj);
                }
                else
                {
                    obj.TipoDocumento_Archivo = "Documento Privado";
                    return gb.ModificaOtrasInteracciones(obj);
                }

            }
            return false;
        }


        public bool ObtenerDocumentoPrivado(int id, string NombreDocumento)
        {
            Mod_OtrasInteracciones obj = gb.ObtenerDocumentoPrivado(id);
            if (obj != null)
            {
                obj.Archivo_OtrasInteracciones = NombreDocumento;
                return gb.ModificaOtrasInteracciones(obj);
            }
            else
            {
                Mod_OtrasInteracciones obj2 = new Mod_OtrasInteracciones();
                obj2.TipoDocumento_Archivo = NombreDocumento;
                return true;
            }

        }


        public bool ModificarNombreOtrasInteracciones(int id, string Nombre)
        {
            Mod_OtrasInteracciones obj = gb.ObtenerDocumentoPrivado(id);
            obj.TipoDocumento_Archivo = Nombre;
            return gb.ModificaOtrasInteracciones(obj);

        }



        public string ObtenerNombreArchivootrasInteracciones(int id)
        {
            return gb.ObtenerNombreArchivoOtrasInteracciones(id);
        }


        /// <summary>
        /// servicio para buscar un archivo segun el nombre ingresado
        /// </summary>
        /// <param name="ID_Empresa"></param>
        /// <param name="Palabra_Busqueda"></param>
        /// <returns></returns>
        public string ObtenerOtrasInteraccionesBusqueda(string Palabra_Busqueda)
        {
            return gb.ObtenerOtrasInteraccionesBusqueda(Palabra_Busqueda);
        }



        public Usuario ValidarExisteFirma(int idempresa)
        {
            Usuario objusur = gb.ValidarExisteFirma(idempresa);



            return objusur;
        }



        /// <summary>
        /// METODO PARA GURADAR EN BASE DE DATOS-AL CHEQUEAR (ANEXAR FIRMA REPRESENTANTE LEGAL MODULO POLITICA)
        /// </summary>
        /// <param name="objpolitic"></param>
        /// <returns></returns>
        public bool ObtenerGuardar_Estadofirma(mPolitica objpolitic)
        {
            mPolitica objpolitica = gb.ObtenerPolitica(objpolitic.FK_Empresa);//obtiene la consulta del RepositorioGobierno

            if (objpolitica != null)
            {

                objpolitica.Firma_Rep = true;

                gb.ModficarPolitica(objpolitica);
                return true;
            }
            else { return false; }




        }

        public bool ObtenerGuardar_Estadofirmas(ActaRevision acta)
        {
            ActaRevision objacta = gb.ObtenerActa(acta.Fk_Id_Empresa, acta.PK_Id_ActaRevision);//obtiene la consulta del RepositorioGobierno
            if (objacta != null)
            {

                objacta.Firma_Representante_SGSST = true;
                gb.ModficarActaRevision(objacta);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ObtenerGuardar_EstadofirmasR(ActaRevision acta)
        {
            ActaRevision objacta = gb.ObtenerActa(acta.Fk_Id_Empresa, acta.PK_Id_ActaRevision);//obtiene la consulta del RepositorioGobierno
            if (objacta != null)
            {

                objacta.Firma_Responsable_SGSST = true;
                gb.ModficarActaRevisionR(objacta);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// VALIDAR EN BASE DE DATOS SI EL CHECKBOX ANEXAR FIRMA SE CHEQUEO - EN EL MODULO PARA ANEXARLA
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <returns></returns>
        public mPolitica validarestadofirma(int idEmpresa)
        {
            mPolitica objpolitica = gb.ObtenerPolitica(idEmpresa);//obtiene la consulta del RepositorioGobierno

            return objpolitica;


        }







    }
}