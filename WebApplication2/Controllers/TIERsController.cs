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
    public class TIERsController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: TIERs
        public ActionResult Index()
        {
            var tIERs = db.TIERs.Include(t => t.MANAGER);
            return View(tIERs.ToList());
        }

        // GET: TIERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIER tIER = db.TIERs.Find(id);
            if (tIER == null)
            {
                return HttpNotFound();
            }
            return View(tIER);
        }

        // GET: TIERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIERs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TierID,TierName,TierDiscount,TierLevel")] TIER tIER)
        {
            if (ModelState.IsValid)
            {
                tIER.ManagerID = (db.MANAGERs.ToList())[0].ManagerID;
                db.TIERs.Add(tIER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIER);
        }

        // GET: TIERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIER tIER = db.TIERs.Find(id);
            if (tIER == null)
            {
                return HttpNotFound();
            }
            return View(tIER);
        }

        // POST: TIERs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TierID,TierName,TierDiscount,TierLevel")] TIER tIER)
        {
            //note add the trigger when update tier
            if (ModelState.IsValid)
            {
                TIER t = db.TIERs.Find(tIER.TierID);
                db.Entry(t).CurrentValues.SetValues(tIER);
                t.ManagerID = (db.MANAGERs.ToList())[0].ManagerID;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIER);
        }

        // GET: TIERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIER tIER = db.TIERs.Find(id);
            if (tIER == null)
            {
                return HttpNotFound();
            }
            return View(tIER);
        }

        // POST: TIERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TIER tIER = db.TIERs.Find(id);
            if(tIER.People.Any())
            {
                ViewBag.message = "Failed. User is already linked with this tier";
            }
            else
            {
                db.TIERs.Remove(tIER);
                db.SaveChanges();
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
