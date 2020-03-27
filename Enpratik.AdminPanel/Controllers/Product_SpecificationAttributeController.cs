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
    public class Product_SpecificationAttributeController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Product_SpecificationAttribute
        public ActionResult Index()
        {
            return View(db.Product_SpecificationAttribute.ToList());
        }

        // GET: Product_SpecificationAttribute/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_SpecificationAttribute product_SpecificationAttribute = db.Product_SpecificationAttribute.Find(id);
            if (product_SpecificationAttribute == null)
            {
                return HttpNotFound();
            }
            return View(product_SpecificationAttribute);
        }

        // GET: Product_SpecificationAttribute/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product_SpecificationAttribute/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DisplayOrder,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] Product_SpecificationAttribute product_SpecificationAttribute)
        {
            if (ModelState.IsValid)
            {
                db.Product_SpecificationAttribute.Add(product_SpecificationAttribute);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_SpecificationAttribute);
        }

        // GET: Product_SpecificationAttribute/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_SpecificationAttribute product_SpecificationAttribute = db.Product_SpecificationAttribute.Find(id);
            if (product_SpecificationAttribute == null)
            {
                return HttpNotFound();
            }
            return View(product_SpecificationAttribute);
        }

        // POST: Product_SpecificationAttribute/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DisplayOrder,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] Product_SpecificationAttribute product_SpecificationAttribute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_SpecificationAttribute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_SpecificationAttribute);
        }

        // GET: Product_SpecificationAttribute/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_SpecificationAttribute product_SpecificationAttribute = db.Product_SpecificationAttribute.Find(id);
            if (product_SpecificationAttribute == null)
            {
                return HttpNotFound();
            }
            return View(product_SpecificationAttribute);
        }

        // POST: Product_SpecificationAttribute/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_SpecificationAttribute product_SpecificationAttribute = db.Product_SpecificationAttribute.Find(id);
            db.Product_SpecificationAttribute.Remove(product_SpecificationAttribute);
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
