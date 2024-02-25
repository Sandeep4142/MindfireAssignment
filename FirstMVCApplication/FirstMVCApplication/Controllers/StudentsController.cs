using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstMVCApplication.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        [Route("Students/Hello")] // Custom route for /Students
        [Route("Students/Hi")]
        public string Hello()
        {
            return "Hello World";
        }

        // GET: Students
        //[Route("Students")] 
        //[Route("Students/Hi")]
        public ActionResult Index()
        {
            return View();
        }

        
    }
}