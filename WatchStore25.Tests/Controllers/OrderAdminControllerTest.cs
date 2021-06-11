using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatchStore25.Controllers;
using WatchStore25.Models;
using System.Transactions;
using System.Web;

namespace WatchStore25.Tests.Controllers
{
    [TestClass]
    public class OrderAdminControllerTest
    {
        [TestMethod]
        public void TestIndex()
        {
            var controller = new OrderAdminController();

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);

            var model = result.Model as List<DETAIL_ORDER>;

            Assert.IsNotNull(model);

            var db = new WS25Entities();
            Assert.AreEqual(db.DETAIL_ORDER.Count(), model.Count);
        }
        [TestMethod]
        public void TestDetails()
        {

            var controller = new OrderAdminController();
            var result0 = controller.Details(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);
            var db = new WS25Entities();
            var detail = db.DETAIL_ORDER.First();
            var result = controller.Details(detail.idDetailOrder) as ViewResult;


            Assert.IsNotNull(result);
            var model = result.Model as DETAIL_ORDER;

            Assert.IsNotNull(model);
            Assert.AreEqual(detail.idDetailOrder, model.idDetailOrder);
            Assert.AreEqual(detail.idOrderProduct, model.idOrderProduct);
            Assert.AreEqual(detail.idProduct, model.idProduct);
            Assert.AreEqual(detail.idStatusOrder, model.idStatusOrder);
            Assert.AreEqual(detail.ORDER_PRODUCT.address, model.ORDER_PRODUCT.address);
            Assert.AreEqual(detail.ORDER_PRODUCT.updateDate, model.ORDER_PRODUCT.updateDate);
            Assert.AreEqual(detail.ORDER_PRODUCT.CUSTOMER.name, model.ORDER_PRODUCT.CUSTOMER.name);
            Assert.AreEqual(detail.PRODUCT.name, model.PRODUCT.name);
            Assert.AreEqual(detail.STATUS_ORDER.Status, model.STATUS_ORDER.Status);
            Assert.AreEqual(detail.totalAmount, model.totalAmount);
            Assert.AreEqual(detail.totalProduct, model.totalProduct);
/**/
        }

        [TestMethod]
        public void TestEdit()
        {

            var controller = new OrderAdminController();
            var result0 = controller.Edit(0) as HttpNotFoundResult;

            var db = new WS25Entities();
            var edit = db.DETAIL_ORDER.First();
            var result = controller.Details(edit.idDetailOrder) as ViewResult;
            Assert.IsNotNull(result0);

            Assert.IsNotNull(result);
            var model = result.Model as DETAIL_ORDER;

            Assert.IsNotNull(model);
            Assert.AreEqual(edit.idDetailOrder, model.idDetailOrder);
            Assert.AreEqual(edit.idOrderProduct, model.idOrderProduct);
            Assert.AreEqual(edit.idProduct, model.idProduct);
            Assert.AreEqual(edit.idStatusOrder, model.idStatusOrder);
            Assert.AreEqual(edit.ORDER_PRODUCT.address, model.ORDER_PRODUCT.address);
            Assert.AreEqual(edit.ORDER_PRODUCT.updateDate, model.ORDER_PRODUCT.updateDate);
            Assert.AreEqual(edit.ORDER_PRODUCT.CUSTOMER.name, model.ORDER_PRODUCT.CUSTOMER.name);
            Assert.AreEqual(edit.PRODUCT.name, model.PRODUCT.name);
            Assert.AreEqual(edit.STATUS_ORDER.Status, model.STATUS_ORDER.Status);
            Assert.AreEqual(edit.totalAmount, model.totalAmount);
            Assert.AreEqual(edit.totalProduct, model.totalProduct);
        }

        [TestMethod]
        public void TestEditP()
        {
            var rand = new Random();
            var db = new WS25Entities();
            var edits = db.DETAIL_ORDER.First();
            edits.STATUS_ORDER.Status = rand.NextDouble().ToString();
            edits.totalProduct = -rand.Next();

            var controller = new OrderAdminController();


            var result0 = controller.Edit(edits) as ViewResult;
            Assert.IsNotNull(result0);
            Assert.AreEqual("Số lượng không được nhỏ hơn 1.", controller.ModelState["totalProduct"].Errors[0].ErrorMessage);

            using (var scope = new TransactionScope())
            {
                edits.totalProduct = -edits.totalProduct;
                controller.ModelState.Clear();


                //var context = new Mock<HttpContextBase>();
                //controller.ControllerContext = new ControllerContext(context.Object, new System.Web.Routing.RouteData(), controller);

                //var result1 = controller.Edit(edits) as RedirectToRouteResult;
                //Assert.IsNotNull(result1);
                //Assert.AreEqual("Index", result1.RouteValues["action"]);

                var entity = db.DETAIL_ORDER.Find(edits.idDetailOrder);
                Assert.IsNotNull(entity);
                Assert.AreEqual(edits.totalProduct, entity.totalProduct);
                Assert.AreEqual(edits.STATUS_ORDER.Status, entity.STATUS_ORDER.Status);
            }

        }




        [TestMethod]
        public void TestDelete()
        {

            var controller = new OrderAdminController();
            var result0 = controller.Delete(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);
            var db = new WS25Entities();
            var del = db.DETAIL_ORDER.First();
            var result = controller.Details(del.idDetailOrder) as ViewResult;


            Assert.IsNotNull(result);
            var model = result.Model as DETAIL_ORDER;

            Assert.IsNotNull(model);
            Assert.AreEqual(del.idDetailOrder, model.idDetailOrder);
            Assert.AreEqual(del.idOrderProduct, model.idOrderProduct);
            Assert.AreEqual(del.idProduct, model.idProduct);
            Assert.AreEqual(del.idStatusOrder, model.idStatusOrder);
            Assert.AreEqual(del.ORDER_PRODUCT.address, model.ORDER_PRODUCT.address);
            Assert.AreEqual(del.ORDER_PRODUCT.updateDate, model.ORDER_PRODUCT.updateDate);
            Assert.AreEqual(del.ORDER_PRODUCT.CUSTOMER.name, model.ORDER_PRODUCT.CUSTOMER.name);
            Assert.AreEqual(del.PRODUCT.name, model.PRODUCT.name);
            Assert.AreEqual(del.STATUS_ORDER.Status, model.STATUS_ORDER.Status);
            Assert.AreEqual(del.totalAmount, model.totalAmount);
            Assert.AreEqual(del.totalProduct, model.totalProduct);
        }

        [TestMethod]
        public void TestDeleteP()
        {
            var rand = new Random();
            var db = new WS25Entities();
            var edits = db.DETAIL_ORDER.First();
            edits.STATUS_ORDER.Status = rand.NextDouble().ToString();
            edits.totalProduct = -rand.Next();

            var controller = new OrderAdminController();
            using (var scope = new TransactionScope())
            {
                var result = controller.DeleteConfirmed(edits.idDetailOrder) as RedirectToRouteResult;
                Assert.IsNotNull(result);
                Assert.AreEqual("Index", result.RouteValues["action"]);

                var entity = db.DETAIL_ORDER.Find(edits.idDetailOrder);
                Assert.IsNotNull(entity);
            }

        }

    }
}
