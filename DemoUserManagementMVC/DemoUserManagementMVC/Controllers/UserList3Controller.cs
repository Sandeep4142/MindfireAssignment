using DemoUserManagement.BussinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoUserManagementMVC.Controllers
{
    public class UserList3Controller : Controller
    {
        // GET: UserList3

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadAllUser(string sortExp, string sortDir, int? page)
        {
            int currentPageIndex = page ?? 1;
            int pageSize = 5;

            string sortExpression = sortExp ?? "UserId";
            string sortDirection = sortDir ?? "ASC";

            ViewBag.PageSize = pageSize;
            ViewBag.SortExpression = sortExpression;
            ViewBag.SortDirection = sortDirection;

            var users = UserDetailsService.Allusers(sortExpression, sortDirection, (currentPageIndex - 1) * pageSize, pageSize);
            int totalUser = UserDetailsService.Lenusers();
            var response = new Dictionary<string, object>
             {
                { "Users", users },
                { "PageSize", pageSize },
                { "TotalUser", totalUser }
             };

            return Json(response, JsonRequestBehavior.AllowGet);
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