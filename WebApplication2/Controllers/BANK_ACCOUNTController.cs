using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class BANK_ACCOUNTController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();

        private class BankModel
        {
            public string Code { get; set; }
            public string Desc { get; set; }
            public List<BankDataModel> Data { get; set; }
        }

        private class BankDataModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string Bin { get; set; }
            public string ShortName { get; set; }
            public string Logo { get; set; }
            public int TransferSupported { get; set; }
            public int LookupSupported { get; set; }
            public string Short_Name { get; set; }
            public int Support { get; set; }
            public int IsTransfer { get; set; }
            public string SwiftCode { get; set; }
        }

        private async Task<HttpResponseMessage> getBanksInfomation()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.vietqr.io");
                HttpResponseMessage response = await client.GetAsync("/v2/banks");
                return response;
            }
        }

        [Authorize]
        // GET: BANK_ACCOUNT
        public ActionResult Index()
        {
            var accID = User.Identity.GetUserId();
            var personID = db.People.FirstOrDefault(p => p.AccountID == accID).PersonID;
            var bANK_ACCOUNT = db.BANK_ACCOUNT.Where(b => b.CustomerID == personID);

            if (TempData["SuccessMessage"] != null)
            {
                string successMessage = TempData["SuccessMessage"].ToString();
                TempData.Remove("SuccessMessage");
                ViewBag.SuccessMessage = successMessage;
            }

            if (TempData["WarningMessage"] != null)
            {
                string warningMessage = TempData["WarningMessage"].ToString();
                TempData.Remove("WarningMessage");
                ViewBag.WarningMessage = warningMessage;
            }

            if (TempData["ErrorMessage"] != null)
            {
                string errorMessage = TempData["ErrorMessage"].ToString();
                TempData.Remove("ErrorMessage");
                ViewBag.ErrorMessage = errorMessage;
            }

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
        public async Task<ActionResult> Create()
        {
            ViewBag.CustomerID = new SelectList(db.People, "PersonID", "PersonName");

            HttpResponseMessage response = await getBanksInfomation();

            if(response.IsSuccessStatusCode)
			{
                string json = await response.Content.ReadAsStringAsync();

                BankModel model = JsonConvert.DeserializeObject<BankModel>(json);

                ViewBag.BankAccountName = new SelectList(model.Data, "ShortName", "ShortName");
            }
            
            return PartialView("_CreatePartialView");
        }

        // POST: BANK_ACCOUNT/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "BankAccountID,BankAccountNumber,BankAccountName,BankCVC")] BANK_ACCOUNT bANK_ACCOUNT)
        {
            string accID = User.Identity.GetUserId();
            var person = db.People.FirstOrDefault(p => p.AccountID == accID);
            bANK_ACCOUNT.CustomerID = person.PersonID;

            if(person.BANK_ACCOUNT.Any(ba => ba.BankAccountName == bANK_ACCOUNT.BankAccountName))
			{
                TempData["ErrorMessage"] = "Ngân hàng đã tồn tại";
                return RedirectToAction("Index");
			}

            if (ModelState.IsValid)
            {
                db.BANK_ACCOUNT.Add(bANK_ACCOUNT);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Liên kết ngân hàng thành công";
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
