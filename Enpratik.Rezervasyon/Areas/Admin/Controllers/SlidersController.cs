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
    public class SlidersController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Sliders
        public ActionResult Index()
        {
            return View(db.Sliders.ToList());
        }
        

        // GET: Sliders/Create
        public ActionResult Create()
        {
            return View(Sliders.initialize);
        }

        // POST: Sliders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Sliders sliders)
        {
            if (ModelState.IsValid)
            {
                db.Sliders.Add(sliders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sliders);
        }

        // GET: Sliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sliders sliders = db.Sliders.Find(id);
            if (sliders == null)
            {
                return HttpNotFound();
            }
            return View(sliders);
        }

        // POST: Sliders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Sliders sliders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sliders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sliders);
        }

        // GET: Sliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sliders sliders = db.Sliders.Find(id);
            if (sliders == null)
            {
                return HttpNotFound();
            }
            return View(sliders);
        }

        // POST: Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sliders sliders = db.Sliders.Find(id);
            db.Sliders.Remove(sliders);
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
