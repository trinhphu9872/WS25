using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatchStore25.Models;

namespace WatchStore25.Controllers
{
    public class HomeController : Controller
    {
        private WS25Entities db = new WS25Entities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductsMan()
        {
            return View(db.PRODUCTs);
        }

        public ActionResult ProductsWoman()
        {
            return View(db.PRODUCTs);
        }

        //GET: Products/Details/5
        public ActionResult ProductDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT product = db.PRODUCTs.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        //Cart
        public ActionResult Cart()
        {
            return View();
        }
    }
}