using System;
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
    public class CUSTOMERsController : Controller
    {
        private WS25Entities db = new WS25Entities();

        // GET: CUSTOMERs
        public ActionResult Index()
        {
            ViewBag.SL = db.CUSTOMERs.Count();
            return View(db.CUSTOMERs.ToList());
        }

        // GET: CUSTOMERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER cUSTOMER = db.CUSTOMERs.Find(id);
            if (cUSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOMER);
        }
    }
}
