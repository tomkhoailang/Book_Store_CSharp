using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using Microsoft.AspNet.Identity;
using static WebApplication2.Models.BOOK_REVIEWViewModel;


namespace WebApplication2.Controllers
{
    public class BookReviewController : Controller
    {
        [HttpPost]
        [Authorize]
        public ActionResult AddReview([Bind(Include = "ReviewID,ReviewDescription,ReviewDate,CustomerID,EditionID,ReviewRating")] BOOK_REVIEW newReview)
        {
            if (ModelState.IsValid)
            {
                string accountId = User.Identity.GetUserId();
                using (var db = new BookStoreManagerEntities())
				{
                    var person = db.People.FirstOrDefault(u => u.AccountID == accountId);
                    if (person == null)
                    {
                        TempData["ErrorMessage"] = "Vui lòng cập nhật thông tin cá nhân để đáng giá";
                        return RedirectToAction("BookDetailsClient", "BOOK_EDITION", new { id = newReview.EditionID });
                    };

					var orderDetails = db.CUSTOMER_ORDER_DETAIL.Where(od => od.EditionID == newReview.EditionID && od.CUSTOMER_ORDER.CustomerID == person.PersonID);

					if (!orderDetails.Any())
					{
						TempData["ErrorMessage"] = "Hãy mua sách để đánh giá";
						return RedirectToAction("BookDetailsClient", "BOOK_EDITION", new { id = newReview.EditionID });
					}

					if (db.BOOK_REVIEW.FirstOrDefault(rv => rv.CustomerID == person.PersonID && rv.EditionID == newReview.EditionID) != null)
					{
                        TempData["ErrorMessage"] = "Bạn đã đánh giá rồi";
                        return RedirectToAction("BookDetailsClient", "BOOK_EDITION", new { id = newReview.EditionID });
                    }

					int personId = person.PersonID;
					newReview.CustomerID = personId;
					newReview.ReviewDate = DateTime.Now;
					db.BOOK_REVIEW.Add(newReview);
					db.SaveChanges();
                    TempData["SuccessMessage"] = "Đánh giá thành công";
                }
            }
            return RedirectToAction("BookDetailsClient", "BOOK_EDITION", new { id = newReview.EditionID });
        }
    }
}