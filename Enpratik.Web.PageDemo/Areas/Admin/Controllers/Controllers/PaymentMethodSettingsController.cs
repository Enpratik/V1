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
    public class PaymentMethodSettingsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: PaymentMethodSettings
        public ActionResult Index()
        {
            return View(db.PaymentMethodSettings.ToList());
        }

        // GET: PaymentMethodSettings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethodSettings paymentMethodSettings = db.PaymentMethodSettings.Find(id);
            if (paymentMethodSettings == null)
            {
                return HttpNotFound();
            }
            return View(paymentMethodSettings);
        }

        // GET: PaymentMethodSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentMethodSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PaymentMethodId,SettingKey,SettingValue,SettingDataType,Descriptions,UpdatedUserId,UpdatedDate")] PaymentMethodSettings paymentMethodSettings)
        {
            if (ModelState.IsValid)
            {
                db.PaymentMethodSettings.Add(paymentMethodSettings);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentMethodSettings);
        }

        // GET: PaymentMethodSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethodSettings paymentMethodSettings = db.PaymentMethodSettings.Find(id);
            if (paymentMethodSettings == null)
            {
                return HttpNotFound();
            }
            return View(paymentMethodSettings);
        }

        // POST: PaymentMethodSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PaymentMethodId,SettingKey,SettingValue,SettingDataType,Descriptions,UpdatedUserId,UpdatedDate")] PaymentMethodSettings paymentMethodSettings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentMethodSettings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentMethodSettings);
        }

        // GET: PaymentMethodSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentMethodSettings paymentMethodSettings = db.PaymentMethodSettings.Find(id);
            if (paymentMethodSettings == null)
            {
                return HttpNotFound();
            }
            return View(paymentMethodSettings);
        }

        // POST: PaymentMethodSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentMethodSettings paymentMethodSettings = db.PaymentMethodSettings.Find(id);
            db.PaymentMethodSettings.Remove(paymentMethodSettings);
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
