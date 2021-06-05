using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            var model = db.DETAIL_ORDER.ToList();
            return View(model);
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
            validationBill(model);
            
            model.startDate = DateTime.Now;
            model.idStatusOrder = false;
            model.idCustomer = 10;
            model.idOrderProduct = 10;
            if (ModelState.IsValid)
            {
                db.ORDER_PRODUCT.Add(model) ;
                db.SaveChanges();

                foreach (var item in ShopingCarts)
                {
                    item.discount = 0;
                    item.idDetailOrder = 10;
                    db.DETAIL_ORDER.Add(new DETAIL_ORDER
                    {
                        idOrderProduct = model.idOrderProduct,
                        idProduct = item.idProduct,
                        totalAmount = item.totalAmount,
                        discount = item.discount,
                        amount = item.amount,
                        totalProduct = item.totalProduct,
                        idStatusOrder = model.idStatusOrder,
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
        private void validationBill(ORDER_PRODUCT model)
        {
            var regex = new Regex("[0-9]{3}");
            getShoppingCart();
            if (ShopingCarts.Count == 0)
                ModelState.AddModelError("", "There is no Item in ShoppingCart");
            if (!regex.IsMatch(model.phone))
            {
                ModelState.AddModelError("Phone", "Wrong Phone number");
            }
        }
    }
}