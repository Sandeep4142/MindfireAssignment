using Microsoft.AspNetCore.Mvc;

namespace StudentAttendance.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
