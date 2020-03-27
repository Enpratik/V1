using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.Core;
using Enpratik.Data;

namespace Enpratik.AdminPanel.Controllers
{
    public class NewsAuthorsController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: NewsAuthors
        public ActionResult Index()
        {
            return View(db.NewsAuthors.ToList());
        }
        

        // GET: NewsAuthors/Create
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NewsAuthors newsAuthors)
        {
            if (ModelState.IsValid)
            {
                if (db.NewsAuthors.Any(o => o.AuthorName == newsAuthors.AuthorName))
                    Functions.SetMessageViewBag(this, "Aynı yazar ismi zaten var!", 0);
                else
                {
                    var url = Functions.GetUrl(newsAuthors.AuthorName);
                    newsAuthors.Url = url;
                    db.NewsAuthors.Add(newsAuthors);
                    db.SaveChanges();
                    Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                }
            }

            return View(newsAuthors);
        }

        // GET: NewsAuthors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            NewsAuthors newsAuthors = db.NewsAuthors.Find(id);
            if (newsAuthors == null)
            {
                return HttpNotFound();
            }
            return View(newsAuthors);
        }

        // POST: NewsAuthors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AuthorName,AuthorInfo,AuthorImages,Email,Url")] NewsAuthors newsAuthors)
        {
            if (ModelState.IsValid)
            {
                if (db.NewsAuthors.Any(o => o.AuthorName == newsAuthors.AuthorName & o.Id!=newsAuthors.Id))
                    Functions.SetMessageViewBag(this, "Aynı yazar ismi zaten var!", 0);
                else
                {
                    var url = Functions.GetUrl(newsAuthors.AuthorName);
                    db.Entry(newsAuthors).State = EntityState.Modified;
                    db.SaveChanges();
                    Functions.SetMessageViewBag(this, "Kayıt başarıyla güncellendi!", 1, "Index");
                }
            }
            return View(newsAuthors);
        }

        // GET: NewsAuthors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsAuthors newsAuthors = db.NewsAuthors.Find(id);
            if (newsAuthors == null)
            {
                return HttpNotFound();
            }
            return View(newsAuthors);
        }

        // POST: NewsAuthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsAuthors newsAuthors = db.NewsAuthors.Find(id);
            db.NewsAuthors.Remove(newsAuthors);
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
