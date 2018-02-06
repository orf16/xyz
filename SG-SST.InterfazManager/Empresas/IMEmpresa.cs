using SG_SST.Interfaces.Empresas;
using SG_SST.Repositorio.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.Empresas
{
    public class IMEmpresa
    {
        public static IEmpresa Empresa()
        {
            return new EmpresaManager();
        }

        public static ISedeRepositorio EmpresaSede()
        {
            return new SedeRepositorioManager ();
        }

        public static IRelacionesLaborales RelacionesLaborales()
        {
            return new RelacionesLaboralesManager();
        }

        public static IEmpresaUsuaria EmpresaUsuaria()
        {
            return new EmpresaUsuariaManager();
        }

    }
}
