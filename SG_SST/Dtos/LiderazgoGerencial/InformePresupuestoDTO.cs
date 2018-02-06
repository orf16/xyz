using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Dtos.LiderazgoGerencial
{
    public class InformePresupuestoDTO
    {
        public InformePresupuestoDTO() {
        }
        public InformePresupuestoDTO(string Actividad, double Planeado, double Ejecutado, double Disponible, string Intervalo_De_Tiempo) 
        {
            this.Actividad = Actividad;
            this.Planeado = Planeado;
            this.Ejecutado = Ejecutado;
            this.Saldo = Disponible;
            this.Intervalo_De_Tiempo = Intervalo_De_Tiempo;
        }

        public string Actividad { get; set; }
        public double Planeado { get; set; }
        public double Ejecutado { get; set; }
        public double Saldo { get; set; }
        public string Intervalo_De_Tiempo { get; set; }

    }
}