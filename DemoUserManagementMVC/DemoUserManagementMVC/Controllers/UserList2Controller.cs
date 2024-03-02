using DemoUserManagement.BussinessLogicLayer;
using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace DemoUserManagementMVC.Controllers
{
    public class UserList2Controller : Controller
    {
        // GET: UserList2
        [CustomAuthentication]
        public ActionResult Index(string sortExp,string sortDir, int? page)
        {
            int pageSize = 5;
            int currpage = page??1;
            var users = UserDetailsServiceMVC.GetUserList(sortExp, sortDir, currpage, pageSize);
            ViewBag.TotalUser = UserDetailsService.Lenusers();
            ViewBag.PageSize = pageSize;
            return View(users);
        }

        [HandleError]
        public void DownloadFile(string fileName)
        {
             UserDetailsServiceMVC.DownloadFile(fileName);
        }

    }
}