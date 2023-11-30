using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CUSTOMER_ORDERController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: CUSTOMER_ORDER
        public ActionResult Index()
        {
            var cUSTOMER_ORDER = db.CUSTOMER_ORDER.Include(c => c.Person).Include(c => c.Person1).Include(c => c.Person2);
            var id = User.Identity.GetUserId();
            ViewBag.currentRole = db.AspNetUsers.FirstOrDefault(p => p.Id == id).AspNetRoles.FirstOrDefault().Id;
            return View(cUSTOMER_ORDER.ToList());
        }

        // GET: CUSTOMER_ORDER/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER_ORDER cUSTOMER_ORDER = db.CUSTOMER_ORDER.Find(id);
            if (cUSTOMER_ORDER == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOMER_ORDER);
        }

        // GET: CUSTOMER_ORDER/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.People, "PersonID", "PersonName");
            ViewBag.ShipperID = new SelectList(db.People, "PersonID", "PersonName");
            ViewBag.StaffID = new SelectList(db.People, "PersonID", "PersonName");
            return View();
        }

        // POST: CUSTOMER_ORDER/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,OrderDate,OrderTotalPrice,OrderStatus,OrderShippingMethod,OrderPaymentMethod,StaffID,CustomerID,ShipperID")] CUSTOMER_ORDER cUSTOMER_ORDER)
        {
            if (ModelState.IsValid)
            {
                db.CUSTOMER_ORDER.Add(cUSTOMER_ORDER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.People, "PersonID", "PersonName", cUSTOMER_ORDER.CustomerID);
            ViewBag.ShipperID = new SelectList(db.People, "PersonID", "PersonName", cUSTOMER_ORDER.ShipperID);
            ViewBag.StaffID = new SelectList(db.People, "PersonID", "PersonName", cUSTOMER_ORDER.StaffID);
            return View(cUSTOMER_ORDER);
        }

        // GET: CUSTOMER_ORDER/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER_ORDER cUSTOMER_ORDER = db.CUSTOMER_ORDER.Find(id);
            if (cUSTOMER_ORDER == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.People, "PersonID", "PersonName", cUSTOMER_ORDER.CustomerID);
            ViewBag.ShipperID = new SelectList(db.People, "PersonID", "PersonName", cUSTOMER_ORDER.ShipperID);
            ViewBag.StaffID = new SelectList(db.People, "PersonID", "PersonName", cUSTOMER_ORDER.StaffID);
            return View(cUSTOMER_ORDER);
        }

        // POST: CUSTOMER_ORDER/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,OrderDate,OrderTotalPrice,OrderStatus,OrderShippingMethod,OrderPaymentMethod,StaffID,CustomerID,ShipperID")] CUSTOMER_ORDER cUSTOMER_ORDER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cUSTOMER_ORDER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.People, "PersonID", "PersonName", cUSTOMER_ORDER.CustomerID);
            ViewBag.ShipperID = new SelectList(db.People, "PersonID", "PersonName", cUSTOMER_ORDER.ShipperID);
            ViewBag.StaffID = new SelectList(db.People, "PersonID", "PersonName", cUSTOMER_ORDER.StaffID);
            return View(cUSTOMER_ORDER);
        }

        // GET: CUSTOMER_ORDER/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER_ORDER cUSTOMER_ORDER = db.CUSTOMER_ORDER.Find(id);
            if (cUSTOMER_ORDER == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOMER_ORDER);
        }

        // POST: CUSTOMER_ORDER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CUSTOMER_ORDER cUSTOMER_ORDER = db.CUSTOMER_ORDER.Find(id);
            db.CUSTOMER_ORDER.Remove(cUSTOMER_ORDER);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Cancel
        public ActionResult CancelByCustomer(int id)
        {

            return RedirectToAction("Index");
        }

        public ActionResult ChangeStatus(int id)
        {
            //using (var context = new BookStoreManagerEntities())
            //{
            var parameter1 = new SqlParameter("@orderID", id);
            db.Database.ExecuteSqlCommand("sp_switch_status @orderID", parameter1);
            db.SaveChanges();

            string accID = User.Identity.GetUserId();
            var CustomerID = db.People.FirstOrDefault(p => p.AccountID == accID).PersonID;

            var cusOrderStatus = db.CUSTOMER_ORDER_STATUS.FirstOrDefault(o => o.OrderID == id).StatusID;
            if (Convert.ToInt32(cusOrderStatus) == 3)
            {
                CUSTOMER_ORDER cusOrder = db.CUSTOMER_ORDER.FirstOrDefault(o => o.OrderID == id);
                cusOrder.StaffID = Convert.ToInt32(CustomerID);
            }
            else if (Convert.ToInt32(cusOrderStatus) == 6)
            {
                var deliverAccID = db.AspNetRoles.FirstOrDefault(a => a.Id == "3").AspNetUsers.FirstOrDefault().Id;
                var deliverID = db.People.FirstOrDefault(d => d.AccountID == deliverAccID).PersonID;
                CUSTOMER_ORDER cusOrder = db.CUSTOMER_ORDER.FirstOrDefault(o => o.OrderID == id);
                cusOrder.ShipperID = Convert.ToInt32(deliverID);
            }
            db.SaveChanges();
            //}
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
