using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.Data;
using Enpratik.Data.Entity;

namespace Enpratik.AdminPanel.Controllers
{
    public class BoxesController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Boxes
        public ActionResult Index()
        {
            return View(db.Boxes.ToList());
        }

        // GET: Boxes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boxes boxes = db.Boxes.Find(id);
            if (boxes == null)
            {
                return HttpNotFound();
            }
            return View(boxes);
        }

        // GET: Boxes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Boxes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Product,Tampon,ProductTitle,Regl,ReglDay,Yogunluk,YogunlukAltBaslik,Marka,IncePed,icindekiler,FiyatAylik,Fiyat3Aylik,Fiyat6Aylik,Regdate,IsActive")] Boxes boxes)
        {
            if (ModelState.IsValid)
            {
                db.Boxes.Add(boxes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boxes);
        }

        // GET: Boxes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boxes boxes = db.Boxes.Find(id);
            if (boxes == null)
            {
                return HttpNotFound();
            }
            return View(boxes);
        }

        // POST: Boxes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Product,Tampon,ProductTitle,Regl,ReglDay,Yogunluk,YogunlukAltBaslik,Marka,IncePed,icindekiler,FiyatAylik,Fiyat3Aylik,Fiyat6Aylik,Regdate,IsActive")] Boxes boxes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boxes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boxes);
        }

        // GET: Boxes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boxes boxes = db.Boxes.Find(id);
            if (boxes == null)
            {
                return HttpNotFound();
            }
            return View(boxes);
        }

        // POST: Boxes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Boxes boxes = db.Boxes.Find(id);
            db.Boxes.Remove(boxes);
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
