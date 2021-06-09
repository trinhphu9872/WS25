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
    public class MockHttpSession : HttpSessionStateBase
    {
        public Hashtable buffe = new Hashtable();
        public override object this[string key]
        {
            get
            {
                return buffe[key];
            }
            set
            {
                buffe[key] = value;
            }
        }
    }
    

    [TestClass]
    public class ShoppingCartControllerTest
    {

        [TestMethod]
        public void testIndex()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);
            var controller = new ShoppingCartController();

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            /* case1*/
            session["ShopingCarts"] = null;

            var res = controller.Index() as ViewResult;
            Assert.IsNotNull(res);

            var model = res.Model as List<DETAIL_ORDER>;
            Assert.IsNotNull(model);
            Assert.AreEqual(0, model.Count);



        }

        [TestMethod]
        public void testIndexNoNull()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);
            var controller = new ShoppingCartController();

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            /* case1*/
            session["ShopingCarts"] = null;

            var res = controller.Index() as ViewResult;
            Assert.IsNotNull(res);

            var model = res.Model as List<DETAIL_ORDER>;
            Assert.IsNotNull(model);
            Assert.AreEqual(0, model.Count);

            var db = new WS25Entities();
            var pro = db.PRODUCTs.First();
            var shoppingCart = new List<DETAIL_ORDER>();
            shoppingCart.Add(new DETAIL_ORDER
            {
                PRODUCT = pro,
                totalProduct = 1
            });

            var detailOrder = new DETAIL_ORDER();
            detailOrder.PRODUCT = pro;
            detailOrder.totalProduct = 2;
            shoppingCart.Add(detailOrder);

            session["ShopingCarts"] = shoppingCart;

            res = controller.Index() as ViewResult;
            Assert.IsNotNull(res);

            model = res.Model as List<DETAIL_ORDER>;
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(pro.idProduct, model.First().PRODUCT.idProduct);
            Assert.AreEqual(3, model.First().totalProduct);



        }

        [TestMethod]
        public void testCreate()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);

            var controller = new ShoppingCartController();

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var db = new WS25Entities();
            var pro = db.PRODUCTs.First();
            var res = controller.Create(pro.idProduct, 2) as RedirectToRouteResult;
            Assert.IsNotNull(res);
            Assert.AreEqual("Index", res.RouteValues["action"]);


            var cart = session["ShopingCarts"] as List<DETAIL_ORDER>;
            Assert.IsNotNull(cart);
            Assert.AreEqual(1, cart.Count);
            Assert.AreEqual(pro.idProduct, cart.First().PRODUCT.idProduct);
            Assert.AreEqual(2, cart.First().totalProduct);




        }

        [TestMethod]
        public void testDelete()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);

            var controller = new ShoppingCartController();

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var res = controller.Delete() as RedirectToRouteResult;
            Assert.IsNotNull(res);
            Assert.AreEqual("Index", res.RouteValues["action"]);


            var cart = session["ShopingCarts"] as List<DETAIL_ORDER>;
            cart.Clear();

            Assert.IsNotNull(cart);
            Assert.AreEqual(0, cart.Count);

        }
        [TestMethod]
        public void testEdit()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);

            var controller = new ShoppingCartController();

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var db = new WS25Entities();
            var data1 = db.PRODUCTs.First();

            session["ShoppingCarts"] = null;
            var res = controller.Edit(null, null) as RedirectToRouteResult;
            Assert.IsNotNull(res);
            Assert.AreEqual("Index", res.RouteValues["action"]);
            var cart = session["ShopingCarts"] as List<DETAIL_ORDER>;
            Assert.IsNotNull(cart);
            Assert.AreEqual(0, cart.Count());




        }

        [TestMethod]
        public void testDelitem()
        {
            var session = new MockHttpSession();
            var context = new Mock<HttpContextBase>();
            context.Setup(c => c.Session).Returns(session);

            var controller = new ShoppingCartController();

            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            var db = new WS25Entities();
            var data1 = db.PRODUCTs.First();
            var shoppingCart = new List<DETAIL_ORDER>();
            shoppingCart.Add(new DETAIL_ORDER
            {
                PRODUCT = data1,
                totalProduct = 0
            });
            /*Remove */
            shoppingCart = null;
            var res = controller.Edit(null, null) as RedirectToRouteResult;
            Assert.IsNotNull(res);
            Assert.AreEqual("Index", res.RouteValues["action"]);
            var cart = session["ShopingCarts"] as List<DETAIL_ORDER>;
            Assert.IsNotNull(cart);
            Assert.AreEqual(0, cart.Count());



        }
    }
}
