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
        // GET: Admin
        public WS25Entities db = new WS25Entities();
        
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
            ViewBag.SL = db.PRODUCTs.Count();
            return View(db.PRODUCTs);
        }
        public ActionResult AddNewProductManager()
        {
            var model = db.PRODUCTs.ToList();
            
            return View(model);


        }

        

    }
}