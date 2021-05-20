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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idProduct,name,idTypeProduct,detail,status,inventory,amount,img,tax")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.PRODUCTs.Add(pRODUCT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idTypeProduct = new SelectList(db.TYPE_PRODUCT, "idTypeProduct", "nameTypeProduct", pRODUCT.idTypeProduct);
            return View(pRODUCT);
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
        public ActionResult Edit([Bind(Include = "idProduct,name,idTypeProduct,detail,status,inventory,amount,img,tax")] PRODUCT pRODUCT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRODUCT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Admin/ProductManager");
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
            return RedirectToAction("Index","Admin/ProductManager");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult addimg()
        {
            return View();
        }
       [HttpPost]
       public ActionResult addimg(PRODUCT img, HttpPostedFileBase imgfile)
        {
            PRODUCT prod = new PRODUCT();
            string path = uploadimage(imgfile);
            if(path.Equals("-1"))
            {

            }
            else
            {
              
                prod.img = path;
                db.PRODUCTs.Add(prod);
                db.SaveChanges();
                ViewBag.msg = "Thêm Thành Công";
            }
            return View();
        }
        public string uploadimage(HttpPostedFileBase file)

        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))

                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/upload"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/upload/" + random + Path.GetFileName(file.FileName);
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
    }
}
