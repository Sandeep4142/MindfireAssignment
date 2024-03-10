using DoctorAppointment.Bussiness;
using DoctorAppointment.Model;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DoctorAppointment.Utility;


namespace DoctorAppointment.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            bool isRegistered = DoctorAppointmentService.RegisterDoctor(user);
            if (isRegistered)
            {
                TempData["RegistrationMessage"] = "Registration successful!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["RegistrationMessage"] = "Registration failed!";
                return RedirectToAction("Register");
            }
        }

        [CustomAuthentication]
        public ActionResult AppointmentDetailedReport(int doctorID)
        {
            ViewBag.doctorID = doctorID;
            return View();
        }

        public ActionResult GetAppointmentDetailedReport(int doctorID, DateTime month)
        {
            ViewBag.doctorID = doctorID;
            List<AppointmentModel> appointmentList = DoctorAppointmentService.GetDetailedAppointmentList(doctorID, month);
            return Json(appointmentList, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthentication]
        public ActionResult AppointmentSummaryReport(int doctorID)
        {
            ViewBag.doctorID = doctorID;
            return View();
        }

        public ActionResult GetAppointmentSummaryReport(int doctorID, DateTime month)
        {
            List<SummaryReport> appointmentList = DoctorAppointmentService.GetAppointmentSummaryReport(doctorID, month);
            return Json(appointmentList, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthentication]
        public ActionResult UpcomingAppointments(int doctorID)
        {
            ViewBag.doctorID = doctorID;
            return View();
        }

        public ActionResult GetUpcomingAppointments(int doctorID, DateTime selectedDate)
        {
            ViewBag.doctorID = doctorID;
            List<AppointmentModel> appointmentList = DoctorAppointmentService.GetUpcomingAppointments(doctorID, selectedDate.Date);
            return Json(appointmentList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CloseAppointment(int appointmentID)
        {
            var response = DoctorAppointmentService.CloseAppointment(appointmentID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CancelAppointment(int appointmentID)
        {
            var response = DoctorAppointmentService.CancelAppointment(appointmentID);
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}