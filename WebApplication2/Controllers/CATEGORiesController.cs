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
    public class CATEGORiesController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();
    // GET: CATEGORies
    public ActionResult Index()
        {
            return View(db.CATEGORies.ToList());
        }

        // GET: CATEGORies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY cATEGORY = db.CATEGORies.Find(id);
            if (cATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORY);
        }

        // GET: CATEGORies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CATEGORies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName,CategoryDescription")] CATEGORY cATEGORY)
        {
            if (ModelState.IsValid)
            {
                if((db.CATEGORies.FirstOrDefault(c => c.CategoryName == cATEGORY.CategoryName)) == null)
                {
                    cATEGORY.ManagerID = (db.MANAGERs.ToList())[0].ManagerID;
                    db.CATEGORies.Add(cATEGORY);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.duplicatedCategory = true;
                }
                return RedirectToAction("Index");
            }

            return View(cATEGORY);
        }

        // GET: CATEGORies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY cATEGORY = db.CATEGORies.Find(id);
            if (cATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORY);
        }

        // POST: CATEGORies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,CategoryDescription")] CATEGORY cATEGORY)
        {
            if (ModelState.IsValid)
            {
                CATEGORY c = db.CATEGORies.Find(cATEGORY.CategoryID);
                if (c != null)
                {
                    db.Entry(c).CurrentValues.SetValues(cATEGORY);
                    c.ManagerID = (db.MANAGERs.ToList())[0].ManagerID;
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.duplicatedCategory = true;
                }
                return RedirectToAction("Index");
            }
            return View(cATEGORY);
        }

        // GET: CATEGORies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CATEGORY cATEGORY = db.CATEGORies.Find(id);
            if (cATEGORY == null)
            {
                return HttpNotFound();
            }
            return View(cATEGORY);
        }

        // POST: CATEGORies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CATEGORY cATEGORY = db.CATEGORies.Find(id);
            if (cATEGORY.BOOK_EDITION.Any() == false)
            {
                db.CATEGORies.Remove(cATEGORY);
                db.SaveChanges();
            }
            else
            {
                // if the category is currently linked with other
                ViewBag.Category_FK_constraint = true;

            }

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
