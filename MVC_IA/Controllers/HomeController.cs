using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_IA.Models;

namespace MVC_IA.Controllers
{

    public class HomeController : Controller
    {
        private Model1 db = new Model1();
        public ActionResult Index()
        {
            ViewBag.News = db.News.OrderBy(o => o.Top).ThenBy(o => o.Date).Take(4).ToList();
            ViewBag.Home = db.HomePictures.OrderBy(o => o.IsTop).ThenBy(o => o.DateTime).ToList();
            ViewBag.Link = db.ExternalLinks.OrderBy(o => o.IsTop).ThenBy(o => o.DateTime).ToList();
            return View(db.News.ToList());
        }
    }
}