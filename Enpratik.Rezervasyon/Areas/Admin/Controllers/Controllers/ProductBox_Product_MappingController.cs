using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.Data;

namespace Enpratik.AdminPanel.Controllers
{
    public class ProductBox_Product_MappingController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: ProductBox_Product_Mapping
        public ActionResult Index()
        {
            return View(db.ProductBox_Product_Mapping.ToList());
        }

        // GET: ProductBox_Product_Mapping/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBox_Product_Mapping productBox_Product_Mapping = db.ProductBox_Product_Mapping.Find(id);
            if (productBox_Product_Mapping == null)
            {
                return HttpNotFound();
            }
            return View(productBox_Product_Mapping);
        }

        // GET: ProductBox_Product_Mapping/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductBox_Product_Mapping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,ProductBoxId")] ProductBox_Product_Mapping productBox_Product_Mapping)
        {
            if (ModelState.IsValid)
            {
                db.ProductBox_Product_Mapping.Add(productBox_Product_Mapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productBox_Product_Mapping);
        }

        // GET: ProductBox_Product_Mapping/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBox_Product_Mapping productBox_Product_Mapping = db.ProductBox_Product_Mapping.Find(id);
            if (productBox_Product_Mapping == null)
            {
                return HttpNotFound();
            }
            return View(productBox_Product_Mapping);
        }

        // POST: ProductBox_Product_Mapping/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,ProductBoxId")] ProductBox_Product_Mapping productBox_Product_Mapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productBox_Product_Mapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productBox_Product_Mapping);
        }

        // GET: ProductBox_Product_Mapping/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBox_Product_Mapping productBox_Product_Mapping = db.ProductBox_Product_Mapping.Find(id);
            if (productBox_Product_Mapping == null)
            {
                return HttpNotFound();
            }
            return View(productBox_Product_Mapping);
        }

        // POST: ProductBox_Product_Mapping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductBox_Product_Mapping productBox_Product_Mapping = db.ProductBox_Product_Mapping.Find(id);
            db.ProductBox_Product_Mapping.Remove(productBox_Product_Mapping);
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
