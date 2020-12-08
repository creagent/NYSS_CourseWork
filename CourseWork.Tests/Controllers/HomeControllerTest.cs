using CourseWork.Controllers;
using CourseWork.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace CourseWork.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController controller;
        private ViewResult result;

        [TestInitialize]
        public void SetupContext()
        {
            controller = new HomeController();
            result = controller.Main() as ViewResult;
        }

        [TestMethod]
        public void MainViewResultNotNull()
        {
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MainViewEqualMainCshtml()
        {
            Assert.AreEqual("Main", result.ViewName);
        }

        [TestMethod]
        public void EncodingOperationIsNotNull()
        {
            Assert.IsNotNull(result.ViewBag.EncodingOperation);
        }

        [TestMethod]
        public void EncodingOperationInMainViewbag()
        {
            Assert.AreEqual(typeof(EncodingOperation), result.ViewBag.EncodingOperation.GetType());
        }
    }
}
