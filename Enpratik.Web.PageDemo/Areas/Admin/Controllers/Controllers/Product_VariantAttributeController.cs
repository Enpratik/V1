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
    public class Product_VariantAttributeController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Product_VariantAttribute
        public ActionResult Index()
        {
            return View(db.Product_VariantAttribute.ToList());
        }

        // GET: Product_VariantAttribute/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_VariantAttribute product_VariantAttribute = db.Product_VariantAttribute.Find(id);
            if (product_VariantAttribute == null)
            {
                return HttpNotFound();
            }
            return View(product_VariantAttribute);
        }

        // GET: Product_VariantAttribute/Create
        public ActionResult Create()
        {
            return View(Product_VariantAttribute.initialize);
        }

        // POST: Product_VariantAttribute/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product_VariantAttribute product_VariantAttribute)
        {
            if (ModelState.IsValid)
            {
                db.Product_VariantAttribute.Add(product_VariantAttribute);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_VariantAttribute);
        }

        // GET: Product_VariantAttribute/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_VariantAttribute product_VariantAttribute = db.Product_VariantAttribute.Find(id);
            if (product_VariantAttribute == null)
            {
                return HttpNotFound();
            }
            return View(product_VariantAttribute);
        }

        // POST: Product_VariantAttribute/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product_VariantAttribute product_VariantAttribute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_VariantAttribute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_VariantAttribute);
        }

        // GET: Product_VariantAttribute/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_VariantAttribute product_VariantAttribute = db.Product_VariantAttribute.Find(id);
            if (product_VariantAttribute == null)
            {
                return HttpNotFound();
            }
            return View(product_VariantAttribute);
        }

        // POST: Product_VariantAttribute/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_VariantAttribute product_VariantAttribute = db.Product_VariantAttribute.Find(id);
            db.Product_VariantAttribute.Remove(product_VariantAttribute);
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
