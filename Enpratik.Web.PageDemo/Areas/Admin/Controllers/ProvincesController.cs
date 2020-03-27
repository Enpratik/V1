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
    public class ProvincesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Provinces
        public ActionResult Index()
        {
            return View(db.Provinces.ToList());
        }

        // GET: Provinces/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provinces provinces = db.Provinces.Find(id);
            if (provinces == null)
            {
                return HttpNotFound();
            }
            return View(provinces);
        }

        // GET: Provinces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Provinces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CountryId,ProvinceName,IsActive")] Provinces provinces)
        {
            if (ModelState.IsValid)
            {
                db.Provinces.Add(provinces);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(provinces);
        }

        // GET: Provinces/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provinces provinces = db.Provinces.Find(id);
            if (provinces == null)
            {
                return HttpNotFound();
            }
            return View(provinces);
        }

        // POST: Provinces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CountryId,ProvinceName,IsActive")] Provinces provinces)
        {
            if (ModelState.IsValid)
            {
                db.Entry(provinces).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(provinces);
        }

        // GET: Provinces/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provinces provinces = db.Provinces.Find(id);
            if (provinces == null)
            {
                return HttpNotFound();
            }
            return View(provinces);
        }

        // POST: Provinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Provinces provinces = db.Provinces.Find(id);
            db.Provinces.Remove(provinces);
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
