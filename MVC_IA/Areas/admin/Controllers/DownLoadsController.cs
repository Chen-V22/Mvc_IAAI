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
    public class DownLoadsController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 3;

        // GET: admin/AboutUs
        public ActionResult Index(int? page)
        {
            string title = Session["DL_Title"] == null ? null : Session["DL_Title"].ToString();
            IsTop? top = Session["DL_Top"] == null ? null : (IsTop?) Session["DL_Top"];
            DateTime? SDate = Session["DL_SDate"] == null ? null : (DateTime?) Session["DL_SDate"];
            DateTime? EDate = Session["DL_EDate"] == null ? null : (DateTime?) Session["DL_EDate"];
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var User = db.DownLoads.OrderBy(x => x.DateTime).AsQueryable();
            if (!string.IsNullOrEmpty(title))
            {
                User = User.Where(x => x.Title.Contains(title));
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
        public ActionResult Index(string Title , IsTop? Top, DateTime? SDate , DateTime? EDate)
        {
            Session["DL_Title"] = Title;
            Session["DL_Top"] = Top;
            Session["DL_SDate"] = SDate;
            Session["DL_EDate"] = EDate;
            return RedirectToAction("Index");
        }

        // GET: admin/DownLoads/Details/5
        public ActionResult Details(int? id)
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
            return View(downLoad);
        }

        // GET: admin/DownLoads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/DownLoads/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Photo,Title,Content,IsTop,Clicks,DateTime")] DownLoad downLoad,HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (Photo.ContentType.IndexOf("image", System.StringComparison.Ordinal)==-1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }
                    downLoad.Photo = Utility.SaveUpImage(Photo);
                    Utility.GenerateThumbnailImage(downLoad.Photo, Photo.InputStream, Server.MapPath("~/UpFile/images"), "s", 167, 115);
                }
                downLoad.DateTime=DateTime.Now;
                db.DownLoads.Add(downLoad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(downLoad);
        }

        // GET: admin/DownLoads/Edit/5
        public ActionResult Edit(int? id)
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
            return View(downLoad);
        }

        // POST: admin/DownLoads/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Photo,Title,Content,IsTop,Clicks,DateTime")] DownLoad downLoad,HttpPostedFileBase Photo)
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (Photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }

                    downLoad.Photo = Utility.SaveUpImage(Photo);
                    Utility.GenerateThumbnailImage(downLoad.Photo, Photo.InputStream, Server.MapPath("~/UpFile/Images"), "s", 167, 115);
                }
                db.Entry(downLoad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(downLoad);
        }

        // GET: admin/DownLoads/Delete/5
        public ActionResult Delete(int? id)
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
            return View(downLoad);
        }

        // POST: admin/DownLoads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DownLoad downLoad = db.DownLoads.Find(id);
            db.DownLoads.Remove(downLoad);
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
