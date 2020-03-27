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
    public class OrderProductMappingsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: OrderProductMappings
        public ActionResult Index()
        {
            return View(db.OrderProductMapping.ToList());
        }

        // GET: OrderProductMappings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProductMapping orderProductMapping = db.OrderProductMapping.Find(id);
            if (orderProductMapping == null)
            {
                return HttpNotFound();
            }
            return View(orderProductMapping);
        }

        // GET: OrderProductMappings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderProductMappings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,CategoryName,BrandName,ManufacturerName,ImporterCompanyName,ProductName,Barcode,StockNumber,IsPromotionProduct,ProductPrice,ProductPictureUrl,Amount,CurrencyCode,CurrencyPrice,Tax,MoneyOrderDiscount,TotalPrice,CreatedDate,IsActive")] OrderProductMapping orderProductMapping)
        {
            if (ModelState.IsValid)
            {
                db.OrderProductMapping.Add(orderProductMapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderProductMapping);
        }

        // GET: OrderProductMappings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProductMapping orderProductMapping = db.OrderProductMapping.Find(id);
            if (orderProductMapping == null)
            {
                return HttpNotFound();
            }
            return View(orderProductMapping);
        }

        // POST: OrderProductMappings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,CategoryName,BrandName,ManufacturerName,ImporterCompanyName,ProductName,Barcode,StockNumber,IsPromotionProduct,ProductPrice,ProductPictureUrl,Amount,CurrencyCode,CurrencyPrice,Tax,MoneyOrderDiscount,TotalPrice,CreatedDate,IsActive")] OrderProductMapping orderProductMapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderProductMapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderProductMapping);
        }

        // GET: OrderProductMappings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderProductMapping orderProductMapping = db.OrderProductMapping.Find(id);
            if (orderProductMapping == null)
            {
                return HttpNotFound();
            }
            return View(orderProductMapping);
        }

        // POST: OrderProductMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderProductMapping orderProductMapping = db.OrderProductMapping.Find(id);
            db.OrderProductMapping.Remove(orderProductMapping);
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
