using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SG_SST.Helpers
{
    public class ValidaArchivos : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            var result = true;
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                ErrorMessage = "Debe seleccionar algún archivo con formato pdf.";
                return false;
            }
            if (file.ContentLength > 512000)//500 KILOBYTES
            {
                ErrorMessage = "El archivo no debe superar los 500Kb de tamaño.";
                result = false;
            }
            if (!file.ContentType.Equals("application/pdf"))
            {
                ErrorMessage = "El archivo debe tener formato pdf.";
                result = false;
            }
            if (!file.FileName.EndsWith(".pdf"))
            {
                ErrorMessage = "El archivo debe tener formato pdf.";
                result = false;
            }
            return result;
        }
    }
}