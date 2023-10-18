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
    public class STOCK_RECEIVED_NOTE_DETAILController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: STOCK_RECEIVED_NOTE_DETAIL
        public ActionResult Index()
        {
            var sTOCK_RECEIVED_NOTE_DETAIL = db.STOCK_RECEIVED_NOTE_DETAIL.Include(s => s.BOOK_EDITION).Include(s => s.STOCK_RECEIVED_NOTE);
            return View(sTOCK_RECEIVED_NOTE_DETAIL.ToList());
        }

        // GET: STOCK_RECEIVED_NOTE_DETAIL/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STOCK_RECEIVED_NOTE_DETAIL sTOCK_RECEIVED_NOTE_DETAIL = db.STOCK_RECEIVED_NOTE_DETAIL.Find(id);
            if (sTOCK_RECEIVED_NOTE_DETAIL == null)
            {
                return HttpNotFound();
            }
            return View(sTOCK_RECEIVED_NOTE_DETAIL);
        }

        // GET: STOCK_RECEIVED_NOTE_DETAIL/Create
        public ActionResult Create()
        {
            ViewBag.EditionID = new SelectList(db.BOOK_EDITION, "EditionID", "EditionDescription");
            ViewBag.StockReceivedNoteID = new SelectList(db.STOCK_RECEIVED_NOTE, "StockReceivedNoteID", "StockReceivedNoteID");
            return View();
        }

        // POST: STOCK_RECEIVED_NOTE_DETAIL/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NoteDetailQuantity,NoteDetailPrice,EditionID,StockReceivedNoteID")] STOCK_RECEIVED_NOTE_DETAIL sTOCK_RECEIVED_NOTE_DETAIL)
        {
            if (ModelState.IsValid)
            {
                db.STOCK_RECEIVED_NOTE_DETAIL.Add(sTOCK_RECEIVED_NOTE_DETAIL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EditionID = new SelectList(db.BOOK_EDITION, "EditionID", "EditionDescription", sTOCK_RECEIVED_NOTE_DETAIL.EditionID);
            ViewBag.StockReceivedNoteID = new SelectList(db.STOCK_RECEIVED_NOTE, "StockReceivedNoteID", "StockReceivedNoteID", sTOCK_RECEIVED_NOTE_DETAIL.StockReceivedNoteID);
            return View(sTOCK_RECEIVED_NOTE_DETAIL);
        }

        // GET: STOCK_RECEIVED_NOTE_DETAIL/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STOCK_RECEIVED_NOTE_DETAIL sTOCK_RECEIVED_NOTE_DETAIL = db.STOCK_RECEIVED_NOTE_DETAIL.Find(id);
            if (sTOCK_RECEIVED_NOTE_DETAIL == null)
            {
                return HttpNotFound();
            }
            ViewBag.EditionID = new SelectList(db.BOOK_EDITION, "EditionID", "EditionDescription", sTOCK_RECEIVED_NOTE_DETAIL.EditionID);
            ViewBag.StockReceivedNoteID = new SelectList(db.STOCK_RECEIVED_NOTE, "StockReceivedNoteID", "StockReceivedNoteID", sTOCK_RECEIVED_NOTE_DETAIL.StockReceivedNoteID);
            return View(sTOCK_RECEIVED_NOTE_DETAIL);
        }

        // POST: STOCK_RECEIVED_NOTE_DETAIL/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NoteDetailQuantity,NoteDetailPrice,EditionID,StockReceivedNoteID")] STOCK_RECEIVED_NOTE_DETAIL sTOCK_RECEIVED_NOTE_DETAIL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sTOCK_RECEIVED_NOTE_DETAIL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EditionID = new SelectList(db.BOOK_EDITION, "EditionID", "EditionDescription", sTOCK_RECEIVED_NOTE_DETAIL.EditionID);
            ViewBag.StockReceivedNoteID = new SelectList(db.STOCK_RECEIVED_NOTE, "StockReceivedNoteID", "StockReceivedNoteID", sTOCK_RECEIVED_NOTE_DETAIL.StockReceivedNoteID);
            return View(sTOCK_RECEIVED_NOTE_DETAIL);
        }

        // GET: STOCK_RECEIVED_NOTE_DETAIL/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STOCK_RECEIVED_NOTE_DETAIL sTOCK_RECEIVED_NOTE_DETAIL = db.STOCK_RECEIVED_NOTE_DETAIL.Find(id);
            if (sTOCK_RECEIVED_NOTE_DETAIL == null)
            {
                return HttpNotFound();
            }
            return View(sTOCK_RECEIVED_NOTE_DETAIL);
        }

        // POST: STOCK_RECEIVED_NOTE_DETAIL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            STOCK_RECEIVED_NOTE_DETAIL sTOCK_RECEIVED_NOTE_DETAIL = db.STOCK_RECEIVED_NOTE_DETAIL.Find(id);
            db.STOCK_RECEIVED_NOTE_DETAIL.Remove(sTOCK_RECEIVED_NOTE_DETAIL);
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
