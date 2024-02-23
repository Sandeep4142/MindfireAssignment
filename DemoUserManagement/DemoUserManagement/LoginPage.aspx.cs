using DemoUserManagement.BussinessLogicLayer;
using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoUserManagement
{
    public partial class LoginPage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static int CheckUser(string email, string password)
        {
            int userId = UserDetailsService.CheckUser(email, password);
            if (userId != -1 && userId!=0)
            {
                SessionData session = new SessionData();
                session.UserId = userId;
                session.IsAdmin = UserDetailsService.CheckIfUserIsAdmin(userId);
                session.Email = email;
                HttpContext.Current.Session["User"] = session;
            }
            return userId;
        }

        [WebMethod]
        public static bool CheckIfUserIsAdmin(int userId)
        {
            return UserDetailsService.CheckIfUserIsAdmin(userId);
        }

        [WebMethod]
        public static string LogOut()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            return "Done";
        }
    }
}