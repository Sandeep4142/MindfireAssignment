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

            User userDB = user.UserModelToUserDB();
            return UserDA.RegisterDoctor(userDB);
        }

        public static bool UpdateDoctor(UserModel user)
        {
            // change format of time
            string appointmentSlotTimeString = user.Doctor.AppointmentSlotTime.ToString();
            int selectedMinutes = int.Parse(appointmentSlotTimeString.Split('.')[0]);
            TimeSpan appointmentSlotTime = TimeSpan.FromMinutes(selectedMinutes);
            user.Doctor.AppointmentSlotTime = appointmentSlotTime;

            return UserDA.UpdateDoctor(user);
        }

        public static bool CheckEmailExist(string email)
        {
            return  UserDA.CheckEmailExists(email);
        }

        public static List<DoctorModel> GetDoctorList()
        {
            List<DoctorModel> doctorList = new List<DoctorModel>();
            List<Doctor> doctorDBList = DoctorDA.GetAllDoctors();
            foreach(Doctor doctor in doctorDBList)
            {
                DoctorModel doc = doctor.DoctorDBToDoctorModel();
                doctorList.Add(doc);
            }
            return doctorList;
        }

        public static TimeSlotAvailability GetAvailableTimeSlots(DateTime selectedDate, int doctorID)
        {
            return AppointmentDA.GetAvailableTimeSlots(selectedDate, doctorID);
        }

        public static DoctorModel GetDoctorDetails(int doctorID)
        {
            Doctor doctorDB = DoctorDA.GetDoctorDetails(doctorID);
            DoctorModel doctor = doctorDB.DoctorDBToDoctorModel();
            return doctor;
        }

        public static bool BookAppointment(AppointmentModel appointment)
        {
            appointment.AppointmentStatus = (int)AppointmentStatus.Open;
            Appointment appointmentDB = appointment.AppointmentModelToAppointmentDB();
            return AppointmentDA.BookAppointment(appointmentDB);
        }

        public static int CheckUser(string email, string password)
        {
            return UserDA.CheckUser(email, password);
        }

        public static List<AppointmentModel> GetAppointmentList(int doctorID)
        {
            List<Appointment> appointmentDBList = AppointmentDA.GetAppointmentList(doctorID);
            List<AppointmentModel> appointmentList = new List<AppointmentModel>();

            foreach(Appointment appointmentDB in appointmentDBList)
            {
                AppointmentModel appointment = appointmentDB.AppointmentDBToAppointmentModel();
                appointmentList.Add(appointment);
            }
            return appointmentList;

        }

        public static List<AppointmentModel> GetDetailedAppointmentList(int doctorID, DateTime selectedMonth)
        {
            List<Appointment> appointmentDBList = AppointmentDA.GetDetailedAppointmentList(doctorID, selectedMonth);
            List<AppointmentModel> appointmentList = new List<AppointmentModel>();

            foreach (Appointment appointmentDB in appointmentDBList)
            {
                AppointmentModel appointment = appointmentDB.AppointmentDBToAppointmentModel();
                appointmentList.Add(appointment);
            }
            return appointmentList;
        }

        public static List<AppointmentModel> GetUpcomingAppointments(int doctorID, DateTime selectedDate)
        {
            List<Appointment> appointmentDBList = AppointmentDA.GetUpcomingAppointments(doctorID, selectedDate);
            List<AppointmentModel> appointmentList = new List<AppointmentModel>();

            foreach (Appointment appointmentDB in appointmentDBList)
            {
                AppointmentModel appointment = appointmentDB.AppointmentDBToAppointmentModel();
                appointmentList.Add(appointment);
            }
            return appointmentList;
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

        public static UserModel GetUserDetails(int doctorID)
        {
            User user = UserDA.GetUserDetails(doctorID);
            return user.UserDBToUserModel();
        }

        public static bool RemoveAllAppointments(int docorID)
        {
            return AppointmentDA.RemoveAllAppointments(docorID);
        }


    }
}
