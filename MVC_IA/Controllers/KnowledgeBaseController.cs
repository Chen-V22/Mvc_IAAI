using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_IA.Models;
using MvcPaging;

namespace MVC_IA.Controllers
{
    public class KnowledgeBaseController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 3;
        // GET: News
        public ActionResult Index(int? page)
        {
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            //var User = db.News.OrderBy(x => x.Id);
            return View(db.DownLoads.OrderByDescending(p => p.DateTime).ToPagedList(UserPage, DefaultPageSize));
        }

        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DownLoad downLoad = db.DownLoads.Find(id);
            if (downLoad == null)
            {
                return HttpNotFound();
            }

            ViewBag.Title = downLoad.Title;
            ViewBag.Date = downLoad.DateTime;
            //ViewBag.Introduction = downLoad.;
            ViewBag.Content = downLoad.Content;
            return View();
        }
    }
}