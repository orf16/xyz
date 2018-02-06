using SG_SST.EntidadesDominio.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Revision
{
    public class EDActaRevision
    {
        public int PK_Id_ActaRevision { get; set; }

        public string Nombre { get; set; }

        public int Num_Acta { get; set; }

        public DateTime Fecha_Creacion_Acta { get; set; }

        public int FK_Empresa { get; set; }

        public string NombreEmpresa { get; set; }

        public string NitEmpresa { get; set; }

        public int FK_Sede { get; set; }

        //public EDSede Sede { get; set; }

        public DateTime Fecha_Inicial_Revision { get; set; }

        public DateTime Fecha_Final_Revision { get; set; }

        public string Elaborada { get; set; }

        public string Firma_Gerente_General { get; set; }

        public bool Firma_Representante_SGSST { get; set; }

        public bool Firma_Responsable_SGSST { get; set; }

        public string NombreParticipante { get; set; }

        public string DocumentoParticipante { get; set; }

        public string CargoParticipante { get; set; }

        public int FK_ActaRevision { get; set; }

        public int PK_Id_Agenda { get; set; }

        public string Tema { get; set; }

        public string Desarrollo { get; set; }

        public List<EDPlanAccionRevision> PlanesAccion { get; set; }

        public List<EDAgendaRevision> Agenda { get; set; }

        public List<EDParticipanteRevision> Participantes { get; set; }

    }
}
