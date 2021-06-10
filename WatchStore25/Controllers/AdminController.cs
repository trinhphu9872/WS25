using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchStore25.Models;

namespace WatchStore25.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller

    {
        WS25Entities db = new WS25Entities();
        // GET: Admin
        public ActionResult HomeAdmin()
        {
            ViewBag.product = db.PRODUCTs;
           
            ViewBag.Order = db.ORDER_PRODUCT;
            return View();
        }
        public ActionResult CustomerManager()
        {
            ViewBag.SL = db.AspNetUsers.Count();
            return View();
        }
        public ActionResult OrderManager()
        {
            return View();
        }
        public ActionResult ProductManager()
        {
            ViewBag.SL = db.PRODUCTs.Count();
            return View(db.PRODUCTs);
        }
        public ActionResult AddNewProductManager()
        {
            return View();
        }
        public ActionResult Search(string keyword)
        {
            var model = db.PRODUCTs.ToList();
            model = model.Where(p => p.name.ToLower().Contains(keyword.ToLower())).ToList();
            ViewBag.num = model.Count();
            ViewBag.keyword = keyword;
            return View(model);
        }


    }
}