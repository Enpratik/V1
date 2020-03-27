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
    public class PaymentBankAccountsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: PaymentBankAccounts
        public ActionResult Index()
        {
            return View(db.PaymentBankAccount.ToList());
        }

        // GET: PaymentBankAccounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentBankAccount paymentBankAccount = db.PaymentBankAccount.Find(id);
            if (paymentBankAccount == null)
            {
                return HttpNotFound();
            }
            return View(paymentBankAccount);
        }

        // GET: PaymentBankAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentBankAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CurrencyId,BankName,BranchName,AccountNumber,Iban,AccountName,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] PaymentBankAccount paymentBankAccount)
        {
            if (ModelState.IsValid)
            {
                db.PaymentBankAccount.Add(paymentBankAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentBankAccount);
        }

        // GET: PaymentBankAccounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentBankAccount paymentBankAccount = db.PaymentBankAccount.Find(id);
            if (paymentBankAccount == null)
            {
                return HttpNotFound();
            }
            return View(paymentBankAccount);
        }

        // POST: PaymentBankAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CurrencyId,BankName,BranchName,AccountNumber,Iban,AccountName,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] PaymentBankAccount paymentBankAccount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentBankAccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentBankAccount);
        }

        // GET: PaymentBankAccounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentBankAccount paymentBankAccount = db.PaymentBankAccount.Find(id);
            if (paymentBankAccount == null)
            {
                return HttpNotFound();
            }
            return View(paymentBankAccount);
        }

        // POST: PaymentBankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentBankAccount paymentBankAccount = db.PaymentBankAccount.Find(id);
            db.PaymentBankAccount.Remove(paymentBankAccount);
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
