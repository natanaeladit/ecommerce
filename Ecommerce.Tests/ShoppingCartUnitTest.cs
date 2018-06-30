using Ecommerce.DomainModels;
using Ecommerce.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Tests
{
    [TestClass]
    public class ShoppingCartUnitTest
    {
        private Mock<IShoppingCartRepo> mockCartRepo = new Mock<IShoppingCartRepo>();

        [TestMethod]
        public void WhenGetAllCarts_ThenReturnAllCarts()
        {
            // Arrange
            List<ShoppingCart> carts = new List<ShoppingCart>() {
                new ShoppingCart() { Id=1, ProductId = 1, ProductName = "A" },
                new ShoppingCart() { Id=2, ProductId = 2, ProductName = "B" }
            };
            mockCartRepo.Setup(x => x.GetAll()).Returns(carts);

            // Act
            var result = mockCartRepo.Object.GetAll();

            // Assert
            Assert.AreEqual(carts.Count, result.Count);
            Assert.AreEqual(carts[0].ProductName, result[0].ProductName);
        }

        [TestMethod]
        public void WhenGetCartById_ThenCartIsReturned()
        {
            // Arrange
            ShoppingCart existing = new ShoppingCart() { Id = 1, ProductId = 1, ProductName = "A" };
            mockCartRepo.Setup(x => x.Get(existing.Id)).Returns(existing);

            // Act
            var result = mockCartRepo.Object.Get(existing.Id);

            // Assert
            Assert.AreEqual(existing.Id, result.Id);
            Assert.AreEqual(existing.ProductId, result.ProductId);
            Assert.AreEqual(existing.ProductName, result.ProductName);
        }

        [TestMethod]
        public void WhenAddCart_ThenCartIsAdded()
        {
            // Arrange
            List<ShoppingCart> carts = new List<ShoppingCart>() {
                new ShoppingCart() { Id=1, ProductName = "A" }
            };
            ShoppingCart newCart = new ShoppingCart() { Id = 2, ProductName = "B" };
            mockCartRepo.Setup(x => x.Add(newCart)).Returns(newCart);

            // Act
            var result = mockCartRepo.Object.Add(newCart);

            // Assert
            Assert.AreEqual(newCart.Id, result.Id);
            Assert.AreEqual(newCart.ProductName, result.ProductName);
        }

        [TestMethod]
        public void WhenUpdateCart_ThenCartIsUpdated()
        {
            // Arrange
            ShoppingCart existing = new ShoppingCart() { Id = 2, ProductName = "B", Quantity = 3 };
            mockCartRepo.Setup(x => x.Update(existing)).Returns(true);

            // Act
            var result = mockCartRepo.Object.Update(existing);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WhenDeleteCart_ThenCartIsDeleted()
        {
            // Arrange
            ShoppingCart existing = new ShoppingCart() { Id = 2, ProductName = "B" };
            mockCartRepo.Setup(x => x.Delete(existing.Id)).Returns(true);

            // Act
            var result = mockCartRepo.Object.Delete(existing.Id);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WhenUpdateNonExistedCart_ThenCartIsNotUpdated()
        {
            // Arrange
            ShoppingCart existing = new ShoppingCart() { Id = 2, ProductName = "B" };
            mockCartRepo.Setup(x => x.Update(existing)).Returns(false);

            // Act
            var result = mockCartRepo.Object.Update(existing);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenDeleteNonExistedCart_ThenCartIsNotDeleted()
        {
            // Arrange
            ShoppingCart existing = new ShoppingCart() { Id = 2, ProductName = "B" };
            mockCartRepo.Setup(x => x.Delete(existing.Id)).Returns(false);

            // Act
            var result = mockCartRepo.Object.Delete(existing.Id);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenClearCart_ThenCartIsEmpty()
        {
            // Arrange
            List<ShoppingCart> carts = new List<ShoppingCart>() {
                new ShoppingCart() { Id=1, ProductId = 1, ProductName = "A" },
                new ShoppingCart() { Id=2, ProductId = 2, ProductName = "B" }
            };
            mockCartRepo.Setup(x => x.Clear()).Returns(true);
            
            // Act
            var result = mockCartRepo.Object.Clear();

            // Assert
            Assert.IsTrue(result);
        }
    }
}