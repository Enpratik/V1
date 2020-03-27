using Enpratik.Core;
using Enpratik.Web.Eticaret.Model;
using System.Linq;
using System.Web.Mvc;

namespace Enpratik.Web.Eticaret.Controllers
{
    public class ProductCartController : Controller
    {
        // GET: ProductCart
        public ActionResult Index()
        {
            return View(Current.GetBasketItem());
        }

        // POST: ProductCart
        [HttpPost]
        public ActionResult Index(string[] ProductId, string[] Quantity)
        {
            var basketItems = Current.GetBasketItem();

            if(basketItems==null || basketItems.Count==0)
                return View(basketItems);

            if (ProductId==null || Quantity==null)
                return View(basketItems);

            for (int i = 0; i < ProductId.Length; i++)
            {
                int id = ProductId[i].ToInt32();
                int quantity = Quantity[i].ToInt32();
                basketItems.Where(b => b.ProductId == id).ToList().ForEach(b => b.Quantity = quantity);
            }
            
            return View(basketItems);
        }

        // POST: ProductCart
        [HttpPost]
        public ActionResult Remove(int itemIndex)
        {
            var basketItems = Current.GetBasketItem();
            basketItems.RemoveAt(itemIndex);
            Session["Current_Basket"] = basketItems;
            return RedirectToAction("Index");
        }
    }
}