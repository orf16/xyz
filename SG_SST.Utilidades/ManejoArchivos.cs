using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using static SG_SST.Utilidades.InpersonatorContext;

namespace SG_SST.Utilidades
{
    public class ManejoArchivos
    {
        /// <summary>
        /// Convierte un Stream en un array de bytes
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static byte[] ConvertirArchivoABytes(HttpPostedFileBase file)
        {
            if (file == null)
                return null;
            try
            {
                long originalPosition = 0;
                using (var datos = file.InputStream)
                {
                    if (datos.CanSeek)
                    {
                        originalPosition = datos.Position;
                        datos.Position = 0;
                    }
                    try
                    {
                        byte[] readBuffer = new byte[4096];

                        int totalBytesRead = 0;
                        int bytesRead;

                        while ((bytesRead = datos.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                        {
                            totalBytesRead += bytesRead;

                            if (totalBytesRead == readBuffer.Length)
                            {
                                int nextByte = datos.ReadByte();
                                if (nextByte != -1)
                                {
                                    byte[] temp = new byte[readBuffer.Length * 2];
                                    Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                                    Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                                    readBuffer = temp;
                                    totalBytesRead++;
                                }
                            }
                        }

                        byte[] buffer = readBuffer;
                        if (readBuffer.Length != totalBytesRead)
                        {
                            buffer = new byte[totalBytesRead];
                            Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                        }
                        return buffer;
                    }
                    catch (Exception ex){ return null; }
                    finally
                    {
                        if (datos.CanSeek)
                        {
                            datos.Position = originalPosition;
                        }
                        if(datos != null) datos.Dispose();
                    }
                }
            }
            catch { return null; }
        }

        public static byte[] ConvertirArchivoABytes(string fullFilePath)
        {
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        /// <summary>
        ///  Guarda un archivo en la ruta especificada
        /// </summary>
        /// <param name="bytesArchivos"></param>
        /// <param name="rutaFisica"></param>
        /// <param name="UsuarioArchivos"></param>
        /// <param name="DominioArchivos"></param>
        /// <param name="ContraseniaArchivos"></param>
        /// <returns></returns>
        public static string GuardarArchivos(HttpPostedFileBase archivo, string directorioSoportes, int codUsuario, string usuarioImp, string passwordImp, string dominio)
        {
            try
            {
                var result = string.Empty;
                if (archivo == null)
                    return string.Empty;

                var tiposSoportados = new[] { "pdf" };
                var extensionArchivo = Path.GetExtension(archivo.FileName).Substring(1);
                if (!tiposSoportados.Contains(extensionArchivo))
                    return string.Empty;

                var archivoBytes = ConvertirArchivoABytes(archivo);
                if (archivoBytes == null)
                    return string.Empty;

                var rutaDirectorio = string.Format(@"{0}\{1}", directorioSoportes, codUsuario);
                if (!Directory.Exists(rutaDirectorio))
                    Directory.CreateDirectory(rutaDirectorio);

                //suplantar el usuario en contexto por el pasado como parámetro, ya que este último
                //tiene privilegios de lectura/escritura en el servidor.
                using (new SG_SST.Utilidades.InpersonatorContext.Impersonator(usuarioImp, dominio, passwordImp))
                {
                    var nombreArchivo = archivo.FileName;
                    File.WriteAllBytes(string.Format(@"{0}\{1}", rutaDirectorio, nombreArchivo), archivoBytes);
                    result = string.Format(@"{0}\{1}", rutaDirectorio, nombreArchivo);
                    archivo = null;
                }
                return result;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Elimina los archivos de la ruta especificada
        /// </summary>
        /// <param name="nid"></param>
        /// <param name="tipoSoporte"></param>
        public static void EliminarArchivos(string directorioSoportes, int codUsuario, string usuarioImp, string passwordImp, string dominio)
        {
            var rutaDirectorio = string.Format(@"{0}\{1}", directorioSoportes, codUsuario);
            using (new SG_SST.Utilidades.InpersonatorContext.Impersonator(usuarioImp, dominio, passwordImp))
            {
                if (Directory.Exists(rutaDirectorio))
                {
                    try
                    {
                        var directorio = new DirectoryInfo(rutaDirectorio);
                        if (directorio.GetFiles().Length > 0)
                            directorio.GetFiles().AsParallel().Where(f => f.Extension.ToUpper().Equals(".PDF")).ForAll((f) => f.Delete());
                        if (directorio.GetFiles().Length == 0)
                            Directory.Delete(rutaDirectorio);
                    }
                    catch { }
                }
            }
        }

    }
}
