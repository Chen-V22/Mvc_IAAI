using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_IA.Models;

namespace MVC_IA.Areas.admin.Controllers
{
    public class PremissionsController : Controller
    {
        private Model1 db = new Model1();

        // GET: admin/Premissions
        public ActionResult Index()
        {
            var premissions = db.Premissions.Include(p => p.Premissions);
            return View(premissions.ToList());
        }

        // GET: admin/Premissions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premission premission = db.Premissions.Find(id);
            if (premission == null)
            {
                return HttpNotFound();
            }
            return View(premission);
        }

        // GET: admin/Premissions/Create
        public ActionResult Create()
        {
            ViewBag.pid = new SelectList(db.Premissions, "Id", "Name");
            return View();
        }

        // POST: admin/Premissions/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,pid,PValue,Url")] Premission premission)
        {
            if (ModelState.IsValid)
            {
                db.Premissions.Add(premission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pid = new SelectList(db.Premissions, "Id", "Name", premission.pid);
            return View(premission);
        }

        // GET: admin/Premissions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premission premission = db.Premissions.Find(id);
            if (premission == null)
            {
                return HttpNotFound();
            }
            ViewBag.pid = new SelectList(db.Premissions, "Id", "Name", premission.pid);
            return View(premission);
        }

        // POST: admin/Premissions/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,pid,PValue,Url")] Premission premission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(premission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pid = new SelectList(db.Premissions, "Id", "Name", premission.pid);
            return View(premission);
        }

        // GET: admin/Premissions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premission premission = db.Premissions.Find(id);
            if (premission == null)
            {
                return HttpNotFound();
            }
            return View(premission);
        }

        // POST: admin/Premissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Premission premission = db.Premissions.Find(id);
            db.Premissions.Remove(premission);
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
