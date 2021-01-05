using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_IA.Areas.admin.Filters;
using MVC_IA.Models;
using MvcPaging;

namespace MVC_IA.Areas.admin.Controllers
{
    [Authorize]
    [Premission]
    public class ExternalLinksController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 3;

        // GET: admin/AboutUs
        public ActionResult Index(int? page)
        {
            string Name =  Session["ExternalLinkName"]==null?null: Session["ExternalLinkName"].ToString();
            IsTop? top = Session["ExternalLinkTop"] ==null?null:(IsTop?)Session["ExternalLinkTop"];
            DateTime? SDate = Session["ExternalLinkSDate"] == null ? null : (DateTime?)Session["ExternalLinkSDate"];
            DateTime? EDate= Session["ExternalLinkEDate"] == null ? null : (DateTime?)Session["ExternalLinkEDate"];
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var User = db.ExternalLinks.OrderBy(x => x.Id).AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                User = User.Where(x => x.Name.Contains(Name));
            }

            if (top.HasValue)
            {
                User = User.Where(x => x.IsTop == top);
            }

            if (SDate.HasValue&&EDate.HasValue)
            {
                EDate = EDate.Value.AddDays(1);
                User = User.Where(x => x.DateTime >= SDate && x.DateTime <= EDate);
            }
            return View(User.ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string Name,IsTop? Top ,DateTime? SDate , DateTime? EDate)
        {
            Session["ExternalLinkName"] = Name;
            Session["ExternalLinkTop"] = Top;
            Session["ExternalLinkSDate"] = SDate;
            Session["ExternalLinkEDate"] = EDate;
            return RedirectToAction("Index");
        }

        // GET: admin/ExternalLinks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExternalLink externalLink = db.ExternalLinks.Find(id);
            if (externalLink == null)
            {
                return HttpNotFound();
            }
            return View(externalLink);
        }

        // GET: admin/ExternalLinks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/ExternalLinks/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Picture,Name,Url,IsTop,Sort,DateTime")] ExternalLink externalLink,HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.ContentType.IndexOf("image", System.StringComparison.Ordinal)==-1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }
                    externalLink.Picture = Utility.SaveUpImage(Picture);
                    Utility.GenerateThumbnailImage(externalLink.Picture, Picture.InputStream, Server.MapPath("~/UpFile/Images"), "s", 167, 115);
                }
                externalLink.DateTime = DateTime.Now;
                db.ExternalLinks.Add(externalLink);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(externalLink);
        }

        // GET: admin/ExternalLinks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExternalLink externalLink = db.ExternalLinks.Find(id);
            if (externalLink == null)
            {
                return HttpNotFound();
            }
            return View(externalLink);
        }

        // POST: admin/ExternalLinks/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Picture,Name,Url,IsTop,Sort,DateTime")] ExternalLink externalLink,HttpPostedFileBase photo)
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
                    externalLink.Picture = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(externalLink.Picture, photo.InputStream, Server.MapPath("~/UpFile/Images"), "s", 167, 115);
                }
                
                db.Entry(externalLink).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(externalLink);
        }

        // GET: admin/ExternalLinks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExternalLink externalLink = db.ExternalLinks.Find(id);
            if (externalLink == null)
            {
                return HttpNotFound();
            }
            return View(externalLink);
        }

        // POST: admin/ExternalLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExternalLink externalLink = db.ExternalLinks.Find(id);
            db.ExternalLinks.Remove(externalLink);
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
