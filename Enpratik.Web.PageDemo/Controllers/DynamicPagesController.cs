using Enpratik.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Enpratik.Web.PageDemo.Controllers
{
    public class DynamicPagesController: Controller
    {

        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Default
        public ActionResult Index(string permalink)
        {

            var webSiteUrl = db.WebSiteUrls.Where(w => w.Url == permalink).FirstOrDefault();

            if (webSiteUrl == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            
            var pages = db.DynamicPages.Where(p => p.UrlId == webSiteUrl.Id).FirstOrDefault();

            if (pages == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            
            return View(pages);
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