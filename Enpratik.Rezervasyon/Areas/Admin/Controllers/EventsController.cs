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
    public class EventsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Admin/Events
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        // GET: Admin/Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // GET: Admin/Events/Create
        public ActionResult Create()
        {
            GetLanguages();
            return View(new Events());
        }

        private void GetLanguages()
        {
            var languages = db.Languages.Where(l => l.IsActive == true).ToList();
            ViewBag.Languages = new SelectList(languages, "Id", "LanguageName");
        }

        // POST: Admin/Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Events events, DateTime[] eventDate)
        {
            if (ModelState.IsValid)
            {
                if (eventDate == null || eventDate.Length == 0)
                {
                    Functions.SetMessageViewBag(this, "Lütfen ders tarihi giriniz!", 0);
                }
                else
                {

                    if (db.Events.Any(o => o.EventName == events.EventName & o.IsActive == true))
                        Functions.SetMessageViewBag(this, "Aynı etkinlik ismi zaten var!", 0);
                    else
                    {

                        var url = Functions.GetUrl(events.EventName);
                        if (db.WebSiteUrls.Any(w => w.LanguageId == events.LanguageId & w.UrlTypeId == 11 & w.Url == url))
                            Functions.SetMessageViewBag(this, "Aynı url sistemde kayıtlıdır!", 0);
                        else
                        {
                            int urlId = WebSiteUrls.initialize.Insert(11, url, events.LanguageId ?? 1);

                            events.UrlId = urlId;
                            events.CreatedDate = DateTime.Now;

                            db.Events.Add(events);
                            db.SaveChanges();

                            foreach (var item in eventDate)
                            {
                                if (item.Year == 1)
                                    continue;

                                EventDateMaps ed = new EventDateMaps();
                                ed.EventId = events.Id;
                                ed.EventDate = item;
                                db.EventDateMaps.Add(ed);
                                db.SaveChanges();
                            }

                            Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                        }
                    }
                }
            }
            GetLanguages();
            return View(events);
        }

        // GET: Admin/Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Events events = db.Events.Find(id);

            if (events == null)
            {
                return HttpNotFound();
            }

            GetLanguages();

            return View(events);
        }

        // POST: Admin/Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Events events, DateTime[] eventDate)
        {
            if (ModelState.IsValid)
            {
                if (eventDate == null || eventDate.Length == 0)
                {
                    Functions.SetMessageViewBag(this, "Lütfen etkinlik tarihi giriniz!", 0);
                }
                else
                {
                    if (db.Events.Any(o => o.EventName == events.EventName & o.IsActive == true & o.Id != events.Id))
                        Functions.SetMessageViewBag(this, "Aynı etkinlik ismi zaten var!", 0);
                    else
                    {

                        events.UpdatedDate = DateTime.Now;

                        db.Entry(events).State = EntityState.Modified;
                        db.SaveChanges();

                        var bc = db.EventDateMaps.Where(b => b.EventId == events.Id);
                        db.EventDateMaps.RemoveRange(bc);
                        db.SaveChanges();

                        foreach (var item in eventDate)
                        {
                            if (item.Year == 1)
                                continue;

                            EventDateMaps ed = new EventDateMaps();
                            ed.EventId = events.Id;
                            ed.EventDate = item;
                            db.EventDateMaps.Add(ed);
                            db.SaveChanges();
                        }

                        Functions.SetMessageViewBag(this, "Kayıt başarıyla eklendi!", 1, "Index");
                    }
                }
            }

            GetLanguages();
            return View(events);
        }

        // GET: Admin/Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events events = db.Events.Find(id);
            if (events == null)
            {
                return HttpNotFound();
            }
            return View(events);
        }

        // POST: Admin/Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Events events = db.Events.Find(id);
            db.Events.Remove(events);
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
