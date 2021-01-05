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
    public class NewsController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 3;
        // GET: News
        public ActionResult Index(int?page)
        {
            Session["News"] = page == null ? 1 : page;
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            //var User = db.News.OrderBy(x => x.Id);
            return View(db.News.OrderByDescending(p => p.Date).ToPagedList(UserPage, DefaultPageSize));
        }

        public ActionResult Show(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news==null)
            {
                return HttpNotFound();
            }
            Session["NewsShow"] = id;

            ViewBag.Title = news.Title;
            ViewBag.Date = news.Date;
            ViewBag.Introduction = news.Introduction;
            ViewBag.Content = news.Content;
            return View();
        }


    }
}