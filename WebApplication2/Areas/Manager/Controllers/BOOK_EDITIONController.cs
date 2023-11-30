using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Areas.Manager.Controllers
{
    [Authorize(Roles = "Manager")]
    public class BOOK_EDITIONController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: BOOK_EDITION
        public ActionResult Index(string searchString, int? page, int? size, string sortOptions)
        {
            IQueryable<BOOK_EDITION> bookEditions = db.BOOK_EDITION;
            //search
            if (!string.IsNullOrEmpty(searchString))
            {
                string[] searchTerms = searchString.Split(' ');
                bookEditions = bookEditions.Where(p => searchTerms.All(term => p.EditionName.Contains(term)));
                ViewBag.Keyword = searchString;
            }
            //sort order
            ViewBag.sortOptions = new SelectList(
                new[] {
                        new SelectListItem { Value = "popular", Text = "Phổ biến" },
                        new SelectListItem { Value = "new", Text = "Mới nhất" },
                        new SelectListItem { Value = "price_desc", Text = "Giá giảm dần" },
                        new SelectListItem { Value = "price_asc", Text = "Giá tăng dần" },
                        new SelectListItem { Value = "name_asc", Text = "A - Z" },
                        new SelectListItem { Value = "name_desc", Text = "Z - A" },

                }
                , "Value", "Text");

            if (string.IsNullOrEmpty(sortOptions))
                sortOptions = "popular";
            switch (sortOptions)
            {
                case "name_desc":
                    bookEditions = bookEditions.OrderByDescending(b => b.EditionName);
                    ViewBag.selectedSort = "name_desc";
                    break;
                case "name_asc":
                    bookEditions = bookEditions.OrderBy(b => b.EditionName);
                    ViewBag.selectedSort = "name_asc";
                    break;
                case "price_desc":
                    bookEditions = bookEditions.OrderByDescending(b => b.EditionPrice);
                    ViewBag.selectedSort = "price_desc";
                    break;
                case "price_asc":
                    bookEditions = bookEditions.OrderBy(b => b.EditionPrice);
                    ViewBag.selectedSort = "price_asc";
                    break;
                case "new":
                    bookEditions = bookEditions.OrderByDescending(b => b.EditionID);
                    ViewBag.selectedSort = "new";
                    break;
                case "popular":
                    bookEditions = (from b in db.BOOK_EDITION
                                    join v in db.V_edition_buy_count on b.EditionID equals v.EditionID into joinedData
                                    from v in joinedData.DefaultIfEmpty()
                                    orderby v != null ? v.BuyCount : 0 descending
                                    select b);
                    ViewBag.selectedSort = "popular";
                    break;
                default:
                    bookEditions = bookEditions.OrderByDescending(b => b.EditionID);
                    ViewBag.selectedSort = "new";
                    break;
            }

            // pagination
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });

            foreach (var item in items)
                if (item.Value == size.ToString()) item.Selected = true;
            ViewBag.size = items;
            ViewBag.currentSize = size;

            int pageSize = size ?? 10;
            int pageNumber = (page ?? 1);

            return View(bookEditions.ToPagedList(pageNumber, pageSize));
        }

        // GET: BOOK_EDITION/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK_EDITION bOOK_EDITION = db.BOOK_EDITION.Find(id);
            if (bOOK_EDITION == null)
            {
                return HttpNotFound();
            }
            //get the current applied promotion
            ViewBag.current_valid_promotion = db.Sp_check_valid_promotion(id);
            var a = ViewBag.current_valid_promotion;

            // get list of review
            ViewBag.list_of_review = db.BOOK_REVIEW.Where(e => e.EditionID == id).ToList();


            return View(bOOK_EDITION);
        }

        // GET: BOOK_EDITION/Create
        public ActionResult Create()
        {
            /* var selectList = new SelectList(db.BOOK_COLLECTION, "BookCollectionID", "selectDataTextField");*/
            var selectList = new SelectList(db.BOOK_COLLECTION, "BookCollectionID", "BookCollectionID");
            var defaultItem = new SelectListItem() { Text = "Null", Value = "0" };
            ViewBag.BookCollectionID = selectList.Prepend(defaultItem);

            ViewBag.CategoryList = db.CATEGORies.ToList();
            return PartialView("_CreatePartialView");
        }

        // POST: BOOK_EDITION/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EditionID,EditionPrice,EditionDescription,EditionYear,EditionPageCount,EditionName,EditionAuthor,BookCollectionID")] BOOK_EDITION bOOK_EDITION, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                if (bOOK_EDITION.BookCollectionID == 0)
                {
                    bOOK_EDITION.BookCollectionID = null;
                }
                //handle category
                for (int i = 8; i < form.AllKeys.Length; i++)
                {
                    int categoryID = int.Parse(form.AllKeys[i]);
                    CATEGORY category = db.CATEGORies.Find(categoryID);
                    bOOK_EDITION.CATEGORies.Add(category);
                }
                bOOK_EDITION.ManagerID = (db.MANAGERs.ToList())[0].ManagerID;
                //handle file
                bool isAttached = false;
                foreach (string filename in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[filename];
                    if (file != null && file.ContentLength > 0)
                    {
                        isAttached = true;
                        break;
                    }
                }
                if (isAttached)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase file = Request.Files[i];
                        string uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
                        var path = Path.Combine(Server.MapPath("~/Images"), uniqueFileName);
                        file.SaveAs(path);

                        BOOK_EDITION_IMAGE insertedImage = new BOOK_EDITION_IMAGE()
                        {
                            EditionID = bOOK_EDITION.EditionID,
                            EditionImage = uniqueFileName
                        };
                        bOOK_EDITION.BOOK_EDITION_IMAGE.Add(insertedImage);
                    }
                }
                db.BOOK_EDITION.Add(bOOK_EDITION);
                db.SaveChanges();


            }

            ViewBag.BookCollectionID = new SelectList(db.BOOK_COLLECTION, "BookCollectionID", "BookCollectionName", bOOK_EDITION.BookCollectionID);
            return RedirectToAction("Index");
        }

        // GET: BOOK_EDITION/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK_EDITION bOOK_EDITION = db.BOOK_EDITION.Find(id);
            if (bOOK_EDITION == null)
            {
                return HttpNotFound();
            }
            ViewBag.images = db.BOOK_EDITION_IMAGE.Where(a => a.EditionID == id).Select(a => a.EditionImage).ToList();


            //var selectList = new SelectList(db.BOOK_COLLECTION, "BookCollectionID", "selectDataTextField");
            var selectList = new SelectList(db.BOOK_COLLECTION, "BookCollectionID", "BookCollectionID");
            var defaultItem = new SelectListItem() { Text = "Null", Value = "0" };
            var c = selectList.Prepend(defaultItem);
            ViewBag.BookCollectionID = c.ToList();

            ViewBag.CategoryList = db.CATEGORies.ToList();

            if (bOOK_EDITION.CATEGORies.Count() > 0)
            {
                string[] SelectedCategoryList = new string[bOOK_EDITION.CATEGORies.Count()];
                int i = 0;
                foreach (var category in bOOK_EDITION.CATEGORies)
                {
                    SelectedCategoryList[i] = category.CategoryID.ToString();
                    i++;
                }
                ViewBag.SelectedCategoryList = SelectedCategoryList.ToList();
            }

            return PartialView("_EditPartialView", bOOK_EDITION);
        }

        // POST: BOOK_EDITION/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EditionID,EditionPrice,EditionDescription,EditionYear,EditionPageCount,EditionName,EditionAuthor,BookCollectionID")] BOOK_EDITION bOOK_EDITION, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                if (bOOK_EDITION.BookCollectionID == 0)
                {
                    bOOK_EDITION.BookCollectionID = null;
                }
                //handle category
                var book = db.BOOK_EDITION.Find(bOOK_EDITION.EditionID);
                if (book != null)
                {
                    book.CATEGORies.Clear();
                    db.Entry(book).CurrentValues.SetValues(bOOK_EDITION);
                    for (int i = 9; i < form.AllKeys.Length; i++)
                    {
                        int categoryID = int.Parse(form.AllKeys[i]);
                        CATEGORY category = db.CATEGORies.Find(categoryID);
                        book.CATEGORies.Add(category);
                    }
                    book.ManagerID = (db.MANAGERs.ToList())[0].ManagerID;
                    //handle file
                    bool isAttached = false;
                    foreach (string filename in Request.Files)
                    {
                        HttpPostedFileBase file = Request.Files[filename];
                        if (file != null && file.ContentLength > 0)
                        {
                            isAttached = true;
                            break;
                        }
                    }
                    var b = Request.Files;
                    var a = isAttached;
                    if (isAttached)
                    {
                        var imgToDelete = db.BOOK_EDITION_IMAGE.Where(i => i.EditionID == book.EditionID);
                        db.BOOK_EDITION_IMAGE.RemoveRange(imgToDelete);
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            HttpPostedFileBase file = Request.Files[i];
                            string uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
                            var path = Path.Combine(Server.MapPath("~/Images"), uniqueFileName);
                            file.SaveAs(path);

                            BOOK_EDITION_IMAGE insertedImage = new BOOK_EDITION_IMAGE()
                            {
                                EditionID = bOOK_EDITION.EditionID,
                                EditionImage = uniqueFileName
                            };
                            book.BOOK_EDITION_IMAGE.Add(insertedImage);
                        }
                    }
                    db.SaveChanges();
                }
                //handle file


                return RedirectToAction("Index");
            }
            ViewBag.BookCollectionID = new SelectList(db.BOOK_COLLECTION, "BookCollectionID", "BookCollectionName", bOOK_EDITION.BookCollectionID);
            ViewBag.EditionID = new SelectList(db.STOCK_INVENTORY, "EditionID", "EditionID", bOOK_EDITION.EditionID);
            return View(bOOK_EDITION);
        }

        // GET: BOOK_EDITION/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK_EDITION bOOK_EDITION = db.BOOK_EDITION.Find(id);
            if (bOOK_EDITION == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShowPopup = false;
            return View(bOOK_EDITION);
        }

        // POST: BOOK_EDITION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BOOK_EDITION bOOK_EDITION = db.BOOK_EDITION.Find(id);
            if (db.CUSTOMER_ORDER_DETAIL.Any(b => b.EditionID == bOOK_EDITION.EditionID))
            {
                //case when the book is existed in customer order
                ViewBag.ErrorMessage = "Không thể xóa. Sách này đã tồn tại trong đơn hàng của khách";
                return PartialView("_ErrorMessagePartialView");

            }
            if (db.STOCK_RECEIVED_NOTE_DETAIL.Any(n => n.EditionID == bOOK_EDITION.EditionID))
            {
                //case when the book is existed in stock receive notge
                ViewBag.ErrorMessage = "Không thể xóa. Sách này đã tồn tại trong phiếu nhập";
                return PartialView("_ErrorMessagePartialView");

            }
            var stockInventory = db.STOCK_INVENTORY.FirstOrDefault(s => s.EditionID == bOOK_EDITION.EditionID);
            var imgToDelete = db.BOOK_EDITION_IMAGE.Where(i => i.EditionID == bOOK_EDITION.EditionID);
            db.BOOK_EDITION_IMAGE.RemoveRange(imgToDelete);
            db.STOCK_INVENTORY.Remove(stockInventory);
            bOOK_EDITION.PROMOTIONs.Clear();
            db.BOOK_EDITION.Remove(bOOK_EDITION);
            db.SaveChanges();
            return Json(new { redirectToAction = true, actionUrl = Url.Action("Index") });

        }

        public ActionResult AddToCart(int id)
        {
            return RedirectToAction("AddToCart", "BookCart", new { id = id });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
