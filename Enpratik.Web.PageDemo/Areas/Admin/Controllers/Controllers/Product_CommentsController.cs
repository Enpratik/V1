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
    public class Product_CommentsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Product_Comments
        public ActionResult Index()
        {
            return View(db.Product_Comments.ToList());
        }

        // GET: Product_Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Comments product_Comments = db.Product_Comments.Find(id);
            if (product_Comments == null)
            {
                return HttpNotFound();
            }
            return View(product_Comments);
        }

        // GET: Product_Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product_Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,CommentMessage,CreatedDate,Status,IsActive")] Product_Comments product_Comments)
        {
            if (ModelState.IsValid)
            {
                db.Product_Comments.Add(product_Comments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product_Comments);
        }

        // GET: Product_Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Comments product_Comments = db.Product_Comments.Find(id);
            if (product_Comments == null)
            {
                return HttpNotFound();
            }
            return View(product_Comments);
        }

        // POST: Product_Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,CommentMessage,CreatedDate,Status,IsActive")] Product_Comments product_Comments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Comments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product_Comments);
        }

        // GET: Product_Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Comments product_Comments = db.Product_Comments.Find(id);
            if (product_Comments == null)
            {
                return HttpNotFound();
            }
            return View(product_Comments);
        }

        // POST: Product_Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Comments product_Comments = db.Product_Comments.Find(id);
            db.Product_Comments.Remove(product_Comments);
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
