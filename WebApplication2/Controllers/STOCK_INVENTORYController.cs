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
    public class STOCK_INVENTORYController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: STOCK_INVENTORY
        public ActionResult Index()
        {
            var sTOCK_INVENTORY = db.STOCK_INVENTORY.Include(s => s.BOOK_EDITION);
            return View(sTOCK_INVENTORY.ToList());
        }

        // GET: STOCK_INVENTORY/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            STOCK_INVENTORY sTOCK_INVENTORY = db.STOCK_INVENTORY.Find(id);
            if (sTOCK_INVENTORY == null)
            {
                return HttpNotFound();
            }
            return View(sTOCK_INVENTORY);
        }

        //// GET: STOCK_INVENTORY/Create
        //public ActionResult Create()
        //{
        //    ViewBag.EditionID = new SelectList(db.BOOK_EDITION, "EditionID", "EditionDescription");
        //    return View();
        //}

        //// POST: STOCK_INVENTORY/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "InventoryStockInTotal,InventoryStockOutTotal,InventoryAvailableStock,EditionID")] STOCK_INVENTORY sTOCK_INVENTORY)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.STOCK_INVENTORY.Add(sTOCK_INVENTORY);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.EditionID = new SelectList(db.BOOK_EDITION, "EditionID", "EditionDescription", sTOCK_INVENTORY.EditionID);
        //    return View(sTOCK_INVENTORY);
        //}

        //// GET: STOCK_INVENTORY/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    STOCK_INVENTORY sTOCK_INVENTORY = db.STOCK_INVENTORY.Find(id);
        //    if (sTOCK_INVENTORY == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.EditionID = new SelectList(db.BOOK_EDITION, "EditionID", "EditionDescription", sTOCK_INVENTORY.EditionID);
        //    return View(sTOCK_INVENTORY);
        //}

        //// POST: STOCK_INVENTORY/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "InventoryStockInTotal,InventoryStockOutTotal,InventoryAvailableStock,EditionID")] STOCK_INVENTORY sTOCK_INVENTORY)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sTOCK_INVENTORY).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.EditionID = new SelectList(db.BOOK_EDITION, "EditionID", "EditionDescription", sTOCK_INVENTORY.EditionID);
        //    return View(sTOCK_INVENTORY);
        //}

        //// GET: STOCK_INVENTORY/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    STOCK_INVENTORY sTOCK_INVENTORY = db.STOCK_INVENTORY.Find(id);
        //    if (sTOCK_INVENTORY == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(sTOCK_INVENTORY);
        //}

        //// POST: STOCK_INVENTORY/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    STOCK_INVENTORY sTOCK_INVENTORY = db.STOCK_INVENTORY.Find(id);
        //    db.STOCK_INVENTORY.Remove(sTOCK_INVENTORY);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
