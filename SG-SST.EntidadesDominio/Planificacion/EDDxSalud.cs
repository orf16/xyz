using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Planificacion
{
    public class EDDxSalud
    {

        public int IdDxCondicionesDeSalud { get; set; }

        public DateTime FechaCreacionDiagnostico { get; set; }

        public string ZonaLugar { get; set; }

        public int NumeroTrabajadoresLugar { get; set; }

        public string NombreDscripcion { get; set; }

        public int Pk_Id_Sede { get; set; }

        public string NombreSede { get; set; }

        public string NombreMunicipio { get; set; }

        public string NombreDepartamento { get; set; }

        public DateTime Fecha_Dx { get; set; }

        public DateTime Fecha_Inicial_Dx { get; set; }

        public DateTime Fecha_Final_Dx { get; set; }

        public int vigencia { get; set; }

        public string Responsable_informacion { get; set; }

        public string Profesion_Responsable { get; set; }

        public string Tarjeta_Profesional { get; set; }

        public string ClasificacionYDescripcionPeligro { get; set; }

        public int? Procesos { get; set; }

   
        public string nombreProceso { get; set; }

        public string nitEmpresa { get; set; }
        public string nombreEmpresa { get; set; }

        public List<EDSintomatologiaDx> EDSintomatologiaDx { get; set; }      

        public List<EDPruebasClinicasDx> EDPruebasClinicasDx { get; set; }

        public List<EDPruebasPClinicasDx> EDPruebasPClinicasDx { get; set; }

        public List<EDDiagnosticoCie10Dx> EDDiagnosticoCie10Dx { get; set; }
        public List<EDClasificacionPeligroDx> EDClasificacionPeligroDx { get; set; }   

       

    }
}
