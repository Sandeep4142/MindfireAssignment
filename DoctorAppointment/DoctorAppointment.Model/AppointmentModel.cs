using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointment.Model
{
    public class AppointmentModel
    {
        public int AppointmentID { get; set; }

        [Required(ErrorMessage = "Appointment Date is required")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Appointment Time is required")]
        public TimeSpan AppointmentTime { get; set; }

        public int? DoctorID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string PatientName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Email is required")]
        public string PatientEmail { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public int PatientPhone { get; set; }

        public string AppointmentStatus { get; set; }

        public virtual DoctorModel Doctor { get; set; }
    }
}
