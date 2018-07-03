using Ecommerce.Controllers;
using Ecommerce.DomainModels;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Tests.FunctionalTests
{
    [TestClass]
    public class ProductControllerTest
    {
        private Mock<IProductRepo> mockProdRepo = new Mock<IProductRepo>();
        private Mock<HttpSessionStateBase> session = new Mock<HttpSessionStateBase>(MockBehavior.Loose);
        private Mock<HttpContextBase> http = new Mock<HttpContextBase>(MockBehavior.Loose);

        /*[ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void WhenDisplayProductList_ThenAllProductsAreDisplayed()
        {
            // Arrange
            List<Product> products = new List<Product>() {
                new Product() { Id=1, Name = "A" },
                new Product() { Id=2, Name = "B" }
            };
            session.SetupGet(s => s["Login"]).Returns("admin@demo");
            http.SetupGet(x => x.Session).Returns(session.Object);
            mockProdRepo.Setup(x => x.GetAll()).Returns(products);

            // Act
            ControllerContext ctx = new ControllerContext();
            ctx.HttpContext = http.Object;
            var controller = new ProductController(mockProdRepo.Object);
            controller.ControllerContext = ctx;
            ViewResult result = controller.Index() as ViewResult;
            var model = result.Model as List<Product>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(products.Count, model.Count);
        }*/

        [TestMethod]
        public void WhenCreateNewProduct_ThenProductIsAdded()
        {
            // Arrange
            ProductModel model = new ProductModel() { Name = "abc", Description = "abc", Price = 1, StockQty = 1 };
            Product product = new Product() { Name = "abc", Description = "abc", Price = 1, StockQty = 1 };
            session.SetupGet(s => s["Login"]).Returns("admin@demo");
            http.SetupGet(x => x.Session).Returns(session.Object);
            mockProdRepo.Setup(x => x.Add(product)).Returns(product);

            // Act
            ControllerContext ctx = new ControllerContext();
            ctx.HttpContext = http.Object;
            var controller = new ProductController(mockProdRepo.Object);
            controller.ControllerContext = ctx;
            var result = controller.Create(model) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void WhenEditProduct_ThenProductIsUpdated()
        {
            // Arrange
            ProductModel model = new ProductModel() { Id = 1, Name = "abc", Description = "abc", Price = 1, StockQty = 1 };
            Product product = new Product() { Id = 1, Name = "def", Description = "def", Price = 1, StockQty = 1 };
            session.SetupGet(s => s["Login"]).Returns("admin@demo");
            http.SetupGet(x => x.Session).Returns(session.Object);
            mockProdRepo.Setup(x => x.Get(product.Id)).Returns(product);
            mockProdRepo.Setup(x => x.Update(product)).Returns(true);

            // Act
            ControllerContext ctx = new ControllerContext();
            ctx.HttpContext = http.Object;
            var controller = new ProductController(mockProdRepo.Object);
            controller.ControllerContext = ctx;
            var result = controller.Edit(model.Id, model) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void WhenEditNonExistedProduct_ThenProductIsNotUpdated()
        {
            // Arrange
            ProductModel model = new ProductModel() { Id = 1, Name = "abc", Description = "abc", Price = 1, StockQty = 1 };
            Product product = new Product() { Id = 1, Name = "def", Description = "def", Price = 1, StockQty = 1 };
            Product nonexisted = null;
            session.SetupGet(s => s["Login"]).Returns("admin@demo");
            http.SetupGet(x => x.Session).Returns(session.Object);
            mockProdRepo.Setup(x => x.Get(product.Id)).Returns(nonexisted);
            mockProdRepo.Setup(x => x.Update(product)).Returns(false);

            // Act
            ControllerContext ctx = new ControllerContext();
            ctx.HttpContext = http.Object;
            var controller = new ProductController(mockProdRepo.Object);
            controller.ControllerContext = ctx;
            var result = controller.Edit(model.Id, model) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void WhenDeleteProduct_ThenProductIsRemoved()
        {
            // Arrange
            ProductModel model = new ProductModel() { Id = 1, Name = "abc", Description = "abc", Price = 1, StockQty = 1 };
            Product product = new Product() { Id = 1, Name = "abc", Description = "abc", Price = 1, StockQty = 1 };
            session.SetupGet(s => s["Login"]).Returns("admin@demo");
            http.SetupGet(x => x.Session).Returns(session.Object);
            mockProdRepo.Setup(x => x.Get(model.Id)).Returns(product);
            mockProdRepo.Setup(x => x.Delete(model.Id)).Returns(true);

            // Act
            ControllerContext ctx = new ControllerContext();
            ctx.HttpContext = http.Object;
            var controller = new ProductController(mockProdRepo.Object);
            controller.ControllerContext = ctx;
            var result = controller.Delete(model.Id, model) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void WhenDeleteNonExistedProduct_ThenProductIsNotDeleted()
        {
            // Arrange
            ProductModel model = new ProductModel() { Id = 1, Name = "abc", Description = "abc", Price = 1, StockQty = 1 };
            Product product = new Product() { Id = 1, Name = "abc", Description = "abc", Price = 1, StockQty = 1 };
            Product nonexisted = null;
            session.SetupGet(s => s["Login"]).Returns("admin@demo");
            http.SetupGet(x => x.Session).Returns(session.Object);
            mockProdRepo.Setup(x => x.Get(model.Id)).Returns(nonexisted);
            mockProdRepo.Setup(x => x.Delete(model.Id)).Returns(false);

            // Act
            ControllerContext ctx = new ControllerContext();
            ctx.HttpContext = http.Object;
            var controller = new ProductController(mockProdRepo.Object);
            controller.ControllerContext = ctx;
            var result = controller.Delete(model.Id, model) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
