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
    public class PromotionsRequirementTypesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: PromotionsRequirementTypes
        public ActionResult Index()
        {
            return View(db.PromotionsRequirementType.ToList());
        }

        // GET: PromotionsRequirementTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsRequirementType promotionsRequirementType = db.PromotionsRequirementType.Find(id);
            if (promotionsRequirementType == null)
            {
                return HttpNotFound();
            }
            return View(promotionsRequirementType);
        }

        // GET: PromotionsRequirementTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PromotionsRequirementTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RequirementType")] PromotionsRequirementType promotionsRequirementType)
        {
            if (ModelState.IsValid)
            {
                db.PromotionsRequirementType.Add(promotionsRequirementType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(promotionsRequirementType);
        }

        // GET: PromotionsRequirementTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsRequirementType promotionsRequirementType = db.PromotionsRequirementType.Find(id);
            if (promotionsRequirementType == null)
            {
                return HttpNotFound();
            }
            return View(promotionsRequirementType);
        }

        // POST: PromotionsRequirementTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RequirementType")] PromotionsRequirementType promotionsRequirementType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotionsRequirementType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promotionsRequirementType);
        }

        // GET: PromotionsRequirementTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsRequirementType promotionsRequirementType = db.PromotionsRequirementType.Find(id);
            if (promotionsRequirementType == null)
            {
                return HttpNotFound();
            }
            return View(promotionsRequirementType);
        }

        // POST: PromotionsRequirementTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PromotionsRequirementType promotionsRequirementType = db.PromotionsRequirementType.Find(id);
            db.PromotionsRequirementType.Remove(promotionsRequirementType);
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
