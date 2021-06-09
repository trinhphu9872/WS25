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
    [Authorize(Roles = "Admin")]
    public class OrderAdminController : Controller
    {
        private WS25Entities db = new WS25Entities();
        // GET: OrderAdmin
        public ActionResult Index()
        {
            var dETAIL_ORDER = db.DETAIL_ORDER.Include(d => d.ORDER_PRODUCT).Include(d => d.PRODUCT);
            ViewBag.SL = db.DETAIL_ORDER.Count();
            return View(dETAIL_ORDER.ToList());

        }

        // GET: OrderAdmin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETAIL_ORDER dETAIL_ORDER = db.DETAIL_ORDER.Find(id);
            if (dETAIL_ORDER == null)
            {
                return HttpNotFound();
            }
            return View(dETAIL_ORDER);
        }

        // GET: OrderAdmin/Create

        // GET: OrderAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            DETAIL_ORDER dETAIL_ORDER = db.DETAIL_ORDER.Find(id);
            if (dETAIL_ORDER == null)
            {
                return HttpNotFound();
            }
            ViewBag.idOrderProduct = new SelectList(db.ORDER_PRODUCT, "idOrderProduct", "address", dETAIL_ORDER.idOrderProduct);
            ViewBag.idProduct = new SelectList(db.PRODUCTs, "idProduct", "name", dETAIL_ORDER.idProduct);
            ViewBag.idStatusOrder = new SelectList(db.STATUS_ORDER, "idStatusOrder", "Status", dETAIL_ORDER.idStatusOrder);
            return View(dETAIL_ORDER);
        }

        // POST: OrderAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DETAIL_ORDER dETAIL_ORDER)
        {
            ValidateEdit(dETAIL_ORDER);
            if (ModelState.IsValid)
            {
                db.Entry(dETAIL_ORDER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idOrderProduct = new SelectList(db.ORDER_PRODUCT, "idOrderProduct", "address", dETAIL_ORDER.idOrderProduct);
            ViewBag.idProduct = new SelectList(db.PRODUCTs, "idProduct", "name", dETAIL_ORDER.idProduct);
            ViewBag.idStatusOrder = new SelectList(db.STATUS_ORDER, "idStatusOrder", "Status", dETAIL_ORDER.idStatusOrder);
            return View(dETAIL_ORDER);
        }

        private void ValidateEdit(DETAIL_ORDER dETAIL_ORDER)
        {
            if (dETAIL_ORDER.totalProduct < 1)
                ModelState.AddModelError("totalProduct", "Số lượng không được nhỏ hơn 1.");
        }

        // GET: OrderAdmin/Delete/5
        public ActionResult Delete(int? id)
        {

            DETAIL_ORDER dETAIL_ORDER = db.DETAIL_ORDER.Find(id);
            if (dETAIL_ORDER == null)
            {
                return HttpNotFound();
            }
            return View(dETAIL_ORDER);
        }

        // POST: OrderAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DETAIL_ORDER dETAIL_ORDER = db.DETAIL_ORDER.Find(id);
            db.DETAIL_ORDER.Remove(dETAIL_ORDER);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
