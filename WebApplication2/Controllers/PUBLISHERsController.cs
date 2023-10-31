using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PUBLISHERsController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: PUBLISHERs
        public ActionResult Index()
        {
            var pUBLISHERs = db.PUBLISHERs.Include(p => p.MANAGER);
            return View(pUBLISHERs.ToList());
        }

        // GET: PUBLISHERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLISHER pUBLISHER = db.PUBLISHERs.Find(id);
            if (pUBLISHER == null)
            {
                return HttpNotFound();
            }
            return View(pUBLISHER);
        }

        // GET: PUBLISHERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PUBLISHERs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PublisherID,PublisherName,PublisherDescription,PublisherImage")] PUBLISHER pUBLISHER)
        {
            if (ModelState.IsValid)
            {
                pUBLISHER.ManagerID = (db.MANAGERs.ToList())[0].ManagerID;
                if(Request["publisherIMG"] != "")
                {
                    HttpPostedFileBase file = Request.Files[0];
                    string uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
                    var path = Path.Combine(Server.MapPath("~/Images"), uniqueFileName);
                    file.SaveAs(path);
                    pUBLISHER.PublisherImage = uniqueFileName;
                }
                db.PUBLISHERs.Add(pUBLISHER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pUBLISHER);
        }

        // GET: PUBLISHERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLISHER pUBLISHER = db.PUBLISHERs.Find(id);
            ViewBag.image = pUBLISHER.PublisherImage;
            if (pUBLISHER == null)
            {
                return HttpNotFound();
            }
            return View(pUBLISHER);
        }

        // POST: PUBLISHERs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PublisherID,PublisherName,PublisherDescription,PublisherImage,ManagerID")] PUBLISHER pUBLISHER)
        {
            if (ModelState.IsValid)
            {
                PUBLISHER p = db.PUBLISHERs.Find(pUBLISHER.PublisherID);
                if (Request["publisherImg"] == "")
                {
                    p.PublisherDescription = pUBLISHER.PublisherDescription;
                    p.PublisherName = pUBLISHER.PublisherName;
                }
                else
                {
                    HttpPostedFileBase file = Request.Files[0];
                    string uniqueFileName = Guid.NewGuid().ToString() + ".jpg";
                    var path = Path.Combine(Server.MapPath("~/Images"), uniqueFileName);
                    file.SaveAs(path);
                    p.PublisherImage = uniqueFileName;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pUBLISHER);
        }

        // GET: PUBLISHERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLISHER pUBLISHER = db.PUBLISHERs.Find(id);
            if (pUBLISHER == null)
            {
                return HttpNotFound();
            }
            return View(pUBLISHER);
        }

        // POST: PUBLISHERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PUBLISHER pUBLISHER = db.PUBLISHERs.Find(id);
            if(db.STOCK_RECEIVED_NOTE.Any(s => s.PublisherID == pUBLISHER.PublisherID))
            {
                ViewBag.existedInStockReceivedNote = true;
            }
            else
            {
                db.PUBLISHERs.Remove(pUBLISHER);
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
