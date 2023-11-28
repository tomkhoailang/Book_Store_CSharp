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
    public class PROMOTIONsController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: PROMOTIONs
        public ActionResult Index(string searchString)
        {

            IQueryable<PROMOTION> promotions = db.PROMOTIONs;
            if (!string.IsNullOrEmpty(searchString))
            {
                string[] searchTerms = searchString.Split(' ');

                promotions = promotions.Where(p => searchTerms.All(term => p.PromotionName.Contains(term)));
                ViewBag.Keyword = searchString;
            }
            return View(promotions.ToList());
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
                for (int i = 6; i < form.AllKeys.Length; i++)
                {
                    if (form.AllKeys[i] != null)
                    {
                        int bookID = int.Parse(form.AllKeys[i]);
                        BOOK_EDITION book = db.BOOK_EDITION.FirstOrDefault(b => b.EditionID == bookID);
                        pROMOTION.BOOK_EDITION.Add(book);
                        promoDetail += "Mã sách: " + book.EditionID.ToString() + " - Tên: " + book.EditionName +" ";
                    }
                }
                promoDetail += " sẽ được áp dụng khuyến mãi " + pROMOTION.PromotionDiscount + "% từ ngày" + pROMOTION.PromotionStartDate.ToString() + " đến ngày " + pROMOTION.PromotionEndDate.ToString();

                if (string.IsNullOrEmpty(pROMOTION.PromotionDetails))
                {
                    pROMOTION.PromotionDetails = promoDetail;
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

                    int init = 6;
                    var autoGenerate = Request["auto-generate"];
                    bool useGenerate = false;
                    if (autoGenerate != null && autoGenerate == "enable")
                    {
                        init = 7;
                        useGenerate = true;
                    }

                    for (; init < form.AllKeys.Count(); init++)
                    {
                        // add the new book id
                        int bookID = int.Parse(form.AllKeys[init]);
                        BOOK_EDITION book = db.BOOK_EDITION.FirstOrDefault(b => b.EditionID == bookID);
                        p.BOOK_EDITION.Add(book);
                        promoDetail += "Mã sách: " + book.EditionID.ToString() + " - Tên: " + book.EditionName + " ";
                    }
                    if (useGenerate)
                    {
                        promoDetail += " sẽ được áp dụng khuyến mãi " + p.PromotionDiscount + "% từ ngày" + p.PromotionStartDate.ToString() + " đến ngày " + p.PromotionEndDate.ToString();
                        p.PromotionDetails = promoDetail;
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
            //if(pROMOTION.BOOK_EDITION == null)
            //{
            //    ViewBag.ErrorMessage = "Unable to delete. The promotion is currently linked with other edition";
            //    return PartialView("_ErrorMessagePartialView");
            //}
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
