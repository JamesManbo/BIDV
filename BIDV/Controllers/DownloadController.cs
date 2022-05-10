using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Common;

namespace BIDV.Controllers
{
    public class DownloadController : Controller
    {
        //
        // GET: /Download/

        public void Index(string url)
        {
            HelperFile.Download(url);
        }

    }
}
