using DoctorAppointment.Model;
using DoctorAppointment.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.DAL
{
    public class AppointmentDA
    {
        public static bool BookAppointment(Appointment appointment)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {        
                    context.Appointments.Add(appointment);
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

        public static List<Appointment> GetAppointmentList(int doctorID)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    List<Appointment> appointmentList = context.Appointments
                        .Where(e => e.DoctorID == doctorID)
                        .OrderBy(e => e.AppointmentDate)
                        .ThenBy(e => e.AppointmentTime)
                        .ToList();

                    return appointmentList;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static bool RemoveAllAppointments(int doctorID)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    var appointmentsToRemove = context.Appointments.Where(e => e.DoctorID == doctorID).ToList();
                    context.Appointments.RemoveRange(appointmentsToRemove);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }

        public static List<Appointment> GetUpcomingAppointments(int doctorID, DateTime selectedDate)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    List<Appointment> appointmentList = context.Appointments
                            .Where(e => e.DoctorID == doctorID &&
                                        DbFunctions.TruncateTime(e.AppointmentDate) == selectedDate.Date &&
                                        e.AppointmentStatus == 1)
                                        .OrderBy(e => e.AppointmentDate)
                                        .ThenBy(e => e.AppointmentTime)
                                        .ToList();
                    return appointmentList;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static List<Appointment> GetDetailedAppointmentList(int doctorID, DateTime selectedMonth)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    List<Appointment> appointmentList = context.Appointments
                        .Where(e => e.DoctorID == doctorID &&
                                    e.AppointmentDate.Year == selectedMonth.Year &&
                                    e.AppointmentDate.Month == selectedMonth.Month)
                        .OrderBy(e => e.AppointmentDate)
                        .ThenBy(e => e.AppointmentTime)
                        .ToList();

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
                            Date = group.Key,
                            TotalAppointments = group.Count(),
                            TotalClosedAppointments = group.Count(a => a.AppointmentStatus == (int)AppointmentStatus.Closed),
                            TotalCancelledAppointments = group.Count(a => a.AppointmentStatus == (int)AppointmentStatus.Cancelled)
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

        public static TimeSlotAvailability GetAvailableTimeSlots(DateTime selectedDate, int doctorID)
        {
            try
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

                    TimeSlotAvailability availability = new TimeSlotAvailability
                    {
                        AvailableTimeSlots = allTimeSlots.ToList(),
                        ExistingAppointments = existingAppointments.Select(a => a.AppointmentTime).ToList()
                    };

                    return availability;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return new TimeSlotAvailability(); 
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
                        appointment.AppointmentStatus = (int)AppointmentStatus.Closed;
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
                        appointment.AppointmentStatus = (int)AppointmentStatus.Open;
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
