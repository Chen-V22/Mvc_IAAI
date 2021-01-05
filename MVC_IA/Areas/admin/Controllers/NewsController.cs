using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using Microsoft.Ajax.Utilities;
using MVC_IA.Areas.admin.Filters;
using MVC_IA.Models;
using MvcPaging;

namespace MVC_IA.Areas.admin.Controllers
{
    //[Authorize]
    [Premission]
    public class NewsController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 3;

        // GET: admin/News
        public ActionResult Index(int? page)
        {
            //ViewData["data"] = Session["Top"] == null ? new SelectList(Enum.GetValues(typeof(MVC_IA.Models.IsTop))): Session["Top"];
            string Title = Session["Title"] == null ? null : Session["Title"].ToString();
            string Content = Session["Content"] == null ? null : Session["Content"].ToString();
            IsTop? isTop = Session["Top"] == null ? null : (IsTop?)Session["Top"];
            DateTime? SdateTime = Session["SDate"] == null ? null : (DateTime?)Session["SDate"];
            DateTime? EdateTime = Session["EDate"] == null ? null : (DateTime?)Session["EDate"];
            Session["NowPage"] = page == 0 ? 1 : page;
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var User = db.News.OrderByDescending(x => x.Date).AsQueryable();

            if (!string.IsNullOrEmpty(Title))
            {
                User = User.Where(x => x.Title.Contains(Title));
            }

            if (!string.IsNullOrEmpty(Content))
            {
                User = User.Where(x => x.Introduction.Contains(Content) || x.Content.Contains(Content));
            }

            if (isTop.HasValue)
            {
                User = User.Where(x => x.Top == isTop);

            }

            if (SdateTime.HasValue && EdateTime.HasValue)
            {
                EdateTime = EdateTime.Value.AddDays(1);
                User = User.Where(x => x.Date >= SdateTime && x.Date <= EdateTime);
            }

            return View(User.ToList().ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string Title, string Content, IsTop? Top, DateTime? SDate, DateTime? EDate)
        {
            Session["Title"] = Title;
            Session["Content"] = Content;
            Session["Top"] = Top;
            Session["SDate"] = SDate;
            Session["EDate"] = EDate;

            return RedirectToAction("Index");
        }

        // GET: admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: admin/News/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/News/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Photo,Title,Introduction,Content,Top,Date")] News news, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null)
                {
                    if (photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }

                    news.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(news.Photo, photo.InputStream, Server.MapPath("~/UpFile/Images"), "s", 167, 115);
                }
                news.Date = DateTime.Now;
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: admin/News/Edit/5
        public ActionResult Edit(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: admin/News/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Photo,Title,Introduction,Content,Top,Date")] News news, HttpPostedFileBase photo, int? page)
        {
            if (ModelState.IsValid)
            {
                int? now = Convert.ToInt32(Request.QueryString["page"]);
                if (now == 0)
                {
                    now = 1;
                }
                if (photo != null)
                {
                    if (photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }

                    news.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(news.Photo, photo.InputStream, Server.MapPath("~/UpFile/Images"), "s", 167, 115);
                }

                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { page = now });
            }
            return View(news);
        }

        // GET: admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
