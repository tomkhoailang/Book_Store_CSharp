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
    public class STOCK_RECEIVED_NOTE_DETAILController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: STOCK_RECEIVED_NOTE_DETAIL
        public ActionResult Index(int? id)
        {
            //note: this id is the StockReceivedNoteID
            var sTOCK_RECEIVED_NOTE_DETAIL = db.STOCK_RECEIVED_NOTE_DETAIL.Include(s => s.BOOK_EDITION).Include(s => s.STOCK_RECEIVED_NOTE);
            var a = id;
            if (id != null)
            {
                sTOCK_RECEIVED_NOTE_DETAIL = sTOCK_RECEIVED_NOTE_DETAIL.Where(s => s.StockReceivedNoteID == id);
                if (sTOCK_RECEIVED_NOTE_DETAIL != null)
                    ViewBag.id = id;
                else
                    return HttpNotFound();
            }
            return View("Index", sTOCK_RECEIVED_NOTE_DETAIL.ToList());
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
        public ActionResult Create(int ?stockReceivedNoteID)
        {
            ViewBag.StockReceivedNoteID = new SelectList(db.STOCK_RECEIVED_NOTE.Where(s => s.StockReceivedNoteID == stockReceivedNoteID), "StockReceivedNoteID", "StockReceivedNoteID");

            if(stockReceivedNoteID != null)
            {
                var currentStockNote = db.STOCK_RECEIVED_NOTE.Find(stockReceivedNoteID);
                if (currentStockNote != null)
                {
                    var usedEditionList = currentStockNote.STOCK_RECEIVED_NOTE_DETAIL.Select(c => c.EditionID).ToList();

                    if (usedEditionList.Count == db.BOOK_EDITION.ToList().Count)
                    {
                        ViewBag.ErrorMessage = "Không thể thêm mới. Phiếu nhập này đã có tất cả sách trong kho.";
                    }
                    ViewBag.EditionID = new SelectList(db.BOOK_EDITION.Where(e => usedEditionList.Contains(e.EditionID) == false), "EditionID", "selectDataTextField");
                    ViewBag.HiddenStockID = true;
                }
                else
                {
                    ViewBag.EditionID = new SelectList(db.BOOK_EDITION, "EditionID", "selectDataTextField");
                }
            }
            else
            {
                ViewBag.EditionID = new SelectList(db.BOOK_EDITION, "EditionID", "selectDataTextField");
            }
            return PartialView("_CreatePartialView");
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
                if (db.STOCK_RECEIVED_NOTE_DETAIL.Any(s => s.StockReceivedNoteID == sTOCK_RECEIVED_NOTE_DETAIL.StockReceivedNoteID && s.EditionID == sTOCK_RECEIVED_NOTE_DETAIL.EditionID) == false)
                {
                    db.STOCK_RECEIVED_NOTE_DETAIL.Add(sTOCK_RECEIVED_NOTE_DETAIL);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", new { id = sTOCK_RECEIVED_NOTE_DETAIL.StockReceivedNoteID});

            }
            return RedirectToAction("Index", "STOCK_RECEIVED_NOTE");
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
            return PartialView("_EditPartialView", sTOCK_RECEIVED_NOTE_DETAIL);

        }

        // POST: STOCK_RECEIVED_NOTE_DETAIL/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NoteDetailID,NoteDetailQuantity,NoteDetailPrice,EditionID,StockReceivedNoteID")] STOCK_RECEIVED_NOTE_DETAIL sTOCK_RECEIVED_NOTE_DETAIL)
        {
            if (ModelState.IsValid)
            {
                var s = db.STOCK_RECEIVED_NOTE_DETAIL.Find(sTOCK_RECEIVED_NOTE_DETAIL.NoteDetailID);
                db.Entry(s).CurrentValues.SetValues(sTOCK_RECEIVED_NOTE_DETAIL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
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
            return Json(new { redirectToAction = true, actionUrl = Url.Action("Index", new { id = sTOCK_RECEIVED_NOTE_DETAIL.StockReceivedNoteID}) });
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
