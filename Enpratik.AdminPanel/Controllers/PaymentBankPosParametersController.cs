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
    public class PaymentBankPosParametersController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: PaymentBankPosParameters
        public ActionResult Index()
        {
            return View(db.PaymentBankPosParameters.ToList());
        }

        // GET: PaymentBankPosParameters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentBankPosParameters paymentBankPosParameters = db.PaymentBankPosParameters.Find(id);
            if (paymentBankPosParameters == null)
            {
                return HttpNotFound();
            }
            return View(paymentBankPosParameters);
        }

        // GET: PaymentBankPosParameters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentBankPosParameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BankId,ParameterName,ParameterValue")] PaymentBankPosParameters paymentBankPosParameters)
        {
            if (ModelState.IsValid)
            {
                db.PaymentBankPosParameters.Add(paymentBankPosParameters);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentBankPosParameters);
        }

        // GET: PaymentBankPosParameters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentBankPosParameters paymentBankPosParameters = db.PaymentBankPosParameters.Find(id);
            if (paymentBankPosParameters == null)
            {
                return HttpNotFound();
            }
            return View(paymentBankPosParameters);
        }

        // POST: PaymentBankPosParameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BankId,ParameterName,ParameterValue")] PaymentBankPosParameters paymentBankPosParameters)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentBankPosParameters).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentBankPosParameters);
        }

        // GET: PaymentBankPosParameters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentBankPosParameters paymentBankPosParameters = db.PaymentBankPosParameters.Find(id);
            if (paymentBankPosParameters == null)
            {
                return HttpNotFound();
            }
            return View(paymentBankPosParameters);
        }

        // POST: PaymentBankPosParameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentBankPosParameters paymentBankPosParameters = db.PaymentBankPosParameters.Find(id);
            db.PaymentBankPosParameters.Remove(paymentBankPosParameters);
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
