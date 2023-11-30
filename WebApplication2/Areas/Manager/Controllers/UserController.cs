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
        public ActionResult Index(string searchString, int? RoleID)
        {
            ViewBag.Keyword = searchString;
            ViewBag.AccountType = new SelectList(db.AspNetRoles, "Id", "Name");
            ICollection<ManageUserViewModelItem> model = new List<ManageUserViewModelItem>();
            IQueryable<Person> people = db.People;
            people = people.Include(p => p.AspNetUser).Include(p => p.MANAGER).Include(p => p.TIER);

            //search
            if (!string.IsNullOrEmpty(searchString))
            {

                string[] searchTerms = searchString.Split(' ');

                people = people.Where(p => searchTerms.All(term => p.PersonName.Contains(term)) || searchTerms.All(term => p.AspNetUser.PhoneNumber.Contains(term)));
                if (people.ToList().Count() == 0)
                {
                    string strimString = searchString.Trim();
                    people = db.People.Where(p => p.AspNetUser.Email.Contains(strimString));
                }

            }
            var translationDictionary = new Dictionary<string, string>
            {
                { "Customer", "Khách hàng" },
                { "Shipper", "Người giao hàng" },
                { "Staff", "Nhân viên" },
            };
            //filter
            var roleForSelectList = db.AspNetRoles.Where(r => r.Id != "1").ToList().Select(r => new { r.Id, Name = translationDictionary[r.Name] });
            var selectList = new SelectList(roleForSelectList, "Id", "Name");
            var defaultSelectItem = new SelectListItem { Value = "1", Text = "Tất cả" };

            ViewBag.RoleID = selectList.Prepend(defaultSelectItem);

            var queryPeople = people.ToList();
            var roles = db.V_UserRole.ToList();

            if (RoleID == null)
            {
                RoleID = 1;
                model = queryPeople.Select(p => new ManageUserViewModelItem()
                { Person = p, AccountType = roles.Find(r => r.PersonID == p.PersonID).Name })
                    .ToList();
            }
            foreach (var p in queryPeople)
            {
                ManageUserViewModelItem item = new ManageUserViewModelItem();
                item.Person = p;
                item.AccountType = roles.Find(r => r.PersonID == p.PersonID).Name;
                if (roles.Find(r => r.PersonID == p.PersonID).Id == RoleID.ToString())
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
