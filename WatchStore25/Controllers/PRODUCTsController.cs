using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatchStore25.Models;

namespace WatchStore25.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PRODUCTsController : Controller
    {
        private WS25Entities db = new WS25Entities();

        // GET: PRODUCTs
        public ActionResult Index()
        {
            var pRODUCTs = db.PRODUCTs.Include(p => p.TYPE_PRODUCT);
            return View(pRODUCTs.ToList());
        }

        // GET: PRODUCTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // GET: PRODUCTs/Create
        public ActionResult Create()
        {
            ViewBag.idTypeProduct = new SelectList(db.TYPE_PRODUCT, "idTypeProduct", "nameTypeProduct");
            return View();
        }

        // POST: PRODUCTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PRODUCT pRODUCT, HttpPostedFileBase imgfile)
        {
            ValidateProduct(pRODUCT);
            PRODUCT a = new PRODUCT();
            if (ModelState.IsValid)
            {
                string path = uploadimage(imgfile);

                if (path.Equals("-1"))
                {
                }
                else
                {
                    a.name = pRODUCT.name;
                    a.idTypeProduct = pRODUCT.idTypeProduct;
                    a.inventory = pRODUCT.inventory;
                    a.detail = pRODUCT.detail;
                    a.img = path;
                    a.tax = pRODUCT.tax;
                    a.status = pRODUCT.status;
                    a.amount = pRODUCT.amount;

                    db.PRODUCTs.Add(a);
                    db.SaveChanges();
                }
                return RedirectToAction("ProductManager", "Admin");
            }
            ViewBag.idTypeProduct = new SelectList(db.TYPE_PRODUCT, "idTypeProduct", "nameTypeProduct", pRODUCT.idTypeProduct);
            return View(pRODUCT);
        }

        private void ValidateProduct(PRODUCT pRODUCT)
        {
            if (pRODUCT.amount < 0)
            {
                ModelState.AddModelError("amount", "Giá nhỏ hơn 0");
            }
        }

        public string uploadimage(HttpPostedFileBase file)

        {
            string path = "-1";
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/Images"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = Path.GetFileName(file.FileName);
                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");
                path = "-1";
            }
            return path;
        }

        // GET: PRODUCTs/Edit/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            ViewBag.idTypeProduct = new SelectList(db.TYPE_PRODUCT, "idTypeProduct", "nameTypeProduct", pRODUCT.idTypeProduct);
            return View(pRODUCT);
        }

        // POST: PRODUCTs/Edit/
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ProductManager", "Admin");
            }
            ViewBag.idTypeProduct = new SelectList(db.TYPE_PRODUCT, "idTypeProduct", "nameTypeProduct", pRODUCT.idTypeProduct);
            return View(pRODUCT);
        }

        // GET: PRODUCTs/Delete/
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            if (pRODUCT == null)
            {
                return HttpNotFound();
            }
            return View(pRODUCT);
        }

        // POST: PRODUCTs/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODUCT pRODUCT = db.PRODUCTs.Find(id);
            db.PRODUCTs.Remove(pRODUCT);
            db.SaveChanges();
            return RedirectToAction("ProductManager", "Admin");
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
