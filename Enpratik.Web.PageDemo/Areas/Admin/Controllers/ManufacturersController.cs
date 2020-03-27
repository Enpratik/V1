using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.Data;
using Enpratik.Core;

namespace Enpratik.AdminPanel.Controllers
{
    public class ManufacturersController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Manufacturers
        public ActionResult Index()
        {
            return View(db.Manufacturers.ToList());
        }

        // GET: Manufacturers/Create
        public ActionResult Create()
        {
            getLanguages();
            return View(Manufacturers.initialize);
        }

        // POST: Manufacturers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Manufacturers manufacturers)
        {
            if (ModelState.IsValid)
            {
                if (db.Manufacturers.Any(o => o.ManufacturerName == manufacturers.ManufacturerName & o.IsActive == true))
                    Functions.SetMessageViewBag(this, "Aynı isimde kayıt zaten var!", 0);
                else
                {
                    var url = Functions.GetUrl(manufacturers.ManufacturerName);

                    if (db.WebSiteUrls.Any(w => w.LanguageId == manufacturers.LanguageId & w.UrlTypeId == 4 & w.Url == url))
                        Functions.SetMessageViewBag(this, "Aynı url sistemde kayıtlıdır!", 0);
                    else
                    {
                        int urlId = WebSiteUrls.initialize.Insert(7, url, manufacturers.LanguageId);

                        manufacturers.UrlId = urlId;
                        db.Manufacturers.Add(manufacturers);
                        db.SaveChanges();
                        Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                    }
                }                
            }

            getLanguages();
            return View(manufacturers);
        }

        // GET: Manufacturers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Manufacturers manufacturers = db.Manufacturers.Find(id);
            if (manufacturers == null)
            {
                return HttpNotFound();
            }
            getLanguages();
            return View(manufacturers);
        }

        // POST: Manufacturers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Manufacturers manufacturers)
        {
            if (ModelState.IsValid)
            {
                if (db.Manufacturers.Any(o => o.ManufacturerName == manufacturers.ManufacturerName & o.IsActive == true & o.Id!=manufacturers.Id))
                    Functions.SetMessageViewBag(this, "Aynı isimde kayıt zaten var!", 0);
                else
                {
                    db.Entry(manufacturers).State = EntityState.Modified;
                    db.SaveChanges();
                    Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                }
            }
            getLanguages();
            return View(manufacturers);
        }

        // GET: Manufacturers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacturers manufacturers = db.Manufacturers.Find(id);
            if (manufacturers == null)
            {
                return HttpNotFound();
            }
            return View(manufacturers);
        }

        // POST: Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manufacturers manufacturers = db.Manufacturers.Find(id);
            db.Manufacturers.Remove(manufacturers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void getLanguages()
        {
            var languages = db.Languages.Where(l => l.IsActive == true).ToList();
            ViewBag.Languages = new SelectList(languages, "Id", "LanguageName");
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
