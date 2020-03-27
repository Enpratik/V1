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
    public class BlogController : Controller
    {
        EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Blog
        public ActionResult Index()
        {
            return View(db.Blogs.Where(b => b.IsActive == true && b.IsPublished==true).ToList());
        }
        // GET: Blog
        public ActionResult Detail(int? id, string blogUrl)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(db.Blogs.Where(b => b.IsActive == true && b.IsPublished==true && b.Id==id).FirstOrDefault());
        }
        // POST: Blog
        [HttpPost]
        public ActionResult Detail(int? id, string blogUrl, Blog_Comments comment)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var data = db.Blogs.Where(b => b.IsActive == true && b.IsPublished == true && b.Id == id).FirstOrDefault();

            if (string.IsNullOrEmpty(comment.CommentMessage))
            {
                ViewBag.Message = "Lütfen yorum alanını doldurunuz!";
                ViewBag.MessageType = 0;
                return View(data);
            }

            comment.BlogId = (int)id;
            comment.CustomerId = Current.User.Id;
            db.Blog_Comments.Add(comment);
            db.SaveChanges();

            ViewBag.Message = "Yorum kaldedildi!";
            ViewBag.MessageType = 1;

            return View(data);
        }

        // GET: Blog
        public ActionResult CategoryList(int? id, string categoryUrl)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            ViewBag.CategoryName = db.Blog_Categories.Where(b => b.Id == id).Select(b => b.BlogCategoryName).FirstOrDefault();

            var data = (from b in db.Blogs
                        join c in db.Blog_CategoryMapping on b.Id equals c.BlogId
                        where c.BlogCategoryId == id & c.IsActive == true & b.IsActive == true & b.IsPublished == true
                        select b).ToList();

            return View(data);
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