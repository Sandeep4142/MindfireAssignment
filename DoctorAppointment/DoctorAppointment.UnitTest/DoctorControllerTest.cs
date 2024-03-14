using DoctorAppointment.Controllers;
using DoctorAppointment.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DoctorAppointment.UnitTest
{
    [TestClass]
    public class DoctorControllerTest
    {
        [TestMethod]
        public void GetUpcomingAppointments_ReturnsAppointments()
        {
            // Arrange
            int doctorID = 1; 
            DateTime selectedDate = DateTime.Now.Date; 

            DoctorController controller = new DoctorController();

            // Act
            var result = controller.GetUpcomingAppointments(doctorID, selectedDate) as JsonResult;
            List<AppointmentModel> actualAppointments = (List<AppointmentModel>)result.Data;

            // Assert
            Assert.IsTrue(actualAppointments.Count == 0); 
        }

        [TestMethod]
        [DataRow(1, "2024-03-01")]
        public void GetAppointmentDetailedReport(int doctorID, string monthString)
        {
            // Arrange
            DateTime month = DateTime.Parse(monthString);
            DoctorController controller = new DoctorController();

            // Act
            var result = controller.GetAppointmentDetailedReport(doctorID, month) as JsonResult;
            List<AppointmentModel> actualAppointments = (List<AppointmentModel>)result.Data;

            // Assert
            Assert.IsNotNull(actualAppointments);
            Assert.IsTrue(actualAppointments.Count > 0); 
        }


    }
}
