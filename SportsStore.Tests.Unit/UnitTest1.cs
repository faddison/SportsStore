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
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] 
            {
                new Product { ProductID = 1, Name = "P1"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 3, Name = "P3"},
                new Product { ProductID = 4, Name = "P4"},
                new Product { ProductID = 5, Name = "P5"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;
            Product[] productArray = result.ToArray();
            Assert.AreEqual(2, productArray.Length);
            Assert.AreEqual("P4", productArray[0].Name);
            Assert.AreEqual("P5", productArray[1].Name);
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
    }
}
