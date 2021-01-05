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
    [Premission]
    [Authorize]
    public class AssociationsController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 6;

        // GET: admin/AboutUs
        public ActionResult Index(int? page)
        {
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var User = db.Associations.OrderBy(x => x.Id);
            return View(User.ToPagedList(UserPage, DefaultPageSize));
        }

        // GET: admin/Associations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Association association = db.Associations.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            return View(association);
        }

        // GET: admin/Associations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Associations/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,PageList,Content,Clicks,Announcer,DateTime,UpDater")] Association association)
        {
            if (ModelState.IsValid)
            {
                association.Announcer = System.Web.HttpContext.Current.User.Identity.Name;
                association.DateTime = DateTime.Now;
                db.Associations.Add(association);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(association);
        }

        // GET: admin/Associations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Association association = db.Associations.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            return View(association);
        }

        // POST: admin/Associations/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,PageList,Content,Clicks,Announcer,DateTime,UpDater")] Association association)
        {
            if (ModelState.IsValid)
            {
                association.UpDater = System.Web.HttpContext.Current.User.Identity.Name;
                db.Entry(association).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(association);
        }

        // GET: admin/Associations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Association association = db.Associations.Find(id);
            if (association == null)
            {
                return HttpNotFound();
            }
            return View(association);
        }

        // POST: admin/Associations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Association association = db.Associations.Find(id);
            db.Associations.Remove(association);
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
