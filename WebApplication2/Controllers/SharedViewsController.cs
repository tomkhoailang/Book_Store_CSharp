
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class SharedViewsController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();
        public ActionResult Navbar()
        {
            var data = db.CATEGORies.ToList();
            ViewBag.cartItemsCount = ((List<CartModels>)Session["ShoppingCart"])?.Count ?? 0; ;
            ViewBag.categoriesList = data;

            if(User.Identity.IsAuthenticated)
			{
                string userId = User.Identity.GetUserId();
                Person relatedPerson = db.People.FirstOrDefault(p => p.AccountID == userId);

                ViewBag.favoriteBooksAmount = relatedPerson.BOOK_EDITION.Count;
			}
			else
			{
                ViewBag.favoriteBooksAmount = 0;
            }
            
            return PartialView("_Navbar");
        }

        public ActionResult Book(BOOK_EDITION book)
		{
            if(User.Identity.IsAuthenticated)
			{
                string userId = User.Identity.GetUserId();
                Person relatedPerson = db.People.FirstOrDefault(p => p.AccountID == userId);

                if(relatedPerson.BOOK_EDITION.Count > 0 && relatedPerson.BOOK_EDITION.Any(b => b.EditionID == book.EditionID))
				{
                    ViewBag.isMarked = true;
				}
				else
				{
                    ViewBag.isMarked = false;
                }
            }

            return PartialView("_Book", book);
        }
    }
}

