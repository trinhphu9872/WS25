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
            return View();
        }
        public ActionResult AddNewProductManager()
        {
            return View();
        }


    }
}