using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using WebApplication2.Custom;
using WebApplication2.Custom_Functions;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class BOOK_EDITIONController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        public ActionResult BookDetailsClient(int id)
        {
            BookDetailsClientViewModel m = new BookDetailsClientViewModel();
            BOOK_EDITION book = db.BOOK_EDITION.Where(b => b.EditionID == id).SingleOrDefault();

            if (book == null) return HttpNotFound();

            m.currentBook = book;
            m.bookReviews = db.BOOK_REVIEW.Where(e => e.EditionID == id).ToList();
            m.relativeCollectionName = db.BOOK_COLLECTION.FirstOrDefault(c => c.BookCollectionID == book.BookCollectionID)?.BookCollectionName ?? "";
            m.imageList = db.BOOK_EDITION_IMAGE.Where(i => i.EditionID == book.EditionID).ToList();
            m.similarBooks = BooksFilter.getSimilarBooks(book.EditionID);

            List<int> categoriesIds = book.CATEGORies.Select(c => c.CategoryID).ToList();
            ViewBag.categories = db.CATEGORies.Where(c => categoriesIds.Contains(c.CategoryID)).ToList();

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

            return View(m);
        }

        public List<(int, int)> getPriceRange(List<BOOK_EDITION> bookList)
		{
            List<(int, int)> priceRange = new List<(int, int)>();

            decimal minPrice = bookList.Aggregate(decimal.MaxValue, (acc, curr) =>
            {
                if (curr.EditionPrice < acc)
                {
                    return curr.EditionPrice;
                }
                return acc;
            });

            decimal maxPrice = bookList.Aggregate(decimal.MinValue, (acc, curr) =>
            {
                if (curr.EditionPrice > acc)
                {
                    return curr.EditionPrice;
                }
                return acc;
            });


            try
            {
                int gap = (int)Math.Ceiling((double)(maxPrice - minPrice) / 4);

                if (gap != 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        priceRange.Add((gap * i, gap * i + gap));
                    }
                    if (priceRange[priceRange.Count - 1].Item2 < maxPrice)
                    {
                        priceRange.Add((priceRange[priceRange.Count - 1].Item2, -1));
                    }
                }
                else
                {
                    priceRange.Add(((int)maxPrice, (int)maxPrice));
                }
            }
            catch (Exception e)
            {
                priceRange.Add((0, 0));
            }

            return priceRange;
        }

        public ActionResult Filter()
        {
            List<BOOK_EDITION> books = (List<BOOK_EDITION>)TempData["bookList"] ?? db.BOOK_EDITION.ToList();

            ViewBag.selectedCategory = TempData["selectedCategory"];
            ViewBag.priceRanges = getPriceRange(books);
            ViewBag.categories = db.CATEGORies.ToList();
            ViewBag.totalItemAmount = books.Count;

            var serializer = new JavaScriptSerializer();
            var requestBody = Request.InputStream;
            var jsonString = new StreamReader(requestBody).ReadToEnd();
            var jsonData = serializer.Deserialize<dynamic>(jsonString);
            int pageSize = 9;
            int pageNumber = 1;

            if (jsonData != null)
			{
                List<object> categoryIDs = ((object[])jsonData["categories"])?.ToList();
                List<int> categoryIDInts = categoryIDs?.Select(c => Convert.ToInt32(c)).ToList();
                var minPrice = jsonData["minVal"];
                var maxPrice = jsonData["maxVal"];
                var page = jsonData["page"];
                var sortOptions = jsonData["sort"];
                var order = jsonData["order"];

                if (minPrice != null && maxPrice != null)
                {
                    books = BooksFilter.filterByPrice((decimal)minPrice, (decimal)maxPrice);
                }
                else
                {
                    books = BooksFilter.filterByPrice();
                }

                if (categoryIDInts != null && categoryIDInts.Count > 0)
                {
                    books = BooksFilter.filterByCategories(categoryIDInts, books);
                }

                if(page != null)
				{
                    pageNumber = page;
				}

                if(sortOptions != null)
				{
                    switch(sortOptions)
					{
                        case "Rating":
                            books = books.OrderByDescending(edition =>
                            {
                                var reviewRatings = edition.BOOK_REVIEW.Select(rv => rv.ReviewRating);
                                return reviewRatings.Any() ? reviewRatings.Average() : 0;
                            }).ToList();
                            break;
                        case "ReleaseYear":
                            books = books.OrderByDescending(edition => edition?.EditionYear).ToList();
                            break;
                        case "Popularity":
                            books = books.OrderByDescending(edition => edition.CUSTOMER_ORDER_DETAIL.Count).ToList();
                            break;
                        default:
                            break;
					}
				}

                if (order != null)
				{
                    switch(order)
					{
                        case "asc":
                            books.Reverse();
                            break;
                        default:
                            break;
					}
				}

                ViewBag.totalItemAmount = books.Count;
                ViewBag.currentPage = pageNumber;

                return PartialView("_FilteredBook", books.ToPagedList(pageNumber, pageSize));
            }

            return View(books.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult FilterByCategory(int id)
        {
            List<int> cate = new List<int>();

            cate.Add(id);

            if (BooksFilter.filterByCategories(cate).Count > 0)
            {
                TempData["bookList"] = BooksFilter.filterByCategories(cate);
            }
            else
            {
                TempData["bookList"] = new List<BOOK_EDITION>();
            }
            TempData["selectedCategory"] = id;

            return RedirectToAction("Filter");
        }

        public ActionResult FilterByText(string query)
        {
            TempData["bookList"] = BooksFilter.filterByText(query);

            return RedirectToAction("Filter");
        }
    }
}
