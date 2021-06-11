using System;
using System.Collections;
using System.Collections.Generic;

using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatchStore25.Models;

namespace WatchStore25.Controllers
{
    public class ShoppingCartController : Controller
    {
        private WS25Entities db = new WS25Entities();
        private List<DETAIL_ORDER> ShopingCarts = null;
        private List<PRODUCT> typePro = null;
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


        private void gettype()
        {

            /*Phiên làm việc quản lí*/
            /*       var session = System.Web.HttpContext.Current.Session;*/
            if (Session["typePro"] != null)
            {
                typePro = Session["typePro"] as List<PRODUCT>;
            }
            else
            {
                typePro = new List<PRODUCT>();
                Session["typePro"] = typePro;
            }

        }
        public ActionResult Index()
        {
            getShoppingCart();
            gettype();

            var hashTable = new Hashtable();
            foreach (var dETAIL_ORDER in ShopingCarts)
            {
                if (hashTable[dETAIL_ORDER.PRODUCT.idProduct] != null)
                {
                    (hashTable[dETAIL_ORDER.PRODUCT.idProduct] as DETAIL_ORDER).totalProduct += dETAIL_ORDER.totalProduct;
                }
                else
                {
                    hashTable[dETAIL_ORDER.PRODUCT.idProduct] = dETAIL_ORDER;
                }
            }
            ShopingCarts.Clear();
            typePro.Clear();
            foreach (DETAIL_ORDER dETAIL_ORDER in hashTable.Values)
            {
                ShopingCarts.Add(dETAIL_ORDER);
               
            }
            return View(ShopingCarts);
        }


        // GET: ShoppingCart/Create
        [HttpPost]
        public ActionResult Create(int productID, int quanlity)
        {
            getShoppingCart();
            gettype();
            var product = db.PRODUCTs.Find(productID);
            typePro.Add(product);
            ShopingCarts.Add(new DETAIL_ORDER
            {
                PRODUCT = product,
                totalProduct = quanlity

            });
            return RedirectToAction("Index");
        }




        [HttpPost]
        public ActionResult Edit(int[] productID, int[] quanlity)
        {
            getShoppingCart();
            ShopingCarts.Clear();
            if (productID != null)
                for (int i = 0; i < quanlity.Length; i++)
                    if (quanlity[i] > 0)
                    {
                        var product = db.PRODUCTs.Find(productID[i]);
                        ShopingCarts.Add(new DETAIL_ORDER
                        {
                            PRODUCT = product,
                            totalProduct = quanlity[i]

                        });
                    }
         
            Session["ShopingCarts"] = ShopingCarts;
            return RedirectToAction("Index");
        }


        // GET: ShoppingCart/Delete/5
        public ActionResult Delete()
        {
            getShoppingCart();
            ShopingCarts.Clear();
            Session["ShopingCarts"] = ShopingCarts;
            return RedirectToAction("Index");
        }

        /*  public ActionResult DeleteItem(int productID)
          {

              getShoppingCart();
              ShopingCarts.Clear();
              ShopingCarts.RemoveAt(productID);
              Session["ShopingCarts"] = ShopingCarts;
              return RedirectToAction("Index");SS
              *//*   getShoppingCart();
                 ShopingCarts.Clear();
                 var product = db.PRODUCTs.Find(productID);
                 quanlity = 0;
                 if (quanlity > 0)
                 {
                     ShopingCarts.Add(new DETAIL_ORDER
                     {
                         PRODUCT = product,
                         totalProduct = quanlity

                     });
                 }

                 Session["ShopingCarts"] = ShopingCarts;
                 return RedirectToAction("Index");*//*
          }*/


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
