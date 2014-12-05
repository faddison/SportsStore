using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Tests.Unit
{
    [TestClass]
    public class CartTests
    {
        private Product p1;
        private Product p2;
        private Cart cart;

        [TestInitialize]
        public void TestInitialize()
        {
            p1 = new Product { ProductID = 1, Price = 1 };
            p2 = new Product { ProductID = 2, Price = 2 };
            cart = new Cart();
        }

        [TestMethod]
        public void NewCartIsEmpty()
        {
            Assert.AreEqual(0, cart.Lines.Count());
        }

        [TestMethod]
        public void AddProductToEmptyCart() 
        {
            cart.AddItem(p1, 1);
            Assert.AreEqual(1, cart.Lines.Count());
            Assert.AreEqual(1, cart.Lines.First().Product.ProductID);
            Assert.AreEqual(1, cart.Lines.First().Quantity);
        }

        [TestMethod]
        public void AddDifferentProductToNonEmptyCart()
        {
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);

            List<CartLine> lines = cart.Lines.OrderBy(l => l.Product.ProductID).ToList();

            Assert.AreEqual(2,lines.Count());
            Assert.AreEqual(1, lines[0].Product.ProductID);
            Assert.AreEqual(1, lines[0].Quantity);
            Assert.AreEqual(2, lines[1].Product.ProductID);
            Assert.AreEqual(2, lines[1].Quantity);
        }

        [TestMethod]
        public void AddSameProductToNonEmptyCart()
        {
            cart.AddItem(p1, 1);
            cart.AddItem(p1, 1);

            Assert.AreEqual(1, cart.Lines.Count());
            Assert.AreEqual(1, cart.Lines.First().Product.ProductID);
            Assert.AreEqual(2, cart.Lines.First().Quantity);
        }

        [TestMethod]
        public void RemoveNonExistingProductFromCart()
        {
            cart.RemoveLine(p1);
            Assert.AreEqual(0, cart.Lines.Count());
        }

        [TestMethod]
        public void RemoveExistingProductFromCart()
        {
            cart.AddItem(p1, 2);

            Assert.AreEqual(1, cart.Lines.Count());

            cart.RemoveLine(p1);

            Assert.AreEqual(0, cart.Lines.Count());
        }

        [TestMethod]
        public void AddProductWithZeroQuanitity()
        {
            cart.AddItem(p1, 0);
            Assert.AreEqual(1, cart.Lines.Count());
            Assert.AreEqual(0, cart.Lines.First().Quantity);
        }

        [TestMethod]
        public void ComputeTotalValueOfEmptyCart()
        {
            Assert.AreEqual(0, cart.ComputeTotalValue());
        }

        [TestMethod]
        public void ComputeTotalValueOfCartWithOneProduct()
        {
            cart.AddItem(p1, 1);
            Assert.AreEqual(1, cart.ComputeTotalValue());
        }

        [TestMethod]
        public void ComputeTotalValueOfCartWithTwoProducts()
        {
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            Assert.AreEqual(3, cart.ComputeTotalValue());
        }

        [TestMethod]
        public void ComputeTotalValueOfCartWithTwoProductsAndSameProduct()
        {
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p2, 1);
            Assert.AreEqual(5, cart.ComputeTotalValue());
        }

        [TestMethod]
        public void ComputeTotalValueOfCartWithTwoProductsWithZeroQuantity()
        {
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 0);
            Assert.AreEqual(1, cart.ComputeTotalValue());
        }

        [TestMethod]
        public void ComputeTotalValueOfCartWithTwoProductsWithZeroPrice()
        {
            p1.Price = 0;
            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);
            Assert.AreEqual(2, cart.ComputeTotalValue());
        }

        [TestMethod]
        public void ClearCartContents()
        {
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            Assert.AreEqual(2, cart.Lines.Count());
            cart.Clear();
            Assert.AreEqual(0, cart.Lines.Count());
        }
    }
}
