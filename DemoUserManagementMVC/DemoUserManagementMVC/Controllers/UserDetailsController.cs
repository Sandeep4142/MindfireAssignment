using DemoUserManagement.BussinessLogicLayer;
using DemoUserManagement.Model;
using DemoUserManagement.Utils;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace DemoUserManagementMVC.Controllers
{
    public class UserDetailsController : Controller
    {
        // GET: UserDetails
        [CustomAuthentication]
        public ActionResult Index(int id = 0)
        {
            if (id == 0)
            {
                return InitializeViewForNewUser();
            }
            return InitializeViewForExistingUser(id);
        }

        private ActionResult InitializeViewForNewUser()
        {
            LoadStateAndCountries();
            return View(new UserDetailsModel());
        }
         
        private ActionResult InitializeViewForExistingUser(int id)
        {
            LoadStateAndCountries();
            var model = UserDetailsService.GetUserDetails(id);
            var notes = UserDetailsService.GetNotes(id, 1);
            var documents = UserDetailsService.GetDocuments(id, 1);

            ViewData["Notes"] = notes;
            ViewData["Documents"] = documents;
            ViewBag.ObjectId = id;
            ViewBag.ObjectType = 1;

            return View(model);
        }
    
        private void LoadStateAndCountries()
        {
            var countryList = UserDetailsService.GetCountries();
            var stateList = UserDetailsService.GetStates();

            ViewBag.AddressCountryList = new SelectList(countryList, "CountryId", "CountryName");
            ViewBag.AddressStateList = new SelectList(stateList, "StateId", "StateName");
        }

        [HttpPost]
        public ActionResult SaveUser(UserDetailsModel user, HttpPostedFileBase profilePic, HttpPostedFileBase aadharCard)
        {
            UserDetailsServiceMVC.SaveAllDetails2(user, profilePic, aadharCard);
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult GetCountries()
        {
            List<CountryModel> countryList = UserDetailsService.GetCountries();
            return Json(countryList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetStates(int countryId)
        {
            List<StateModel> States = UserDetailsService.GetStates();
            List<StateModel> states = States.Where(s => s.CountryId == countryId).ToList();
            return Json(states, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveNote(string objectId, string objectType, string noteText)
        {
            UserDetailsServiceMVC.SaveNote(objectId, objectType, noteText); 
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult SaveDocument(string objectId, string objectType, HttpPostedFileBase file, string fileType)
        {
           UserDetailsServiceMVC.SaveDocument(objectId, objectType, file, fileType);
            return Json(new { success = true });
        }

    }
}