using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Web.Controllers;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SportsStore.Tests
{
    [TestClass]
    class UnitTestsOld
    {
        [TestMethod]
        public void CanPaginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] 
                {
                    new Product {ProductID = 1, Name = "P1"},
                    new Product {ProductID = 2, Name = "P2"},
                    new Product {ProductID = 3, Name = "P3"},
                    new Product {ProductID = 4, Name = "P4"},
                    new Product {ProductID = 5, Name = "P5"}
                }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;
            Product[] productArray = result.ToArray();

            Assert.AreEqual(2, productArray.Length);
            Assert.AreEqual("P4", productArray[0].Name);
            Assert.AreEqual("P5", productArray[1].Name);
        }
    }
}
