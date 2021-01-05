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
    public class MembersController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 3;

        // GET: admin/AboutUs
        public ActionResult Index(int? page)
        {
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var User = db.Members.OrderBy(x => x.Id);
            return View(User.ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult Index(string Name, GenderType? Gender, MemberKind? MemberKind, DateTime? SDate,
            DateTime? EDate, int? page)
        {
            Session["MemberName"] = Name;
            Session["MemberGender"] = Gender;
            Session["MemderSDate"] = SDate;
            Session["MemderEDate"] = EDate;
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var User = db.Members.OrderBy(x => x.Id).AsQueryable();
            if (!string.IsNullOrEmpty(Name))
            {
                User = User.Where(x => x.Name.Contains(Name));
            }

            if (Gender.HasValue)
            {
                User = User.Where(x => x.Gender == Gender);
            }

            if (MemberKind.HasValue)
            {
                User = User.Where(x => x.MemberKind == MemberKind);
            }

            if (SDate.HasValue && EDate.HasValue)
            {
                EDate = EDate.Value.AddDays(1);
                User = User.Where(x => x.DateTime >= SDate && x.DateTime <= EDate);
            }
            return View(User.ToPagedList(UserPage, DefaultPageSize));
        }

        // GET: admin/Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: admin/Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Members/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Account,Password,Gender,Birthday,MemberKind,Address,Email,GlobalMember,WorkDepartment,WorkPosition,Education,ServiceDepartment,ServicePosition,DateStart,DateEnd,SecServiceDepartment,SecServicePosition,SecDateStart,SecDateEnd,DateTotal")] Member member)
        {
            if (ModelState.IsValid)
            {
                member.DateTime = DateTime.Now;
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: admin/Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: admin/Members/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Account,Password,Gender,Birthday,MemberKind,Address,Email,GlobalMember,WorkDepartment,WorkPosition,Education,ServiceDepartment,ServicePosition,DateStart,DateEnd,SecServiceDepartment,SecServicePosition,SecDateStart,SecDateEnd,DateTotal,DateTime")] Member member, string DateTimeNew)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(DateTimeNew))
                {
                    member.Birthday = Convert.ToDateTime(DateTimeNew);
                }
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: admin/Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: admin/Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
