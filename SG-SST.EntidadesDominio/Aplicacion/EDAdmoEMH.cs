using SG_SST.EntidadesDominio.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_SST.EntidadesDominio.Aplicacion
{
    public class EDAdmoEMH
    {
        public int Pk_Id_AdmoEMH { get; set; }
        public string TipoElemento { get; set; }
        public string NombreElemento { get; set; }
        public string CodigoElemento { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Fabricante { get; set; }
        public DateTime Fecha_Fab { get; set; }
        public int HorasVida { get; set; }
        public string Ubicacion { get; set; }
        public string Caracteristicas { get; set; }
        public string NombreResponsable { get; set; }
        public string CargoResponsable { get; set; }
        public short Estado { get; set; }
        public string Imgdownload1 { get; set; }
        public string ArchivoImagen1 { get; set; }
        public string RutaImage1 { get; set; }
        public string Imgdownload2 { get; set; }
        public string ArchivoImagen2 { get; set; }
        public string RutaImage2 { get; set; }
        public string Imgdownload3 { get; set; }
        public string ArchivoImagen3 { get; set; }
        public string RutaImage3 { get; set; }
        public string Imgdownload4 { get; set; }
        public string ArchivoImagen4 { get; set; }
        public string RutaImage4 { get; set; }
        public string Imgdownload5 { get; set; }
        public string ArchivoImagen5 { get; set; }
        public string RutaImage5 { get; set; }
        public string Filedownload1 { get; set; }
        public string NombreArchivo1 { get; set; }
        public string Ruta1 { get; set; }
        public string Filedownload2 { get; set; }
        public string NombreArchivo2 { get; set; }
        public string Ruta2 { get; set; }
        public string Filedownload3 { get; set; }
        public string NombreArchivo3 { get; set; }
        public string Ruta3 { get; set; }
        public int FK_Clasificacion_De_Peligro { get; set; }
        public string Actividad { get; set; }
        public DateTime Fecha_prog { get; set; }
        public string ResponsableInsp { get; set; }
        public int Fk_Id_Empresa { get; set; }


        public List<int?> elementos { get; set; }
        public Nullable<System.DateTime> Fecha_Baja { get; set; }
        public string Motivo_Baja { get; set; }

        public List<EDPeligro> ListaPeligros { get; set; }

    }
}
