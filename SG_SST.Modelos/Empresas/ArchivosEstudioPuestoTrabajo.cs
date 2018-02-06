using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.Models.Empresas
{
    [Table("Tbl_Archivos_Estudio_Puesto_Trabajo")]
    public class ArchivosEstudioPuestoTrabajo
    {
        [Key]
        public int PK_Id_Archivo_Estudio_Puesto_Trabajo { get; set; }

        public string NombreArchivo { get; set; }

        public string Ruta { get; set; }
    }
}
