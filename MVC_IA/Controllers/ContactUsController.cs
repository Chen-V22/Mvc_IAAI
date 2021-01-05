using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using  MVC_IA.Models;

namespace MVC_IA.Controllers
{
    public class ContactUsController : Controller
    {
        
        private Model1 db = new Model1();
        // GET: ContactUs
        public ActionResult Index()
        {
            return View();
        }

        // POST: admin/ContactUs/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Id,Name,Gender,Phone,Email,Title,Content")] ContactUs contactUs)
        {
            if (ModelState.IsValid)
            {
                contactUs.DateTime = DateTime.Now;
                db.ContactUs.Add(contactUs);
                db.SaveChanges();
                TempData["message"] = "傳送成功";
                return RedirectToAction("Index");
            }

            return View(contactUs);
        }
    }
}