using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MVC_IA.Filters
{
    public class Class1
    {
        string strUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
    }
}