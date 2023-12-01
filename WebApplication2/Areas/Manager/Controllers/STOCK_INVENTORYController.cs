using Newtonsoft.Json;
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
    public class STOCK_INVENTORYController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: STOCK_INVENTORY
        public ActionResult Index(string searchString, int? page, int? size, string sortOptions)
        {
            IQueryable<STOCK_INVENTORY> stockInventory = db.STOCK_INVENTORY;
            if (!string.IsNullOrEmpty(searchString))
            {
                string[] searchTerms = searchString.Split(' ');
                if (int.TryParse(searchString, out int currentID))
                {
                    stockInventory = stockInventory.Where(s => s.EditionID == currentID || searchTerms.All(term => s.BOOK_EDITION.EditionName.Contains(term)));
                }
                else
                {
                    stockInventory = stockInventory.Where(p => searchTerms.All(term => p.BOOK_EDITION.EditionName.Contains(term)));
                }
                ViewBag.Keyword = searchString;
            }
            //sort order
            ViewBag.sortOptions = new SelectList(
                new[] {
                        new SelectListItem { Value = "newest", Text = "Mới nhất" },
                        new SelectListItem { Value = "oldest", Text = "Cũ nhất" },
                        new SelectListItem { Value = "stock_desc", Text = "Số tồn kho giảm dần" },
                        new SelectListItem { Value = "stock_asc", Text = "Số tồn kho tăng dần" },
                }
                , "Value", "Text");

            if (string.IsNullOrEmpty(sortOptions))
                sortOptions = "newest";
            switch (sortOptions)
            {
                case "newest":
                    stockInventory = stockInventory.OrderByDescending(b => b.EditionID);
                    ViewBag.selectedSort = "newest";
                    break;
                case "oldest":
                    stockInventory = stockInventory.OrderBy(b => b.EditionID);
                    ViewBag.selectedSort = "oldest";
                    break;
                case "stock_desc":
                    stockInventory = stockInventory.OrderByDescending(b => b.InventoryAvailableStock);
                    ViewBag.selectedSort = "oldest";
                    break;
                case "stock_asc":
                    stockInventory = stockInventory.OrderBy(b => b.InventoryAvailableStock);
                    ViewBag.selectedSort = "oldest";
                    break;
                default:
                    stockInventory = stockInventory.OrderByDescending(b => b.EditionID);
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

            return View(stockInventory.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult StockDetails()
        {
            var stockDetails = db.V_edition_total_stock_quantity_price_in_this_and_previous_month.ToDictionary(i => i.EditionID, i => new
            {
                TongNhapCuaThang = i.TongNhapCuaThang,
                TongTienNhapCuaThang = i.TongTienNhapCuaThang,
                TongNhapThangTruoc = i.TongNhapThangTruoc,
                TongTienNhapThangTruoc = i.TongTienNhapThangTruoc,
            });
            string jsonRs = JsonConvert.SerializeObject(stockDetails);
            return Json(jsonRs, JsonRequestBehavior.AllowGet);
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
            ViewBag.stockInHistory = db.STOCK_RECEIVED_NOTE_DETAIL.Where(s => s.EditionID == sTOCK_INVENTORY.EditionID).ToList();
            ViewBag.totalPriceHistory = db.CUSTOMER_ORDER_DETAIL.Where(s => s.EditionID == sTOCK_INVENTORY.EditionID).ToList();
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
