using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchStore25.Models;

namespace WatchStore25.Controllers
{
    public class AdminController : Controller

    {
        WS25Entities db = new WS25Entities();
        // GET: Admin
        public ActionResult HomeAdmin()
        {
            return View();
        }
        public ActionResult CustomerManager()
        {
            return View();
        }
        public ActionResult OrderManager()
        {
            return View();
        }
        public ActionResult ProductManager()
        {
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