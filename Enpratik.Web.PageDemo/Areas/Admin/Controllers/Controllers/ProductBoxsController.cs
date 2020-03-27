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
    public class ProductBoxsController : Controller
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: ProductBoxs
        public ActionResult Index()
        {
            return View(db.ProductBoxs.ToList());
        }

        // GET: ProductBoxs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBoxs productBoxs = db.ProductBoxs.Find(id);
            if (productBoxs == null)
            {
                return HttpNotFound();
            }
            return View(productBoxs);
        }

        // GET: ProductBoxs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductBoxs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductType,TamponType,ProductSubTitle,ReglDay,ReglDaySubTitle,DensityType,DensitySubTitle,BrandName,IsThinPad,Contents,MonthlyPrice,ThreeMonthsPrice,SixMonthsPrice,UpdatedBy,UpdatedDate")] ProductBoxs productBoxs)
        {
            if (ModelState.IsValid)
            {
                db.ProductBoxs.Add(productBoxs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productBoxs);
        }

        // GET: ProductBoxs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBoxs productBoxs = db.ProductBoxs.Find(id);
            if (productBoxs == null)
            {
                return HttpNotFound();
            }
            return View(productBoxs);
        }

        // POST: ProductBoxs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductType,TamponType,ProductSubTitle,ReglDay,ReglDaySubTitle,DensityType,DensitySubTitle,BrandName,IsThinPad,Contents,MonthlyPrice,ThreeMonthsPrice,SixMonthsPrice,UpdatedBy,UpdatedDate")] ProductBoxs productBoxs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productBoxs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productBoxs);
        }

        // GET: ProductBoxs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductBoxs productBoxs = db.ProductBoxs.Find(id);
            if (productBoxs == null)
            {
                return HttpNotFound();
            }
            return View(productBoxs);
        }

        // POST: ProductBoxs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductBoxs productBoxs = db.ProductBoxs.Find(id);
            db.ProductBoxs.Remove(productBoxs);
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
