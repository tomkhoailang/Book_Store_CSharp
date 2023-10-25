using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
            // get list of review

            return View(bOOK_EDITION);
        }

        // GET: BOOK_EDITION/Create
        public ActionResult Create()
        {
            ViewBag.BookCollectionID = new SelectList(db.BOOK_COLLECTION, "BookCollectionID", "BookCollectionName");
            //ViewBag.EditionID = new SelectList(db.STOCK_INVENTORY, "EditionID", "EditionID");
            return View();
        }

        // POST: BOOK_EDITION/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EditionID,EditionPrice,EditionDescription,EditionYear,EditionPageCount,EditionName,EditionAuthor,BookCollectionID")] BOOK_EDITION bOOK_EDITION)
        {
            if (ModelState.IsValid)
            {
                db.BOOK_EDITION.Add(bOOK_EDITION);
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
            //bOOK_EDITION.EditionPrice = Convert.ToInt32(bOOK_EDITION.EditionPrice);
            ViewBag.BookCollectionID = new SelectList(db.BOOK_COLLECTION, "BookCollectionID", "BookCollectionName", bOOK_EDITION.BookCollectionID);
            return View(bOOK_EDITION);
        }

        // POST: BOOK_EDITION/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EditionID,EditionPrice,EditionDescription,EditionYear,EditionPageCount,EditionName,EditionAuthor,BookCollectionID")] BOOK_EDITION bOOK_EDITION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bOOK_EDITION).State = EntityState.Modified;
                db.SaveChanges();
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
