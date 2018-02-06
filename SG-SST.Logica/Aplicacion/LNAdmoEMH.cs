using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Aplicacion;
using SG_SST.InterfazManager.Aplicacion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Logica.Aplicacion
{
    public class LNAdmoEMH
    {
        private static IEHM aehm = IMEHM.IEHM();
        public bool GuardarHojaVidaEMH(EDAdmoEMH EDAdmoEMH, List<EDPeligroEMH> ListaPeligro)
        {
            bool ProbarGuardar = aehm.GuardarHojaVidaEMH(EDAdmoEMH, ListaPeligro);
            return ProbarGuardar;
        }
        public bool EditarHojaVidaEMH(EDAdmoEMH EDAdmoEMH, List<EDPeligroEMH> ListaPeligro)
        {
            bool ProbarGuardar = aehm.EditarHojaVidaEMH(EDAdmoEMH, ListaPeligro);
            return ProbarGuardar;
        }
        public bool DarBajaEMH(EDAdmoEMH EDAdmoEMH)
        {
            bool ProbarGuardar = aehm.DarBajaEMH(EDAdmoEMH);
            return ProbarGuardar;
        }
        public bool SubirEMH(EDAdmoEMH EDAdmoEMH)
        {
            bool ProbarGuardar = aehm.SubirEMH(EDAdmoEMH);
            return ProbarGuardar;
        }
        public List<EDAdmoEMH> ConsultaAdmoEMH(string Tipo, string Nombre, int idEmpresa)
        {
            List<EDAdmoEMH> NuevaLista = aehm.ConsultaAdmoEMH(Tipo, Nombre, idEmpresa);
            return NuevaLista;
        }
        public bool EliminarEHM(int IdElemento, int IdEmpresa)
        {
            bool ProbarEliminar = false;
            ProbarEliminar = aehm.EliminarEHM(IdElemento, IdEmpresa);
            return ProbarEliminar;
        }
        public EDAdmoEMH ConsultarEHM(int IdElemento, int IdEmpresa)
        {
            EDAdmoEMH EDAdmoEMH = aehm.ConsultarEHM(IdElemento, IdEmpresa);
            return EDAdmoEMH;
        }

        public List<EDProceso> ConsultaSubProcesos(int IdProceso, int Idempresa)
        {
            List<EDProceso> NuevaLista = aehm.ConsultaSubProcesos(IdProceso, Idempresa);
            return NuevaLista;
        }
        public List<EDEHMInspecciones> ConsultaInspeccion(string FechaI, string FechaD, int IdEHM, int IdEmpresa)
        {
            List<EDEHMInspecciones> NuevaLista = aehm.ConsultaInspeccion(FechaI, FechaD, IdEHM, IdEmpresa);
            return NuevaLista;
        }
        public EDEHMInspecciones ConsultaEHMInspeccion(int IdEHMIns, int IdEmpresa)
        {
            EDEHMInspecciones Inspeccion = aehm.ConsultaEHMInspeccion(IdEHMIns, IdEmpresa);
            return Inspeccion;
        }


    }
}
