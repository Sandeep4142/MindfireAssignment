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
        public ActionResult Index()
        {
            return View();
        }

        public void SaveAllDetails(AllDetailsModel userData)
        {
            try
            {
                UserDetailsModel userDetails = userData.user;
                userDetails.Addresses = userData.addresses;
                userDetails.EducationDetails = userData.educations;
                UserDetailsService.SaveUserDetails(userDetails);            
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
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