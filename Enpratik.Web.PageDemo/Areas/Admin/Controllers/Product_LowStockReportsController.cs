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
    public class Product_LowStockReportsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Product_LowStockReports
        public ActionResult Index()
        {
            return View(db.Product_LowStockReports.ToList());
        }

        // GET: Product_LowStockReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_LowStockReports product_LowStockReports = db.Product_LowStockReports.Find(id);
            if (product_LowStockReports == null)
            {
                return HttpNotFound();
            }
            return View(product_LowStockReports);
        }

        // GET: Product_LowStockReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product_LowStockReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,CreatedDate")] Product_LowStockReports product_LowStockReports)
        {
            if (ModelState.IsValid)
            {
                db.Product_LowStockReports.Add(product_LowStockReports);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_LowStockReports);
        }

        // GET: Product_LowStockReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_LowStockReports product_LowStockReports = db.Product_LowStockReports.Find(id);
            if (product_LowStockReports == null)
            {
                return HttpNotFound();
            }
            return View(product_LowStockReports);
        }

        // POST: Product_LowStockReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,CreatedDate")] Product_LowStockReports product_LowStockReports)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_LowStockReports).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_LowStockReports);
        }

        // GET: Product_LowStockReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_LowStockReports product_LowStockReports = db.Product_LowStockReports.Find(id);
            if (product_LowStockReports == null)
            {
                return HttpNotFound();
            }
            return View(product_LowStockReports);
        }

        // POST: Product_LowStockReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_LowStockReports product_LowStockReports = db.Product_LowStockReports.Find(id);
            db.Product_LowStockReports.Remove(product_LowStockReports);
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
