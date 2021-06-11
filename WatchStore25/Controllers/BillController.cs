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
        private List<ORDER_PRODUCT> Order = null;
        private List<DETAIL_ORDER> CartItem = null;
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
        private void getCartItem()
        {

            /*Phiên làm việc quản lí*/
            /*       var session = System.Web.HttpContext.Current.Session;*/
            if (Session["CartItem"] != null)
            {
                CartItem = Session["CartItem"] as List<DETAIL_ORDER>;
            }
            else
            {
                CartItem = new List<DETAIL_ORDER>();
                Session["CartItem"] = CartItem;
            }

        }
        private void getOrder()
        {

            /*Phiên làm việc quản lí*/
            /*       var session = System.Web.HttpContext.Current.Session;*/
            if (Session["Order"] != null)
            {
                Order = Session["Order"] as List<ORDER_PRODUCT>;
            }
            else
            {
                Order = new List<ORDER_PRODUCT>();
                Session["Order"] = Order;
            }

        }

        // GET: Bill

        public ActionResult Index()
        {
            return View();
        
        }
        public ActionResult Show()
        {
            getOrder();
            var data = Order;
            getCartItem();
            ViewBag.CartItem = CartItem;
            return View(data);

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
            ViewBag.user = db.CUSTOMERs;
            var date = DateTime.Now;
          
            model.idStatusOrder = false;
            model.idCustomer = 10;
  
            model.startDate = date;
            model.updateDate = date;
            model.CUSTOMER.address = model.address;
            model.CUSTOMER.phoneNumber = model.phone;
   

            if (ModelState.IsValid)
            {
                db.ORDER_PRODUCT.Add(model);
                db.SaveChanges();

                foreach (var item in ShopingCarts)
                {
                    item.discount = 0;
                    item.idDetailOrder = 10;
                    db.DETAIL_ORDER.Add(new DETAIL_ORDER
                    {
                        
                        idOrderProduct = model.idOrderProduct,
                        idProduct = item.PRODUCT.idProduct,
                        totalAmount = item.PRODUCT.amount * item.totalProduct,
                        discount = item.discount,
                        amount = item.PRODUCT.amount,
                        totalProduct = item.totalProduct,
                        idStatusOrder = true,
            

                    });
                    getCartItem();
                    CartItem.Add(item);
                    Session["CartItem"] = CartItem;

                }
                var idOder = model.idOrderProduct;
                db.SaveChanges();
                getShoppingCart();
                ShopingCarts.Clear();
                ShopingCarts = null;
                Session["ShoppingCart"] = ShopingCarts;
                getOrder();
                Order.Clear();
                Order.Add(model);
                Session["Order"] = Order;
                return RedirectToAction("Show", "Bill" );
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
                ModelState.AddModelError("Phone", "Số Điện thoại không hợp lệ");
            }
        }
    }
}