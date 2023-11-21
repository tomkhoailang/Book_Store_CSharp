//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using WebApplication2.Models;


//namespace WebApplication2.Controllers
//{
//    public class UserController : Controller
//    {
//        private BookStoreManagerEntities db = new BookStoreManagerEntities();

//        GET: User
//        public ActionResult Index()
//        {
//            ICollection<ManageUserViewModelItem> model = new List<ManageUserViewModelItem>();

//            var people = db.People.Include(p => p.AspNetUser).Include(p => p.MANAGER).Include(p => p.TIER).ToList();
//            var roles = db.V_UserRole.ToList();
//            foreach (var p in people)
//            {
//                ManageUserViewModelItem item = new ManageUserViewModelItem();
//                item.Person = p;
//                item.AccountType = roles.Find(r => r.PersonID == p.PersonID).Name;
//                model.Add(item);
//            }
//            return View(model.ToList());
//        }

//        GET: User/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Person person = db.People.Find(id);
//            if (person == null)
//            {
//                return HttpNotFound();
//            }
//            return View(person);
//        }
//        Post: User/ChangeUserStatus/5
//        [HttpPost]
//        public ActionResult ChangeUserStatus(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Person person = db.People.Find(id);
//            if (person == null)
//            {
//                return HttpNotFound();
//            }
//            if (person.PersonStatus == "ACTIVE")
//            {
//                person.PersonStatus = "LOCKED";
//            }
//            else
//                person.PersonStatus = "ACTIVE";
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }



//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
