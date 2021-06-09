using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatchStore25;
using WatchStore25.Controllers;
using WatchStore25.Models;

namespace WatchStore25.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDetails()
        {
            var controller = new HomeController();
            var result0 = controller.ProductDetail(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new WS25Entities();
            var product = db.PRODUCTs.First();
            var result1 = controller.ProductDetail(product.idProduct) as ViewResult;
            Assert.IsNotNull(result1);

            var model = result1.Model as PRODUCT;
            Assert.IsNotNull(model);
            Assert.AreEqual(product.img, model.img);
            Assert.AreEqual(product.name, model.name);
            Assert.AreEqual(product.amount, model.amount);
            Assert.AreEqual(product.detail, model.detail);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            //ViewResult result = controller.Contact() as ViewResult;

            // Assert
            //Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Search()
        {
            var db = new WS25Entities();
            var products = db.PRODUCTs.ToList();
            var keyword = products.First().name.Split().First();
            products = products.Where(p => p.name.ToLower().Contains(keyword.ToLower())).ToList();

            var controller = new HomeController();
            var result = controller.Search(keyword) as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<PRODUCT>;
            Assert.IsNotNull(model);

            Assert.AreEqual(products.Count(), model.Count());
            Assert.AreEqual(keyword, result.ViewData["keyword"]);
        }
    }
}
