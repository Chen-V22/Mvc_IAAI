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
    public class HomePicturesController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 3;

        // GET: admin/AboutUs
        public ActionResult Index(int? page)
        {
            IsTop? top = Session["HomePictureTop"] == null ? null : (IsTop?) Session["HomePictureTop"];
            DateTime? SDate = Session["HomePictureSDate"] == null ? null : (DateTime?)Session["HomePictureSDate"];
            DateTime? EDate = Session["HomePictureEDate"] == null ? null : (DateTime?)Session["HomePictureEDate"];
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var User = db.HomePictures.OrderBy(x => x.Id).AsQueryable();
            if (top.HasValue)
            {
                User = User.Where(x => x.IsTop == top);
            }

            if (SDate.HasValue&&EDate.HasValue)
            {
                User = User.Where(x => x.DateTime >= SDate && x.DateTime <= EDate);
            }
            return View(User.ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(IsTop? top, DateTime? SDate, DateTime? EDate)
        {
            Session["HomePictureTop"] = top;
            Session["HomePictureSDate"] = SDate;
            Session["HomePictureEDate"] = EDate;
            return RedirectToAction("Index");
        }
        // GET: admin/HomePictures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomePicture homePicture = db.HomePictures.Find(id);
            if (homePicture == null)
            {
                return HttpNotFound();
            }
            return View(homePicture);
        }

        // GET: admin/HomePictures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/HomePictures/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Picture,IsTop,Sort,DateTime")] HomePicture homePicture,HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                 if (Picture != null)
                 {
                     if (Picture.ContentType.IndexOf("image",System.StringComparison.Ordinal)==-1)
                     {
                         ViewBag.message = "檔案類型錯誤";
                         return View();
                     }
                     homePicture.Picture = Utility.SaveUpImage(Picture);
                     Utility.GenerateThumbnailImage(homePicture.Picture, Picture.InputStream,Server.MapPath("~/UpFile/Images"),"s",167,115);
                 }
                 homePicture.DateTime = DateTime.Now;
                db.HomePictures.Add(homePicture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(homePicture);
        }

        // GET: admin/HomePictures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomePicture homePicture = db.HomePictures.Find(id);
            if (homePicture == null)
            {
                return HttpNotFound();
            }
            return View(homePicture);
        }

        // POST: admin/HomePictures/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Picture,IsTop,Sort,DateTime")] HomePicture homePicture, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    if (Picture.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }
                    homePicture.Picture = Utility.SaveUpImage(Picture);
                    Utility.GenerateThumbnailImage(homePicture.Picture, Picture.InputStream, Server.MapPath("~/UpFile/Images"), "s", 167, 115);
                }
                homePicture.DateTime = DateTime.Now;
                
                db.Entry(homePicture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(homePicture);
        }

        // GET: admin/HomePictures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomePicture homePicture = db.HomePictures.Find(id);
            if (homePicture == null)
            {
                return HttpNotFound();
            }
            return View(homePicture);
        }

        // POST: admin/HomePictures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HomePicture homePicture = db.HomePictures.Find(id);
            db.HomePictures.Remove(homePicture);
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
