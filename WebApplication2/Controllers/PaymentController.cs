using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PaymentController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();
        // GET: Payment
        public ActionResult Index(string listID)
        {
            List<CartModels> SelectedCarts = getSelectedCarts(listID);
            decimal total = 0;
            foreach (CartModels i in SelectedCarts)
            {
                total += i.Total;
            }
            TempData["Total"] = total.ToString("#,##0").Replace(",", ".");
            TempData["ListID"] = listID;
            return View(SelectedCarts);
        }

        public ActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(List<CartModels> selectedCarts)
        {
            var listID = TempData["ListID"].ToString();
            List<CartModels> carts = getSelectedCarts(listID);
            CUSTOMER_ORDER new_Order = new CUSTOMER_ORDER
            {
                OrderTotalPrice = 0,
                OrderDate = DateTime.Now,
                OrderShippingMethod = Request["ShippingMethod"].ToString(),
                OrderPaymentMethod = Request["PaymentMethod"].ToString(),
                CustomerID = 1
            };
            db.CUSTOMER_ORDER.Add(new_Order);
            db.SaveChanges();
            var id = db.CUSTOMER_ORDER.OrderByDescending(row => row.OrderID).FirstOrDefault().OrderID;
            foreach (CartModels cart in carts)
            {
                CUSTOMER_ORDER_DETAIL oRDER_DETAIL = new CUSTOMER_ORDER_DETAIL
                {
                    DetailCurrentPrice = cart.Book_Information.EditionPrice * (100 - cart.Discount) / 100,
                    DetailQuantity = cart.Amount,
                    OrderID = id,
                    EditionID = cart.Book_Information.EditionID
                };
                db.CUSTOMER_ORDER_DETAIL.Add(oRDER_DETAIL);   
            }
            //db.SP_CREATE_CUSTOMER_ORDER_STATUS(id, 2);
            CUSTOMER_ORDER_STATUS cos = new CUSTOMER_ORDER_STATUS();
            cos.OrderID = id;
            cos.StatusID = 2;
            cos.UpdateTime = DateTime.Now;
            db.CUSTOMER_ORDER_STATUS.Add(cos);

            db.SaveChanges();

            List<CartModels> BookCart = Session["ShoppingCart"] as List<CartModels>;
            foreach (CartModels cart in carts)
            {
                BookCart.Remove(cart);
            }
            return RedirectToAction("Index", "BookCart");
        }

        public List<CartModels> getSelectedCarts(string listID)
        {
            List<int> idList = listID.Split(',').Select(int.Parse).ToList();
            List<CartModels> BookCart = Session["ShoppingCart"] as List<CartModels>;
            List<CartModels> SelectedCarts = new List<CartModels>();
            foreach (int i in idList)
            {
                CartModels cart = BookCart.FirstOrDefault(m => m.Book_Information.EditionID == i);
                SelectedCarts.Add(cart);
            }
            return SelectedCarts;
        }
    }
}