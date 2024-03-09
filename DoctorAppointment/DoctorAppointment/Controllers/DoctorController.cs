using DoctorAppointment.Bussiness;
using DoctorAppointment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            DoctorAppointmentService.RegisterDoctor(user);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AppointmentDetailedReport(int doctorID)
        {
            ViewBag.doctorID = doctorID;
            return View();
        }

        public ActionResult GetAppointmentDetailedReport(int doctorID, string month, string year)
        {
            ViewBag.doctorID = doctorID;
            List<AppointmentModel> appointmentList = DoctorAppointmentService.GetAppointmentList(doctorID, int.Parse(month), int.Parse(year));
            return Json(appointmentList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AppointmentSummaryReport(int doctorID)
        {
            ViewBag.doctorID = doctorID;
            return View();
        }

        public ActionResult GetAppointmentSummaryReport(int doctorID, string month, string year)
        {
            List<SummaryReport> appointmentList = DoctorAppointmentService.GetAppointmentSummaryReport(doctorID, int.Parse(month), int.Parse(year));
            return Json(appointmentList, JsonRequestBehavior.AllowGet);
        }

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
    }
}