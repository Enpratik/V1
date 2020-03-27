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
    public class OrdersController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Orders
        public ActionResult Index()
        {
            var data = db.Product_BasketItem.Where(p => p.IsActive == true).ToList();
            return View(data);
        }

        // GET: Orders/Details/5
        public ActionResult Details(string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var orders = db.Product_BasketItem.Where(p => p.OrderNumber == orderNumber).ToList();

            var o = db.OrderUserAddress.Where(p => p.OrderBasketId == orderNumber).FirstOrDefault();

            ViewBag.OrderUserAddress = o;

            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,OrderNumber,PaymentTypeId,CreditCardId,InstallmentAmountId,InstallmentDiscount,IsGiftPackage,CargoCompany,CargoPrice,CargoNumber,CargoDate,ApprovalDate,ApprovalUser,DeliveryDate,DeliveyUser,CancellationDate,CancellationUser,CancellationText,OrderDescription,DeliveryAddressId,InvoiceAddressId,TotalPrice,UsedPoints,LetMeKnowByStep,CreatedDate,IsActive")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,OrderNumber,PaymentTypeId,CreditCardId,InstallmentAmountId,InstallmentDiscount,IsGiftPackage,CargoCompany,CargoPrice,CargoNumber,CargoDate,ApprovalDate,ApprovalUser,DeliveryDate,DeliveyUser,CancellationDate,CancellationUser,CancellationText,OrderDescription,DeliveryAddressId,InvoiceAddressId,TotalPrice,UsedPoints,LetMeKnowByStep,CreatedDate,IsActive")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
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
