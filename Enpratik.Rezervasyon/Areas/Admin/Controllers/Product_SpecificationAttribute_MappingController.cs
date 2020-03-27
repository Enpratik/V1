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
    public class Product_SpecificationAttribute_MappingController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Product_SpecificationAttribute_Mapping
        public ActionResult Index()
        {
            var product_SpecificationAttribute_Mapping = db.Product_SpecificationAttribute_Mapping.Include(p => p.Products).Include(p => p.Product_SpecificationAttributeOption);
            return View(product_SpecificationAttribute_Mapping.ToList());
        }

        // GET: Product_SpecificationAttribute_Mapping/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_SpecificationAttribute_Mapping product_SpecificationAttribute_Mapping = db.Product_SpecificationAttribute_Mapping.Find(id);
            if (product_SpecificationAttribute_Mapping == null)
            {
                return HttpNotFound();
            }
            return View(product_SpecificationAttribute_Mapping);
        }

        // GET: Product_SpecificationAttribute_Mapping/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName");
            ViewBag.SpecificationAttributeOptionId = new SelectList(db.Product_SpecificationAttributeOption, "Id", "Name");
            return View();
        }

        // POST: Product_SpecificationAttribute_Mapping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,SpecificationAttributeOptionId,CustomValue,AllowFiltering,ShowOnProductPage,DisplayOrder")] Product_SpecificationAttribute_Mapping product_SpecificationAttribute_Mapping)
        {
            if (ModelState.IsValid)
            {
                db.Product_SpecificationAttribute_Mapping.Add(product_SpecificationAttribute_Mapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName", product_SpecificationAttribute_Mapping.ProductId);
            ViewBag.SpecificationAttributeOptionId = new SelectList(db.Product_SpecificationAttributeOption, "Id", "Name", product_SpecificationAttribute_Mapping.SpecificationAttributeOptionId);
            return View(product_SpecificationAttribute_Mapping);
        }

        // GET: Product_SpecificationAttribute_Mapping/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_SpecificationAttribute_Mapping product_SpecificationAttribute_Mapping = db.Product_SpecificationAttribute_Mapping.Find(id);
            if (product_SpecificationAttribute_Mapping == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName", product_SpecificationAttribute_Mapping.ProductId);
            ViewBag.SpecificationAttributeOptionId = new SelectList(db.Product_SpecificationAttributeOption, "Id", "Name", product_SpecificationAttribute_Mapping.SpecificationAttributeOptionId);
            return View(product_SpecificationAttribute_Mapping);
        }

        // POST: Product_SpecificationAttribute_Mapping/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,SpecificationAttributeOptionId,CustomValue,AllowFiltering,ShowOnProductPage,DisplayOrder")] Product_SpecificationAttribute_Mapping product_SpecificationAttribute_Mapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_SpecificationAttribute_Mapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName", product_SpecificationAttribute_Mapping.ProductId);
            ViewBag.SpecificationAttributeOptionId = new SelectList(db.Product_SpecificationAttributeOption, "Id", "Name", product_SpecificationAttribute_Mapping.SpecificationAttributeOptionId);
            return View(product_SpecificationAttribute_Mapping);
        }

        // GET: Product_SpecificationAttribute_Mapping/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_SpecificationAttribute_Mapping product_SpecificationAttribute_Mapping = db.Product_SpecificationAttribute_Mapping.Find(id);
            if (product_SpecificationAttribute_Mapping == null)
            {
                return HttpNotFound();
            }
            return View(product_SpecificationAttribute_Mapping);
        }

        // POST: Product_SpecificationAttribute_Mapping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_SpecificationAttribute_Mapping product_SpecificationAttribute_Mapping = db.Product_SpecificationAttribute_Mapping.Find(id);
            db.Product_SpecificationAttribute_Mapping.Remove(product_SpecificationAttribute_Mapping);
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
