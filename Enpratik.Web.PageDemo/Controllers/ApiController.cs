using Enpratik.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enpratik.Web.PageDemo.Controllers
{
    public class ApiController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();


        // GET: Api
        public JsonResult GetWebPageSetting()
        {
            var pageSetting = db.WebPageSettings.ToList();
            return Json(pageSetting, JsonRequestBehavior.AllowGet);
        }
        // POST: Api
        [HttpPost]
        public JsonResult UpdateWebPageSetting(string dataKey, string dataValue)
        {
            try
            {
                var pageSetting = db.WebPageSettings.Where(p => p.SettingKey == dataKey).FirstOrDefault();

                if (pageSetting == null)
                {
                    return Json("0");
                }

                pageSetting.SettingValue = dataValue.Trim();
                db.Entry(pageSetting).State = EntityState.Modified;
                db.SaveChanges();

                return Json("1");
            }
            catch
            {
                return Json("0");
            }

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