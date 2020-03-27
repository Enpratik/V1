using Enpratik.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Enpratik.Web.PageDemo.Controllers
{
    public class BlogController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Blog
        public ActionResult Index()
        {
            var blogs = db.Blogs.Where(b => b.IsActive == true && b.IsPublished == true).ToList();
            ViewBag.BlogCategories = db.Blog_Categories.Where(c => c.IsActive == true).ToList();

            return View(blogs);
        }
        // GET: Blog
        public ActionResult Detail(int? id, string blogUrl)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(db.Blogs.Where(b => b.IsActive == true && b.IsPublished == true && b.Id == id).FirstOrDefault());
        }


        [HttpPost]
        public JsonResult UpdateWebPageSetting(string dataKey, string dataValue, int id)
        {
            try
            {

                var blog = db.Blogs.Find(id);

                switch (dataKey)
                {
                    case "blog_title":
                        blog.BlogTitle = dataValue.Trim();
                        break;
                    case "blog_images":
                        string matchString = Regex.Match(dataValue.Trim(), "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
                        blog.BlogImages = matchString;
                        break;
                    case "blog_shortdescription":
                        blog.ShortDescription = dataValue.Trim();
                        break;
                    case "blog_fulldescription":
                        blog.FullDescription = dataValue.Trim();
                        break;
                    default:
                        break;
                }

                db.Entry(blog).State = EntityState.Modified;
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