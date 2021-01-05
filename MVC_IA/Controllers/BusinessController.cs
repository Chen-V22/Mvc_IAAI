using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_IA.Models;

namespace MVC_IA.Controllers
{
    public class BusinessController : Controller
    {
        Model1 db = new Model1();
        // GET: Business
        public ActionResult Index(int? id)
        {
            id = id == null ? 1 : id;
            Association association = db.Associations.Find(id);
            if (association ==null)
            {
                return HttpNotFound();
            }
            Session["association"] = id;
            ViewBag.Page = association.PageList;
            ViewBag.Content = association.Content;
            return View(db.Associations.ToList());
        }
    }
}