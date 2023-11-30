using PagedList;
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
    [Authorize(Roles = "Manager")]
    public class TIERsController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: TIERs
        public ActionResult Index(string searchString, int? page, int? size, string sortOptions)
        {
            IQueryable<TIER> tiers = db.TIERs;
            if (!string.IsNullOrEmpty(searchString))
            {
                string[] searchTerms = searchString.Split(' ');

                tiers = tiers.Where(p => searchTerms.All(term => p.TierName.Contains(term)));
                ViewBag.Keyword = searchString;
            }
            //sort order
            ViewBag.sortOptions = new SelectList(
                new[] {
                        new SelectListItem { Value = "newest", Text = "Mới nhất" },
                        new SelectListItem { Value = "oldest", Text = "Cũ nhất" },
                        new SelectListItem { Value = "range_asc", Text = "Cấp độ tăng dần" },
                        new SelectListItem { Value = "range_desc", Text = "Cấp độ giảm dần" }
                }
                , "Value", "Text");
            if (string.IsNullOrEmpty(sortOptions))
                sortOptions = "newest";
            switch (sortOptions)
            {
                case "newest":
                    tiers = tiers.OrderByDescending(b => b.TierID);
                    ViewBag.selectedSort = "newest";
                    break;
                case "oldest":
                    tiers = tiers.OrderBy(b => b.TierID);
                    ViewBag.selectedSort = "oldest";
                    break;
                case "range_asc":
                    tiers = tiers.OrderBy(b => b.TierLevel);
                    ViewBag.selectedSort = "range_asc";
                    break;
                case "range_desc":
                    tiers = tiers.OrderByDescending(b => b.TierLevel);
                    ViewBag.selectedSort = "range_desc";
                    break;
                default:
                    tiers = tiers.OrderByDescending(b => b.TierID);
                    ViewBag.selectedSort = "newest";
                    break;
            }

            // pagination
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });

            foreach (var item in items)
                if (item.Value == size.ToString()) item.Selected = true;
            ViewBag.size = items;
            ViewBag.currentSize = size;

            int pageSize = size ?? 10;
            int pageNumber = (page ?? 1);

            return View(tiers.ToPagedList(pageNumber, pageSize));

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
                if (db.TIERs.FirstOrDefault(b => b.TierName == tIER.TierName) != null)
                {
                    return RedirectToAction("Index");
                }
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
