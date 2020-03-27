using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.Data;
using System.Web.Script.Serialization;
using Enpratik.Core;

namespace Enpratik.AdminPanel.Controllers
{
    public class WidgetZonesParamMappingsController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: WidgetZonesParamMappings
        public ActionResult Index()
        {
            return View(db.WidgetZonesParamMapping.ToList());
        }

        // GET: WidgetZonesParamMappings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WidgetZonesParamMapping widgetZonesParamMapping = db.WidgetZonesParamMapping.Find(id);
            if (widgetZonesParamMapping == null)
            {
                return HttpNotFound();
            }
            return View(widgetZonesParamMapping);
        }

        // GET: WidgetZonesParamMappings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WidgetZonesParamMappings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( WidgetZonesParamMapping widgetZonesParamMapping)
        {
            if (ModelState.IsValid)
            {
                db.WidgetZonesParamMapping.Add(widgetZonesParamMapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(widgetZonesParamMapping);
        }

        // GET: WidgetZonesParamMappings/Edit/5
        public ActionResult Edit(int? id, int? widgetId)
        {
            if (Session["widget_edit_message"] != null)
            {
                ViewBag.Message = Session["widget_edit_message"].ToString();
                Session.Remove("widget_edit_message");
            }

            if (id == null && widgetId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<WidgetZonesParamMappingDTO> data = new List<WidgetZonesParamMappingDTO>();

            var mappingData = db.WidgetZonesParamMapping.Where(w => w.WidgetId == widgetId && w.WidgetZoneMappingId == id).FirstOrDefault();

            if (mappingData == null)
            {
                data = (from c in db.WidgetParams
                        join w in db.Widgets on c.WidgetId equals w.Id
                        where c.WidgetId == widgetId
                        select new WidgetZonesParamMappingDTO
                        {
                            Id = 0,
                            WidgetZoneMappingId = (int)id,
                            WidgetId = c.WidgetId,
                            IsControlWidget = w.IsControlWidget,
                            ParameterDescription = c.ParameterDescription,
                            ParameterName = c.ParameterName,
                            ParameterValue = c.ParameterValue
                            }).ToList();

                return View(data);
            }
            
            string jsonParam = mappingData.ParamJson;

            if (string.IsNullOrEmpty(jsonParam))
            {
                data = (from c in db.WidgetParams
                        join w in db.Widgets on c.WidgetId equals w.Id
                        where c.WidgetId == widgetId
                        select new WidgetZonesParamMappingDTO
                        {
                            Id = 0,
                            WidgetZoneMappingId = (int)id,
                            WidgetId = c.WidgetId,
                            IsControlWidget = w.IsControlWidget,
                            ParameterDescription = c.ParameterDescription,
                            ParameterName = c.ParameterName,
                            ParameterValue = c.ParameterValue
                        }).ToList();

                return View(data);
            }

            List<ParamJsonDTO> paramJsonList = new JavaScriptSerializer().Deserialize<List<ParamJsonDTO>>(jsonParam);

            data = new List<WidgetZonesParamMappingDTO>();

            var widgetParams = db.WidgetParams.Where(w => w.WidgetId == widgetId & w.IsActive == true).ToList();

            foreach (var item in widgetParams)
            {
                string name = "", value = "";
                name = item.ParameterName;
                value = item.ParameterValue;

                foreach (var item1 in paramJsonList)
                {
                    if (!item.ParameterName.Equals(item1.ParameterName))
                        continue;

                    value = item1.ParameterValue;
                }

                bool isControlWidget = db.Widgets.Where(w => w.Id == widgetId).Select(w=>w.IsControlWidget).FirstOrDefault();

                data.Add(new WidgetZonesParamMappingDTO()
                {
                    Id = 0,
                    WidgetId = widgetId.ToInt32(),
                    WidgetZoneMappingId = id.ToInt32(),
                    IsControlWidget = isControlWidget,
                    ParameterDescription = item.ParameterDescription,
                    ParameterName = name,
                    ParameterValue = value
                });
            }


            return View(data);
        }
        

        // POST: WidgetZonesParamMappings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(List<Enpratik.Data.WidgetZonesParamMappingDTO> widgetZonesParamMapping, int? id, int? widgetId)
        {

            if (id == null && widgetId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                List<ParamJsonDTO> jsonParamList = new List<ParamJsonDTO>();
                foreach (string key in Request.Form.AllKeys)
                {
                    if (key.StartsWith("txt"))
                    {
                        jsonParamList.Add(new ParamJsonDTO() {
                            ParameterName = key.Replace("txt", ""),
                            ParameterValue = Request.Form[key].Replace("../", "").Replace("Upload", "/admin/Upload")
                        });
                    }
                }

                // önce kaydı sil
                WidgetZonesParamMapping w = db.WidgetZonesParamMapping.Where(wz => wz.WidgetZoneMappingId == id).FirstOrDefault();
                if (w != null)
                {
                    w.ParamJson = new JavaScriptSerializer().Serialize(jsonParamList);
                    db.Entry(w).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    w = new WidgetZonesParamMapping();

                    w.WidgetId = widgetId.ToInt32();
                    w.WidgetZoneMappingId = id.ToInt32();
                    w.ParamJson = new JavaScriptSerializer().Serialize(jsonParamList);
                    db.WidgetZonesParamMapping.Add(w);
                    db.SaveChanges();
                }
            }

            Session["widget_edit_message"]= "Kayıt güncellenmiştir.";
            
            return RedirectToAction("Edit", new { id = id, widgetId = widgetId });
        }

        // GET: WidgetZonesParamMappings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WidgetZonesParamMapping widgetZonesParamMapping = db.WidgetZonesParamMapping.Find(id);
            if (widgetZonesParamMapping == null)
            {
                return HttpNotFound();
            }
            return View(widgetZonesParamMapping);
        }

        // POST: WidgetZonesParamMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WidgetZonesParamMapping widgetZonesParamMapping = db.WidgetZonesParamMapping.Find(id);
            db.WidgetZonesParamMapping.Remove(widgetZonesParamMapping);
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
