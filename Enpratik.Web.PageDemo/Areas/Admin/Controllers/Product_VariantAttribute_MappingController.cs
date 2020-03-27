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
    public class Product_VariantAttribute_MappingController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Product_VariantAttribute_Mapping
        public ActionResult Index()
        {
            return View(db.Product_VariantAttribute_Mapping.ToList());
        }

        // GET: Product_VariantAttribute_Mapping/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_VariantAttribute_Mapping product_VariantAttribute_Mapping = db.Product_VariantAttribute_Mapping.Find(id);
            if (product_VariantAttribute_Mapping == null)
            {
                return HttpNotFound();
            }
            return View(product_VariantAttribute_Mapping);
        }

        // GET: Product_VariantAttribute_Mapping/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product_VariantAttribute_Mapping/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product_VariantAttribute_Mapping product_VariantAttribute_Mapping)
        {
            if (ModelState.IsValid)
            {
                db.Product_VariantAttribute_Mapping.Add(product_VariantAttribute_Mapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_VariantAttribute_Mapping);
        }

        // GET: Product_VariantAttribute_Mapping/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_VariantAttribute_Mapping product_VariantAttribute_Mapping = db.Product_VariantAttribute_Mapping.Find(id);
            if (product_VariantAttribute_Mapping == null)
            {
                return HttpNotFound();
            }
            return View(product_VariantAttribute_Mapping);
        }

        // POST: Product_VariantAttribute_Mapping/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product_VariantAttribute_Mapping product_VariantAttribute_Mapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_VariantAttribute_Mapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_VariantAttribute_Mapping);
        }

        // GET: Product_VariantAttribute_Mapping/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_VariantAttribute_Mapping product_VariantAttribute_Mapping = db.Product_VariantAttribute_Mapping.Find(id);
            if (product_VariantAttribute_Mapping == null)
            {
                return HttpNotFound();
            }
            return View(product_VariantAttribute_Mapping);
        }

        // POST: Product_VariantAttribute_Mapping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_VariantAttribute_Mapping product_VariantAttribute_Mapping = db.Product_VariantAttribute_Mapping.Find(id);
            db.Product_VariantAttribute_Mapping.Remove(product_VariantAttribute_Mapping);
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
