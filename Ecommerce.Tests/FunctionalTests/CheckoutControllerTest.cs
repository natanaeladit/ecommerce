using Ecommerce.Controllers;
using Ecommerce.DomainModels;
using Ecommerce.Interfaces;
using Ecommerce.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Tests.FunctionalTests
{
    [TestClass]
    public class CheckoutControllerTest
    {
        private Mock<IProductRepo> mockProdRepo = new Mock<IProductRepo>();
        private Mock<IShoppingCartRepo> mockCartRepo = new Mock<IShoppingCartRepo>();

        private Mock<HttpSessionStateBase> session = new Mock<HttpSessionStateBase>(MockBehavior.Loose);
        private Mock<HttpContextBase> http = new Mock<HttpContextBase>(MockBehavior.Loose);


        /*[TestMethod]
        public void WhenCheckout_ThenShoppingCartIsDisplayed()
        {
            // Arrange
            List<Product> products = new List<Product>() {
                new Product() { Id=1, Name = "A" },
                new Product() { Id=2, Name = "B" }
            };
            List<ShoppingCart> carts = new List<ShoppingCart>() {
                new ShoppingCart() { Id=1, ProductId = 1, ProductName = "A" },
                new ShoppingCart() { Id=2, ProductId = 2, ProductName = "B" }
            };
            mockProdRepo.Setup(x => x.GetAll()).Returns(products);
            mockCartRepo.Setup(x => x.GetAll()).Returns(carts);
            
            // Act
            var controller = new CheckoutController(mockProdRepo.Object, mockCartRepo.Object);
            var viewResult = controller.Index() as ViewResult;
            var list = viewResult.ViewBag.Cart as List<ShoppingCart>;

            // Assert
            Assert.IsNotNull(viewResult);
            Assert.AreEqual(carts.Count, list.Count);
        }*/

        [TestMethod]
        public void WhenAddQtyInCart_ThenQtyInCartIsAdded()
        {
            // Arrange
            List<Product> products = new List<Product>() {
                new Product() { Id=1, Name = "A", StockQty=2 },
                new Product() { Id=2, Name = "B", StockQty=2 }
            };
            List<ShoppingCart> carts = new List<ShoppingCart>() {
                new ShoppingCart() { Id=1, ProductId = 1, ProductName = "A", Quantity = 1 },
                new ShoppingCart() { Id=2, ProductId = 2, ProductName = "B", Quantity = 1 }
            };
            mockCartRepo.Setup(x => x.GetByProductId(products[0].Id)).Returns(carts[0]);
            mockProdRepo.Setup(x => x.Get(products[0].Id)).Returns(products[0]);
            mockCartRepo.Setup(x => x.GetAll()).Returns(carts);

            // Act
            var controller = new CheckoutController(mockProdRepo.Object, mockCartRepo.Object);
            var result = controller.QuantityChange(1, products[0].Id) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(carts[0].Quantity, GetVal<int>(result, "d"));
            Assert.AreEqual(carts.Count, GetVal<int>(result, "t"));
        }

        [TestMethod]
        public void WhenDecreaseQtyInCart_ThenQtyInCartIsDecreased()
        {
            // Arrange
            List<Product> products = new List<Product>() {
                new Product() { Id=1, Name = "A", StockQty=2 },
                new Product() { Id=2, Name = "B", StockQty=2 }
            };
            List<ShoppingCart> carts = new List<ShoppingCart>() {
                new ShoppingCart() { Id=1, ProductId = 1, ProductName = "A", Quantity = 2 }
            };
            mockCartRepo.Setup(x => x.GetByProductId(products[0].Id)).Returns(carts[0]);
            mockProdRepo.Setup(x => x.Get(products[0].Id)).Returns(products[0]);
            mockCartRepo.Setup(x => x.GetAll()).Returns(carts);

            // Act
            var controller = new CheckoutController(mockProdRepo.Object, mockCartRepo.Object);
            var result = controller.QuantityChange(0, products[0].Id) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(carts[0].Quantity, GetVal<int>(result, "d"));
            Assert.AreEqual(carts.Count, GetVal<int>(result, "t"));
        }

        [TestMethod]
        public void WhenRemoveProductFromCart_ThenProductIsRemovedFromCart()
        {
            // Arrange
            List<Product> products = new List<Product>() {
                new Product() { Id=1, Name = "A", StockQty=2 },
                new Product() { Id=2, Name = "B", StockQty=2 }
            };
            List<ShoppingCart> carts = new List<ShoppingCart>() {
                new ShoppingCart() { Id=1, ProductId = 1, ProductName = "A", Quantity = 1 },
                new ShoppingCart() { Id=2, ProductId = 2, ProductName = "B", Quantity = 1 }
            };
            mockCartRepo.Setup(x => x.GetByProductId(products[0].Id)).Returns(carts[0]);
            mockProdRepo.Setup(x => x.Get(products[0].Id)).Returns(products[0]);
            mockCartRepo.Setup(x => x.GetAll()).Returns(carts);

            // Act
            var controller = new CheckoutController(mockProdRepo.Object, mockCartRepo.Object);
            var result = controller.QuantityChange(-1, products[0].Id) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(carts[0].Quantity, GetVal<int>(result, "d"));
            Assert.AreEqual(carts.Count, GetVal<int>(result, "t"));
        }

        [TestMethod]
        public void WhenUpdateTotalPrice_ThenTotalPriceIsUpdated()
        {
            // Arrange
            List<Product> products = new List<Product>() {
                new Product() { Id=1, Name = "A", StockQty=2 },
                new Product() { Id=2, Name = "B", StockQty=2 }
            };
            List<ShoppingCart> carts = new List<ShoppingCart>() {
                new ShoppingCart() { Id=1, ProductId = 1, ProductName = "A", Quantity = 1 },
                new ShoppingCart() { Id=2, ProductId = 2, ProductName = "B", Quantity = 1 }
            };
            mockCartRepo.Setup(x => x.GetAll()).Returns(carts);

            // Act
            var controller = new CheckoutController(mockProdRepo.Object, mockCartRepo.Object);
            var result = controller.UpdateTotal() as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.IsNotNull(GetVal<string>(result, "d"));
        }

        [TestMethod]
        public void WhenClearShoppingCart_ThenShoppingCartIsEmpty()
        {
            // Arrange
            List<Product> products = new List<Product>() {
                new Product() { Id=1, Name = "A", StockQty=2 }
            };
            List<ShoppingCart> carts = new List<ShoppingCart>() {
                new ShoppingCart() { Id=1, ProductId = 1, ProductName = "A", Quantity = 1 }
            };
            mockCartRepo.Setup(x => x.GetAll()).Returns(carts);
            mockProdRepo.Setup(x => x.Get(products[0].Id)).Returns(products[0]);
            mockCartRepo.Setup(x => x.Clear()).Returns(true);

            // Act
            var controller = new CheckoutController(mockProdRepo.Object, mockCartRepo.Object);
            var result = controller.Clear() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        /*[TestMethod]
        public void WhenPaymentIsFinished_ThenShoppingCartIsClearedAndProductStockIsUpdated()
        {
            // Arrange
            OrderModel model = new OrderModel() { FirstName = "A", LastName = "B" };
            List<Product> products = new List<Product>() {
                new Product() { Id=1, Name = "A", StockQty=2 }
            };
            List<ShoppingCart> carts = new List<ShoppingCart>() {
                new ShoppingCart() { Id=1, ProductId = 1, ProductName = "A", Quantity = 1 }
            };
            mockCartRepo.Setup(x => x.GetAll()).Returns(carts);

            // Act
            var controller = new CheckoutController(mockProdRepo.Object, mockCartRepo.Object);
            var result = controller.Payment() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }*/
        
        private T GetVal<T>(JsonResult jsonResult, string propertyName)
        {
            var property = jsonResult.Data.GetType().GetProperties()
                    .Where(p => string.Compare(p.Name, propertyName) == 0)
                    .FirstOrDefault();
            if (null == property)
                throw new ArgumentException("propertyName not found", "propertyName");
            return (T)property.GetValue(jsonResult.Data, null);
        }
    }
}
