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
    public class TaxDisplayTypesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: TaxDisplayTypes
        public ActionResult Index()
        {
            return View(db.TaxDisplayType.ToList());
        }

        // GET: TaxDisplayTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxDisplayType taxDisplayType = db.TaxDisplayType.Find(id);
            if (taxDisplayType == null)
            {
                return HttpNotFound();
            }
            return View(taxDisplayType);
        }

        // GET: TaxDisplayTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaxDisplayTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DisplayType")] TaxDisplayType taxDisplayType)
        {
            if (ModelState.IsValid)
            {
                db.TaxDisplayType.Add(taxDisplayType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taxDisplayType);
        }

        // GET: TaxDisplayTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxDisplayType taxDisplayType = db.TaxDisplayType.Find(id);
            if (taxDisplayType == null)
            {
                return HttpNotFound();
            }
            return View(taxDisplayType);
        }

        // POST: TaxDisplayTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DisplayType")] TaxDisplayType taxDisplayType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taxDisplayType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taxDisplayType);
        }

        // GET: TaxDisplayTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxDisplayType taxDisplayType = db.TaxDisplayType.Find(id);
            if (taxDisplayType == null)
            {
                return HttpNotFound();
            }
            return View(taxDisplayType);
        }

        // POST: TaxDisplayTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaxDisplayType taxDisplayType = db.TaxDisplayType.Find(id);
            db.TaxDisplayType.Remove(taxDisplayType);
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
