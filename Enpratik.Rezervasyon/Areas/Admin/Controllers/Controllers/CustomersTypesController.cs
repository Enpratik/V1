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
    public class CustomersTypesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: CustomersTypes
        public ActionResult Index()
        {
            return View(db.CustomersType.ToList());
        }

        // GET: CustomersTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersType customersType = db.CustomersType.Find(id);
            if (customersType == null)
            {
                return HttpNotFound();
            }
            return View(customersType);
        }

        // GET: CustomersTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeName")] CustomersType customersType)
        {
            if (ModelState.IsValid)
            {
                db.CustomersType.Add(customersType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customersType);
        }

        // GET: CustomersTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersType customersType = db.CustomersType.Find(id);
            if (customersType == null)
            {
                return HttpNotFound();
            }
            return View(customersType);
        }

        // POST: CustomersTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeName")] CustomersType customersType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customersType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customersType);
        }

        // GET: CustomersTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersType customersType = db.CustomersType.Find(id);
            if (customersType == null)
            {
                return HttpNotFound();
            }
            return View(customersType);
        }

        // POST: CustomersTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomersType customersType = db.CustomersType.Find(id);
            db.CustomersType.Remove(customersType);
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
