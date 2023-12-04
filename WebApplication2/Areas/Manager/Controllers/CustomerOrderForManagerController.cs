using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Areas.Manager.Controllers
{
    [Authorize(Roles = "Manager")]

    public class CustomerOrderForManagerController : Controller
    {
        // GET: Manager/CustomerOrderForManager
        private BookStoreManagerEntities db = new BookStoreManagerEntities();
        public ActionResult Index(DateTime? startDate, DateTime? endDate, int? page, int? size, string sortOptions)
        {

            var cUSTOMER_ORDER = db.CUSTOMER_ORDER.Include(c => c.Person).Include(c => c.Person1).Include(c => c.Person2);

            IQueryable<CUSTOMER_ORDER> orders = db.CUSTOMER_ORDER.Include(c => c.Person).Include(c => c.Person1).Include(c => c.Person2);
            //filter
            if (startDate != null)
                orders = orders.Where(p => p.OrderDate >= startDate);
            if (endDate != null)
                orders = orders.Where(p => p.OrderDate <= endDate);
            ViewBag.startDate = String.Format("{0:yyyy-MM-dd}", startDate);
            ViewBag.endDate = String.Format("{0:yyyy-MM-dd}", endDate);

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
                    orders = orders.OrderByDescending(b => b.OrderDate);
                    ViewBag.selectedSort = "newest";
                    break;
                case "oldest":
                    orders = orders.OrderBy(b => b.OrderDate);
                    ViewBag.selectedSort = "oldest";
                    break;
                default:
                    orders = orders.OrderByDescending(b => b.OrderDate);
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

            return View(orders.ToPagedList(pageNumber, pageSize));

        }
        public ActionResult Details(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var order = db.CUSTOMER_ORDER.Find(id);
            if (order == null)
                return HttpNotFound();
            ViewBag.OrderDetailList = db.CUSTOMER_ORDER_DETAIL.Where(m => m.OrderID == id).ToList();
            return View(order);
        }
    }
}