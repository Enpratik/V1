using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Enpratik.Core;
using Enpratik.Data;

namespace Enpratik.AdminPanel.Controllers
{
    public class OrdersController : BaseController
    {
        private EnPratik_DataHelper db = new EnPratik_DataHelper();

        // GET: Orders
        public ActionResult Index(string OrderStartDate, string OrderEndDate, int? senderUpdate)
        {
            var data = db.Product_BasketItem.Where(p => p.IsActive == true).ToList();
            try
            {

                if (!string.IsNullOrEmpty(OrderStartDate))
                {
                    DateTime dateStart = Convert.ToDateTime(OrderStartDate);

                    data = data.Where(o => o.RegDate >= dateStart).ToList();

                }
                if (!string.IsNullOrEmpty(OrderEndDate))
                {
                    DateTime dateEnd = Convert.ToDateTime(OrderEndDate);

                    data = data.Where(o => o.RegDate <= dateEnd).ToList();
                }
            }
            catch (Exception)
            {

            }
            List<Product_BasketItem> orders = new List<Product_BasketItem>();

            foreach (var item in data)
            {
                if (orders.Any(p => p.OrderNumber == item.OrderNumber))
                    continue;

                orders.Add(item);
            }

            if (senderUpdate != null)
                Functions.SetMessageViewBag(this, "Sipariş durumu güncellenmiştir!", 1);


            return View(orders.OrderByDescending(s=>s.RegDate).ToList());
        }

        // POST: Orders
        [HttpPost]
        public ActionResult Index(string OrderStartDate, string OrderEndDate)
        {
            var data = db.Product_BasketItem.Where(p => p.IsActive == true).ToList();

            try
            {

                if (!string.IsNullOrEmpty(OrderStartDate))
                {
                    DateTime dateStart = Convert.ToDateTime(OrderStartDate);

                    data = data.Where(o => o.RegDate >= dateStart).ToList();

                }
                if (!string.IsNullOrEmpty(OrderEndDate))
                {
                    DateTime dateEnd = Convert.ToDateTime(OrderEndDate);

                    data = data.Where(o => o.RegDate <= dateEnd).ToList();
                }
            }
            catch (Exception)
            {

            }

            List<Product_BasketItem> orders = new List<Product_BasketItem>();

            foreach (var item in data)
            {
                if (orders.Any(p => p.OrderNumber == item.OrderNumber))
                    continue;

                orders.Add(item);
            }
            return View(orders.OrderByDescending(s => s.RegDate).ToList());
        }


        public ActionResult OrderReport()
        {
            var data = db.Product_BasketItem.Where(p => p.IsActive == true).ToList();
            var customers = db.Customers.Where(c => c.IsActive == true).ToList();

            ViewBag.TotalPrices = data.Sum(o => o.Price * o.Quantity);
            ViewBag.TotalOrders = data.Sum(o => o.Quantity);
            ViewBag.TotalCustomers = customers.Count();
            ViewBag.OrderProducts = data
                .GroupBy(l => l.ProductId)
                .Select(cl => new Product_BasketItem
                {
                    ProductId = cl.First().ProductId,
                    Quantity = cl.Count(),
                    Price = cl.First().Price,
                }).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult OrderReport(string OrderStartDate, string OrderEndDate)
        {
            var data = db.Product_BasketItem.Where(p => p.IsActive == true).ToList();
            var customers = db.Customers.Where(c => c.IsActive == true).ToList();
            
            if (!string.IsNullOrEmpty(OrderStartDate))
            {
                DateTime dateStart = Convert.ToDateTime(OrderStartDate);

                data = data.Where(o => o.RegDate >= dateStart).ToList();
                customers = customers.Where(o => o.CreatedDate >= dateStart).ToList();

            }
            if (!string.IsNullOrEmpty(OrderEndDate))
            {
                DateTime dateEnd = Convert.ToDateTime(OrderEndDate);

                data = data.Where(o => o.RegDate <= dateEnd).ToList();
                customers = customers.Where(o => o.CreatedDate <= dateEnd).ToList();
            }

            ViewBag.TotalPrices = data.Sum(o => o.Price * o.Quantity);
            ViewBag.TotalOrders = data.Sum(o => o.Quantity);
            ViewBag.TotalCustomers = customers.Count();
            ViewBag.OrderProducts = data
                .GroupBy(l => l.ProductId)
                .Select(cl => new Product_BasketItem
                {
                    ProductId = cl.First().ProductId,
                    Quantity = cl.Sum(c => c.Quantity),
                    Price = cl.First().Price,
                }).ToList();

            return View();
        }


        [HttpPost]
        public ActionResult UpdateStatus(string OrderStartDate1, string OrderEndDate1, string OrderNumber, int OrderStatus)
        {
            var order = db.Product_BasketItem.Where(p => p.OrderNumber == OrderNumber).FirstOrDefault();
            order.Status = Convert.ToInt32(OrderStatus);

            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Orders", new { OrderStartDate=OrderStartDate1, OrderEndDate = OrderEndDate1, senderUpdate=1 });
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

        // GET: Orders/PrintDetails/5
        public ActionResult PrintDetails(string orderNumber)
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
