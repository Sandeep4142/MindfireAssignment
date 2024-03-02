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
        public ActionResult Index(string sortExp, int? page)
        {

            int currentPageIndex = page ?? 1;
            int pageSize = 5;
            ViewBag.PageSize = pageSize;

            string sortExpression = sortExp ?? "UserId";
            string sortDirection;

            if (sortExp == null)
            {
                sortDirection = "ASC";
            }
            else
            {
                sortDirection = (string)TempData["SortDirection"] == "ASC" ? "DSC" : "ASC";
            }

            TempData["SortDirection"] = sortDirection;

            var users = UserDetailsService.Allusers(sortExpression, sortDirection, (currentPageIndex - 1) * pageSize, pageSize);
            ViewBag.TotalUser = UserDetailsService.Lenusers();
            return View(users);
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