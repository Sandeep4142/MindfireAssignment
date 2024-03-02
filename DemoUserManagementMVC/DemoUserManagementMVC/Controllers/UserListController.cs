using DemoUserManagement.BussinessLogicLayer;
using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoUserManagementMVC.Controllers
{
    public class UserListController : Controller
    {
        // GET: UserList
        [CustomAuthentication]
        public ActionResult Index()
        {
            List<UserDetailsModel> userList = UserDetailsService.GetAllUserDetails();
            ViewData["UserList"] = userList;
            return View();
        }

        [HandleError]
        public void DownloadFile(string fileName)
        {
            UserDetailsServiceMVC.DownloadFile(fileName);
        }

    }
}