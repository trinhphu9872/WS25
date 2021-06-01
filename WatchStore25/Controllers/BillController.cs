using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchStore25.Models;


namespace WatchStore25.Controllers
{
    public class BillController : Controller
    {
        private WS25Entities db = new WS25Entities();
        private List<DETAIL_ORDER> ShopingCarts = null;
        private void getShoppingCart()
        {

            /*Phiên làm việc quản lí*/
            /*       var session = System.Web.HttpContext.Current.Session;*/
            if (Session["ShopingCarts"] != null)
            {
                ShopingCarts = Session["ShopingCarts"] as List<DETAIL_ORDER>;
            }
            else
            {
                ShopingCarts = new List<DETAIL_ORDER>();
                Session["ShopingCarts"] = ShopingCarts;
            }

        }
        // GET: Bill
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            getShoppingCart();
            ViewBag.Cart = ShopingCarts;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ORDER_PRODUCT model)
        {
            if (ModelState.IsValid)
            {
                db.ORDER_PRODUCT.Add(new ORDER_PRODUCT
                {
                    idOrderProduct = 10,
                    idCustomer = 10,
                    address = model.address,
                    phone = model.phone,
                    noteOrder = model.noteOrder,
                    startDate = model.startDate,
                    updateDate = model.startDate

                });

                foreach (var item in ShopingCarts)
                {
                    db.DETAIL_ORDER.Add(new DETAIL_ORDER
                    {
                        idDetailOrder = 10,
                        idOrderProduct = model.idOrderProduct,
                        idProduct = item.idProduct,
                        totalAmount = item.totalAmount,
                        discount = 0,
                        amount = item.amount,
                        totalProduct = item.totalProduct,

                    });
                }
                db.SaveChanges();
                Session["ShoppingCart"] = null;
                return RedirectToAction("Index", "Product");
            }
            getShoppingCart();
            ViewBag.Cart = ShopingCarts;
            return View(model);

        }
    }
}