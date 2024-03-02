using DemoUserManagement.BussinessLogicLayer;
using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace DemoUserManagementMVC.Controllers
{
    public class UserDetails2Controller : Controller
    {
        // GET: UserDetails2

        [CustomAuthentication]

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void SaveAllDetails(AllDetailsModel userData)
        {
            UserDetailsServiceMVC.SaveAllDetails(userData);
        }

        public JsonResult GetCountries()
        {
            List<CountryModel> countryList = UserDetailsService.GetCountries();
            return Json(countryList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStates(int countryId)
        {
            List<StateModel> States = UserDetailsService.GetStates();
            List<StateModel> states = States.Where(s => s.CountryId == countryId).ToList();
            return Json(states, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LoadUser(int id)
        {
            var user = UserDetailsService.GetUser(id);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
    }
}