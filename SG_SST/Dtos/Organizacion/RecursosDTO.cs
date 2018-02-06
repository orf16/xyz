using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Dtos.Organizacion
{
    public class RecursosDTO
    {

        public RecursosDTO()
        {
        }


        public RecursosDTO(
            string Nombre_Sede,
            string Nombre_Recurso,
            int Periodo_Asignado,
            string Descripcion_Fase,
            string Descripcion_Tipo_Recurso
            )
        {

            this.Nombre_Sede = Nombre_Sede;
            this.Nombre_Recurso = Nombre_Recurso;
            this.Periodo_Asignado = Periodo_Asignado;
            this.Descripcion_Fase = Descripcion_Fase;
            this.Descripcion_Tipo_Recurso = Descripcion_Tipo_Recurso;




        }

        public string Nombre_Sede { get; set; }
        public string Nombre_Recurso { get; set; }
        public int Periodo_Asignado { get; set; }
        public string Descripcion_Fase { get; set; }
        public string Descripcion_Tipo_Recurso { get; set; }


    }
}