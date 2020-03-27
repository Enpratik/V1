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
    public class WidgetZonesMappingsController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        private void GetWidgetZones()
        {
            var widgetZones = db.WidgetZones.Where(a => a.IsActive == true).ToList();
            widgetZones.Insert(0, new WidgetZones() { Id = 0, WidgetZoneName = "-- Sayfa Seçiniz --" });
            ViewBag.WidgetZones = new SelectList(widgetZones, "Id", "WidgetZoneName");
        }

        // GET: WidgetZonesMappings
        public ActionResult Index()
        {
            GetWidgetZones();

            List<WidgetZonesMappingDTO> data = new List<WidgetZonesMappingDTO>();

            return View(data);
        }


        [HttpPost]
        public ActionResult Index(string WidgetZoneId)
        {
            GetWidgetZones();
            List<WidgetZonesMappingDTO> data;

            if (string.IsNullOrEmpty(WidgetZoneId) || WidgetZoneId.Equals("0"))
            {
                Functions.SetMessageViewBag(this, "Lütfen sayfa seçiniz!", 0);
                data = new List<WidgetZonesMappingDTO>();

                return View(data);
            }
            int widgetZoneId = WidgetZoneId.ToInt32();
            data = (from wzm in db.WidgetZonesMapping
                    join w in db.Widgets on wzm.WidgetId equals w.Id
                    join wz in db.WidgetZones on wzm.WidgetZoneId equals wz.Id
                    where wzm.WidgetZoneId == widgetZoneId
                    select new WidgetZonesMappingDTO
                    {
                        Id = wzm.Id,
                        WidgetZoneId = wzm.WidgetZoneId,
                        WidgetZoneName = wz.WidgetZoneName,
                        WidgetId = wzm.WidgetId,
                        WidgetName = w.WidgetName,
                        OrderIndex = wzm.OrderIndex,
                        IsActive = wzm.IsActive
                    }).OrderBy(o => o.OrderIndex).ToList();

            return View(data);
        }

        // GET: WidgetZonesMappings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WidgetZonesMapping widgetZonesMapping = db.WidgetZonesMapping.Find(id);
            if (widgetZonesMapping == null)
            {
                return HttpNotFound();
            }
            return View(widgetZonesMapping);
        }

        // GET: WidgetZonesMappings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WidgetZonesMappings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,WidgetZoneId,WidgetId,OrderIndex,IsActive")] WidgetZonesMapping widgetZonesMapping)
        {
            if (ModelState.IsValid)
            {
                db.WidgetZonesMapping.Add(widgetZonesMapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(widgetZonesMapping);
        }

        // GET: WidgetZonesMappings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WidgetZonesMapping widgetZonesMapping = db.WidgetZonesMapping.Find(id);
            if (widgetZonesMapping == null)
            {
                return HttpNotFound();
            }
            return View(widgetZonesMapping);
        }

        // POST: WidgetZonesMappings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WidgetZoneId,WidgetId,OrderIndex,IsActive")] WidgetZonesMapping widgetZonesMapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(widgetZonesMapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(widgetZonesMapping);
        }

        // GET: WidgetZonesMappings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WidgetZonesMapping widgetZonesMapping = db.WidgetZonesMapping.Find(id);
            if (widgetZonesMapping == null)
            {
                return HttpNotFound();
            }
            return View(widgetZonesMapping);
        }

        // POST: WidgetZonesMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WidgetZonesMapping widgetZonesMapping = db.WidgetZonesMapping.Find(id);
            db.WidgetZonesMapping.Remove(widgetZonesMapping);
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
