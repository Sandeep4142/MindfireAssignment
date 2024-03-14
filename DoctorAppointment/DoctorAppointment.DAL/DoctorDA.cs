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
        public static List<Doctor> GetAllDoctors()
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    var doctors = context.Doctors.ToList();
                    return doctors;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static Doctor GetDoctorDetails(int doctorID)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    Doctor doctor = context.Doctors.Find(doctorID);
                    return doctor;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
    }
}
