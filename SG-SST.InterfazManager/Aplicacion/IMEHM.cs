﻿using SG_SST.Interfaces.Aplicacion;
using SG_SST.Repositorio.Aplicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.InterfazManager.Aplicacion
{
    public class IMEHM
    {
        public static IEHM IEHM()
        {
            return new EHMManager();
        }
    }
}