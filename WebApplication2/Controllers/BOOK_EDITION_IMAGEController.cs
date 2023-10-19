using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class BOOK_EDITION_IMAGEController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: BOOK_EDITION_IMAGE
        public ActionResult Index()
        {
            var bOOK_EDITION_IMAGE = db.BOOK_EDITION_IMAGE.Include(b => b.BOOK_EDITION);
            return View(bOOK_EDITION_IMAGE.ToList());
        }

        // GET: BOOK_EDITION_IMAGE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK_EDITION_IMAGE bOOK_EDITION_IMAGE = db.BOOK_EDITION_IMAGE.Find(id);
            if (bOOK_EDITION_IMAGE == null)
            {
                return HttpNotFound();
            }
            return View(bOOK_EDITION_IMAGE);
        }

        // GET: BOOK_EDITION_IMAGE/Create
        public ActionResult Create()
        {
            ViewBag.EditionID = new SelectList(db.BOOK_EDITION, "EditionID", "EditionDescription");
            return View();
        }

        // POST: BOOK_EDITION_IMAGE/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EditionImageID,EditionImage,EditionID")] BOOK_EDITION_IMAGE bOOK_EDITION_IMAGE)
        {
            if (ModelState.IsValid)
            {
                db.BOOK_EDITION_IMAGE.Add(bOOK_EDITION_IMAGE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EditionID = new SelectList(db.BOOK_EDITION, "EditionID", "EditionDescription", bOOK_EDITION_IMAGE.EditionID);
            return View(bOOK_EDITION_IMAGE);
        }

        // GET: BOOK_EDITION_IMAGE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK_EDITION_IMAGE bOOK_EDITION_IMAGE = db.BOOK_EDITION_IMAGE.Find(id);
            if (bOOK_EDITION_IMAGE == null)
            {
                return HttpNotFound();
            }
            ViewBag.EditionID = new SelectList(db.BOOK_EDITION, "EditionID", "EditionDescription", bOOK_EDITION_IMAGE.EditionID);
            return View(bOOK_EDITION_IMAGE);
        }

        // POST: BOOK_EDITION_IMAGE/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EditionImageID,EditionImage,EditionID")] BOOK_EDITION_IMAGE bOOK_EDITION_IMAGE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bOOK_EDITION_IMAGE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EditionID = new SelectList(db.BOOK_EDITION, "EditionID", "EditionDescription", bOOK_EDITION_IMAGE.EditionID);
            return View(bOOK_EDITION_IMAGE);
        }

        // GET: BOOK_EDITION_IMAGE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOK_EDITION_IMAGE bOOK_EDITION_IMAGE = db.BOOK_EDITION_IMAGE.Find(id);
            if (bOOK_EDITION_IMAGE == null)
            {
                return HttpNotFound();
            }
            return View(bOOK_EDITION_IMAGE);
        }

        // POST: BOOK_EDITION_IMAGE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BOOK_EDITION_IMAGE bOOK_EDITION_IMAGE = db.BOOK_EDITION_IMAGE.Find(id);
            db.BOOK_EDITION_IMAGE.Remove(bOOK_EDITION_IMAGE);
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
