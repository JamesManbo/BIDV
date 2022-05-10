using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace BIDV.Common
{
    public class HelperFile
    {
       public static void Download(string url)
       
        {   
            var filePath = HttpContext.Current.Server.MapPath(url);
            var fileName = Path.GetFileName(filePath);
            var fileInfo = new System.IO.FileInfo(filePath);
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ";");
            response.TransmitFile(filePath);
            response.Flush();
            response.End();  
        }
    }
}
