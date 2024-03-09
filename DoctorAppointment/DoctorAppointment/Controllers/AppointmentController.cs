using DoctorAppointment.Bussiness;
using DoctorAppointment.Model;
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
            ViewBag.DoctorID = id;
            return View();
        }

        public JsonResult GetAvailableTimeSlots(string selectedDate, int doctorID)
        {
            DateTime date = DateTime.Parse(selectedDate);
            List<TimeSpan> availableTimeSlots = DoctorAppointmentService.GetAvailableTimeSlots(date, doctorID);
            TimeSpan doctorSlotTime = DoctorAppointmentService.GetDoctorDetails(doctorID).AppointmentSlotTime;

            var slotsData = new Dictionary<string, object>
            {
                { "availableTimeSlots", availableTimeSlots },
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