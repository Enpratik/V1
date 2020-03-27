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
    public class BrandsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Brands
        public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }

        // GET: Brands/Create
        public ActionResult Create()
        {
            getLanguages();
            return View(Brands.initialize);
        }

        // POST: Brands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brands brands)
        {
            if (ModelState.IsValid)
            {
                if (db.Brands.Any(o => o.BrandName == brands.BrandName & o.IsActive == true))
                    Functions.SetMessageViewBag(this, "Aynı isim daha önce eklenmiştir!", 0);
                else
                {
                    var url = Functions.GetUrl(brands.BrandName);

                    if (db.WebSiteUrls.Any(w => w.LanguageId == brands.LanguageId & w.UrlTypeId == 3 & w.Url == url))
                        Functions.SetMessageViewBag(this, "Aynı url sistemde kayıtlıdır!", 0);
                    else
                    {
                        int urlId = WebSiteUrls.initialize.Insert(3, url, brands.LanguageId);

                        brands.UrlId = urlId;
                        db.Brands.Add(brands);
                        db.SaveChanges();
                        Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                    }
                }
            }
            getLanguages();
            return View(brands);
        }

        // GET: Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Brands brands = db.Brands.Find(id);
            if (brands == null)
            {
                return HttpNotFound();
            }
            getLanguages();
            return View(brands);
        }

        // POST: Brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brands brands)
        {
            if (ModelState.IsValid)
            {

                if (db.Brands.Any(o => o.BrandName == brands.BrandName & o.IsActive == true & o.Id != brands.Id))
                    Functions.SetMessageViewBag(this, "Aynı isim daha önce eklenmiştir!", 0);
                else
                {
                    db.Entry(brands).State = EntityState.Modified;
                    db.SaveChanges();
                    Functions.SetMessageViewBag(this, "Kayıt başarıyla güncellendi!", 1, "Index");
                }
            }
            getLanguages();
            return View(brands);
        }

        // GET: Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brands brands = db.Brands.Find(id);
            if (brands == null)
            {
                return HttpNotFound();
            }
            return View(brands);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brands brands = db.Brands.Find(id);
            db.Brands.Remove(brands);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void getLanguages() {
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
