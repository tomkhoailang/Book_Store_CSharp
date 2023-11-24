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
        //[ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddReview([Bind(Include = "ReviewID,ReviewDescription,ReviewDate,CustomerID,EditionID,ReviewRating")] BOOK_REVIEW newReview)
        {
            if (ModelState.IsValid)
            {
                string accountId = User.Identity.GetUserId();
                using (var db = new BookStoreManagerEntities())
				{
                    var person = db.People.FirstOrDefault(u => u.AccountID == accountId);
                    if (person == null) return null; // no person info
                    if (db.BOOK_REVIEW.FirstOrDefault(rv => rv.CustomerID == person.PersonID) != null)
					{
                        // have already review
                        return null;
					}
                    int personId = person.PersonID;
                    newReview.CustomerID = personId;
                    newReview.ReviewDate = DateTime.Now;
                    db.BOOK_REVIEW.Add(newReview);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("BookDetailsClient", "BOOK_EDITION", new { id = newReview.EditionID });
        }
    }
}