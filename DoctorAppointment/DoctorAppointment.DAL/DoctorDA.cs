using DoctorAppointment.Model;
using DoctorAppointment.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.DAL
{
    public class DoctorDA
    {
        public static List<DoctorModel> GetAllDoctors()
        {
            List<DoctorModel> doctorList = new List<DoctorModel>();
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    var doctors = context.Doctors.ToList();
                    foreach (var doctor in doctors)
                    {
                        DoctorModel doc = new DoctorModel
                        {
                            DoctorID = doctor.DoctorID,
                            DoctorName = doctor.DoctorName,
                            UserID = doctor.UserID,
                            AppointmentSlotTime = doctor.AppointmentSlotTime,
                            DayStartTime = doctor.DayStartTime,
                            DayEndTime = doctor.DayEndTime,
                        };
                        doctorList.Add(doc);
                    }
                }
                return doctorList;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static DoctorModel GetDoctorDetails(int doctorID)
        {
            DoctorModel doctor = null;
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    Doctor doc = context.Doctors.Find(doctorID);
                    if (doc != null)
                    {
                        doctor = new DoctorModel()
                        {
                            DoctorID = doctorID,
                            DoctorName = doc.DoctorName,
                            UserID = doc.UserID,
                            AppointmentSlotTime = doc.AppointmentSlotTime,
                            DayStartTime = doc.DayStartTime,
                            DayEndTime = doc.DayEndTime
                        };
                    }
                }
                return doctor;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
    }
}
