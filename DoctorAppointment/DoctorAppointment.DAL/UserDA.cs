using DoctorAppointment.Model;
using DoctorAppointment.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.DAL
{
    public static class UserDA
    {
        public static bool RegisterDoctor(UserModel user)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    var newUser = new User
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                    };

                    newUser.Doctors = new List<Doctor>();

                    var newDoctor = new Doctor()
                    {
                        DoctorName = user.Name, // for user name is same as doctor name
                        AppointmentSlotTime = user.Doctor.AppointmentSlotTime,
                        DayStartTime = user.Doctor.DayStartTime,
                        DayEndTime = user.Doctor.DayEndTime
                    };
                    newUser.Doctors.Add(newDoctor);

                    context.Users.Add(newUser);
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

        public static int CheckUser(string email, string password)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    var user = context.Users.FirstOrDefault(u => u.Email == email);

                    if (user != null)
                    {
                        if (user.Password == password)
                        {
                            return user.UserID;
                        }
                        else
                        {
                            return 0; // wrong password
                        }
                    }
                    else
                    {
                        return -1; // user does not exist
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return -1;
            }
        }

    }
}

