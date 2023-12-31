﻿using PagedList;
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

namespace WebApplication2.Areas.Manager.Controllers
{
    [Authorize(Roles = "Manager")]
    public class PUBLISHERsController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        // GET: PUBLISHERs
        public ActionResult Index(string searchString, int? page, int? size, string sortOptions)
        {
            IQueryable<PUBLISHER> publishers = db.PUBLISHERs;
            if (!string.IsNullOrEmpty(searchString))
            {
                string[] searchTerms = searchString.Split(' ');
                publishers = publishers.Where(p => searchTerms.All(term => p.PublisherName.Contains(term)));
                ViewBag.Keyword = searchString;
            }
            var pUBLISHERs = publishers.Include(p => p.MANAGER);
            ViewBag.Publishers = pUBLISHERs;

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
                    publishers = publishers.OrderByDescending(b => b.PublisherID);
                    ViewBag.selectedSort = "newest";
                    break;
                case "oldest":
                    publishers = publishers.OrderBy(b => b.PublisherID);
                    ViewBag.selectedSort = "oldest";
                    break;
                default:
                    publishers = publishers.OrderByDescending(b => b.PublisherID);
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

            return View(publishers.ToPagedList(pageNumber, pageSize));

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
            ViewBag.usedName = db.PUBLISHERs.Select(p => p.PublisherName);
            return PartialView("_CreatePartialView");
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
                if (db.PUBLISHERs.FirstOrDefault(b => b.PublisherName == pUBLISHER.PublisherName) != null)
                {
                    return RedirectToAction("Index");
                }
                pUBLISHER.ManagerID = (db.MANAGERs.ToList())[0].ManagerID;

                bool isAttached = false;
                foreach (string filename in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[filename];
                    if (file != null && file.ContentLength > 0)
                    {
                        isAttached = true;
                        break;
                    }
                }
                if (isAttached)
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
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PUBLISHER pUBLISHER = db.PUBLISHERs.Find(id);
        //    ViewBag.image = pUBLISHER.PublisherImage;
        //    if (pUBLISHER == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(pUBLISHER);
        //}

        // GET: PUBLISHERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.message = "Không tìm thấy id";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLISHER pUBLISHER = db.PUBLISHERs.Find(id);
            if(pUBLISHER.PublisherImage != null)
            {
                ViewBag.publisherIcon = pUBLISHER.PublisherImage;
            }
            if (pUBLISHER == null)
            {
                ViewBag.message = "Đã xảy ra lỗi";
                return HttpNotFound();
            }
            ViewBag.usedName = db.PUBLISHERs
                .Where(p => p.PublisherID != pUBLISHER.PublisherID)
                .Select(p => p.PublisherName);
            return PartialView("_EditPartialView", pUBLISHER);
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
                p.PublisherDescription = pUBLISHER.PublisherDescription;
                p.PublisherName = pUBLISHER.PublisherName;
                bool isAttached = false;
                foreach (string filename in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[filename];
                    if (file != null && file.ContentLength > 0)
                    {
                        isAttached = true;
                        break;
                    }
                }
                if (isAttached)
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
            if (db.STOCK_RECEIVED_NOTE.Any(s => s.PublisherID == pUBLISHER.PublisherID))
            {
                ViewBag.ErrorMessage = "Unable to delete. The publisher is currently linked with stock ticket";
                return PartialView("_ErrorMessagePartialView");
            }
            else
            {
                db.PUBLISHERs.Remove(pUBLISHER);
                db.SaveChanges();
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
