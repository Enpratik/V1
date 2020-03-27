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
    public class CustomersRolesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: CustomersRoles
        public ActionResult Index()
        {
            return View(db.CustomersRole.ToList());
        }

        // GET: CustomersRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersRole customersRole = db.CustomersRole.Find(id);
            if (customersRole == null)
            {
                return HttpNotFound();
            }
            return View(customersRole);
        }

        // GET: CustomersRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,SystemName,FreeShipping,TaxDisplayTypeId,TaxExempt,IsActive")] CustomersRole customersRole)
        {
            if (ModelState.IsValid)
            {
                db.CustomersRole.Add(customersRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customersRole);
        }

        // GET: CustomersRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersRole customersRole = db.CustomersRole.Find(id);
            if (customersRole == null)
            {
                return HttpNotFound();
            }
            return View(customersRole);
        }

        // POST: CustomersRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,SystemName,FreeShipping,TaxDisplayTypeId,TaxExempt,IsActive")] CustomersRole customersRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customersRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customersRole);
        }

        // GET: CustomersRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersRole customersRole = db.CustomersRole.Find(id);
            if (customersRole == null)
            {
                return HttpNotFound();
            }
            return View(customersRole);
        }

        // POST: CustomersRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomersRole customersRole = db.CustomersRole.Find(id);
            db.CustomersRole.Remove(customersRole);
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
