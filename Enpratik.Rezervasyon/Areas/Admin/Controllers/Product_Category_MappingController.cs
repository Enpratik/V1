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
    public class Product_Category_MappingController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Product_Category_Mapping
        public ActionResult Index()
        {
            return View(db.Product_Category_Mapping.ToList());
        }

        // GET: Product_Category_Mapping/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Category_Mapping product_Category_Mapping = db.Product_Category_Mapping.Find(id);
            if (product_Category_Mapping == null)
            {
                return HttpNotFound();
            }
            return View(product_Category_Mapping);
        }

        // GET: Product_Category_Mapping/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product_Category_Mapping/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,CategoryId")] Product_Category_Mapping product_Category_Mapping)
        {
            if (ModelState.IsValid)
            {
                db.Product_Category_Mapping.Add(product_Category_Mapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_Category_Mapping);
        }

        // GET: Product_Category_Mapping/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Category_Mapping product_Category_Mapping = db.Product_Category_Mapping.Find(id);
            if (product_Category_Mapping == null)
            {
                return HttpNotFound();
            }
            return View(product_Category_Mapping);
        }

        // POST: Product_Category_Mapping/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,CategoryId")] Product_Category_Mapping product_Category_Mapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Category_Mapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_Category_Mapping);
        }

        // GET: Product_Category_Mapping/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Category_Mapping product_Category_Mapping = db.Product_Category_Mapping.Find(id);
            if (product_Category_Mapping == null)
            {
                return HttpNotFound();
            }
            return View(product_Category_Mapping);
        }

        // POST: Product_Category_Mapping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Category_Mapping product_Category_Mapping = db.Product_Category_Mapping.Find(id);
            db.Product_Category_Mapping.Remove(product_Category_Mapping);
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
