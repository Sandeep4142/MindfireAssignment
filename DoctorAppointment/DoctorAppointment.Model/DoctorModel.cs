using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Model
{
    public class DoctorModel
    {
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public int UserID { get; set; }
        public System.TimeSpan AppointmentSlotTime { get; set; }
        public System.TimeSpan DayStartTime { get; set; }
        public System.TimeSpan DayEndTime { get; set; }
        public virtual ICollection<AppointmentModel> Appointments { get; set; }
        public virtual UserModel User { get; set; }
    }
}
