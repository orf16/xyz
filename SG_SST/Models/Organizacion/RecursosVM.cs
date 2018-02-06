using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Organizacion
{
    public class RecursosVM
    {
          public string Nombre_Recurso { get; set; }
          public int Periodo { get; set; }

          public int Fk_Id_Sede { get; set; }
          public int Fk_Id_TipoRecurso { get; set; }

          public int Fk_Id_Fase { get; set; } 


          
    }
}