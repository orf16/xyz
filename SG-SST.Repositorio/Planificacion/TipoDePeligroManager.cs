using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Models;
using SG_SST.Interfaces.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Planificacion
{
    public class TipoDePeligroManager : ITipoDePeligro
    {
        public List<EDTipoDePeligro> ObtenerTiposDePeligro()
        {
            List<EDTipoDePeligro> tdp = null;
            using (SG_SSTContext contex = new SG_SSTContext())
            {
                tdp = (from tp in contex.Tbl_Tipo_De_Peligro
                       select new EDTipoDePeligro
                                {
                                    PK_Tipo_De_Peligro = tp.PK_Tipo_De_Peligro,
                                    Descripcion_Del_Peligro = tp.Descripcion_Del_Peligro
                                }
                               ).ToList();
            }

            return tdp;
        
        }

    }
}
