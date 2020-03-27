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
    public class CargoDesiGroupsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: CargoDesiGroups
        public ActionResult Index()
        {
            return View(db.CargoDesiGroups.ToList());
        }

        // GET: CargoDesiGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoDesiGroups cargoDesiGroups = db.CargoDesiGroups.Find(id);
            if (cargoDesiGroups == null)
            {
                return HttpNotFound();
            }
            return View(cargoDesiGroups);
        }

        // GET: CargoDesiGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CargoDesiGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CargoId,Defination,CountryId,ProvinceId,StartDesi,StartingPrice,DesiIncrementStage,PriceIncrementStage,EndDesi,IsPublished,DisplayOrder,LanguageId,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] CargoDesiGroups cargoDesiGroups)
        {
            if (ModelState.IsValid)
            {
                db.CargoDesiGroups.Add(cargoDesiGroups);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cargoDesiGroups);
        }

        // GET: CargoDesiGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoDesiGroups cargoDesiGroups = db.CargoDesiGroups.Find(id);
            if (cargoDesiGroups == null)
            {
                return HttpNotFound();
            }
            return View(cargoDesiGroups);
        }

        // POST: CargoDesiGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CargoId,Defination,CountryId,ProvinceId,StartDesi,StartingPrice,DesiIncrementStage,PriceIncrementStage,EndDesi,IsPublished,DisplayOrder,LanguageId,CreatedDate,CreatedUserId,UpdatedDate,UpdatedUserId,DeletedDate,DeletedUserId,IsActive")] CargoDesiGroups cargoDesiGroups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargoDesiGroups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cargoDesiGroups);
        }

        // GET: CargoDesiGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoDesiGroups cargoDesiGroups = db.CargoDesiGroups.Find(id);
            if (cargoDesiGroups == null)
            {
                return HttpNotFound();
            }
            return View(cargoDesiGroups);
        }

        // POST: CargoDesiGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CargoDesiGroups cargoDesiGroups = db.CargoDesiGroups.Find(id);
            db.CargoDesiGroups.Remove(cargoDesiGroups);
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
