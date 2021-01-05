using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using MVC_IA.Areas.admin.Filters;
using MVC_IA.Migrations;
using MVC_IA.Models;
using Newtonsoft.Json;
using Login = MVC_IA.Models.Login;
using MvcPaging;
using Premission = MVC_IA.Models.Premission;

namespace MVC_IA.Areas.admin.Controllers
{
    [Premission] /*登入確認*/
    public class UsersController : Controller
    {
        private Model1 db = new Model1();
        private const int DefaultPageSize = 3;

        // GET: admin/Users
        [HttpGet]
        public ActionResult Index(int? page)
        {
            page = page == 0 ? 1 : page;
            string pageName = Session["Name"] == null ? null : Session["Name"].ToString();
            DateTime? pageStrDate = Session["StrDate"] == null ? null : (DateTime?)Session["StrDate"];
            DateTime? pageEndDate = Session["EndDate"] == null ? null : (DateTime?)Session["EndDate"];
            Session["table"] = page == null ? 1 : page;
            int UserPage = page.HasValue ? page.Value - 1 : 0;
            var User = db.Users.OrderBy(x => x.Id).AsQueryable();//AsQueryable 以sql語法串接條件字串組成搜尋語法

            if (!string.IsNullOrEmpty(pageName))
            {
                User = User.Where(x => x.Name.Contains(pageName) || x.Email.Contains(pageName));
            }

            if (pageStrDate.HasValue && pageEndDate.HasValue)
            {
                pageEndDate = pageEndDate.Value.AddDays(1);
                User = User.Where(x => x.Date >= pageStrDate && x.Date <= pageEndDate);
            }
            return View(User.ToPagedList(UserPage, DefaultPageSize));
        }

        [HttpPost]
        public ActionResult index(string Name, DateTime? SDate, DateTime? EDate)
        {
            Session["Name"] = Name;
            Session["StrDate"] = SDate;
            Session["EndDate"] = EDate;
            return RedirectToAction("Index");
        }

        // GET: admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: admin/Users/Create
        public ActionResult Create()
        {
            var premission = db.Premissions.ToList();
            StringBuilder sb = new StringBuilder("[");
            getpreission(premission.Where(x => x.pid == null).ToList(), sb);
            sb.Append("]");
            ViewBag.data = sb.ToString();
            return View();
        }

        private void getpreission(ICollection<Premission> list, StringBuilder sb)
        {
            foreach (Premission permission in list)
            {
                sb.Append("{\"id\": \"" + permission.PValue + "\", \"text\": \"" + permission.Name + "\""); //if only got first layer then only use the ones at top and bottom
                if (permission.premissionSon.Count() > 0) //if has another children layer
                {
                    sb.Append(",\"children\":["); //get data is got another children
                    getpreission(permission.premissionSon, sb); //run the function again in loop to get more data
                    sb.Append("]");
                }
                sb.Append("},"); //at the end of the array there's ',', use trim.end to get rid of the ,
            }
            string temp = sb.ToString();
            sb = new StringBuilder(temp);
        }

        // POST: admin/Users/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,Name,photo,Email,Password,Authority,PasswordSalt,Date")] User user, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null)
                {
                    if (photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }
                    user.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(user.Photo, photo.InputStream, Server.MapPath("~/UpFile/Images"), "s", 35, 35);
                }

                user.PasswordSalt = Utility.CreateSalt();
                user.Password = Utility.GenerateHashWithSalt(user.Password, user.PasswordSalt);
                user.Date = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var premission = db.Premissions.ToList();
            StringBuilder sb = new StringBuilder("[");
            getpreission(premission.Where(x => x.pid == null).ToList(), sb);
            sb.Append("]");
            ViewBag.data = sb.ToString();
            return View(user);
        }

        // POST: admin/Users/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,photo,Email,Password,Authority,PasswordSalt,Date")] User user, string passwordNew, HttpPostedFileBase photo)
        {
            int? NowPage = Convert.ToInt32(Session["table"]);
            ModelState.Remove("Password");
            if (ModelState.IsValid)
            {
                if (photo != null)
                {
                    if (photo.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.message = "檔案類型錯誤";
                        return View();
                    }

                    user.Photo = Utility.SaveUpImage(photo);
                    Utility.GenerateThumbnailImage(user.Photo, photo.InputStream, Server.MapPath("~/UpFile/Images"), "s", 35, 35);
                }

                if (!string.IsNullOrEmpty(passwordNew))
                {
                    user.PasswordSalt = Utility.CreateSalt();
                    user.Password = Utility.GenerateHashWithSalt(passwordNew, user.PasswordSalt);
                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { page = NowPage });
            }
            return View(user);
        }

        // GET: admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int? Page = Convert.ToInt32(Session["table"]);
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index", new { page = Page });
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
