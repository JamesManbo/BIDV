using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BIDV.Repository;

namespace BIDV.Controllers
{
    public class NewsImageController : Controller
    {
        //
        // GET: /NewsImage/
        readonly NewsImageRepository _newsImageRepository = new NewsImageRepository();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetByNewsId(int newsId)
        {
            _newsImageRepository.GetAll().Where(a => a.news_id.Equals(newsId)).ToString();
            return View();
        }
    }
}
