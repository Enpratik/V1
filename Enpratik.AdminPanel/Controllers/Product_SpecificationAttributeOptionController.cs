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
    public class Product_SpecificationAttributeOptionController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Product_SpecificationAttributeOption
        public ActionResult Index()
        {
            var product_SpecificationAttributeOption = db.Product_SpecificationAttributeOption.Include(p => p.Product_SpecificationAttribute);
            return View(product_SpecificationAttributeOption.ToList());
        }

        // GET: Product_SpecificationAttributeOption/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_SpecificationAttributeOption product_SpecificationAttributeOption = db.Product_SpecificationAttributeOption.Find(id);
            if (product_SpecificationAttributeOption == null)
            {
                return HttpNotFound();
            }
            return View(product_SpecificationAttributeOption);
        }

        // GET: Product_SpecificationAttributeOption/Create
        public ActionResult Create()
        {
            ViewBag.SpecificationAttributeId = new SelectList(db.Product_SpecificationAttribute, "Id", "Name");
            return View();
        }

        // POST: Product_SpecificationAttributeOption/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SpecificationAttributeId,Name,DisplayOrder,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] Product_SpecificationAttributeOption product_SpecificationAttributeOption)
        {
            if (ModelState.IsValid)
            {
                db.Product_SpecificationAttributeOption.Add(product_SpecificationAttributeOption);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecificationAttributeId = new SelectList(db.Product_SpecificationAttribute, "Id", "Name", product_SpecificationAttributeOption.SpecificationAttributeId);
            return View(product_SpecificationAttributeOption);
        }

        // GET: Product_SpecificationAttributeOption/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_SpecificationAttributeOption product_SpecificationAttributeOption = db.Product_SpecificationAttributeOption.Find(id);
            if (product_SpecificationAttributeOption == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecificationAttributeId = new SelectList(db.Product_SpecificationAttribute, "Id", "Name", product_SpecificationAttributeOption.SpecificationAttributeId);
            return View(product_SpecificationAttributeOption);
        }

        // POST: Product_SpecificationAttributeOption/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SpecificationAttributeId,Name,DisplayOrder,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] Product_SpecificationAttributeOption product_SpecificationAttributeOption)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_SpecificationAttributeOption).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpecificationAttributeId = new SelectList(db.Product_SpecificationAttribute, "Id", "Name", product_SpecificationAttributeOption.SpecificationAttributeId);
            return View(product_SpecificationAttributeOption);
        }

        // GET: Product_SpecificationAttributeOption/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_SpecificationAttributeOption product_SpecificationAttributeOption = db.Product_SpecificationAttributeOption.Find(id);
            if (product_SpecificationAttributeOption == null)
            {
                return HttpNotFound();
            }
            return View(product_SpecificationAttributeOption);
        }

        // POST: Product_SpecificationAttributeOption/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_SpecificationAttributeOption product_SpecificationAttributeOption = db.Product_SpecificationAttributeOption.Find(id);
            db.Product_SpecificationAttributeOption.Remove(product_SpecificationAttributeOption);
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
