using CLOUD462022.Controllers;
using CLOUD462022.Models;
using CLOUD462022.Repositories.Interfaces;
using CLOUD462022.Tests.FakeRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CLOUD462022.Tests
{
    [TestClass]
    public class ProductControllerTest
    {


        [TestMethod]
        public void GetProductModelShouldContainTheRightProduct()
        {
            // Arrange
            var fakeProductRepository = new FakeProductRepository();
            var fakeCategoryRepository = new FakeCategoryRepository();
            var productController = new ProductController(fakeProductRepository, fakeCategoryRepository);
            // Act
            var viewResult = productController.Details(2) as ViewResult;
            Product product = viewResult.Model as Product;
            // Assert
            Assert.AreEqual(product.ProductId, 2);
        }
    }
}
