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
    public class PaymentCreditCardInstallmentsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: PaymentCreditCardInstallments
        public ActionResult Index()
        {
            return View(db.PaymentCreditCardInstallments.ToList());
        }

        // GET: PaymentCreditCardInstallments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentCreditCardInstallments paymentCreditCardInstallments = db.PaymentCreditCardInstallments.Find(id);
            if (paymentCreditCardInstallments == null)
            {
                return HttpNotFound();
            }
            return View(paymentCreditCardInstallments);
        }

        // GET: PaymentCreditCardInstallments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentCreditCardInstallments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CreditCardId,InstallmentNumber,InstallmentText,BankInstallmentNumber,InstallmentRatio,DisplayOrder,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] PaymentCreditCardInstallments paymentCreditCardInstallments)
        {
            if (ModelState.IsValid)
            {
                db.PaymentCreditCardInstallments.Add(paymentCreditCardInstallments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentCreditCardInstallments);
        }

        // GET: PaymentCreditCardInstallments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentCreditCardInstallments paymentCreditCardInstallments = db.PaymentCreditCardInstallments.Find(id);
            if (paymentCreditCardInstallments == null)
            {
                return HttpNotFound();
            }
            return View(paymentCreditCardInstallments);
        }

        // POST: PaymentCreditCardInstallments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CreditCardId,InstallmentNumber,InstallmentText,BankInstallmentNumber,InstallmentRatio,DisplayOrder,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] PaymentCreditCardInstallments paymentCreditCardInstallments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentCreditCardInstallments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentCreditCardInstallments);
        }

        // GET: PaymentCreditCardInstallments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentCreditCardInstallments paymentCreditCardInstallments = db.PaymentCreditCardInstallments.Find(id);
            if (paymentCreditCardInstallments == null)
            {
                return HttpNotFound();
            }
            return View(paymentCreditCardInstallments);
        }

        // POST: PaymentCreditCardInstallments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentCreditCardInstallments paymentCreditCardInstallments = db.PaymentCreditCardInstallments.Find(id);
            db.PaymentCreditCardInstallments.Remove(paymentCreditCardInstallments);
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
