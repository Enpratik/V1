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
    public class Product_BasketItemController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Product_BasketItem
        public ActionResult Index()
        {
            return View(db.Product_BasketItem.ToList());
        }

        // GET: Product_BasketItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_BasketItem product_BasketItem = db.Product_BasketItem.Find(id);
            if (product_BasketItem == null)
            {
                return HttpNotFound();
            }
            return View(product_BasketItem);
        }

        // GET: Product_BasketItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product_BasketItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderNumber,CustomerId,ProductId,Quantity,VariantAttributesJson,Status,IsActive,RegDate,IsNew")] Product_BasketItem product_BasketItem)
        {
            if (ModelState.IsValid)
            {
                db.Product_BasketItem.Add(product_BasketItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_BasketItem);
        }

        // GET: Product_BasketItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_BasketItem product_BasketItem = db.Product_BasketItem.Find(id);
            if (product_BasketItem == null)
            {
                return HttpNotFound();
            }
            return View(product_BasketItem);
        }

        // POST: Product_BasketItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderNumber,CustomerId,ProductId,Quantity,VariantAttributesJson,Status,IsActive,RegDate,IsNew")] Product_BasketItem product_BasketItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_BasketItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_BasketItem);
        }

        // GET: Product_BasketItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_BasketItem product_BasketItem = db.Product_BasketItem.Find(id);
            if (product_BasketItem == null)
            {
                return HttpNotFound();
            }
            return View(product_BasketItem);
        }

        // POST: Product_BasketItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_BasketItem product_BasketItem = db.Product_BasketItem.Find(id);
            db.Product_BasketItem.Remove(product_BasketItem);
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
