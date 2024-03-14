using DoctorAppointment.Bussiness;
using DoctorAppointment.Model;
using DoctorAppointment.Utility;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DoctorAppointment.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BookAppointment(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.DoctorID = id;
            var doctorName = DoctorAppointmentService.GetDoctorDetails(id).DoctorName;
            if(doctorName == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.DoctorName = doctorName;
            return View();
        }

        public JsonResult GetAvailableTimeSlots(string selectedDate, int doctorID)
        {
            DateTime date = DateTime.Parse(selectedDate);
            TimeSlotAvailability timeSlots = DoctorAppointmentService.GetAvailableTimeSlots(date, doctorID);
            TimeSpan doctorSlotTime = DoctorAppointmentService.GetDoctorDetails(doctorID).AppointmentSlotTime;

            var slotsData = new Dictionary<string, object>
            {
                { "availableTimeSlots", timeSlots.AvailableTimeSlots },
                { "existingAppointments", timeSlots.ExistingAppointments },
                { "doctorSlotTime", doctorSlotTime }
             };
            return Json(slotsData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BookAppointment(AppointmentModel appointmentData)
        {
            bool response = DoctorAppointmentService.BookAppointment(appointmentData);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

    }
}