using DoctorAppointment.DAL;
using DoctorAppointment.Model;
using DoctorAppointment.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Bussiness
{
    public static class DoctorAppointmentService
    {
        public static bool RegisterDoctor(UserModel user)
        {
            // change format of time
            string appointmentSlotTimeString = user.Doctor.AppointmentSlotTime.ToString();
            int selectedMinutes = int.Parse(appointmentSlotTimeString.Split('.')[0]);
            TimeSpan appointmentSlotTime = TimeSpan.FromMinutes(selectedMinutes);
            user.Doctor.AppointmentSlotTime = appointmentSlotTime;

            return UserDA.RegisterDoctor(user);
        }

        public static List<DoctorModel> GetDoctorList()
        {
            return DoctorDA.GetAllDoctors();
        }

        public static List<TimeSpan> GetAvailableTimeSlots(DateTime selectedDate, int doctorID)
        {
            return AppointmentDA.GetAvailableTimeSlots(selectedDate, doctorID);
        }

        public static DoctorModel GetDoctorDetails(int doctorID)
        {
            return DoctorDA.GetDoctorDetails(doctorID);
        }

        public static bool BookAppointment(AppointmentModel appointment)
        {
            return AppointmentDA.BookAppointment(appointment);
        }

        public static int CheckUser(string email, string password)
        {
            return UserDA.CheckUser(email, password);
        }

        public static List<AppointmentModel> GetAppointmentList(int doctorID)
        {
            return AppointmentDA.GetAppointmentList(doctorID);
        }

        public static List<AppointmentModel> GetDetailedAppointmentList(int doctorID, DateTime selectedMonth)
        {
            return AppointmentDA.GetDetailedAppointmentList(doctorID, selectedMonth);
        }

        public static List<AppointmentModel> GetUpcomingAppointments(int doctorID, DateTime selectedDate)
        {
            return AppointmentDA.GetUpcomingAppointments(doctorID, selectedDate);
        }

        public static List<SummaryReport> GetAppointmentSummaryReport(int doctorID, DateTime selectedMonth)
        {
            return AppointmentDA.GetAppointmentSummaryReport(doctorID, selectedMonth);
        }

        public static bool CloseAppointment(int appointmentID)
        {
            return AppointmentDA.CloseAppointment(appointmentID);
        }

        public static bool CancelAppointment(int appointmentID)
        {
            return AppointmentDA.CancelAppointment(appointmentID);
        }

    }
}
