using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_IA.Areas.admin.Filters;
using MVC_IA.Models;
using Newtonsoft.Json;

namespace MVC_IA.Areas.admin.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 3;

        // GET: admin/Users/Home
        [Premission]
        [Authorize]
        public ActionResult Home()
        {
            return View();
        }

        //POST：admin/Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "id,Email,Password")] Login login)
        {
            if (ModelState.IsValid)
            {
                var member = db.Users.FirstOrDefault(x => x.Email == login.Email);
                if (member == null)
                {
                    TempData["message"] = "登入失敗";
                    return View(login);
                }

                string pass = Utility.GenerateHashWithSalt(login.Password, member.PasswordSalt);
                if (member.Password != pass)
                {
                    TempData["message"] = "登入失敗";
                    return View(login);
                }
                string userData = JsonConvert.SerializeObject(member);
                Utility.SetAuthenTicket(userData, member.Name);
                Session["Id"] = member.Id;
                Session["PhotoP"] = member.Photo;
                return RedirectToAction("Home");
            }

            return View(login);
        }

        //GET：admin/Users/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return Redirect(FormsAuthentication.LoginUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToActionPermanent("index", "Home", new { Area = "" });
        }

    }
}