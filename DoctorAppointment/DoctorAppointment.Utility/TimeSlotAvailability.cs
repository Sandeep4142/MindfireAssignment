using DoctorAppointment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Utility
{
    public class TimeSlotAvailability
    {
        public List<TimeSpan> AvailableTimeSlots { get; set; }
        public List<TimeSpan> ExistingAppointments { get; set; }
    }
}
