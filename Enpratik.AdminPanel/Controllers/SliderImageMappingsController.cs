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
using Enpratik.AdminPanel.Model;

namespace Enpratik.AdminPanel.Controllers
{
    public class SliderImageMappingsController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: SliderImageMappings
        public ActionResult Index(int? sliderId)
        {
            if (sliderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<SliderImageMappings> sliderImageList = db.SliderImageMappings.Where(s => s.SliderId == sliderId).ToList();

            if (sliderImageList == null)
            {
                return HttpNotFound();
            }


            return View(sliderImageList);
        }
        

        // GET: SliderImageMappings/Create
        public ActionResult Create(int? sliderId)
        {
            if (sliderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderImageMappings s = new SliderImageMappings();
            s.SliderId = sliderId.ToInt32();
            return View(s);
        }

        // POST: SliderImageMappings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(SliderImageMappings sliderImageMappings)
        {
            if (ModelState.IsValid)
            {
                sliderImageMappings.CreatedDate = DateTime.Now;
                sliderImageMappings.CreatedUserId = 1;
                db.SliderImageMappings.Add(sliderImageMappings);
                db.SaveChanges();
                return RedirectToAction("Index", "SliderImageMappings", new { sliderId = sliderImageMappings.SliderId});
            }

            return View(sliderImageMappings);
        }



        // GET: SliderImageMappings/Edit/5
        public ActionResult EditIframe(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderImageMappings sliderImageMappings = db.SliderImageMappings.Find(id);
            if (sliderImageMappings == null)
            {
                return HttpNotFound();
            }
            return View(sliderImageMappings);
        }

        // POST: SliderImageMappings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditIframe(SliderImageMappings sliderImageMappings)
        {
            if (ModelState.IsValid)
            {
                sliderImageMappings.CreatedDate = DateTime.Now;
                sliderImageMappings.CreatedUserId = Current.User.Id;
                sliderImageMappings.UpdatedDate = DateTime.Now;
                sliderImageMappings.UpdatedUserId = Current.User.Id;

                db.Entry(sliderImageMappings).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "SliderImageMappings", new { sliderId = sliderImageMappings.SliderId });
            }
            return View(sliderImageMappings);
        }




        // GET: SliderImageMappings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderImageMappings sliderImageMappings = db.SliderImageMappings.Find(id);
            if (sliderImageMappings == null)
            {
                return HttpNotFound();
            }
            return View(sliderImageMappings);
        }

        // POST: SliderImageMappings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(SliderImageMappings sliderImageMappings)
        {
            if (ModelState.IsValid)
            {
                sliderImageMappings.CreatedDate = DateTime.Now;
                sliderImageMappings.CreatedUserId = Current.User.Id;
                sliderImageMappings.UpdatedDate = DateTime.Now;
                sliderImageMappings.UpdatedUserId = Current.User.Id;

                db.Entry(sliderImageMappings).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "SliderImageMappings", new { sliderId = sliderImageMappings.SliderId });
            }
            return View(sliderImageMappings);
        }

        // GET: SliderImageMappings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderImageMappings sliderImageMappings = db.SliderImageMappings.Find(id);
            if (sliderImageMappings == null)
            {
                return HttpNotFound();
            }
            return View(sliderImageMappings);
        }

        // POST: SliderImageMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SliderImageMappings sliderImageMappings = db.SliderImageMappings.Find(id);
            db.SliderImageMappings.Remove(sliderImageMappings);
            db.SaveChanges();
            return RedirectToAction("Index", "SliderImageMappings", new { sliderId = sliderImageMappings.SliderId });
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
