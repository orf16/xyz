using SG_SST.EntidadesDominio.Ausentismo;
using SG_SST.Interfaces.Ausentismo;
using SG_SST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Repositorio.Ausentismo
{
    public class MunicipioManager : IMunicipio
    {
        /// <summary>
        /// Obtiene el listado de empresas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EDMunicipio> ObtenerMunicipio(int idDepto)
        {
            try
            {
                List<EDMunicipio> municipio = null;
                using (var context = new SG_SSTContext())
                {
                    municipio = context.Tbl_Municipio.Where(c => c.Fk_Nombre_Departamento == idDepto).Select(c => new EDMunicipio()
                    {
                        IdMunicipio = c.Pk_Id_Municipio,
                        Nombre = c.Nombre_Municipio,
                    }).ToList();
                }
                return municipio;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

       
        public List<EDMunicipio> ObtenerMunicipiosConDetps()
        {
            List<EDMunicipio> municipios = new List<EDMunicipio>();
            using (var context = new SG_SSTContext())
            {
                municipios = (from mp in context.Tbl_Municipio
                              join dto in context.Tbl_Departamento on mp.Fk_Nombre_Departamento equals dto.Pk_Id_Departamento
                              select new EDMunicipio
                              {
                                  IdMunicipio = mp.Pk_Id_Municipio,
                                  Nombre = mp.Nombre_Municipio,
                                  NombreDepartamento = dto.Nombre_Departamento
                              }).ToList();                
            }
            return municipios;
        }

        public int ValidarMunicipio(int idMunicipio, int idDepartamento)
        {
            int result = 0;
            using (var context = new SG_SSTContext())
            {
                var municipio = context.Tbl_Municipio.Where(m => m.Pk_Id_Municipio == idMunicipio && m.Fk_Nombre_Departamento == idDepartamento).Select(m => m).FirstOrDefault();
                if (municipio != null)
                    result = municipio.Pk_Id_Municipio;
            }

            return result;
        }
    }

}
