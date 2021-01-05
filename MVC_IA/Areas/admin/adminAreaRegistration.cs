using System.Web.Mvc;

namespace MVC_IA.Areas.admin
{
    public class adminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "admin_default",
                "admin/{controller}/{action}/{id}",
                new {Controllers ="Home" ,action = "Home", id = UrlParameter.Optional },
                new[] { "MVC_IA.Areas.Admin.Controllers" }
            );
        }
    }
}