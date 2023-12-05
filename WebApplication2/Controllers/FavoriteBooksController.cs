using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class FavoriteBooksController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        [Authorize(Roles = "Customer")]
        public ActionResult Index(int? page)
        {
            string currentUserId = User.Identity.GetUserId();
            Person relatedPerson = db.People.FirstOrDefault(p => p.AccountID == currentUserId);
            List<BOOK_EDITION> personFavoriteBooks = relatedPerson.BOOK_EDITION.ToList();

            int pageNumber = page ?? 1;
            int pageSize = 9;

            return View(personFavoriteBooks.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
		[Authorize(Roles = "Customer")]
		public ActionResult addToFavorite(int bookId)
		{
            BOOK_EDITION needToAddBook = db.BOOK_EDITION.Find(bookId);
            string currentUserId = User.Identity.GetUserId();
            Person relatedPerson = db.People.FirstOrDefault(p => p.AccountID == currentUserId);
            try
            {
                if(!relatedPerson.BOOK_EDITION.Any(b => b.EditionID == bookId))
				{
                    relatedPerson.BOOK_EDITION.Add(needToAddBook);
                    db.SaveChanges();
                }

                return Json(new
                {
                    ok = true,
                    message = "Thêm vào danh sách yêu thích thành công"
                });
            }
            catch(Exception e)
            {
                return Json(new
                {
                    ok = false,
                    message = "Thêm vào danh sách yêu thích thất bại, vui lòng thử lại sau"
                });
            }
		}

        [HttpGet]
        [Authorize]
        public ActionResult getFavoriteBookAmount()
		{
            string currentUserId = User.Identity.GetUserId();
            Person relatedPerson = db.People.FirstOrDefault(p => p.AccountID == currentUserId);

            return Json(new
            {
                amount = relatedPerson.BOOK_EDITION.ToList().Count
            }, JsonRequestBehavior.AllowGet);
		}

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult removeFromFavorite(int bookId)
        {
            BOOK_EDITION needToDeleteBook = db.BOOK_EDITION.Find(bookId);
            string currentUserId = User.Identity.GetUserId();
            Person relatedPerson = db.People.FirstOrDefault(p => p.AccountID == currentUserId);
            try
            {
                if (relatedPerson.BOOK_EDITION.Any(b => b.EditionID == bookId))
                {
                    relatedPerson.BOOK_EDITION.Remove(needToDeleteBook);
                    db.SaveChanges();
                }

                return Json(new
                {
                    ok = true,
                    message = "Đã xóa khỏi danh sách yêu thích thành công"
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    ok = false,
                    message = "Xóa khỏi danh sách yêu thích thất bại, vui lòng thử lại sau"
                });
            }
        }
    }
}