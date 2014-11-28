using System.Web;
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Web.Controllers;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;
using SportsStore.Web.Models;
using SportsStore.Web.HtmlHelpers;

namespace SportsStore.Tests.Unit
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanPaginate()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] 
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"}
            }.AsQueryable());
            // create a controller and make the page size 3 items
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            // Act
            ProductsListViewModel result
            = (ProductsListViewModel)controller.List(null, 2).Model;
            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void CanGeneratePageLinks()
        {
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            MvcHtmlString result = PagingHelpers.PageLinks(myHelper, pagingInfo, pageUrlDelegate);

            string expectedResult = @"<a href=""Page1"">1</a><a class=""selected"" href=""Page2"">2</a><a href=""Page3"">3</a>";

            Assert.AreEqual(expectedResult, result.ToString());
        }

        [TestMethod]
        public void CanFilterProducts()
        {
            var products = new Product[] 
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 3, Name = "P2", Category = "Cat2"},
                new Product {ProductID = 4, Name = "P3", Category = "Cat2"},
                new Product {ProductID = 5, Name = "P4", Category = "Cat3"}
            }.AsQueryable();

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(products);

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            Product[] result = ((ProductsListViewModel)controller.List("Cat2", 1).Model).Products.OrderBy(p => p.Name).ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual("P2", result[0].Name);
            Assert.AreEqual("Cat2", result[0].Category);
            Assert.AreEqual("P3", result[1].Name);
            Assert.AreEqual("Cat2", result[1].Category);
        }

        [TestMethod]
        public void NoDuplicateCategories()
        {
            var products = new Product[] 
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P1", Category = "Cat1"}
            }.AsQueryable();

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(products);

            NavController controller = new NavController(mock.Object);

            string[] results = ((IEnumerable<string>) controller.Menu().Model).ToArray();

            Assert.AreEqual(1, results.Count());
            Assert.AreEqual("Cat1", results[0]);
        }

        [TestMethod]
        public void CategoriesAreOrdered()
        {
            var products = new Product[] 
            {
                new Product {ProductID = 4, Name = "P4", Category = "Cat4"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat5"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat3"}
            }.AsQueryable();

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(products);

            NavController controller = new NavController(mock.Object);

            string[] results = ((IEnumerable<string>)controller.Menu().Model).ToArray();

            Assert.AreEqual(3, results.Count());
            Assert.AreEqual("Cat3", results[0]);
            Assert.AreEqual("Cat4", results[1]);
            Assert.AreEqual("Cat5", results[2]);
        }
    }
}
