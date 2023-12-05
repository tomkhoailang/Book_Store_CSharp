using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Custom;
using WebApplication2.Custom_Functions;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
	public class HomeController : Controller
	{
		private BookStoreManagerEntities db = new BookStoreManagerEntities();

		public ActionResult Index()
		{
			var categories = db.CATEGORies.ToList();

			ViewBag.categoriesList = categories;

			return View();
		}

		public ActionResult HighestRatingBooks()
		{
			var bookEditions = db.BOOK_EDITION.OrderByDescending(edition => edition.BOOK_REVIEW.Average(rv => rv.ReviewRating)).Take(8).ToList();
			ViewBag.Title = "Sách có đánh giá cao nhất";
			return PartialView("BookGrid", bookEditions);
		}

		public ActionResult LatestBooksSection()
		{
			var bookEditions = db.BOOK_EDITION.OrderByDescending(edition => edition.EditionYear).Take(8).ToList();
			ViewBag.Title = "Sách mới nhất theo năm xuất bản";
			return PartialView("BookGrid", bookEditions);
		}

		public ActionResult Shop()
		{
			return RedirectToAction("Filter", "BOOK_EDITION");
		}
	}
}