using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDPeligro
    {
     
        public int PK_Peligro { get; set; }

        public string Nombre_Del_Profesional { get; set; }

        public string Numero_De_Documento { get; set; }
   
        public string Numero_De_Licencia_SST { get; set; }
    
        public DateTime Fecha_De_Evaluacion { get; set; }
      
        public string Lugar { get; set; }
     
        public string Actividad { get; set; }
       
        public string Tarea { get; set; }
       
        public bool FLG_Rutinaria { get; set; }
        
        public string Fuente_Generadora_De_Peligro { get; set; }
      
        public string Otro { get; set; }
       
        public string Fuente { get; set; }
               
        public string Medio { get; set; }
      
        public string Eliminacion { get; set; }
       
        public string Sustitucion { get; set; }
            
        public string Controles_De_Ingenieria { get; set; }
      
        public string Controles_Administrativos { get; set; }
      
        public string Elementos_De_Proteccion { get; set; }
      
        public string Accion_De_Prevencion { get; set; }

        public int FK_Clasificacion_De_Peligro { get; set; }

        public int FK_Sede { get; set; }

        public int FK_Proceso { get; set; }

        public EDGTC45 gtc45 { get; set; }

        public EDConsecuenciasPorPeligros ConsecuenciasPorPeligro { get; set; }

    }
}
