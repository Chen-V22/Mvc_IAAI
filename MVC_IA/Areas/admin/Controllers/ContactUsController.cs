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
    public class ContactUsController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 3;

        // GET: admin/AboutUs
        public ActionResult Index(int? page)
        {
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var User = db.ContactUs.OrderBy(x => x.Id);
            return View(User.ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string Name , GenderType? Gender ,string Title , DateTime? SDate , DateTime? EDate ,int? page)
        {
            Session["UsName"] = Name;
            Session["UsTitle"] = Title;
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var User = db.ContactUs.OrderBy(x => x.Id).AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                User = User.Where(x => x.Name.Contains(Name));
            }

            if (Gender.HasValue)
            {
                User = User.Where(x => x.Gender == Gender);
            }

            if (!string.IsNullOrEmpty(Title))
            {
                User = User.Where(x => x.Title.Contains(Title));
            }

            if (SDate.HasValue&&EDate.HasValue)
            {
                EDate = EDate.Value.AddDays(1);
                User = User.Where(x => x.DateTime >= SDate && x.DateTime <= EDate);
            }
            return View(User.ToPagedList(UserPage, DefaultPageSize));
        }

        // GET: admin/ContactUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUs contactUs = db.ContactUs.Find(id);
            if (contactUs == null)
            {
                return HttpNotFound();
            }
            return View(contactUs);
        }

        // GET: admin/ContactUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/ContactUs/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Gender,Phone,Email,Title,Content,DateTime")] ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                db.ContactUs.Add(contactUs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactUs);
        }

        // GET: admin/ContactUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUs contactUs = db.ContactUs.Find(id);
            if (contactUs == null)
            {
                return HttpNotFound();
            }
            return View(contactUs);
        }

        // POST: admin/ContactUs/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Gender,Phone,Email,Title,Content,DateTime")] ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactUs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactUs);
        }

        // GET: admin/ContactUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUs contactUs = db.ContactUs.Find(id);
            if (contactUs == null)
            {
                return HttpNotFound();
            }
            return View(contactUs);
        }

        // POST: admin/ContactUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactUs contactUs = db.ContactUs.Find(id);
            db.ContactUs.Remove(contactUs);
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
