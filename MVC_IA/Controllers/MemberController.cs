using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_IA.Models;

namespace MVC_IA.Controllers
{
    public class MemberController : Controller
    {

        private Model1 db = new Model1();

        public ActionResult Index()
        {
            return View();
        }

        // GET: Member

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "id,Account,Password")] Member login)
        {

            var member = db.Members.FirstOrDefault(x => x.Account == login.Account);
            if (member == null)
            {
                ViewBag.Messge = "登入失敗";
                ViewBag.email = true;
                return View(login);
            }

            string pass = Utility.GenerateHashWithSalt(login.Password, member.PasswordSalt);
            if (member.Password != pass)
            {
                ViewBag.Messge = "登入失敗";
                ViewBag.password = true;
                return View(login);
            }

            //Utility.SetAuthenTicket(member.Name, member.Email);
            return RedirectToAction("Forum");


            return View(login);
        }

        [AllowAnonymous]
        public ActionResult Forum()
        {
            return View();
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
        public ActionResult Create([Bind(Include = "Id,Name,Account,Password,Gender,Birthday,MemberKind,Address,Email,GlobalMember,WorkDepartment,WorkPosition,Education,ServiceDepartment,ServicePosition,DateStart,DateEnd,SecServiceDepartment,SecServicePosition,SecDateStart,SecDateEnd,DateTotal,PasswordSalt,DateTime")] Member member)
        {
            if (ModelState.IsValid)
            {
                member.DateTime = DateTime.Now;
                member.PasswordSalt = Utility.CreateSalt();
                member.Password = Utility.GenerateHashWithSalt(member.Password, member.PasswordSalt);
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }
    }
}