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
    public class PromotionsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Promotions
        public ActionResult Index()
        {
            return View(db.Promotions.ToList());
        }

        // GET: Promotions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotions promotions = db.Promotions.Find(id);
            if (promotions == null)
            {
                return HttpNotFound();
            }
            return View(promotions);
        }

        // GET: Promotions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Promotions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DiscountTypeId,UsePercentage,DiscountPercentage,DiscountAmount,StartDate,EndDate,RequiresCouponCode,CouponCode,DiscountLimitationId,LimitationTimes")] Promotions promotions)
        {
            if (ModelState.IsValid)
            {
                db.Promotions.Add(promotions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(promotions);
        }

        // GET: Promotions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotions promotions = db.Promotions.Find(id);
            if (promotions == null)
            {
                return HttpNotFound();
            }
            return View(promotions);
        }

        // POST: Promotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DiscountTypeId,UsePercentage,DiscountPercentage,DiscountAmount,StartDate,EndDate,RequiresCouponCode,CouponCode,DiscountLimitationId,LimitationTimes")] Promotions promotions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promotions);
        }

        // GET: Promotions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotions promotions = db.Promotions.Find(id);
            if (promotions == null)
            {
                return HttpNotFound();
            }
            return View(promotions);
        }

        // POST: Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promotions promotions = db.Promotions.Find(id);
            db.Promotions.Remove(promotions);
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
