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
    public class TRANSACTION_DETAILSController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: TRANSACTION_DETAILS
        public ActionResult Index()
        {
            var accID = User.Identity.GetUserId();
            var personID = db.People.FirstOrDefault(p => p.AccountID == accID).PersonID;
            var walletID = db.WALLETs.FirstOrDefault(w => w.CustomerID == personID).WalletID;
            var tRANSACTION_DETAILS = db.TRANSACTION_DETAILS.Where(t => t.WalletID == walletID);
            return View(tRANSACTION_DETAILS.ToList());
        }

        // GET: TRANSACTION_DETAILS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANSACTION_DETAILS tRANSACTION_DETAILS = db.TRANSACTION_DETAILS.Find(id);
            if (tRANSACTION_DETAILS == null)
            {
                return HttpNotFound();
            }
            return View(tRANSACTION_DETAILS);
        }

        // GET: TRANSACTION_DETAILS/Create
        public ActionResult CreateDeposit()
        {
            var accID = User.Identity.GetUserId();
            int cusID = db.People.FirstOrDefault(c => c.AccountID == accID).PersonID;
            IEnumerable<BANK_ACCOUNT> bAcc = db.BANK_ACCOUNT.Where(b => b.CustomerID == cusID);
            ViewBag.BankAccountID = new SelectList(bAcc, "BankAccountID", "BankAccountNumber");
            //ViewBag.OrderID = new SelectList(db.CUSTOMER_ORDER, "OrderID", "OrderStatus");
            //ViewBag.WalletID = new SelectList(db.WALLETs, "WalletID", "WalletID");
            return PartialView("_CreateDepositPartialView");
        }

        public ActionResult CreateWithdrawal()
        {
            var accID = User.Identity.GetUserId();
            int cusID = db.People.FirstOrDefault(c => c.AccountID == accID).PersonID;
            IEnumerable<BANK_ACCOUNT> bAcc = db.BANK_ACCOUNT.Where(b => b.CustomerID == cusID);
            ViewBag.BankAccountID = new SelectList(bAcc, "BankAccountID", "BankAccountNumber");
            //ViewBag.OrderID = new SelectList(db.CUSTOMER_ORDER, "OrderID", "OrderStatus");
            //ViewBag.WalletID = new SelectList(db.WALLETs, "WalletID", "WalletID");
            return PartialView("_CreateWithdrawalPartialView");
        }

        // POST: TRANSACTION_DETAILS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDeposit([Bind(Include = "TransactionID,TransactionAmount,BankAccountID")] TRANSACTION_DETAILS tRANSACTION_DETAILS)
        {
            tRANSACTION_DETAILS.TransactionDate = DateTime.Now;
            tRANSACTION_DETAILS.OrderID = null;
            tRANSACTION_DETAILS.TransactionType = "Deposit";
            var accID = User.Identity.GetUserId();
            int cusID = db.People.FirstOrDefault(c => c.AccountID == accID).PersonID;
            int walletID = db.WALLETs.FirstOrDefault(w => w.CustomerID == cusID).WalletID;
            tRANSACTION_DETAILS.WalletID = walletID;
            if (ModelState.IsValid)
            {
                db.TRANSACTION_DETAILS.Add(tRANSACTION_DETAILS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BankAccountID = new SelectList(db.BANK_ACCOUNT, "BankAccountID", "BankAccountNumber", tRANSACTION_DETAILS.BankAccountID);
            ViewBag.OrderID = new SelectList(db.CUSTOMER_ORDER, "OrderID", "OrderStatus", tRANSACTION_DETAILS.OrderID);
            ViewBag.WalletID = new SelectList(db.WALLETs, "WalletID", "WalletID", tRANSACTION_DETAILS.WalletID);
            return View(tRANSACTION_DETAILS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWithdrawal([Bind(Include = "TransactionID,TransactionAmount,WalletID,BankAccountID")] TRANSACTION_DETAILS tRANSACTION_DETAILS)
        { 
            tRANSACTION_DETAILS.TransactionDate = DateTime.Now;
            tRANSACTION_DETAILS.OrderID = null;
            tRANSACTION_DETAILS.TransactionType = "Withdrawal";
            var accID = User.Identity.GetUserId();
            int cusID = db.People.FirstOrDefault(c => c.AccountID == accID).PersonID;
            int walletID = db.WALLETs.FirstOrDefault(w => w.CustomerID == cusID).WalletID;
            tRANSACTION_DETAILS.WalletID = walletID;

            var currentBalace = db.WALLETs.FirstOrDefault(c => c.CustomerID == cusID).Balance;
            ViewBag.currentBalance = currentBalace;
            if(currentBalace < tRANSACTION_DETAILS.TransactionAmount)
            {
                ViewBag.ErrorMessage = "Số dư không đủ";
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                db.TRANSACTION_DETAILS.Add(tRANSACTION_DETAILS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BankAccountID = new SelectList(db.BANK_ACCOUNT, "BankAccountID", "BankAccountNumber", tRANSACTION_DETAILS.BankAccountID);
            ViewBag.OrderID = new SelectList(db.CUSTOMER_ORDER, "OrderID", "OrderStatus", tRANSACTION_DETAILS.OrderID);
            ViewBag.WalletID = new SelectList(db.WALLETs, "WalletID", "WalletID", tRANSACTION_DETAILS.WalletID);
            return View(tRANSACTION_DETAILS);
        }

        // GET: TRANSACTION_DETAILS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANSACTION_DETAILS tRANSACTION_DETAILS = db.TRANSACTION_DETAILS.Find(id);
            if (tRANSACTION_DETAILS == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankAccountID = new SelectList(db.BANK_ACCOUNT, "BankAccountID", "BankAccountNumber", tRANSACTION_DETAILS.BankAccountID);
            ViewBag.OrderID = new SelectList(db.CUSTOMER_ORDER, "OrderID", "OrderStatus", tRANSACTION_DETAILS.OrderID);
            ViewBag.WalletID = new SelectList(db.WALLETs, "WalletID", "WalletID", tRANSACTION_DETAILS.WalletID);
            return View(tRANSACTION_DETAILS);
        }

        // POST: TRANSACTION_DETAILS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionID,TransactionDate,TransactionType,TransactionAmount,WalletID,BankAccountID,OrderID")] TRANSACTION_DETAILS tRANSACTION_DETAILS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tRANSACTION_DETAILS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankAccountID = new SelectList(db.BANK_ACCOUNT, "BankAccountID", "BankAccountNumber", tRANSACTION_DETAILS.BankAccountID);
            ViewBag.OrderID = new SelectList(db.CUSTOMER_ORDER, "OrderID", "OrderStatus", tRANSACTION_DETAILS.OrderID);
            ViewBag.WalletID = new SelectList(db.WALLETs, "WalletID", "WalletID", tRANSACTION_DETAILS.WalletID);
            return View(tRANSACTION_DETAILS);
        }

        // GET: TRANSACTION_DETAILS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TRANSACTION_DETAILS tRANSACTION_DETAILS = db.TRANSACTION_DETAILS.Find(id);
            if (tRANSACTION_DETAILS == null)
            {
                return HttpNotFound();
            }
            return View(tRANSACTION_DETAILS);
        }

        // POST: TRANSACTION_DETAILS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TRANSACTION_DETAILS tRANSACTION_DETAILS = db.TRANSACTION_DETAILS.Find(id);
            db.TRANSACTION_DETAILS.Remove(tRANSACTION_DETAILS);
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
