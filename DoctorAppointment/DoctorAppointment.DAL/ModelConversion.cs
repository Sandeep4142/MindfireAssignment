using DoctorAppointment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.DAL
{
    public static class ModelConversion
    {
        public static Appointment AppointmentModelToAppointmentDB(this AppointmentModel appointment)
        {
            Appointment appointmentDB = new Appointment()
            {
                AppointmentID = appointment.AppointmentID,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                AppointmentStatus = appointment.AppointmentStatus,
                PatientName = appointment.PatientName,
                PatientEmail = appointment.PatientEmail,
                PatientPhone = appointment.PatientPhone,
                DoctorID = appointment.DoctorID,
            };
            return appointmentDB;
        }

        public static AppointmentModel AppointmentDBToAppointmentModel(this Appointment appointmentDB)
        {
            AppointmentModel appointment = new AppointmentModel()
            {
                AppointmentID = appointmentDB.AppointmentID,
                AppointmentDate = appointmentDB.AppointmentDate,
                AppointmentTime = appointmentDB.AppointmentTime,
                AppointmentStatus = appointmentDB.AppointmentStatus,
                PatientName = appointmentDB.PatientName,
                PatientEmail = appointmentDB.PatientEmail,
                PatientPhone = appointmentDB.PatientPhone,
                DoctorID = appointmentDB.DoctorID,
            };
            return appointment;
        }

        public static User UserModelToUserDB(this UserModel user)
        {
            User userDB = new User()
            {
                UserID = user.UserID,
                Name = user.Name.Trim(),
                Email = user.Email.Trim(),
                Password = user.Password.Trim(),
            };
            userDB.Doctors = new List<Doctor>();

            var newDoctor = new Doctor()
            {
                DoctorID = user.Doctor.DoctorID,
                DoctorName = user.Name.Trim(), // for user name is same as doctor name
                AppointmentSlotTime = user.Doctor.AppointmentSlotTime,
                DayStartTime = user.Doctor.DayStartTime,
                DayEndTime = user.Doctor.DayEndTime
            };
            userDB.Doctors.Add(newDoctor);

            return userDB;
        }

        public static UserModel UserDBToUserModel(this User userDB)
        {
            UserModel user = new UserModel()
            {
                UserID = userDB.UserID,
                Name = userDB.Name,
                Email = userDB.Email,
                Password = userDB.Password,
                Doctor = new DoctorModel()
                {
                    DoctorID = userDB.Doctors.First().DoctorID,
                    DoctorName = userDB.Doctors.First().DoctorName,
                    AppointmentSlotTime = userDB.Doctors.First().AppointmentSlotTime,
                    DayStartTime = userDB.Doctors.First().DayStartTime,
                    DayEndTime = userDB.Doctors.First().DayEndTime
                }
            };
            return user;
        }

        public static Doctor DoctorModelToDoctorDB(this DoctorModel doctor)
        {
            Doctor doctorDB = new Doctor()
            {
                DoctorID = doctor.DoctorID,
                DoctorName= doctor.DoctorName,
                AppointmentSlotTime = doctor.AppointmentSlotTime,
                DayStartTime = doctor.DayStartTime,
                DayEndTime = doctor.DayEndTime
            };
            return doctorDB;
        }

        public static DoctorModel DoctorDBToDoctorModel(this Doctor doctorDB)
        {
            DoctorModel doctor = null;
            if(doctorDB != null)
            {
                doctor = new DoctorModel()
                {
                    DoctorID = doctorDB.DoctorID,
                    DoctorName = doctorDB.DoctorName,
                    AppointmentSlotTime = doctorDB.AppointmentSlotTime,
                    DayStartTime = doctorDB.DayStartTime,
                    DayEndTime = doctorDB.DayEndTime
                };
            }
            return doctor;
        }
        
    }
}
