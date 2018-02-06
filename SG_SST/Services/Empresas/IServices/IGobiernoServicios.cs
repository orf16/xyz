
namespace SG_SST.Services.Empresas.IServices
{

    using SG_SST.Models.Empresas;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    interface IGobiernoServicios
    {

        void GrabarGobierno(Gobierno gobierno,int Pk_Id_Empresa);

        /// <summary>
        /// Se graba una mision para un gobierno.
        /// </summary>
        /// <param name="mision"> mision a ingresar.</param>
        /// <param name="nit">id/nit de la empresa.</param>
        /// <returns></returns>
        /// 
        bool GrabarMision(string mision,int Pk_Id_Empresa);

        //Comentarlos aca en la Interface, en el gobiernoservicio no tanto
        void GrabarGobiernoV(Gobierno gobiernov,int Pk_Id_Empresa,int nit);

        bool GrabarVision(string vision, int Pk_Id_Empresa, int nit);

        string ObtenerMision(int Pk_Id_Empresa);

        string ObtenerVision(int Pk_Id_Empresa);

        bool EliminarMision(int Pk_Id_Empresa);

        bool EliminarVision(int Pk_Id_Empresa);

        bool GrabarProceso(Proceso proceso);
        //Proceso ObtenerProceso(int Pk_Id_Empresa);


        //Proceso ObtenerProceso(int Pk_Id_Empresa, int Pk_Id_Proceso);


    }



}
