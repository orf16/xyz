using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.Models;
using OfficeOpenXml;
using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.Interfaces.Aplicacion;
using SG_SST.InterfazManager.Aplicacion;



 namespace SG_SST.Logica.Aplicacion
{        

    public class LNGestionDelCambio
    {
        private static IGestionDelCambio em = IMGestionDelCambio.GestionDelCambio();

       
        public EDGestionDelCambio GuardarGestionDelCambio(EDGestionDelCambio perfilsoc)
        {
            EDGestionDelCambio mp = em.GuardarGestionDelCambio(perfilsoc);

            return mp;
        }



        public bool EliminarGestionDelCambio(int idgestion)
        {
            return em.EliminarGestionDelCambio(idgestion);

        }







    }
     




}
