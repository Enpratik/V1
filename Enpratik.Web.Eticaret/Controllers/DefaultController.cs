using Enpratik.Core;
using Enpratik.Data;
using Enpratik.Web.Eticaret.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Enpratik.Web.Eticaret.Controllers
{
    public class DefaultController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Default
        public ActionResult Index()
        {
            if(string.IsNullOrEmpty(Request["s"]))
                return View();


            return RedirectToAction("Search", new { s = Request["s"] });

        }
        
        // GET: Search
        public ActionResult Search(string s)
        {

            SearchModel search = new SearchModel();

            List<Products> products = new List<Products>();
            List<DynamicPages> pages = new List<DynamicPages>();
            List<Blogs> blogs = new List<Blogs>();


            products = db.Products.Where(n => n.IsActive == true & n.Published == true & (n.ProductName.Contains(s) || n.ShortDescription.Contains(s))).OrderByDescending(n => n.CreatedDate).ToList();

            pages = db.DynamicPages.Where(n => n.IsActive == true & n.IsPublished == true & (n.PageName.Contains(s) || n.PageContent.Contains(s))).OrderByDescending(n => n.CreatedDate).ToList();

            blogs = db.Blogs.Where(n => n.IsActive == true & n.IsPublished == true & (n.BlogTitle.Contains(s) || n.FullDescription.Contains(s))).OrderByDescending(n => n.CreatedDate).ToList();

            search.blogs = blogs;
            search.pages = pages;
            search.product = products;
            
            return View(search);
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