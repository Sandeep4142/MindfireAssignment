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
        public ActionResult DownloadFile(string fileName)
        {
            //throw new Exception();
            string filePath = ConfigurationManager.AppSettings["documents"] + fileName;
            if (System.IO.File.Exists(filePath))
            {
                string contentType = MimeMapping.GetMimeMapping(fileName);
                return File(filePath, contentType);
            }
            else
            {
                return HttpNotFound();
            }
        }

    }
}