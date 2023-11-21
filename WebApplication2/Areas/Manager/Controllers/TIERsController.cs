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
    public class TIERsController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: TIERs
        public ActionResult Index(string searchString)
        {
            IQueryable<TIER> tiers = db.TIERs;
            if (!string.IsNullOrEmpty(searchString))
            {
                string[] searchTerms = searchString.Split(' ');

                tiers = tiers.Where(p => searchTerms.All(term => p.TierName.Contains(term)));
                ViewBag.Keyword = searchString;
            }
            //List<string> tierNameList = new List<string>();
            //foreach (TIER tier in tiers.ToList())
            //{
            //    tierNameList.Add(tier.TierName);
            //}

            //ViewBag.TierNameList = tierNameList;
            return View(tiers.ToList());
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
            ViewBag.usedName = db.TIERs.Select(t => t.TierName);
            return PartialView("_CreatePartialView");
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
            ViewBag.usedName = db.TIERs
                .Where(t => t.TierID != tIER.TierID)
                .Select(t => t.TierName);
            return PartialView("_EditPartialView", tIER);
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
            db.TIERs.Remove(tIER);
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
