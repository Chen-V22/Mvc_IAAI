using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_IA.Models;

namespace MVC_IA.Controllers
{
    public class AboutController : Controller
    {
        private  Model1 db = new Model1();
        // GET: About
        [ValidateInput(false)]
        public ActionResult Index(int? id,AboutUs about)
        {

            id = id == null ? 1 : id;
            AboutUs aboutUs = db.AboutUs.Find(id);
            if (aboutUs == null)
            {
                return HttpNotFound();
            }

            
            Session["About"] = id;
            ViewBag.Page = aboutUs.PageList;
            ViewBag.Content = aboutUs.Content;
            return View(db.AboutUs.ToList());
        }
    }
}