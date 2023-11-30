using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Areas.Manager.Controllers
{
    public class CATEGORiesController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();
        // GET: CATEGORies
        public ActionResult Index(string searchString, int? page, int? size, string sortOptions)
        {
            IQueryable<CATEGORY> categories = db.CATEGORies;
            if (!string.IsNullOrEmpty(searchString))
            {
                string[] searchTerms = searchString.Split(' ');
                categories = categories.Where(p => searchTerms.All(term => p.CategoryName.Contains(term)));
                ViewBag.Keyword = searchString;
            }
            //sort order
            ViewBag.sortOptions = new SelectList(
                new[] {
                        new SelectListItem { Value = "newest", Text = "Mới nhất" },
                        new SelectListItem { Value = "oldest", Text = "Cũ nhất" }
                 
                }
                , "Value", "Text");

            if (string.IsNullOrEmpty(sortOptions))
                sortOptions = "popular";
            switch (sortOptions)
            {
                case "newest":
                    categories = categories.OrderByDescending(b => b.CategoryName);
                    ViewBag.selectedSort = "newest";
                    break;
                case "oldest":
                    categories = categories.OrderBy(b => b.CategoryName);
                    ViewBag.selectedSort = "oldest";
                    break;
                default:
                    categories = categories.OrderByDescending(b => b.CategoryName);
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

            return View(categories.ToPagedList(pageNumber, pageSize));


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
            ViewBag.usedName = db.CATEGORies.Select(c => c.CategoryName).ToList();
            return PartialView("_CreatePartialView");
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
                if (db.CATEGORies.FirstOrDefault(b => b.CategoryName == cATEGORY.CategoryName) != null)
                {
                    return RedirectToAction("Index");
                }
                if ((db.CATEGORies.FirstOrDefault(c => c.CategoryName == cATEGORY.CategoryName)) == null)
                {
                    cATEGORY.ManagerID = (db.MANAGERs.ToList())[0].ManagerID;
                    db.CATEGORies.Add(cATEGORY);
                    db.SaveChanges();
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
            ViewBag.usedName = db.CATEGORies.Where(c => c.CategoryID != cATEGORY.CategoryID)
                .Select(c => c.CategoryName).ToList();

            return PartialView("_EditPartialView", cATEGORY);
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
                    ViewBag.ErrorMessage = "Không thể tạo tên danh mục trùng";
                    return PartialView("_ErrorMessagePartialView");
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

            if (cATEGORY == null)
            {
                return HttpNotFound();
            }
            if (cATEGORY.BOOK_EDITION.Any() == false)
            {
                db.CATEGORies.Remove(cATEGORY);
                db.SaveChanges();
            }
            else
            {
                ViewBag.ErrorMessage = "Không thể xóa. Danh mục này đang được liên kết với sách";
                return PartialView("_ErrorMessagePartialView");

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
