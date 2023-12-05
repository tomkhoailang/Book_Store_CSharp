using Microsoft.AspNet.Identity;
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
    public class BANK_ACCOUNTController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();
        [Authorize]
        // GET: BANK_ACCOUNT
        public ActionResult Index()
        {
            var accID = User.Identity.GetUserId();
            var personID = db.People.FirstOrDefault(p => p.AccountID == accID).PersonID;
            var bANK_ACCOUNT = db.BANK_ACCOUNT.Where(b => b.CustomerID == personID);
            return View(bANK_ACCOUNT.ToList());
        }

        // GET: BANK_ACCOUNT/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANK_ACCOUNT bANK_ACCOUNT = db.BANK_ACCOUNT.Find(id);
            if (bANK_ACCOUNT == null)
            {
                return HttpNotFound();
            }
            return View(bANK_ACCOUNT);
        }

        // GET: BANK_ACCOUNT/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.People, "PersonID", "PersonName");
            return PartialView("_CreatePartialView");
        }

        // POST: BANK_ACCOUNT/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BankAccountID,BankAccountNumber,BankAccountName,BankCVC")] BANK_ACCOUNT bANK_ACCOUNT)
        {
            string accID = User.Identity.GetUserId();
            var CustomerID = db.People.FirstOrDefault(p => p.AccountID == accID).PersonID;
            bANK_ACCOUNT.CustomerID = CustomerID;

            if (ModelState.IsValid)
            {
                db.BANK_ACCOUNT.Add(bANK_ACCOUNT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.People, "PersonID", "PersonName", bANK_ACCOUNT.CustomerID);
            return View(bANK_ACCOUNT);
        }

        // GET: BANK_ACCOUNT/Edit/5
        
        public ActionResult Edit1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANK_ACCOUNT bANK_ACCOUNT = db.BANK_ACCOUNT.Find(id);
            if (bANK_ACCOUNT == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.People, "PersonID", "PersonName", bANK_ACCOUNT.CustomerID);
            return PartialView("_EditPartialView", bANK_ACCOUNT);
        } 

        // POST: BANK_ACCOUNT/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BankAccountID,BankAccountNumber,BankAccountName,BankCVC,CustomerID")] BANK_ACCOUNT bANK_ACCOUNT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bANK_ACCOUNT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.People, "PersonID", "PersonName", bANK_ACCOUNT.CustomerID);
            return View(bANK_ACCOUNT);
        }

        // GET: BANK_ACCOUNT/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    BANK_ACCOUNT bANK_ACCOUNT = db.BANK_ACCOUNT.Find(id);
        //    if (bANK_ACCOUNT == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(bANK_ACCOUNT);
        //}

        // POST: BANK_ACCOUNT/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANK_ACCOUNT bANK_ACCOUNT = db.BANK_ACCOUNT.Find(id);
            if (bANK_ACCOUNT == null)
            {
                return HttpNotFound();
            }
            db.BANK_ACCOUNT.Remove(bANK_ACCOUNT);
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
