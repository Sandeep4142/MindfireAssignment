using DoctorAppointment.Bussiness;
using DoctorAppointment.Model;
using DoctorAppointment.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DoctorAppointment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<DoctorModel> doctorList = DoctorAppointmentService.GetDoctorList();
            return View(doctorList);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            int userID = DoctorAppointmentService.CheckUser(email, password);
            if (userID != -1 && userID != 0)
            {
                DoctorModel doctor = DoctorAppointmentService.GetDoctorDetails(userID);
                SessionData sessionData = new SessionData()
                {
                    DoctorID = doctor.DoctorID,
                    DoctorName = doctor.DoctorName
                };
                Session["Doctor"] = sessionData;
                userID = doctor.DoctorID;
            }
            return Json(userID, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}