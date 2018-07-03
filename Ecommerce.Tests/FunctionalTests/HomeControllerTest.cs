using Ecommerce.Controllers;
using Ecommerce.DomainModels;
using Ecommerce.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ecommerce.Tests.FunctionalTests
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<IProductRepo> mockProdRepo = new Mock<IProductRepo>();
        private Mock<IShoppingCartRepo> mockCartRepo = new Mock<IShoppingCartRepo>();

        /*[TestMethod]
        public void WhenHomeIsDisplayed_ThenShowAllProducts()
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
            var controller = new HomeController(mockProdRepo.Object, mockCartRepo.Object);
            var viewResult = controller.Index() as ViewResult;
            var model = viewResult.Model as List<Product>;

            // Assert
            Assert.AreEqual(products.Count, model.Count);
            Assert.AreEqual(products[0].Id, model[0].Id);
            Assert.AreEqual(products[0].Name, model[0].Name);
        }*/

        [TestMethod]
        public void WhenAddProductToCartAndProductDoesNotExistInCart_ThenProductIsAddedToCart()
        {
            // Arrange
            List<Product> products = new List<Product>() {
                new Product() { Id=1, Name = "A", StockQty = 2 },
            };
            List<ShoppingCart> carts = new List<ShoppingCart>();
            mockCartRepo.Setup(x => x.GetAll()).Returns(carts);

            Product product = new Product() { Id = 1, Name = "Product1", StockQty = 1 };
            ShoppingCart nonExistedCart = null;
            ShoppingCart newCart = new ShoppingCart() { Id = 1, ProductId = 1, ProductName = "A" };

            mockProdRepo.Setup(x => x.Get(product.Id)).Returns(product);
            mockCartRepo.Setup(x => x.GetByProductId(product.Id)).Returns(nonExistedCart);
            mockCartRepo.Setup(x => x.Add(newCart)).Returns(newCart);
            mockProdRepo.Setup(x => x.GetAll()).Returns(products);
            mockCartRepo.Setup(x => x.GetAll()).Returns(carts);

            // Act
            var controller = new HomeController(mockProdRepo.Object, mockCartRepo.Object);
            var viewResult = controller.AddToCart(product.Id) as RedirectToRouteResult;
            var addResult = mockCartRepo.Object.Add(newCart);

            // Assert
            Assert.AreEqual("Index", viewResult.RouteValues["action"]);
            Assert.AreEqual(addResult.Id, addResult.Id);
        }

        [TestMethod]
        public void WhenAddProductToCartAndProductExistedInCart_ThenCartQtyIsIncreased()
        {
            // Arrange
            List<Product> products = new List<Product>() {
                new Product() { Id=1, Name = "A" }
            };
            List<ShoppingCart> carts = new List<ShoppingCart>() {
                new ShoppingCart() { Id=1, ProductId = 1, ProductName = "A" }
            };
            mockCartRepo.Setup(x => x.GetAll()).Returns(carts);

            Product product = new Product() { Id = 1, Name = "Product1", StockQty = 1 };
            ShoppingCart cart = new ShoppingCart() { Id = 1, ProductId = 1, ProductName = "A" };
            mockProdRepo.Setup(x => x.Get(product.Id)).Returns(product);
            mockCartRepo.Setup(x => x.GetByProductId(product.Id)).Returns(cart);
            
            // Act
            var controller = new HomeController(mockProdRepo.Object, mockCartRepo.Object);
            var result = controller.AddToCart(product.Id) as RedirectToRouteResult;
            cart.Quantity++;

            // Assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual(2, cart.Quantity);
        }
    }
}
