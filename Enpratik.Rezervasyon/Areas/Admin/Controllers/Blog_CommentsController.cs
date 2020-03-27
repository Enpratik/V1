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
    public class Blog_CommentsController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Blog_Comments
        public ActionResult Index()
        {
            return View(db.Blog_Comments.ToList());
        }

        // GET: Blog_Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog_Comments blog_Comments = db.Blog_Comments.Find(id);
            if (blog_Comments == null)
            {
                return HttpNotFound();
            }
            return View(blog_Comments);
        }

        // GET: Blog_Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog_Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Blog_Comments blog_Comments)
        {
            if (ModelState.IsValid)
            {
                db.Blog_Comments.Add(blog_Comments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog_Comments);
        }

        // GET: Blog_Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog_Comments blog_Comments = db.Blog_Comments.Find(id);
            if (blog_Comments == null)
            {
                return HttpNotFound();
            }
            return View(blog_Comments);
        }

        // POST: Blog_Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog_Comments blog_Comments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog_Comments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog_Comments);
        }

        // GET: Blog_Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog_Comments blog_Comments = db.Blog_Comments.Find(id);
            if (blog_Comments == null)
            {
                return HttpNotFound();
            }
            return View(blog_Comments);
        }

        // POST: Blog_Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog_Comments blog_Comments = db.Blog_Comments.Find(id);
            db.Blog_Comments.Remove(blog_Comments);
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
