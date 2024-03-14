using DoctorAppointment.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;

namespace DoctorAppointment.UnitTest
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestMethod]
        [DataRow("mohit@gmail.com", "mohit", 0)]
        [DataRow("mohit@gmail.com", "wrongPassword", 0)]
        [DataRow("invalid@gmail.com", "mohit", -1)]

        public void CheckLoginCredentials(string email, string password, int expectedResult)
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var result = controller.Login(email, password) as JsonResult;
            int actualDoctorID = (int)result.Data;

            // Assert
            Assert.AreEqual(expectedResult, actualDoctorID);
        }

        

        [TestMethod]
        [DataRow("mohit@gmail.com", true)]
        [DataRow("invalid@gmail.com", false)]
        public void CheckEmailExist(string email, bool expectedResult)
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var result = controller.CheckEmailExist(email) as JsonResult;
            bool actualResponse = (bool)result.Data;

            // Assert
            Assert.AreEqual(expectedResult, actualResponse);
        }
    }
}
