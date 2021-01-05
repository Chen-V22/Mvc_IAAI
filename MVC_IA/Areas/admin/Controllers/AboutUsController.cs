using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_IA.Areas.admin.Filters;
using MVC_IA.Migrations;
using MVC_IA.Models;
using MvcPaging;

namespace MVC_IA.Areas.admin.Controllers
{
    [Premission]
    [Authorize]
    public class AboutUsController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 15;

        // GET: admin/AboutUs
        public ActionResult Index(int?page)
        {
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var User = db.AboutUs.OrderBy(x => x.Id);
            Session["tableUs"] = page == null ? 1 : page;
            return View(User.ToPagedList(UserPage, DefaultPageSize));
        }

        // GET: admin/AboutUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUs aboutUs = db.AboutUs.Find(id);
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            return View(aboutUs);
        }

        // GET: admin/AboutUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/AboutUs/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,PageList,Content,Clicks,Announcer,DateTime,UpDater")] AboutUs aboutUs)
        {
            if (ModelState.IsValid)
            {
                aboutUs.Announcer = System.Web.HttpContext.Current.User.Identity.Name;
                aboutUs.DateTime=DateTime.Now;
                db.AboutUs.Add(aboutUs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aboutUs);
        }

        // GET: admin/AboutUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUs aboutUs = db.AboutUs.Find(id);
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            return View(aboutUs);
        }

        // POST: admin/AboutUs/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,PageList,Content,Clicks,Announcer,DateTime,UpDater")] AboutUs aboutUs)
        {
            if (ModelState.IsValid)
            {
                int nowpage = Convert.ToInt32(Session["tableUs"]);
                aboutUs.UpDater = System.Web.HttpContext.Current.User.Identity.Name;
                db.Entry(aboutUs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new{page= "nowpage" });
            }
            return View(aboutUs);
        }

        // GET: admin/AboutUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutUs aboutUs = db.AboutUs.Find(id);
            if (aboutUs == null)
            {
                return HttpNotFound();
            }
            return View(aboutUs);
        }

        // POST: admin/AboutUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AboutUs aboutUs = db.AboutUs.Find(id);
            db.AboutUs.Remove(aboutUs);
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
