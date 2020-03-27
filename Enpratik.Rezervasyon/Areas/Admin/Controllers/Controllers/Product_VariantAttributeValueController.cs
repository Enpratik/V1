using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.Data;
using Enpratik.Core;

namespace Enpratik.AdminPanel.Controllers
{
    public class Product_VariantAttributeValueController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Product_VariantAttributeValue
        public ActionResult Index(int? variantId)
        {
            if (variantId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            return View(db.Product_VariantAttributeValue.Where(v=>v.ProductVariantAttributeId==variantId).ToList());
        }

        // GET: Product_VariantAttributeValue/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_VariantAttributeValue product_VariantAttributeValue = db.Product_VariantAttributeValue.Find(id);
            if (product_VariantAttributeValue == null)
            {
                return HttpNotFound();
            }
            return View(product_VariantAttributeValue);
        }

        // GET: Product_VariantAttributeValue/Create
        public ActionResult Create(int? variantId)
        {
            if (variantId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product_VariantAttributeValue product_VariantAttributeValue = new Product_VariantAttributeValue();
            product_VariantAttributeValue.ProductVariantAttributeId = variantId.ToInt32();

            return View(product_VariantAttributeValue);
        }

        // POST: Product_VariantAttributeValue/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product_VariantAttributeValue product_VariantAttributeValue, int? variantId)
        {
            if (ModelState.IsValid)
            {
                if (variantId == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                db.Product_VariantAttributeValue.Add(product_VariantAttributeValue);
                db.SaveChanges();

                return RedirectToAction("Index", new { variantId = variantId });

            }

            return View(product_VariantAttributeValue);
        }

        // GET: Product_VariantAttributeValue/Edit/5
        public ActionResult Edit(int? id, int? variantId)
        {
            if (id == null || variantId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_VariantAttributeValue product_VariantAttributeValue = db.Product_VariantAttributeValue.Find(id);
            if (product_VariantAttributeValue == null)
            {
                return HttpNotFound();
            }
            return View(product_VariantAttributeValue);
        }

        // POST: Product_VariantAttributeValue/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product_VariantAttributeValue product_VariantAttributeValue, int? variantId)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_VariantAttributeValue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { variantId = variantId });
            }
            return View(product_VariantAttributeValue);
        }

        // GET: Product_VariantAttributeValue/Delete/5
        public ActionResult Delete(int? id, int? variantId)
        {
            if (id == null || variantId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_VariantAttributeValue product_VariantAttributeValue = db.Product_VariantAttributeValue.Find(id);
            if (product_VariantAttributeValue == null)
            {
                return HttpNotFound();
            }
            return View(product_VariantAttributeValue);
        }

        // POST: Product_VariantAttributeValue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int? variantId)
        {
            Product_VariantAttributeValue product_VariantAttributeValue = db.Product_VariantAttributeValue.Find(id);
            db.Product_VariantAttributeValue.Remove(product_VariantAttributeValue);
            db.SaveChanges();
            return RedirectToAction("Index", new { variantId = variantId });
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
