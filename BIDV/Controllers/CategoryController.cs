using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Repository;

namespace BIDV.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/
        
        public ActionResult Index()
        {
            return View();
        }

    }
}
