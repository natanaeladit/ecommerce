using Ecommerce.Interfaces;
using Ecommerce.DomainModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Ecommerce.Tests
{
    [TestClass]
    public class ProductUnitTest
    {
        private Mock<IProductRepo> mockProdRepo = new Mock<IProductRepo>();
        
        [TestMethod]
        public void WhenGetAllProducts_ThenReturnAllProducts()
        {
            // Arrange
            List<Product> products = new List<Product>() {
                new Product() { Id=1, Name = "A" },
                new Product() { Id=2, Name = "B" }
            };
            mockProdRepo.Setup(x => x.GetAll()).Returns(products);

            // Act
            var result = mockProdRepo.Object.GetAll();

            // Assert
            Assert.AreEqual(products.Count, result.Count);
            Assert.AreEqual(products[0].Name, result[0].Name);
        }

        [TestMethod]
        public void WhenGetProductById_ThenProductIsReturned()
        {
            // Arrange
            Product existing = new Product() { Id = 100, Name = "Product100" };
            mockProdRepo.Setup(x => x.Get(existing.Id)).Returns(existing);

            // Act
            var result = mockProdRepo.Object.Get(existing.Id);

            // Assert
            Assert.AreEqual(existing.Id, result.Id);
            Assert.AreEqual(existing.Name, result.Name);
        }

        [TestMethod]
        public void WhenAddProduct_ThenProductIsAdded()
        {
            // Arrange
            List<Product> products = new List<Product>() {
                new Product() { Id=1, Name = "A" }
            };
            Product newProd = new Product() { Id=2, Name = "B" };
            mockProdRepo.Setup(x => x.Add(newProd)).Returns(newProd);

            // Act
            var result = mockProdRepo.Object.Add(newProd);

            // Assert
            Assert.AreEqual(newProd.Id, result.Id);
            Assert.AreEqual(newProd.Name, result.Name);
        }

        [TestMethod]
        public void WhenUpdateProduct_ThenProductIsUpdated()
        {
            // Arrange
            Product existingProd = new Product() { Id = 2, Name = "B" };
            mockProdRepo.Setup(x => x.Update(existingProd)).Returns(true);

            // Act
            var result = mockProdRepo.Object.Update(existingProd);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WhenDeleteProduct_ThenProductIsDeleted()
        {
            // Arrange
            Product existingProd = new Product() { Id = 2, Name = "B" };
            mockProdRepo.Setup(x => x.Delete(existingProd.Id)).Returns(true);

            // Act
            var result = mockProdRepo.Object.Delete(existingProd.Id);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WhenUpdateNonExistedProduct_ThenProductIsNotUpdated()
        {
            // Arrange
            Product existingProd = new Product() { Id = 2, Name = "B" };
            mockProdRepo.Setup(x => x.Update(existingProd)).Returns(false);

            // Act
            var result = mockProdRepo.Object.Update(existingProd);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenDeleteNonExistedProduct_ThenProductIsNotDeleted()
        {
            // Arrange
            Product existingProd = new Product() { Id = 2, Name = "B" };
            mockProdRepo.Setup(x => x.Delete(existingProd.Id)).Returns(false);

            // Act
            var result = mockProdRepo.Object.Delete(existingProd.Id);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
