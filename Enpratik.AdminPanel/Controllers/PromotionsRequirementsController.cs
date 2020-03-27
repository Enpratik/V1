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
    public class PromotionsRequirementsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: PromotionsRequirements
        public ActionResult Index()
        {
            return View(db.PromotionsRequirements.ToList());
        }

        // GET: PromotionsRequirements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsRequirements promotionsRequirements = db.PromotionsRequirements.Find(id);
            if (promotionsRequirements == null)
            {
                return HttpNotFound();
            }
            return View(promotionsRequirements);
        }

        // GET: PromotionsRequirements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PromotionsRequirements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RequirementTypeId,CustomerRoleId,BillingCountryId,ShippingCountryId,RestrictedProducts,AmountSpent,PaymentMethodIds,CargoCompanyIds,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] PromotionsRequirements promotionsRequirements)
        {
            if (ModelState.IsValid)
            {
                db.PromotionsRequirements.Add(promotionsRequirements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(promotionsRequirements);
        }

        // GET: PromotionsRequirements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsRequirements promotionsRequirements = db.PromotionsRequirements.Find(id);
            if (promotionsRequirements == null)
            {
                return HttpNotFound();
            }
            return View(promotionsRequirements);
        }

        // POST: PromotionsRequirements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RequirementTypeId,CustomerRoleId,BillingCountryId,ShippingCountryId,RestrictedProducts,AmountSpent,PaymentMethodIds,CargoCompanyIds,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] PromotionsRequirements promotionsRequirements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotionsRequirements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promotionsRequirements);
        }

        // GET: PromotionsRequirements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsRequirements promotionsRequirements = db.PromotionsRequirements.Find(id);
            if (promotionsRequirements == null)
            {
                return HttpNotFound();
            }
            return View(promotionsRequirements);
        }

        // POST: PromotionsRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PromotionsRequirements promotionsRequirements = db.PromotionsRequirements.Find(id);
            db.PromotionsRequirements.Remove(promotionsRequirements);
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
