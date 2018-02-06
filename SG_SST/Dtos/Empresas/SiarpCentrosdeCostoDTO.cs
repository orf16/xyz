using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SG_SST.Dtos.Empresas
{
    public class SiarpCentrosdeCostoDTO
    {
        public SiarpCentrosdeCostoDTO()
        {
        }

        public SiarpCentrosdeCostoDTO(

            string idActiEconomica,
            string idCentroTrabajo,
            string codCentroTrabajo,
            string departamento,
            string municipio,
            string telEmpresa,
            string faxEmpresa, 
            string idZona,
            string estado,
            string nombreActEcon,
            string direccionEmpresa,
            string nomDepartamento, 
            string nomMunicipio

                   )

        {

            this.idActiEconomica = idActiEconomica;
            this.idCentroTrabajo = idCentroTrabajo;
            this.codCentroTrabajo = codCentroTrabajo;
            this.departamento = departamento;
            this.municipio = municipio;
            this.telEmpresa = telEmpresa;
            this.faxEmpresa = faxEmpresa;
            this.idZona = idZona;
            this.estado = estado;
            this.nombreActEcon = nombreActEcon;
            this.direccionEmpresa = direccionEmpresa;
            this.nomDepartamento = nomDepartamento;
            this.nomMunicipio = nomMunicipio;



        }
        public  string idActiEconomica {get;set;}
        public string idCentroTrabajo { get; set; }

        public string codCentroTrabajo { get; set; }
        public string departamento { get; set; }
        public string municipio { get; set; }
        public string telEmpresa { get; set; }
        public string faxEmpresa { get; set; }
        public string idZona { get; set; }
        public string estado { get; set; }
        public string nombreActEcon { get; set; }
        public string direccionEmpresa { get; set; }
        public string nomDepartamento { get; set; }
        public string nomMunicipio { get; set; }




    }

    

}