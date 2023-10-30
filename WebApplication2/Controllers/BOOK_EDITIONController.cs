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
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class BOOK_EDITIONController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: BOOK_EDITION
        public ActionResult Index()
        {
            var bOOK_EDITION = db.BOOK_EDITION.Include(b => b.BOOK_COLLECTION).Include(b => b.STOCK_INVENTORY);
            return View(bOOK_EDITION.ToList());
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
            ViewBag.BookCollectionID = new SelectList(db.BOOK_COLLECTION, "BookCollectionID", "BookCollectionName");
            //add try catch to this
            ViewBag.CategoryList = db.CATEGORies.ToList();
            return View();
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
                //handle category
                for(int i = 8; i < form.AllKeys.Length; i++)
                {
                    int categoryID = int.Parse(form.AllKeys[i]);
                    CATEGORY category = db.CATEGORies.Find(categoryID);
                    bOOK_EDITION.CATEGORies.Add(category);
                }
                db.BOOK_EDITION.Add(bOOK_EDITION);
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
                if(isAttached)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase file = Request.Files[i];
                        string uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
                        var path = Path.Combine(Server.MapPath("~/Images"), uniqueFileName);
                        file.SaveAs(path);
                        BOOK_EDITION_IMAGE insertImage = new BOOK_EDITION_IMAGE()
                        {
                            EditionID = bOOK_EDITION.EditionID,
                            EditionImage = uniqueFileName
                        };
                        db.BOOK_EDITION_IMAGE.Add(insertImage);
                    }
                }
                
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
            ViewBag.BookCollectionID = new SelectList(db.BOOK_COLLECTION, "BookCollectionID", "BookCollectionName", bOOK_EDITION.BookCollectionID);

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

            return View(bOOK_EDITION);
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
                //handle category
                var book = db.BOOK_EDITION.Find(bOOK_EDITION.EditionID);
                if(book != null)
                {
                    book.CATEGORies.Clear();
                    db.Entry(book).CurrentValues.SetValues(bOOK_EDITION);
                    for (int i = 9; i < form.AllKeys.Length; i++)
                    {
                        int categoryID = int.Parse(form.AllKeys[i]);
                        CATEGORY category = db.CATEGORies.Find(categoryID);
                        bOOK_EDITION.CATEGORies.Add(category);
                    }
                    db.SaveChanges();
                }
                //handle file

                //bool isAttached = false;
                //foreach(string filename in Request.Files)
                //{
                //    HttpPostedFileBase file = Request.Files[filename];
                //    if (file != null && file.ContentLength >0)
                //    {
                //        isAttached = true;
                //        break;
                //    }
                //}
                //if (isAttached == true)
                //{
                //    var imagesToDelete = db.BOOK_EDITION_IMAGE.Where(img => img.EditionID == bOOK_EDITION.EditionID);
                //    if(imagesToDelete.Count() != 0)
                //    {
                //        foreach (var image in imagesToDelete)
                //        {
                //            db.BOOK_EDITION_IMAGE.Remove(image);
                //        }
                //    }
                //    for (int i = 0; i < Request.Files.Count; i++)
                //    {
                //        HttpPostedFileBase file = Request.Files[i];
                //        string uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
                //        var path = Path.Combine(Server.MapPath("~/Images"), uniqueFileName);
                //        file.SaveAs(path);
                //        BOOK_EDITION_IMAGE insertImage = new BOOK_EDITION_IMAGE()
                //        {
                //            EditionID = bOOK_EDITION.EditionID,
                //            EditionImage = uniqueFileName
                //        };
                //        db.BOOK_EDITION_IMAGE.Add(insertImage);
                //    }
                //    db.SaveChanges();
                //}
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
            db.BOOK_EDITION.Remove(bOOK_EDITION);
            db.SaveChanges();
            return RedirectToAction("Index");
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
