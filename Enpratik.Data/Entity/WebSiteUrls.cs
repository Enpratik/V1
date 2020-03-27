using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enpratik.Data
{
    public class WebSiteUrls
    {
        public int Id { get; set; }
        public int UrlTypeId { get; set; }
        public int LanguageId { get; set; }
        public string Url { get; set; }

        public static WebSiteUrls initialize = new WebSiteUrls();

        public int Insert(int urlTypeId, string url, int languageId) {
            EnPratik_DataHelper db = new EnPratik_DataHelper();
            WebSiteUrls w = new WebSiteUrls();
            w.UrlTypeId = urlTypeId;
            w.Url = url;
            w.LanguageId = languageId;
            db.WebSiteUrls.Add(w);
            db.SaveChanges();
            return w.Id;
        }
        public int Update(int id, int urlTypeId, string url, int languageId)
        {
            EnPratik_DataHelper db = new EnPratik_DataHelper();
            WebSiteUrls w = new WebSiteUrls();
            w.Id = id;
            w.UrlTypeId = urlTypeId;
            w.Url = url;
            w.LanguageId = languageId;
            db.WebSiteUrls.Add(w);
            db.Entry(w).State = EntityState.Modified;
            db.SaveChanges();
            return w.Id;
        }
    }
}
