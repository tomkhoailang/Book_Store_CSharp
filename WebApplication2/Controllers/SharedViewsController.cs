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

			return PartialView("_Navbar");
        }
    }
}