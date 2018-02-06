using SG_SST.Interfaces.Aplicacion;
using SG_SST.Models.Aplicacion;
using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SG_SST.Repositorio.Aplicacion
{
   public  class TipoInspeccionManager:IInspeccion
    {

       public List<EDTipoInspeccion> ObtenerTiposInspeccion()
       {

           List<EDTipoInspeccion> tiposinspeccion = null;
           using (SG_SSTContext datos= new SG_SSTContext())
           {
               tiposinspeccion = (from m in datos.Tbl_Maestro_Planeación_Inspeccion
                               select new EDTipoInspeccion
                               {
                                   IdTipoInspeccion = m.Pk_Id_Maestro_Tipo_Inspeccion,
                                   DescripcionTipoInspeccion = m.Descripcion_Tipo_Inspeccion
                               }
                               ).ToList();

           return tiposinspeccion;
           }
               

       }

    }
    


}





