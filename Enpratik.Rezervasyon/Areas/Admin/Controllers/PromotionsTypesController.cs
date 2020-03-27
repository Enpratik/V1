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
    public class PromotionsTypesController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: PromotionsTypes
        public ActionResult Index()
        {
            return View(db.PromotionsType.ToList());
        }

        // GET: PromotionsTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsType promotionsType = db.PromotionsType.Find(id);
            if (promotionsType == null)
            {
                return HttpNotFound();
            }
            return View(promotionsType);
        }

        // GET: PromotionsTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PromotionsTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DiscountType")] PromotionsType promotionsType)
        {
            if (ModelState.IsValid)
            {
                db.PromotionsType.Add(promotionsType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(promotionsType);
        }

        // GET: PromotionsTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsType promotionsType = db.PromotionsType.Find(id);
            if (promotionsType == null)
            {
                return HttpNotFound();
            }
            return View(promotionsType);
        }

        // POST: PromotionsTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DiscountType")] PromotionsType promotionsType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotionsType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promotionsType);
        }

        // GET: PromotionsTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsType promotionsType = db.PromotionsType.Find(id);
            if (promotionsType == null)
            {
                return HttpNotFound();
            }
            return View(promotionsType);
        }

        // POST: PromotionsTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PromotionsType promotionsType = db.PromotionsType.Find(id);
            db.PromotionsType.Remove(promotionsType);
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
