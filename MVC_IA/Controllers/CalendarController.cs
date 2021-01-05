using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_IA.Areas.admin.Filters;
using MVC_IA.Models;

namespace MVC_IA.Controllers
{
    public class CalendarController : Controller
    {
        private Model1 db = new Model1();
        // GET: Calendar
        public ActionResult Index()
        {
            //Calendar calendar = db.Calendars.FirstOrDefault();
            //if (calendar==null)
            //{
            //    return new HttpNotFoundResult();
            //}
            //ViewBag.calendars = calendar.Content;
            return View();
        }
    }
}