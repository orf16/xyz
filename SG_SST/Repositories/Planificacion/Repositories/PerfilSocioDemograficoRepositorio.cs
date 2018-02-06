using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SG_SST.Models.Planificacion;
using System.Data.Entity;
using System.Collections.Generic;
using SG_SST.Models;
using SG_SST.Models.Empleado;
using SG_SST.Repositories.Planificacion.IRepositories;

namespace SG_SST.Repositories.Planificacion.Repositories
{
    public class PerfilSocioDemograficoRepositorio
    {

                 SG_SSTContext dbPerfilSoc;
         public PerfilSocioDemograficoRepositorio()
        {
            dbPerfilSoc = new SG_SSTContext();
        }


        #region "Métodos Públicos - Consultas en BD"
        public bool GrabarPerfilsocioDemoGrafico(tblEmpleado perfilsoc)
        {
            try
            {
                dbPerfilSoc.tblEmpleado.Add(perfilsoc);           
                dbPerfilSoc.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ModficarPerfilsocioDemoGrafico(tblEmpleado perfilsoc)
        {
            try
            {
                dbPerfilSoc.Entry(perfilsoc).State = EntityState.Modified;            
                dbPerfilSoc.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion





    }
}