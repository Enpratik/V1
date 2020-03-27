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
    public class PaymentBanksController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: PaymentBanks
        public ActionResult Index()
        {
            return View(db.PaymentBanks.ToList());
        }

        // GET: PaymentBanks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentBanks paymentBanks = db.PaymentBanks.Find(id);
            if (paymentBanks == null)
            {
                return HttpNotFound();
            }
            return View(paymentBanks);
        }

        // GET: PaymentBanks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentBanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PosTypeId,BankName,Is3DSecure,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] PaymentBanks paymentBanks)
        {
            if (ModelState.IsValid)
            {
                db.PaymentBanks.Add(paymentBanks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentBanks);
        }

        // GET: PaymentBanks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentBanks paymentBanks = db.PaymentBanks.Find(id);
            if (paymentBanks == null)
            {
                return HttpNotFound();
            }
            return View(paymentBanks);
        }

        // POST: PaymentBanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PosTypeId,BankName,Is3DSecure,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] PaymentBanks paymentBanks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentBanks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentBanks);
        }

        // GET: PaymentBanks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentBanks paymentBanks = db.PaymentBanks.Find(id);
            if (paymentBanks == null)
            {
                return HttpNotFound();
            }
            return View(paymentBanks);
        }

        // POST: PaymentBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentBanks paymentBanks = db.PaymentBanks.Find(id);
            db.PaymentBanks.Remove(paymentBanks);
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
