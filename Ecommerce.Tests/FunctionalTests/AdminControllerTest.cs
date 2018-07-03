using Ecommerce.Controllers;
using Ecommerce.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Tests.FunctionalTests
{
    [TestClass]
    public class AdminControllerTest
    {
        private Mock<HttpSessionStateBase> session = new Mock<HttpSessionStateBase>(MockBehavior.Loose);
        private Mock<HttpContextBase> http = new Mock<HttpContextBase>(MockBehavior.Loose);

        [TestMethod]
        public void WhenNoAdminSessionExisted_ThenRedirectToLoginPage()
        {
            // Arrange
            session.SetupGet(s => s["Login"]).Returns(null);
            http.SetupGet(x => x.Session).Returns(session.Object);

            // Act
            ControllerContext ctx = new ControllerContext();
            ctx.HttpContext = http.Object;
            var controller = new AdminController();
            controller.ControllerContext = ctx;
            RedirectToRouteResult result = controller.Index() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }

        [TestMethod]
        public void WhenAdminLoginSuccess_ThenRedirectToProductController()
        {
            // Arrange
            UserModel model = new UserModel() { Email = "admin@demo", Password = "admin" };
            session.SetupGet(s => s["Login"]).Returns(model.Email);
            http.SetupGet(x => x.Session).Returns(session.Object);

            // Act
            ControllerContext ctx = new ControllerContext();
            ctx.HttpContext = http.Object;
            var controller = new AdminController();
            controller.ControllerContext = ctx;
            RedirectToRouteResult result = controller.Login(model) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void WhenAdminLogout_ThenRedirectToLoginPage()
        {
            // Arrange
            session.SetupGet(s => s["Login"]).Returns(null);
            http.SetupGet(x => x.Session).Returns(session.Object);

            // Act
            ControllerContext ctx = new ControllerContext();
            ctx.HttpContext = http.Object;
            var controller = new AdminController();
            controller.ControllerContext = ctx;
            RedirectToRouteResult result = controller.Logout() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Login", result.RouteValues["action"]);
        }
    }
}
