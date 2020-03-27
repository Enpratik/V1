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
    public class CargoCompaniesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: CargoCompanies
        public ActionResult Index()
        {
            return View(db.CargoCompanies.ToList());
        }

        // GET: CargoCompanies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoCompanies cargoCompanies = db.CargoCompanies.Find(id);
            if (cargoCompanies == null)
            {
                return HttpNotFound();
            }
            return View(cargoCompanies);
        }

        // GET: CargoCompanies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CargoCompanies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CargoName,CargoIconUrl,IsPayAtTheDoor,IsPayAtTheDoorKK,LanguageId,IsPublished,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] CargoCompanies cargoCompanies)
        {
            if (ModelState.IsValid)
            {
                db.CargoCompanies.Add(cargoCompanies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cargoCompanies);
        }

        // GET: CargoCompanies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoCompanies cargoCompanies = db.CargoCompanies.Find(id);
            if (cargoCompanies == null)
            {
                return HttpNotFound();
            }
            return View(cargoCompanies);
        }

        // POST: CargoCompanies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CargoName,CargoIconUrl,IsPayAtTheDoor,IsPayAtTheDoorKK,LanguageId,IsPublished,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] CargoCompanies cargoCompanies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargoCompanies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cargoCompanies);
        }

        // GET: CargoCompanies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoCompanies cargoCompanies = db.CargoCompanies.Find(id);
            if (cargoCompanies == null)
            {
                return HttpNotFound();
            }
            return View(cargoCompanies);
        }

        // POST: CargoCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CargoCompanies cargoCompanies = db.CargoCompanies.Find(id);
            db.CargoCompanies.Remove(cargoCompanies);
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
