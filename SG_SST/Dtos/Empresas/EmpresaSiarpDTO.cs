using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Dtos.Empresas
{
    public class EmpresaSiarpDTO
    {
        public EmpresaSiarpDTO()
        {
        }

        public EmpresaSiarpDTO(
            string tipoDoc,
            string idEmpresa,
            string NitEmpresa,
            int idRepresentanteLegal,
            string razonSocial, 
            string direccionEmpresa,
            
            string idSeccional,
            string emailEmpresa,
            string paginaWeb,
            string telefonoEmpresa,
            string faxEmpresa,
            string zona,
            string riesgo,
            string numeroDeTrabajadores,
            string estado,
            string fecAfiliaEfectiva,
            string departamento,
            string municipio,
            int idSectorEconomico,
            string actividadEconomica,
            string nomActEconomico
            )
        {
            this.tipoDoc = tipoDoc;
            this.idEmpresa = idEmpresa;
            this.NitEmpresa = NitEmpresa;
            this.idRepresentanteLegal = idRepresentanteLegal;
            this.razonSocial = razonSocial;
            this.direccionEmpresa = direccionEmpresa;
            
            this.idSeccional = idSeccional;
            this.emailEmpresa=emailEmpresa;
            this.paginaWeb=paginaWeb;
            this.telefonoEmpresa=telefonoEmpresa;
            this.faxEmpresa=faxEmpresa;
            this.zona=zona;
            this.riesgo=riesgo;
            this.numeroDeTrabajadores=numeroDeTrabajadores;
            this.estado=estado;
            this.fecAfiliaEfectiva=fecAfiliaEfectiva;
            this.departamento=departamento;
            this.municipio=municipio;
            this.idSectorEconomico=idSectorEconomico;
            this.actividadEconomica=actividadEconomica;
            this.nomActEconomico=nomActEconomico;

        }

        public string tipoDoc { get; set; }
        public string idEmpresa { get; set; }
        public string NitEmpresa { get; set; }

        public int idRepresentanteLegal { get; set; }

        public string razonSocial { get; set; }

        public string direccionEmpresa { get; set; }
        

        public string idSeccional { get; set; }
        public string emailEmpresa { get; set; }

        public string paginaWeb { get; set; }

        public string telefonoEmpresa {get;set;}

        public string faxEmpresa {get;set;}
        public string zona {get;set;}
        public string riesgo {get;set;}
        public string numeroDeTrabajadores {get;set;}
        public string estado {get; set;}
        public string fecAfiliaEfectiva {get; set;}
        public string departamento {get; set;}
        public string municipio {get;set;}
        public int idSectorEconomico {get;set;}
        public string actividadEconomica{get; set;}
        public string nomActEconomico {get;set;}




    }
}