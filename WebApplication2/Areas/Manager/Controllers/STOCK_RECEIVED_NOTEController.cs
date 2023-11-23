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
    public class STOCK_RECEIVED_NOTEController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: STOCK_RECEIVED_NOTE
        public ActionResult Index()
        {
            var sTOCK_RECEIVED_NOTE = db.STOCK_RECEIVED_NOTE.Include(s => s.MANAGER).Include(s => s.PUBLISHER);
            return View(sTOCK_RECEIVED_NOTE.ToList());
        }

        // GET: STOCK_RECEIVED_NOTE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STOCK_RECEIVED_NOTE sTOCK_RECEIVED_NOTE = db.STOCK_RECEIVED_NOTE.Find(id);
            if (sTOCK_RECEIVED_NOTE == null)
            {
                return HttpNotFound();
            }
            return View(sTOCK_RECEIVED_NOTE);
        }

        // GET: STOCK_RECEIVED_NOTE/Create
        public ActionResult Create()
        {
            ViewBag.PublisherID = new SelectList(db.PUBLISHERs, "PublisherID", "selectDataTextField");
            ViewBag.havePublisher = db.PUBLISHERs.ToList();
            return PartialView("_CreatePartialView");
        }

        // POST: STOCK_RECEIVED_NOTE/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockReceivedNoteID,StockReceivedNoteDate,PublisherID,ManagerID")] STOCK_RECEIVED_NOTE sTOCK_RECEIVED_NOTE)
        {
            if (ModelState.IsValid)
            {
                sTOCK_RECEIVED_NOTE.ManagerID = (db.MANAGERs.ToList())[0].ManagerID;
                db.STOCK_RECEIVED_NOTE.Add(sTOCK_RECEIVED_NOTE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManagerID = new SelectList(db.MANAGERs, "ManagerID", "AccountID", sTOCK_RECEIVED_NOTE.ManagerID);
            return View(sTOCK_RECEIVED_NOTE);
        }

        // GET: STOCK_RECEIVED_NOTE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STOCK_RECEIVED_NOTE sTOCK_RECEIVED_NOTE = db.STOCK_RECEIVED_NOTE.Find(id);
            if (sTOCK_RECEIVED_NOTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.PublisherID = new SelectList(db.PUBLISHERs, "PublisherID", "selectDataTextField", sTOCK_RECEIVED_NOTE.PublisherID);
            ViewBag.StockDate = Custom.Custom_Function.ConvertDate(sTOCK_RECEIVED_NOTE.StockReceivedNoteDate);
            return PartialView("_EditPartialView", sTOCK_RECEIVED_NOTE);
        }

        // POST: STOCK_RECEIVED_NOTE/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockReceivedNoteID,StockReceivedNoteDate,PublisherID,ManagerID")] STOCK_RECEIVED_NOTE sTOCK_RECEIVED_NOTE)
        {
            if (ModelState.IsValid)
            {
                STOCK_RECEIVED_NOTE s = db.STOCK_RECEIVED_NOTE.Find(sTOCK_RECEIVED_NOTE.StockReceivedNoteID);
                db.Entry(s).CurrentValues.SetValues(sTOCK_RECEIVED_NOTE);
                s.ManagerID = (db.MANAGERs.ToList())[0].ManagerID;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sTOCK_RECEIVED_NOTE);
        }

        // GET: STOCK_RECEIVED_NOTE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STOCK_RECEIVED_NOTE sTOCK_RECEIVED_NOTE = db.STOCK_RECEIVED_NOTE.Find(id);
            if (sTOCK_RECEIVED_NOTE == null)
            {
                return HttpNotFound();
            }
            return View(sTOCK_RECEIVED_NOTE);
        }

        // POST: STOCK_RECEIVED_NOTE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            STOCK_RECEIVED_NOTE sTOCK_RECEIVED_NOTE = db.STOCK_RECEIVED_NOTE.Find(id);
            if (db.STOCK_RECEIVED_NOTE_DETAIL.Any(s => s.StockReceivedNoteID == id))
            {
                ViewBag.ErrorMessage = "Vui lòng xóa chi tiết của phiếu nhập này trước !";
                return PartialView("_ErrorMessagePartialView");
            }
            else
            {
                db.STOCK_RECEIVED_NOTE.Remove(sTOCK_RECEIVED_NOTE);
                db.SaveChanges();
            }
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
