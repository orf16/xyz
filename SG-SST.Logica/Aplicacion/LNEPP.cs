using SG_SST.EntidadesDominio.Aplicacion;
using SG_SST.EntidadesDominio.Empleado;
using SG_SST.EntidadesDominio.Empresas;
using SG_SST.EntidadesDominio.Planificacion;
using SG_SST.Interfaces.Aplicacion;
using SG_SST.InterfazManager.Aplicacion;
using SG_SST.Logica.MedicionEvaluacion;
using SG_SST.Models.Empleado;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using RestSharp;
using SG_SST.ClienteServicios;
using SG_SST.Interfaces.Planificacion;
using SG_SST.InterfazManager.Planificacion;

namespace SG_SST.Logica.Aplicacion
{
    public class LNEPP
    {
        private static IEPP aepp = IMEPP.IEPP();
        private static ITipoDePeligro tipoDePeligro = IMIdentificacionPeligros.TipoDePeligro();

        LNAcciones LNAcciones = new LNAcciones();
        public bool GuardarMasivoEPP(List<EDEPP> ListaEPP)
        {
            bool ProbarGuardar = aepp.GuardarMasivoEPP(ListaEPP);
            return ProbarGuardar;
        }
        public bool GuardarEPP(EDEPP EDEPP)
        {
            bool ProbarGuardar = aepp.GuardarEPP(EDEPP);
            return ProbarGuardar;
        }
        public bool EditarEPP(EDEPP EDEPP)
        {
            bool ProbarEditar = aepp.EditarEPP(EDEPP);
            return ProbarEditar;
        }
        public List<EDEPP> ConsultaMatrizEpp(string Nombre, int IdClasPel, int IdCargo, int idEmpresa)
        {
            List<EDEPP> NuevaLista = aepp.ConsultaMatrizEpp(Nombre, IdClasPel, IdCargo, idEmpresa);
            return NuevaLista;
        }
        public List<EDEPP> ConsultaMatrizEppCargo(int IdCargo, int idEmpresa)
        {
            List<EDEPP> NuevaLista = aepp.ConsultaMatrizEppCargo(IdCargo, idEmpresa);
            return NuevaLista;
        }
        public List<EDEPP> ConsultaMatrizEppPersona(string IdPersona, int idEmpresa)
        {
            List<EDEPP> NuevaLista = aepp.ConsultaMatrizEppPersona(IdPersona, idEmpresa);
            return NuevaLista;
        }
        public EDEPP ConsultarEPPDownload(int IdEPP, int idEmpresa)
        {
            EDEPP EDEPP = aepp.ConsultarEPPDownload(IdEPP, idEmpresa);
            return EDEPP;
        }
        public EDEPP ConsultarEPP(int IdEPP, int idEmpresa)
        {
            EDEPP EDEPP = aepp.ConsultarEPP(IdEPP, idEmpresa);
            return EDEPP;
        }
        public bool EliminarEPP(int IdElemento, int IdEmpresa)
        {
            bool ProbarEliminar = false;
            ProbarEliminar = aepp.EliminarEPP(IdElemento, IdEmpresa);
            return ProbarEliminar;
        }
        public bool ComprobarAsignacionEPP(int IdElemento, int IdEmpresa)
        {
            bool ProbarAsignacion = false;
            ProbarAsignacion = aepp.ComprobarAsignacionEPP(IdElemento, IdEmpresa);
            return ProbarAsignacion;
        }
        public bool GuardarControlSuministro(EDEPPSuministro EDEPPSuministro, List<EDEPPSuministroEPP> ListaSuministros)
        {
            bool ProbarGuardar = aepp.GuardarControlSuministro(EDEPPSuministro, ListaSuministros);
            return ProbarGuardar;
        }
        public EDEPPSuministro UltimoSuministro(int Id_Empresa)
        {
            EDEPPSuministro UltimoSuministro = aepp.UltimoSuministro(Id_Empresa);
            if (UltimoSuministro.Pk_Id_SuministroEPP==0)
            {
                return null;
            }
            return UltimoSuministro;
        }
        public List<EDEPPSuministro> ConsultaListaAsignacion(string FechaAntes, string FechaDespues, int Cargo, string Cedula, int Riesgo, int Sede, int idEmpresa)
        {
            List<EDEPPSuministro> NuevaLista = aepp.ConsultaListaAsignacion(FechaAntes, FechaDespues, Cargo, Cedula, Riesgo, Sede, idEmpresa);
            return NuevaLista;
        }
        public EDEPPSuministro ConsultaListaAsignacionId(int id, int idEmpresa)
        {
            EDEPPSuministro NuevaLista = aepp.ConsultaListaAsignacionId(id, idEmpresa);
            return NuevaLista;
        }
        public bool EliminarAsigEPP(int IdAsig, int IdEmpresa)
        {
            bool ProbarEliminar = false;
            ProbarEliminar = aepp.EliminarAsigEPP(IdAsig, IdEmpresa);
            return ProbarEliminar;
        }
        public List<EDCargo> ListaCargos()
        {
            List<EDCargo> ListaCargos = aepp.ListaCargos();
            return ListaCargos;

        }
        public List<EDEPP> ConsultaMatrizEppUsuario(string documento, string Nit, string clienteS, string requestS)
        {
            EDCargo EDCargo = ClienteEmpresa.ObtenerCargoUsuarioSiarp(documento, Nit, clienteS, requestS);
            List<EDCargo> ListaEDCargo = ListaCargos();
            List<EDEPP> NuevaLista = aepp.ConsultaMatrizEppUsuario(EDCargo, ListaEDCargo, Nit);
            return NuevaLista;
        }
        public EDCargo pruebaservicio(string documento, string Nit, string clienteS, string requestS)
        {
            EDCargo EDCargo = ClienteEmpresa.ObtenerCargoUsuarioSiarp(documento, Nit, clienteS, requestS);
            return EDCargo;
        }
        public List<EDCargo> pruebaservicio1(string documento, string Nit, string clienteS, string requestS)
        {
            List<EDCargo> ListaEDCargo = ListaCargos();
            return ListaEDCargo;
        }
        public EDEPP ConsultarEPPAPP(int IdEPP, string Nit)
        {
            EDEPP EDEPP = aepp.ConsultarEPPAPP(IdEPP, Nit);
            return EDEPP;
        }
        public List<EDEPP> ConsultaMatrizEppCargo2(string NIT)
        {
            List<EDEPP> NuevaLista = aepp.ConsultaMatrizEppCargo2(NIT);
            return NuevaLista;

        }
        public List<EDTipoDePeligro> ObtenerTiposDePeligro()
        {
            return tipoDePeligro.ObtenerTiposDePeligro();
        }

    }
}
