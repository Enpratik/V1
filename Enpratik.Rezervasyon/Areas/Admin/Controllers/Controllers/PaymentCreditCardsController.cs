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
    public class PaymentCreditCardsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: PaymentCreditCards
        public ActionResult Index()
        {
            return View(db.PaymentCreditCards.ToList());
        }

        // GET: PaymentCreditCards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentCreditCards paymentCreditCards = db.PaymentCreditCards.Find(id);
            if (paymentCreditCards == null)
            {
                return HttpNotFound();
            }
            return View(paymentCreditCards);
        }

        // GET: PaymentCreditCards/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentCreditCards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BankId,CreditCardName,CardIcon,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] PaymentCreditCards paymentCreditCards)
        {
            if (ModelState.IsValid)
            {
                db.PaymentCreditCards.Add(paymentCreditCards);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentCreditCards);
        }

        // GET: PaymentCreditCards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentCreditCards paymentCreditCards = db.PaymentCreditCards.Find(id);
            if (paymentCreditCards == null)
            {
                return HttpNotFound();
            }
            return View(paymentCreditCards);
        }

        // POST: PaymentCreditCards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BankId,CreditCardName,CardIcon,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] PaymentCreditCards paymentCreditCards)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentCreditCards).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentCreditCards);
        }

        // GET: PaymentCreditCards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentCreditCards paymentCreditCards = db.PaymentCreditCards.Find(id);
            if (paymentCreditCards == null)
            {
                return HttpNotFound();
            }
            return View(paymentCreditCards);
        }

        // POST: PaymentCreditCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentCreditCards paymentCreditCards = db.PaymentCreditCards.Find(id);
            db.PaymentCreditCards.Remove(paymentCreditCards);
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
