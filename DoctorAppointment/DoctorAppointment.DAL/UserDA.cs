using DoctorAppointment.Model;
using DoctorAppointment.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DoctorAppointment.DAL
{
    public static class UserDA
    {
        public static bool RegisterDoctor(User user)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    context.Users.Add(user);
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

        public static bool UpdateDoctor(UserModel user)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    var existingUser = context.Users.FirstOrDefault(u => u.UserID == user.UserID);

                    if (existingUser != null)
                    {
                        existingUser.Name = user.Name.Trim();
                        existingUser.Email = user.Email.Trim();
                        existingUser.Password = user.Password.Trim();

                        if (user.Doctor != null)
                        {
                            if (existingUser.Doctors.Count > 0)
                            {
                                // user has only one doctor associated
                                var existingDoctor = existingUser.Doctors.First();
                                existingDoctor.DoctorName = user.Name.Trim();
                                existingDoctor.AppointmentSlotTime = user.Doctor.AppointmentSlotTime;
                                existingDoctor.DayStartTime = user.Doctor.DayStartTime;
                                existingDoctor.DayEndTime = user.Doctor.DayEndTime;
                            }
                        }

                        context.SaveChanges();
                        return true;
                    }
                    else
                    {
                        // User not found
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }

        public static User GetUserDetails(int doctorID)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    Doctor doc = context.Doctors.Find(doctorID);
                    User user = context.Users.Include(u => u.Doctors).FirstOrDefault(u => u.UserID == doc.UserID);
                    return user;
                }                
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
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

        public static bool CheckEmailExists(string email)
        {
            try
            {
                using (var context = new DoctorAppointmentEntities())
                {
                    return context.Users.Any(u => u.Email == email);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }

    }
}

