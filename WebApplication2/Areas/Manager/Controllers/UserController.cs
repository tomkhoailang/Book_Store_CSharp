using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Net;

namespace WebApplication2.Areas.Manager.Controllers
{
    public class UserController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();
        // GET: Manager/User/Index
        public ActionResult Index(string searchString, int ?RoleID)
        {
            ViewBag.Keyword = searchString;
            ViewBag.AccountType = new SelectList(db.AspNetRoles, "Id", "Name");
            ICollection<ManageUserViewModelItem> model = new List<ManageUserViewModelItem>();
            IQueryable<Person> people = db.People;
            people = people.Include(p => p.AspNetUser).Include(p => p.MANAGER).Include(p => p.TIER);

            //search
            if (!string.IsNullOrEmpty(searchString))
            {
                //people = people.Where(p => p.PersonName.Contains(searchString));

                string[] searchTerms = searchString.Split(' ');
                //people = people.Where(p => searchTerms.Any(term => p.PersonName.Contains(term)) || searchTerms.Any(term => p.AspNetUser.PhoneNumber.Contains(term)));
                people = people.Where(p => searchTerms.All(term => p.PersonName.Contains(term)) || searchTerms.All(term => p.AspNetUser.PhoneNumber.Contains(term)));

            }
            
            var selectList = new SelectList(db.AspNetRoles.Where(r => r.Id != "1"), "Id", "Name");
            var defaultSelectItem = new SelectListItem { Value = "1", Text = "All" };
            ViewBag.RoleID = selectList.Prepend(defaultSelectItem);

            var queryPeople = people.ToList();
            var roles = db.V_UserRole.ToList();
            if(RoleID == null)
            {
                RoleID = 1;
            }
            foreach (var p in queryPeople)
            {
                ManageUserViewModelItem item = new ManageUserViewModelItem();
                item.Person = p;
                item.AccountType = roles.Find(r => r.PersonID == p.PersonID).Name;
                if (RoleID == 1)
                {
                    model.Add(item);
                }else if (roles.Find(r => r.PersonID == p.PersonID).Id == RoleID.ToString())
                {
                    model.Add(item);
                }
            }
            return View(model.ToList());
        }

        // GET: Manager/User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }
        //Post: User/ChangeUserStatus/5
        [HttpPost]
        public ActionResult ChangeUserStatus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            if (person.PersonStatus == "ACTIVE")
            {
                person.PersonStatus = "LOCKED";
            }
            else
                person.PersonStatus = "ACTIVE";
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

    }
}
