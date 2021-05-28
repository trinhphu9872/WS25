using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatchStore25.Controllers;
using WatchStore25.Models;

namespace WatchStore25.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var controller = new PRODUCTsController();

            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<PRODUCT>;
            Assert.IsNotNull(model);

            var db = new WS25Entities();
            Assert.AreEqual(db.PRODUCTs.Count(), model.Count);
        }
    }
}
