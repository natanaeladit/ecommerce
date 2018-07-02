using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ecommerce.Tests.FunctionalTests
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void WhenNoAdminSessionExisted_ThenRedirectToLoginPage()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public void WhenAdminLoginSuccess_ThenRedirectToProductController()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public void WhenAdminLogout_ThenRedirectToLoginPage()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
