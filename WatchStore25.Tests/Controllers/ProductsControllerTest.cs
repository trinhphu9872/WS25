using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatchStore25.Controllers;
using WatchStore25.Models;
using System.Transactions;

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
        [TestMethod]
        public void TestCreateGet()
        {
            var controller = new PRODUCTsController();

            var result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TestCreatePost()
        {
            var random = new Random();

            var product = new PRODUCT
            {
                name = random.NextDouble().ToString(),
                inventory = random.Next(),
                detail = random.Next().ToString(),
                tax = random.Next(),
                amount = -random.Next(),
            };
            var controller = new PRODUCTsController();

            var result = controller.Create(product, null) as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual("Giá nhỏ hơn 0", controller.ModelState["amount"].Errors[0].ErrorMessage);
        }
        [TestMethod]
        public void TestEditGet()
        {
            var controller = new PRODUCTsController();
            var product = new PRODUCT();

            var result = controller.Edit(0) as HttpNotFoundResult;
            Assert.IsNotNull(result);
            var db = new WS25Entities();
            var edit = db.PRODUCTs.First();

            var result1 = controller.Edit(edit.idProduct) as ViewResult;
            Assert.IsNotNull(result1);

            var model = result1.Model as PRODUCT;

            Assert.IsNotNull(model);
            Assert.AreEqual(edit.name, model.name);
            Assert.AreEqual(edit.idTypeProduct, model.idTypeProduct);
            Assert.AreEqual(edit.img, model.img);
            Assert.AreEqual(edit.inventory, model.inventory);
            Assert.AreEqual(edit.status, model.status);
            Assert.AreEqual(edit.detail, model.detail);
            Assert.AreEqual(edit.amount, model.amount);
        }
        [TestMethod]
        public void TestEditPost()
        {
            var controller = new PRODUCTsController();
            Random ran = new Random();
            var db = new WS25Entities();
            var edit = db.PRODUCTs.First();
            edit.name = ran.NextDouble().ToString();
            edit.inventory = ran.Next();
            edit.detail = ran.Next().ToString();
            edit.tax = ran.Next();
            edit.amount = -ran.Next();

            var result = controller.Edit(edit) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Giá nhỏ hơn 0", controller.ModelState["amount"].Errors[0].ErrorMessage);
            edit.amount = -edit.amount;
            controller.ModelState.Clear();
            using (var scope = new TransactionScope())
            {
                var entity = db.PRODUCTs.Find(edit.idProduct);
                Assert.IsNotNull(entity);
                Assert.AreEqual(edit.name, entity.name);
                Assert.AreEqual(edit.inventory, entity.inventory);
                Assert.AreEqual(edit.detail, entity.detail);
                Assert.AreEqual(edit.tax, entity.tax);
                Assert.AreEqual(edit.amount, entity.amount);
            }

        }
        [TestMethod]
        public void TestDeleteG()
        {
            var controller = new PRODUCTsController();

            var result = controller.Edit(0) as HttpNotFoundResult;
            Assert.IsNotNull(result);
            var db = new WS25Entities();
            var del = db.PRODUCTs.First();

            var result1 = controller.Edit(del.idProduct) as ViewResult;
            Assert.IsNotNull(result1);

            var model = result1.Model as PRODUCT;

            Assert.IsNotNull(model);
            Assert.AreEqual(del.name, model.name);
            Assert.AreEqual(del.idTypeProduct, model.idTypeProduct);
            Assert.AreEqual(del.img, model.img);
            Assert.AreEqual(del.inventory, model.inventory);
            Assert.AreEqual(del.status, model.status);
            Assert.AreEqual(del.detail, model.detail);
            Assert.AreEqual(del.amount, model.amount);
        }
        [TestMethod]
        public void TestDeletePost()
        {
            var ran = new Random();
            var db = new WS25Entities();
            var del = db.PRODUCTs.Find(10);
            del.name = ran.NextDouble().ToString();
            del.amount = ran.Next();

            var controller = new PRODUCTsController();
            using (var scope = new TransactionScope())
            {
                var result = controller.DeleteConfirmed(del.idProduct) as RedirectToRouteResult;
                Assert.IsNotNull(result);
                Assert.AreEqual("ProductManager", result.RouteValues["action"]);

                var entity = db.PRODUCTs.Find(del.idProduct);
                Assert.IsNotNull(entity);
/**/
            }
        }
    }
}