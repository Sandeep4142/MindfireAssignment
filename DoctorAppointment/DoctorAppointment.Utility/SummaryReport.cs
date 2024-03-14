using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Utility
{
    public class SummaryReport
    {
        public System.DateTime Date { get; set; }
        public int TotalAppointments { get ; set; }
        public int TotalClosedAppointments { get; set; }
        public int TotalCancelledAppointments { get; set; }
    }
}
