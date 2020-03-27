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
    public class MoneyPointsSettingsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: MoneyPointsSettings
        public ActionResult Index()
        {
            return View(db.MoneyPointsSettings.ToList());
        }

        // GET: MoneyPointsSettings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoneyPointsSettings moneyPointsSettings = db.MoneyPointsSettings.Find(id);
            if (moneyPointsSettings == null)
            {
                return HttpNotFound();
            }
            return View(moneyPointsSettings);
        }

        // GET: MoneyPointsSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MoneyPointsSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExchangeRate,RewardPoint,RoundDownPoints,PointsForRegistration,PointsForAProductReview,SpentWillEarn,EarnedPoints,AwardedOrderStatus,CanceledOrderStatus,UpdatedUserId,UpdatedDate,IsActive")] MoneyPointsSettings moneyPointsSettings)
        {
            if (ModelState.IsValid)
            {
                db.MoneyPointsSettings.Add(moneyPointsSettings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(moneyPointsSettings);
        }

        // GET: MoneyPointsSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoneyPointsSettings moneyPointsSettings = db.MoneyPointsSettings.Find(id);
            if (moneyPointsSettings == null)
            {
                return HttpNotFound();
            }
            return View(moneyPointsSettings);
        }

        // POST: MoneyPointsSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExchangeRate,RewardPoint,RoundDownPoints,PointsForRegistration,PointsForAProductReview,SpentWillEarn,EarnedPoints,AwardedOrderStatus,CanceledOrderStatus,UpdatedUserId,UpdatedDate,IsActive")] MoneyPointsSettings moneyPointsSettings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moneyPointsSettings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(moneyPointsSettings);
        }

        // GET: MoneyPointsSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MoneyPointsSettings moneyPointsSettings = db.MoneyPointsSettings.Find(id);
            if (moneyPointsSettings == null)
            {
                return HttpNotFound();
            }
            return View(moneyPointsSettings);
        }

        // POST: MoneyPointsSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MoneyPointsSettings moneyPointsSettings = db.MoneyPointsSettings.Find(id);
            db.MoneyPointsSettings.Remove(moneyPointsSettings);
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
