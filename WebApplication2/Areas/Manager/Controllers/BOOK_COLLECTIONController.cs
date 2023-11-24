using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Areas.Manager.Controllers
{
    public class BOOK_COLLECTIONController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: BOOK_COLLECTION
        public ActionResult Index(string searchString)
        {
            IQueryable<BOOK_COLLECTION> bc = db.BOOK_COLLECTION;
            if (!string.IsNullOrEmpty(searchString))
            {
                string[] searchTerms = searchString.Split(' ');

                bc = bc.Where(p => searchTerms.All(term => p.BookCollectionName.Contains(term)));
                ViewBag.Keyword = searchString;
            }
            return View(bc.ToList());
        }

        // GET: BOOK_COLLECTION/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK_COLLECTION bOOK_COLLECTION = db.BOOK_COLLECTION.Find(id);
            if (bOOK_COLLECTION == null)
            {
                return HttpNotFound();
            }
            return View(bOOK_COLLECTION);
        }

        // GET: BOOK_COLLECTION/Create
        public ActionResult Create()
        {
            ViewBag.list_of_product = db.BOOK_EDITION;
            ViewBag.usedName = db.BOOK_COLLECTION.Select(c => c.BookCollectionName).ToList();
            return PartialView("_CreatePartialView");
        }

        // POST: BOOK_COLLECTION/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookCollectionID,BookCollectionName")] BOOK_COLLECTION bOOK_COLLECTION)
        {
            if (ModelState.IsValid)
            {
                if ((db.BOOK_COLLECTION.FirstOrDefault(c => c.BookCollectionName == bOOK_COLLECTION.BookCollectionName)) == null)
                {
                    bOOK_COLLECTION.ManagerID = (db.MANAGERs.ToList())[0].ManagerID;
                    db.BOOK_COLLECTION.Add(bOOK_COLLECTION);
                    db.SaveChanges();
                }
                else
                {
                    return View(bOOK_COLLECTION);
                }
                return RedirectToAction("Index");
            }

            return View(bOOK_COLLECTION);
        }

        // GET: BOOK_COLLECTION/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK_COLLECTION bOOK_COLLECTION = db.BOOK_COLLECTION.Find(id);
            if (bOOK_COLLECTION == null)
            {
                return HttpNotFound();
            }
            ViewBag.usedName = db.BOOK_COLLECTION
                .Where(c => c.BookCollectionID != bOOK_COLLECTION.BookCollectionID)
                .Select(c => c.BookCollectionName).ToList();
            return PartialView("_EditPartialView", bOOK_COLLECTION);
        }

        // POST: BOOK_COLLECTION/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookCollectionID,BookCollectionName")] BOOK_COLLECTION bOOK_COLLECTION)
        {
            if (ModelState.IsValid)
            {
                if ((db.BOOK_EDITION.FirstOrDefault(e => e.BookCollectionID == bOOK_COLLECTION.BookCollectionID)) != null)
                {
                    //case when there are books with the same name
                    ViewBag.ShowPopup = true;
                    return View(bOOK_COLLECTION);
                }
                else
                {
                    bOOK_COLLECTION.ManagerID = (db.MANAGERs.ToList())[0].ManagerID;
                    db.Entry(bOOK_COLLECTION).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(bOOK_COLLECTION);
        }

        // GET: BOOK_COLLECTION/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK_COLLECTION bOOK_COLLECTION = db.BOOK_COLLECTION.Find(id);
            if (bOOK_COLLECTION == null)
            {
                return HttpNotFound();
            }
            return View(bOOK_COLLECTION);
        }

        // POST: BOOK_COLLECTION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BOOK_COLLECTION bOOK_COLLECTION = db.BOOK_COLLECTION.Find(id);
            bOOK_COLLECTION.BOOK_EDITION.Clear();
            db.BOOK_COLLECTION.Remove(bOOK_COLLECTION);
            db.SaveChanges();
            return Json(new { redirectToAction = true, actionUrl = Url.Action("Index") });
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
