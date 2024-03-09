using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Utility
{
    public class SummaryReport
    {
        public System.DateTime date { get; set; }
        public int totalAppointments { get ; set; }
        public int totalClosedAppointments { get; set; }
        public int totalCancelledAppointments { get; set; }
    }
}
