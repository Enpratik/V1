using Enpratik.Data;
using Enpratik.Web.Eticaret.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Enpratik.Web.Eticaret.Controllers
{
    public class NewsController : Controller
    {
        EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Blog
        public ActionResult Index()
        {
            return View(db.News.Where(b => b.IsActive == true && b.IsPublished==true).ToList());
        }
        // GET: Blog
        public ActionResult Detail(int? id, string newsUrl)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var data = db.News.Where(b => b.IsActive == true && b.Id == id).FirstOrDefault();

            return View(data);
        }
        // POST: Blog
        [HttpPost]
        public ActionResult Detail(int? id, string newsUrl, Blog_Comments comment)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var data = db.News.Where(b => b.IsActive == true && b.Id == id).FirstOrDefault();

            if (string.IsNullOrEmpty(comment.CommentMessage))
            {
                ViewBag.Message = "Lütfen yorum alanını doldurunuz!";
                ViewBag.MessageType = 0;
                return View(data);
            }

            //comment.BlogId = (int)id;
            //comment.CustomerId = Current.User.Id;
            //db.Blog_Comments.Add(comment);
            //db.SaveChanges();

            ViewBag.Message = "Yorum kaldedildi!";
            ViewBag.MessageType = 1;

            return View(data);
        }


        int SayfaBoyutu = 12;

        // GET: Blog
        public ActionResult CategoryList(string categoryUrl, int? sayfano)
        {
            List<News> haberler = null;

            var wId = db.WebSiteUrls.Where(w => w.Url == categoryUrl & w.UrlTypeId == 9).Select(w => w.Id).FirstOrDefault();
           
            var newsCategory= db.News_Categories.Where(b => b.UrlId == wId).FirstOrDefault();
            ViewBag.CategoryName = newsCategory.NewsCategoryName;

            if (sayfano == null)
            {
                haberler = (from b in db.News
                            join c in db.News_CategoryMapping on b.Id equals c.NewsId
                            where c.NewsCategoryId == newsCategory.Id & c.IsActive == true & b.IsActive == true & b.IsPublished == true
                            select b).OrderByDescending(n => n.CreatedDate).Take(SayfaBoyutu).ToList();

            }
            else
            {
                haberler = (from b in db.News
                            join c in db.News_CategoryMapping on b.Id equals c.NewsId
                            where c.NewsCategoryId == newsCategory.Id & c.IsActive == true & b.IsActive == true & b.IsPublished == true
                            select b).OrderByDescending(n=>n.CreatedDate).Skip(SayfaBoyutu * sayfano.Value).Take(SayfaBoyutu).ToList();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Themes/AyakliGazete/Views/Shared/NewsCategoryList.cshtml", haberler);
            }

            return View(haberler);
        }


        public ActionResult NewsAuthor(string authorUrl) {
            
            var author = db.NewsAuthors.Where(w => w.Url == authorUrl).FirstOrDefault();
            ViewBag.NewsAuthors = author;

           var haberler = db.News.Where(c => c.IsActive == true & c.IsPublished == true & c.AuthorId == author.Id).OrderByDescending(n => n.CreatedDate).Take(SayfaBoyutu).ToList();

            return View(haberler);

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