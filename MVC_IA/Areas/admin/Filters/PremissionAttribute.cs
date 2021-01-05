using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVC_IA.Areas.admin.Controllers;
using MVC_IA.Models;
using Newtonsoft.Json;

namespace MVC_IA.Areas.admin.Filters
{
    public class PremissionAttribute : ActionFilterAttribute//動作過濾器
    {
        public string Module { get; set; }
        private Model1 db = new Model1();

        public override void OnActionExecuting(ActionExecutingContext filter)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filter.Controller.ViewBag.menu = "";
                filter.Result = new RedirectResult("~/Admin/Home/Login");
                return;
            }
            var permissions = db.Premissions.ToList();
            StringBuilder sb = new StringBuilder();
            sb.Append(GetPremission(permissions.Where(x => x.pid == null).ToList()));
            filter.Controller.ViewBag.side = sb.ToString();
        }

        private string GetPremission(ICollection<Premission> list)
        {
            StringBuilder sb = new StringBuilder();
            string strUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            User user = JsonConvert.DeserializeObject<User>(strUserData);
            foreach (Premission premission in list)
            {
                if (user.Authority.IndexOf(premission.PValue)>-1)
                {
                    if (premission.pid == null)
                    {
                        if (premission.premissionSon.Count() > 0)
                        {
                            sb.Append(@"<li class='sub-menu'>");
                            sb.Append($"<a href='javascript:; ' class=''>");
                            sb.Append($"<i class='icon_documents_alt'></i>");
                            sb.Append($"<span>{premission.Name}</span>");
                            sb.Append($"<span class='menu-arrow arrow_carrot-right'></span>");
                            sb.Append("</a>");
                            sb.Append($"<ul class='sub'>");
                            sb.Append(GetPremission(premission.premissionSon));
                            sb.Append("</ul>");
                            sb.Append("</li>");
                        }
                        else
                        {
                            sb.Append("<li>");
                            sb.Append($"<a class=''href='{premission.Url}'>");
                            sb.Append($"<i class='icon_documents_alt'></i>");
                            sb.Append($"<span>{premission.Name}</span>");
                            sb.Append("</a>");
                            sb.Append("</li>");
                        }
                    }
                    else
                    {
                        sb.Append($"<li><a href='{premission.Url}'>{premission.Name}</a></li>");
                    }
                }
            }
            return sb.ToString();
        }
    }
}