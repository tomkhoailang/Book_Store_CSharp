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
            ViewBag.ManagerID = new SelectList(db.MANAGERs, "ManagerID", "ManagerName");
            ViewBag.PublisherID = new SelectList(db.PUBLISHERs, "PublisherID", "PublisherName");
            return View();
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
                db.STOCK_RECEIVED_NOTE.Add(sTOCK_RECEIVED_NOTE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ManagerID = new SelectList(db.MANAGERs, "ManagerID", "ManagerName", sTOCK_RECEIVED_NOTE.ManagerID);
            ViewBag.PublisherID = new SelectList(db.PUBLISHERs, "PublisherID", "PublisherName", sTOCK_RECEIVED_NOTE.PublisherID);
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
            ViewBag.ManagerID = new SelectList(db.MANAGERs, "ManagerID", "ManagerName", sTOCK_RECEIVED_NOTE.ManagerID);
            ViewBag.PublisherID = new SelectList(db.PUBLISHERs, "PublisherID", "PublisherName", sTOCK_RECEIVED_NOTE.PublisherID);
            return View(sTOCK_RECEIVED_NOTE);
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
                db.Entry(sTOCK_RECEIVED_NOTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ManagerID = new SelectList(db.MANAGERs, "ManagerID", "ManagerName", sTOCK_RECEIVED_NOTE.ManagerID);
            ViewBag.PublisherID = new SelectList(db.PUBLISHERs, "PublisherID", "PublisherName", sTOCK_RECEIVED_NOTE.PublisherID);
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
            db.STOCK_RECEIVED_NOTE.Remove(sTOCK_RECEIVED_NOTE);
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
