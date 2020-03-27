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
    public class Product_Picture_MappingController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Product_Picture_Mapping
        public ActionResult Index(int? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var data = db.Product_Picture_Mapping.Where(s => s.ProductId== productId).ToList();
            
            return View(data);
        }
        

        // GET: Product_Picture_Mapping/Create
        public ActionResult Create(int? productId)
        {
            if (productId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Product_Picture_Mapping p = new Product_Picture_Mapping();
            p.ProductId = productId.ToInt32();
            return View(p);
        }

        // POST: Product_Picture_Mapping/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product_Picture_Mapping product_Picture_Mapping)
        {
            if (ModelState.IsValid)
            {
                if (product_Picture_Mapping.IsProductImage == true)
                {
                    db.Database.ExecuteSqlCommand("Update Product_Picture_Mapping set IsProductImage=0 Where ProductId=" + product_Picture_Mapping.ProductId);
                }

                product_Picture_Mapping.CreatedDate = DateTime.Now;
                product_Picture_Mapping.CreatedUserId = 1;
                db.Product_Picture_Mapping.Add(product_Picture_Mapping);
                db.SaveChanges();
                return RedirectToAction("Index", "Product_Picture_Mapping", new { productId = product_Picture_Mapping.ProductId });
            }

            return View(product_Picture_Mapping);
        }

        // GET: Product_Picture_Mapping/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Product_Picture_Mapping product_Picture_Mapping = db.Product_Picture_Mapping.Find(id);
            if (product_Picture_Mapping == null)
            {
                return HttpNotFound();
            }
            return View(product_Picture_Mapping);
        }

        // POST: Product_Picture_Mapping/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product_Picture_Mapping product_Picture_Mapping)
        {
            if (ModelState.IsValid)
            {
                if (product_Picture_Mapping.IsProductImage == true)
                {
                    db.Database.ExecuteSqlCommand("Update Product_Picture_Mapping set IsProductImage=0 Where ProductId=" + product_Picture_Mapping.ProductId);
                }

                db.Entry(product_Picture_Mapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Product_Picture_Mapping", new { productId = product_Picture_Mapping.ProductId });
            }
            return View(product_Picture_Mapping);
        }

        // GET: Product_Picture_Mapping/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Picture_Mapping product_Picture_Mapping = db.Product_Picture_Mapping.Find(id);
            if (product_Picture_Mapping == null)
            {
                return HttpNotFound();
            }
            return View(product_Picture_Mapping);
        }

        // POST: Product_Picture_Mapping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Picture_Mapping product_Picture_Mapping = db.Product_Picture_Mapping.Find(id);
            db.Product_Picture_Mapping.Remove(product_Picture_Mapping);
            db.SaveChanges();
            return RedirectToAction("Index", "Product_Picture_Mapping", new { productId = product_Picture_Mapping.ProductId });
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
