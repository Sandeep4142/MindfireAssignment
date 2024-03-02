using DemoUserManagement.BussinessLogicLayer;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DemoUserManagementMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string password, string ReturnUrl)
        {
            if (IsValid(email, password) == true)
            {
                FormsAuthentication.SetAuthCookie(email, false);
                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    if (((SessionData)Session["User"]).IsAdmin)
                    {
                        return RedirectToAction("Index", "UserList2");
                    }
                    else
                    {
                        int id = ((SessionData)Session["User"]).UserId;
                        return RedirectToAction("Index", "UserDetails", new { id });
                    }
                }
            }
            else
            {
                return View();
            }
        }

        public bool IsValid(string email, string password)
        {
            int userId = UserDetailsService.CheckUser(email, password);
            if (userId != -1 && userId != 0)
            {
                SessionData session = new SessionData();
                session.UserId = userId;
                session.IsAdmin = UserDetailsService.CheckIfUserIsAdmin(userId);
                session.Email = email;
                return true;
            }
            else
            {
                return false;
            }     
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}