using DoctorAppointment.Model;
using DoctorAppointment.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.DAL
{
    public class AppointmentDA
    {
        public static bool BookAppointment(AppointmentModel appointment)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    var newAppointment = new Appointment()
                    {
                        DoctorID = appointment.DoctorID,
                        AppointmentDate = appointment.AppointmentDate,
                        AppointmentTime = appointment.AppointmentTime,
                        PatientName = appointment.PatientName,
                        PatientEmail = appointment.PatientEmail,
                        PatientPhone = appointment.PatientPhone,
                        AppointmentStatus = "Open",
                    };
                    context.Appointments.Add(newAppointment);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }

        public static List<AppointmentModel> GetAppointmentList(int doctorID)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    List<AppointmentModel> appointmentList = context.Appointments
                        .Where(e => e.DoctorID == doctorID)
                        .OrderBy(e => e.AppointmentDate)
                        .ThenBy(e => e.AppointmentTime)
                        .Select(appointment => new AppointmentModel()
                        {
                            AppointmentID = appointment.AppointmentID,
                            AppointmentDate = appointment.AppointmentDate,
                            AppointmentTime = appointment.AppointmentTime,
                            PatientName = appointment.PatientName,
                            PatientPhone = appointment.PatientPhone,
                            PatientEmail = appointment.PatientEmail,
                            AppointmentStatus = appointment.AppointmentStatus,
                            DoctorID = doctorID
                        }).ToList();

                    return appointmentList;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static List<AppointmentModel> GetUpcomingAppointments(int doctorID, DateTime selectedDate)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    List<AppointmentModel> appointmentList = context.Appointments
                            .Where(e => e.DoctorID == doctorID &&
                                        DbFunctions.TruncateTime(e.AppointmentDate) == selectedDate.Date &&
                                        e.AppointmentStatus == "Open")
                                        .OrderBy(e => e.AppointmentDate)
                                        .ThenBy(e => e.AppointmentTime)
                                        .Select(appointment => new AppointmentModel()
                                        {
                                            AppointmentID = appointment.AppointmentID,
                                            AppointmentDate = appointment.AppointmentDate,
                                            AppointmentTime = appointment.AppointmentTime,
                                            PatientName = appointment.PatientName,
                                            PatientPhone = appointment.PatientPhone,
                                            PatientEmail = appointment.PatientEmail,
                                            AppointmentStatus = appointment.AppointmentStatus,
                                            DoctorID = doctorID
                                        }).ToList();
                    return appointmentList;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static List<AppointmentModel> GetDetailedAppointmentList(int doctorID, DateTime selectedMonth)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    List<AppointmentModel> appointmentList = context.Appointments
                        .Where(e => e.DoctorID == doctorID &&
                                    e.AppointmentDate.Year == selectedMonth.Year &&
                                    e.AppointmentDate.Month == selectedMonth.Month)
                        .OrderBy(e => e.AppointmentDate)
                        .ThenBy(e => e.AppointmentTime)
                        .Select(appointment => new AppointmentModel()
                        {
                            AppointmentID = appointment.AppointmentID,
                            AppointmentDate = appointment.AppointmentDate,
                            AppointmentTime = appointment.AppointmentTime,
                            PatientName = appointment.PatientName,
                            PatientPhone = appointment.PatientPhone,
                            PatientEmail = appointment.PatientEmail,
                            AppointmentStatus = appointment.AppointmentStatus,
                            DoctorID = doctorID
                        }).ToList();

                    return appointmentList;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static List<SummaryReport> GetAppointmentSummaryReport(int doctorID, DateTime selectedMonth)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    List<SummaryReport> appointmentList = context.Appointments
                        .Where(e => e.DoctorID == doctorID &&
                                    e.AppointmentDate.Year == selectedMonth.Year &&
                                    e.AppointmentDate.Month == selectedMonth.Month)
                        .OrderBy(e => e.AppointmentDate)
                        .ThenBy(e => e.AppointmentTime)
                        .GroupBy(e => e.AppointmentDate)
                        .Select(group => new SummaryReport()
                        {
                            date = group.Key,
                            totalAppointments = group.Count(),
                            totalClosedAppointments = group.Count(a => a.AppointmentStatus == "Closed"),
                            totalCancelledAppointments = group.Count(a => a.AppointmentStatus == "Cancelled")
                        }).ToList();

                    return appointmentList;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static List<TimeSpan> GetAvailableTimeSlots(DateTime selectedDate, int doctorID)
        {
            using (var context = new DoctorAppointmentEntities())
            {
                Doctor doctor = context.Doctors.Find(doctorID);
                TimeSpan appointmentSlotTime = doctor.AppointmentSlotTime;

                List<Appointment> existingAppointments = context.Appointments
                    .Where(a => a.AppointmentDate == selectedDate && a.DoctorID == doctorID)
                    .ToList();

                // Get all possible time slots based on doctor's start and end time
                TimeSpan startTime = doctor.DayStartTime;
                TimeSpan endTime = doctor.DayEndTime;
                List<TimeSpan> allTimeSlots = new List<TimeSpan>();

                while (startTime.Add(appointmentSlotTime) <= endTime)
                {
                    allTimeSlots.Add(startTime);
                    startTime = startTime.Add(appointmentSlotTime);
                }

                // Exclude existing appointments' time slots from all time slots to get available slots
                List<TimeSpan> availableTimeSlots = allTimeSlots
                    .Except(existingAppointments.Select(a => a.AppointmentTime))
                    .ToList();

                return availableTimeSlots;
            }
        }

        public static bool CloseAppointment(int appointmentID)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    var appointment = context.Appointments.Find(appointmentID);

                    if (appointment != null)
                    {
                        appointment.AppointmentStatus = "Closed";
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }

        public static bool CancelAppointment(int appointmentID)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    var appointment = context.Appointments.Find(appointmentID);
                    if (appointment != null)
                    {
                        appointment.AppointmentStatus = "Cancelled";
                        context.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }

    }
}
