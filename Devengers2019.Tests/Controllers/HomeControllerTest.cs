using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Devengers2019;
using Devengers2019.Controllers;

namespace Devengers2019.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void HomeIndexTest()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void HomeAboutTest()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.About() as ViewResult;
            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void HomeContactTest()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Contact() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }



        [TestMethod]
        public void SampleTest1()
        {
            Assert.AreEqual("HomeController", "HomeController");
        }

        [TestMethod]
        public void SampleTest2()
        {
            Assert.AreEqual("AccountController", "AccountController");
        }

        [TestMethod]
        public void SampleTest3()
        {
            Assert.AreEqual("ManageController", "ManageController");
        }

        [TestMethod]
        public void SampleTest4()
        {
            Assert.AreEqual("StudentsController", "StudentsController");
        }


    }
}
