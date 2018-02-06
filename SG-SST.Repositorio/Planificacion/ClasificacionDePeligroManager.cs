using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Planificacion;
using SG_SST.Models;

namespace SG_SST.Repositorio.Planificacion
{
  public  class ClasificacionDePeligroManager:IClasificacionPeligros

    {

        public List<EDClasificacionDePeligro> ObtenerPeligrosConTiposPeligros()
        {
            List<EDClasificacionDePeligro> peligros = new List<EDClasificacionDePeligro>();
            using (var context = new SG_SSTContext())
            {
                peligros = (from pg in context.Tbl_Clasificacion_De_Peligro
                              join tpgo in context.Tbl_Tipo_De_Peligro on pg.FK_Tipo_De_Peligro equals tpgo.PK_Tipo_De_Peligro
                              select new EDClasificacionDePeligro
                              {
                                  IdClasificacionDePeligro = pg.PK_Clasificacion_De_Peligro,
                                  DescripcionClaseDePeligro = pg.Descripcion_Clase_De_Peligro,
                                  TipoDePeligro = tpgo.Descripcion_Del_Peligro
                              }).ToList();
            }
            return peligros;
        }
    }
}
