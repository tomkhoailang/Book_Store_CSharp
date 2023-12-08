using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class BookCartController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: BookCart
        public ActionResult Index()
        {
            if (TempData["ErrorMessage"] != null)
            {
                string errorMessage = TempData["ErrorMessage"].ToString();
                TempData.Remove("ErrorMessage");
                ViewBag.ErrorMessage = errorMessage;
            }

            if (TempData["SuccessMessage"] != null)
            {
                string successMessage = TempData["SuccessMessage"].ToString();
                TempData.Remove("SuccessMessage");
                ViewBag.SuccessMessage = successMessage;
            }

            if (TempData["WarningMessage"] != null)
            {
                string warningMessage = TempData["WarningMessage"].ToString();
                TempData.Remove("WarningMessage");
                ViewBag.WarningMessage = warningMessage;
            }
            if (Session["ShoppingCart"] == null)
            {
                Session["ShoppingCart"] = new List<CartModels>();
            }
            List<CartModels> BookCart = Session["ShoppingCart"] as List<CartModels>;
            
            return View(BookCart);
        }

        public ActionResult AddToCart(int id)
        {
            if (Session["ShoppingCart"] == null)
            {
                Session["ShoppingCart"] = new List<CartModels>();
            }
            List<CartModels> BookCart = Session["ShoppingCart"] as List<CartModels>;
            
            if (BookCart.FirstOrDefault(m => m.Book_Information.EditionID == id) == null)
            {
                BOOK_EDITION bOOK = db.BOOK_EDITION.Find(id);
                if(bOOK == null)
                {
                    return RedirectToAction("Index", "BookCart");
                }
                PROMOTION b = new PROMOTION();
                var bOOK_IMAGE = db.BOOK_EDITION_IMAGE.Where(e => e.EditionID == bOOK.EditionID).FirstOrDefault();

                var pROMOTION = db.Sp_check_valid_promotion(bOOK.EditionID).FirstOrDefault();

                CartModels cart = new CartModels
                {
                    Book_Information = bOOK,

                    BookImage = (bOOK_IMAGE == null) ? "default-book-img.png" : bOOK_IMAGE.EditionImage,
                    Discount = (pROMOTION == null) ? 0 : pROMOTION.PromotionDiscount,
                    Amount = 1,
                    Total = 0
                };
                cart.Total = UpdateTotal(cart);
                BookCart.Add(cart);
            }

            return Redirect(Request.UrlReferrer.ToString());
            //return RedirectToAction("Index", "BookCart");
        }

        public decimal UpdateTotal(CartModels cart)
        {
            decimal discount = (100 - cart.Discount) / 100;
            return cart.Book_Information.EditionPrice * cart.Amount * discount;
        }

        public ActionResult UpdateAmount(int BookID, int Amount)
        {
            List<CartModels> BookCart = Session["ShoppingCart"] as List<CartModels>;
            CartModels cart = BookCart.FirstOrDefault(m => m.Book_Information.EditionID == BookID);
            if (cart != null)
            {
                cart.Amount = Amount;
                cart.Total = UpdateTotal(cart);
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult getTotalCart()
        {
            List<CartModels> BookCart = Session["ShoppingCart"] as List<CartModels>;

            if (BookCart == null)
                return Content("0");
            return Content(BookCart.Count().ToString());
        }

        public ActionResult getTotalPrice()
        {
            List<CartModels> BookCart = Session["ShoppingCart"] as List<CartModels>;

            if (BookCart == null)
                return Content("0");
            decimal total = 0;
            foreach (CartModels i in BookCart)
                total += i.Total;
            return Content(total.ToString("#,##0").Replace(",",".") + "VND");
        }

        public ActionResult DeleteCart(int BookID)
        {
            List<CartModels> BookCart = Session["ShoppingCart"] as List<CartModels>;
            CartModels delItem = BookCart.FirstOrDefault(m => m.Book_Information.EditionID == BookID);
            if (delItem != null)
                BookCart.Remove(delItem);
            
            return Redirect(Request.UrlReferrer.ToString());
        }

        
        public RedirectToRouteResult SubmitCart(string listID)
        {
            return RedirectToAction("Index", "Payment", new { listID = listID});
        }
    }
}