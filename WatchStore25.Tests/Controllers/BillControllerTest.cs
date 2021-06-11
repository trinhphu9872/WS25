using System;
using Moq;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatchStore25.Controllers;
using WatchStore25.Models;
using System.Transactions;
using System.Web;
using System.Collections;
using System.Web.Routing;

namespace WatchStore25.Tests.Controllers
{
 

    [TestClass]
    public class BillControllerTest
    {

        [TestMethod]
        public void testIndex()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);
            var controller = new BillController();

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            /* case1*/
            session["ShopingCarts"] = null;
            session["CartItem"] = null;
            session["Order"] = null;

            var res = controller.Index() as ViewResult;
            Assert.IsNotNull(res);
/**/
        }
        [TestMethod]
        public void testShow()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);
            var controller = new BillController();

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var db = new WS25Entities();
            var data = db.DETAIL_ORDER.First();
            session["ShopingCarts"] = null;
            session["CartItem"] = null;
            var order = session["Order"] as List<DETAIL_ORDER>;
            order.Add(data);

            var res = controller.Show() as ViewResult;
            Assert.IsNotNull(res);
            Assert.AreEqual(res,order);
        }

    }
}
