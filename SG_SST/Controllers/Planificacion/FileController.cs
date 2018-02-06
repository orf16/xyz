using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SG_SST.Controllers.Planificacion
{
    public class FileController : ApiController
    {
        [HttpPost]
        public string Post()
        {
            byte[] buffer;
            var request = HttpContext.Current.Request;
            if (request.Files.Count > 0)
            {
                foreach (string file in request.Files)
                {
                    var postedFile = request.Files[file];
                    int length = postedFile.ContentLength;
                    buffer = new byte[length];
                    postedFile.InputStream.Read(buffer, 0, length);

                    return Convert.ToBase64String(buffer);
                }
            }
            return "";
        }
    }
}
