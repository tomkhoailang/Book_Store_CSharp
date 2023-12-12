using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PagedList;
using PagedList.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Areas.Manager.Controllers
{
    [Authorize(Roles = "Manager")]
    public class PROMOTIONsController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: PROMOTIONs
        public ActionResult Index(string searchString, DateTime? startDate, DateTime? endDate, int? page, int? size, string sortOptions)
        {

            IQueryable<PROMOTION> promotions = db.PROMOTIONs;
            //filter
            if (startDate != null)
                promotions = promotions.Where(p => p.PromotionStartDate >= startDate);
            if (endDate != null)
                promotions = promotions.Where(p => p.PromotionEndDate <= endDate);
            ViewBag.startDate = String.Format("{0:yyyy-MM-dd}", startDate);
            ViewBag.endDate = String.Format("{0:yyyy-MM-dd}", endDate);
            //search
            if (!string.IsNullOrEmpty(searchString))
            {
                string[] searchTerms = searchString.Split(' ');

                promotions = promotions.Where(p => searchTerms.All(term => p.PromotionName.Contains(term)));
                ViewBag.Keyword = searchString;
            }

            //sort order
            ViewBag.sortOptions = new SelectList(
                new[] {
                        new SelectListItem { Value = "newest", Text = "Mới nhất" },
                        new SelectListItem { Value = "oldest", Text = "Cũ nhất" },
                }
                , "Value", "Text");

            if (string.IsNullOrEmpty(sortOptions))
                sortOptions = "newest";
            switch (sortOptions)
            {
                case "newest":
                    promotions = promotions.OrderByDescending(b => b.PromotionID);
                    ViewBag.selectedSort = "newest";
                    break;
                case "oldest":
                    promotions = promotions.OrderBy(b => b.PromotionID);
                    ViewBag.selectedSort = "oldest";
                    break;
                default:
                    promotions = promotions.OrderByDescending(b => b.PromotionID);
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

            return View(promotions.ToPagedList(pageNumber, pageSize));
        }

        // GET: PROMOTIONs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROMOTION pROMOTION = db.PROMOTIONs.Find(id);
            if (pROMOTION == null)
            {
                return HttpNotFound();
            }
            return View(pROMOTION);
        }

        // GET: PROMOTIONs/Create
        public ActionResult Create()
        {
            ViewBag.list_of_book = db.BOOK_EDITION;

            return PartialView("_CreatePartialView");
        }

        // POST: PROMOTIONs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PromotionID,PromotionName,PromotionDiscount,PromotionDetails,PromotionStartDate,PromotionEndDate")] PROMOTION pROMOTION, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                string promoDetail = "";
                List<int> editionList = JsonConvert.DeserializeObject<List<int>>(Request["selectedValueInput"]);

                foreach (int editionID in editionList)
                {
                    BOOK_EDITION book = db.BOOK_EDITION.FirstOrDefault(b => b.EditionID == editionID);
                    pROMOTION.BOOK_EDITION.Add(book);
                    promoDetail += "Mã sách: " + book.EditionID.ToString() + " - Tên: " + book.EditionName + " ,";
                }

                //for (int i = 6; i < form.AllKeys.Length; i++)
                //{
                //    if (form.AllKeys[i] != null)
                //    {
                //        int bookID = int.Parse(form.AllKeys[i]);
                //        BOOK_EDITION book = db.BOOK_EDITION.FirstOrDefault(b => b.EditionID == bookID);
                //        pROMOTION.BOOK_EDITION.Add(book);
                //        promoDetail += "Mã sách: " + book.EditionID.ToString() + " - Tên: " + book.EditionName +" ";
                //    }
                //}
                promoDetail += " sẽ được áp dụng khuyến mãi " + pROMOTION.PromotionDiscount + "% từ ngày " + pROMOTION.PromotionStartDate.ToString() + " đến ngày " + pROMOTION.PromotionEndDate.ToString();

                if (string.IsNullOrEmpty(pROMOTION.PromotionDetails))
                {
                    string uniPromoDetail = "N'" + promoDetail + "'";
                    pROMOTION.PromotionDetails = uniPromoDetail;
                }
                pROMOTION.ManagerID = (db.MANAGERs.ToList())[0].ManagerID;
                db.PROMOTIONs.Add(pROMOTION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.list_of_book = db.BOOK_EDITION;
            return View(pROMOTION);
        }
        // GET: PROMOTIONs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROMOTION pROMOTION = db.PROMOTIONs.Find(id);
            if (pROMOTION == null)
            {
                return HttpNotFound();
            }
            ViewBag.list_of_book = db.BOOK_EDITION.ToList();
            if (pROMOTION.BOOK_EDITION.Count() > 0)
            {
                string[] list_of_selected_book = new string[pROMOTION.BOOK_EDITION.Count()];
                int i = 0;
                foreach (var book in pROMOTION.BOOK_EDITION)
                {
                    list_of_selected_book[i] = book.EditionID.ToString();
                    i++;
                }
                ViewBag.list_of_selected_book = list_of_selected_book.ToList();
            }
            ViewBag.promotionID = id;
            ViewBag.startDateFormat = Custom.Custom_Function.ConvertDate(pROMOTION.PromotionStartDate);
            ViewBag.endDateFormat = Custom.Custom_Function.ConvertDate(pROMOTION.PromotionEndDate);
            return PartialView("_EditPartialView", pROMOTION);

        }

        // POST: PROMOTIONs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PromotionID,PromotionName,PromotionDiscount,PromotionDetails,PromotionStartDate,PromotionEndDate")] PROMOTION pROMOTION, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var p = db.PROMOTIONs.Find(pROMOTION.PromotionID);
                if (p != null)
                {
                    string promoDetail = "";
                    p.BOOK_EDITION.Clear();
                    p.PromotionName = pROMOTION.PromotionName;
                    p.PromotionDiscount = pROMOTION.PromotionDiscount;
                    p.PromotionDetails = pROMOTION.PromotionDetails;
                    p.PromotionEndDate = pROMOTION.PromotionEndDate;
                    p.ManagerID = (db.MANAGERs.ToList())[0].ManagerID;


                    var autoGenerate = Request["auto-generate"];
                    bool useGenerate = false;
                    if (autoGenerate != null && autoGenerate == "enable")
                    {
                        useGenerate = true;
                    }
                    if (p.PromotionStartDate > DateTime.Now)
                    {
                        List<int> editionList = JsonConvert.DeserializeObject<List<int>>(Request["editSelectedValueInput"]);
                        foreach (int editionID in editionList)
                        {
                            BOOK_EDITION book = db.BOOK_EDITION.FirstOrDefault(b => b.EditionID == editionID);
                            p.BOOK_EDITION.Add(book);
                            promoDetail += "Mã sách: " + book.EditionID.ToString() + " - Tên: " + book.EditionName + " , ";
                        }
                    }

                    if (useGenerate)
                    {
                        promoDetail += " sẽ được áp dụng khuyến mãi " + p.PromotionDiscount + "% từ ngày" + p.PromotionStartDate.ToString() + " đến ngày " + p.PromotionEndDate.ToString();
                        string uPromoDetail = "N'" + promoDetail + "'";
                        p.PromotionDetails = uPromoDetail;
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }



            return View(pROMOTION);
        }

        // GET: PROMOTIONs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PROMOTION pROMOTION = db.PROMOTIONs.Find(id);
            if (pROMOTION == null)
            {
                return HttpNotFound();
            }
            return View(pROMOTION);
        }

        // POST: PROMOTIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PROMOTION pROMOTION = db.PROMOTIONs.Find(id);
            if (pROMOTION.BOOK_EDITION == null)
            {
                ViewBag.ErrorMessage = "Không thể xóa.";
                return PartialView("_ErrorMessagePartialView");
            }
            db.PROMOTIONs.Remove(pROMOTION);
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
