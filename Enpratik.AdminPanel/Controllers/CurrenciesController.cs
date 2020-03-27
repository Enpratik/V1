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
    public class CurrenciesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Currencies
        public ActionResult Index()
        {
            return View(db.Currencies.ToList());
        }

        // GET: Currencies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currencies currencies = db.Currencies.Find(id);
            if (currencies == null)
            {
                return HttpNotFound();
            }
            return View(currencies);
        }

        // GET: Currencies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Currencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CurrencyName,CurrencyCode,Rate,CustomFormatting,DisplayOrder,UpdatedDate,IsActive")] Currencies currencies)
        {
            if (ModelState.IsValid)
            {
                db.Currencies.Add(currencies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(currencies);
        }

        // GET: Currencies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currencies currencies = db.Currencies.Find(id);
            if (currencies == null)
            {
                return HttpNotFound();
            }
            return View(currencies);
        }

        // POST: Currencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CurrencyName,CurrencyCode,Rate,CustomFormatting,DisplayOrder,UpdatedDate,IsActive")] Currencies currencies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currencies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(currencies);
        }

        // GET: Currencies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currencies currencies = db.Currencies.Find(id);
            if (currencies == null)
            {
                return HttpNotFound();
            }
            return View(currencies);
        }

        // POST: Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Currencies currencies = db.Currencies.Find(id);
            db.Currencies.Remove(currencies);
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
