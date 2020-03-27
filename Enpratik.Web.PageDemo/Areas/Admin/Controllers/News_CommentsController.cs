using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.Data;

namespace Enpratik.AdminPanel.Controllers
{
    public class News_CommentsController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: News_Comments
        public ActionResult Index()
        {
            return View(db.News_Comments.ToList());
        }

        // GET: News_Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News_Comments news_Comments = db.News_Comments.Find(id);
            if (news_Comments == null)
            {
                return HttpNotFound();
            }
            return View(news_Comments);
        }

        // GET: News_Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: News_Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ParentId,NewsId,CustomerId,CommentMessage,NewsPoint,CreatedDate,Status,IsActive")] News_Comments news_Comments)
        {
            if (ModelState.IsValid)
            {
                db.News_Comments.Add(news_Comments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news_Comments);
        }

        // GET: News_Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News_Comments news_Comments = db.News_Comments.Find(id);
            if (news_Comments == null)
            {
                return HttpNotFound();
            }
            return View(news_Comments);
        }

        // POST: News_Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ParentId,NewsId,CustomerId,CommentMessage,NewsPoint,CreatedDate,Status,IsActive")] News_Comments news_Comments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news_Comments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news_Comments);
        }

        // GET: News_Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News_Comments news_Comments = db.News_Comments.Find(id);
            if (news_Comments == null)
            {
                return HttpNotFound();
            }
            return View(news_Comments);
        }

        // POST: News_Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News_Comments news_Comments = db.News_Comments.Find(id);
            db.News_Comments.Remove(news_Comments);
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
