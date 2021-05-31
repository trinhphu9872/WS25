﻿using System;
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
    public class OrderAdminController : Controller
    {
        private WS25Entities db = new WS25Entities();

        // GET: OrderAdmin
        public ActionResult Index()
        {
            var dETAIL_ORDER = db.DETAIL_ORDER.Include(d => d.ORDER_PRODUCT).Include(d => d.PRODUCT);
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
        public ActionResult Create()
        {
            ViewBag.idOrderProduct = new SelectList(db.ORDER_PRODUCT, "idOrderProduct", "address");
            ViewBag.idProduct = new SelectList(db.PRODUCTs, "idProduct", "name");
            return View();
        }

        // POST: OrderAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDetailOrder,idOrderProduct,totalProduct,amount,discount,totalAmount,idProduct,status")] DETAIL_ORDER dETAIL_ORDER)
        {
            if (ModelState.IsValid)
            {
                db.DETAIL_ORDER.Add(dETAIL_ORDER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idOrderProduct = new SelectList(db.ORDER_PRODUCT, "idOrderProduct", "address", dETAIL_ORDER.idOrderProduct);
            ViewBag.idProduct = new SelectList(db.PRODUCTs, "idProduct", "name", dETAIL_ORDER.idProduct);
            return View(dETAIL_ORDER);
        }

        // GET: OrderAdmin/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.idOrderProduct = new SelectList(db.ORDER_PRODUCT, "idOrderProduct", "address", dETAIL_ORDER.idOrderProduct);
            ViewBag.idProduct = new SelectList(db.PRODUCTs, "idProduct", "name", dETAIL_ORDER.idProduct);
            return View(dETAIL_ORDER);
        }

        // POST: OrderAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDetailOrder,idOrderProduct,totalProduct,amount,discount,totalAmount,idProduct")] DETAIL_ORDER dETAIL_ORDER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dETAIL_ORDER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idOrderProduct = new SelectList(db.ORDER_PRODUCT, "idOrderProduct", "address", dETAIL_ORDER.idOrderProduct);
            ViewBag.idProduct = new SelectList(db.PRODUCTs, "idProduct", "name", dETAIL_ORDER.idProduct);
            return View(dETAIL_ORDER);
        }

        // GET: OrderAdmin/Delete/5
        public ActionResult Delete(int? id)
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