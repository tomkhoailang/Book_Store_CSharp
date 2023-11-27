using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using Microsoft.AspNet.Identity;

// Email sdt, ten, dia chi, hang nguoi dung

namespace WebApplication2.Controllers
{
	public class UserController : Controller
	{
		private BookStoreManagerEntities db = new BookStoreManagerEntities();

		[Authorize]
		public ActionResult UserDetail()
		{
			ViewBag.email = User.Identity.Name;
			string userID = User.Identity.GetUserId();
			var personInformation = db.People.FirstOrDefault(p => p.AccountID == userID);
			ViewBag.address = personInformation?.PersonAddress ?? "";
			ViewBag.name = personInformation?.PersonName ?? "";
			ViewBag.level = personInformation?.TIER?.TierName ?? "";
			
			return View();
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
