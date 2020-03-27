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
    public class CustomersAddressesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: CustomersAddresses
        public ActionResult Index()
        {
            return View(db.CustomersAddress.ToList());
        }

        // GET: CustomersAddresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersAddress customersAddress = db.CustomersAddress.Find(id);
            if (customersAddress == null)
            {
                return HttpNotFound();
            }
            return View(customersAddress);
        }

        // GET: CustomersAddresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,AdressType,InvoiceType,IdentityNumber,TaxNumber,AddressTitle,CompanyName,FirstName,LastName,Adres,ZipCode,CountryId,ProvinceId,DistinctId,Telephone,CreatedDate,IsActive")] CustomersAddress customersAddress)
        {
            if (ModelState.IsValid)
            {
                db.CustomersAddress.Add(customersAddress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customersAddress);
        }

        // GET: CustomersAddresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersAddress customersAddress = db.CustomersAddress.Find(id);
            if (customersAddress == null)
            {
                return HttpNotFound();
            }
            return View(customersAddress);
        }

        // POST: CustomersAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,AdressType,InvoiceType,IdentityNumber,TaxNumber,AddressTitle,CompanyName,FirstName,LastName,Adres,ZipCode,CountryId,ProvinceId,DistinctId,Telephone,CreatedDate,IsActive")] CustomersAddress customersAddress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customersAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customersAddress);
        }

        // GET: CustomersAddresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomersAddress customersAddress = db.CustomersAddress.Find(id);
            if (customersAddress == null)
            {
                return HttpNotFound();
            }
            return View(customersAddress);
        }

        // POST: CustomersAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomersAddress customersAddress = db.CustomersAddress.Find(id);
            db.CustomersAddress.Remove(customersAddress);
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
