using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SG_SST.Models.Aplicacion
{
    public class ContinuarPlaneacionVM
    {


        //informacion de los no Ejecutados///
        public string Responsable { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public string FechaPI { get; set; }

        public string DescripcionTipoInspecciona { get; set; }

        public int Consecutivoplan { get; set; }

        public int IDEmpresa { get; set; }

        public int IdPlaneacion { get; set; }

        //////////////
    }
}