using SG_SST.EntidadesDominio.Empresas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SG_SST.Models.Empresa
{
    public class EmpresaUsuariaModel
    {
        public int IdTipoDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string RazonSocial { get; set; }
        public string DocumentoEmpresa { get; set; }
        public string Direccion { get; set; }
        public int IdDepartamento { get; set; }
        public string Departamento { get; set; }
        public int Id_Municipio { get; set; }
        public string Municipio { get; set; }

        bool seleccionado;
        public bool Seleccionado
        {
            get
            {
                return this.seleccionado;
            }
            set
            {
                this.seleccionado = value;

                if (this.seleccionado)
                    SeleccionadoStr = "checked";
            }
        }
        public string SeleccionadoStr { get; set; }

        public List<EDEmpresa_Usuaria> listEmpresasUsuaria { get; set; }

        public List<SelectListItem> lstDepartamentos { get; set; }

        public List<SelectListItem> lstMunicipios { get; set; }

        public EmpresaUsuariaModel()
        {
            Seleccionado = false;
            SeleccionadoStr = "";
            TipoDocumento = "NI";
        }

        public int idDepartamento { get; set; }
        public int idMunicipio { get; set; }

        public List<SelectListItem> lstDocumentos { get; set; }
    }
}