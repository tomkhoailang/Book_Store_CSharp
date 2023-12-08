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
		//[Authorize(Roles = "Customer")]
		public ActionResult UserDetail()
		{
			string userID = User.Identity.GetUserId();
			var user = db.AspNetUsers.FirstOrDefault(u => u.Id == userID);
			var personInformation = db.People.FirstOrDefault(p => p.AccountID == userID);

			ViewBag.email = user.Email;
			ViewBag.phoneNumber = user.PhoneNumber ?? "";
			ViewBag.userName = user?.UserName ?? "";
			ViewBag.address = personInformation?.PersonAddress ?? "";
			ViewBag.name = personInformation?.PersonName ?? "";
			ViewBag.level = personInformation?.TIER?.TierName ?? "Chưa có hạng";

			return PartialView();
		}

		//[Authorize(Roles = "Customer")]
		[HttpPost]
		public ActionResult UpdateInfomation()
		{
			string userID = User.Identity.GetUserId();

			string email = Request["userEmail"].ToString() ?? "";
			string phoneNumber = Request["userPhoneNumber"].ToString() ?? "";
			string address = Request["userAddress"].ToString() ?? "";
			string username = Request["userName"].ToString() ?? "";

			if(phoneNumber == "" && address == "" && username == "") return RedirectToAction("Index", "Manage");

			var user = db.AspNetUsers.FirstOrDefault(u => u.Id == userID);
			var personInformation = db.People.FirstOrDefault(p => p.AccountID == userID);
			bool isCurrentPhoneNum = phoneNumber.Equals(user.PhoneNumber);
			bool isCurrentEmail = email.Equals(user.Email);

			if (!string.IsNullOrEmpty(email) && !isCurrentEmail && db.AspNetUsers.Any(u => u.Email == email))
			{
				TempData["ErrorMessage"] = "Email đã tồn tại";
				return RedirectToAction("Index", "Manage");
			}

			if (!string.IsNullOrEmpty(phoneNumber) && !isCurrentPhoneNum && db.AspNetUsers.Any(u => u.PhoneNumber == phoneNumber))
			{
				TempData["ErrorMessage"] = "Số điện thoại đã tồn tại";
				return RedirectToAction("Index", "Manage");
			}

			user.Email = email;
			user.PhoneNumber = phoneNumber;
			user.UserName = username;

			if (personInformation != null)
			{
				personInformation.PersonAddress = address;
				personInformation.PersonName = username;
			}
			else
			{
				string accountId = db.AspNetUsers.FirstOrDefault(u => u.Id == userID).Id;
				int managerId = db.MANAGERs.FirstOrDefault().ManagerID;

				Person person = new Person()
				{
					AccountID = accountId,
					PersonName = username,
					PersonAddress = address,
					ManagerID = managerId,
					PersonStatus = "ACTIVE"
				};

				db.People.Add(person);
			}

			db.SaveChanges();

			TempData["SuccessMessage"] = "Cập nhật thông tin thành công";

			return RedirectToAction("Index", "Manage");
		}

		[Authorize]
		//[Authorize(Roles = "Customer")]
		public ActionResult UserOrdersHistory()
		{
			IDictionary<string, string> orderStatusTranslate = new Dictionary<string, string>() {
				{ "WAITING", "CHỜ XÁC NHẬN" },
				{ "CANCEL BY CUSTOMER", "ĐÃ HỦY" },
				{"PROCESSING", "ĐANG XỬ LÝ"},
				{"IS AVAILABLE AT STORE", "ĐÃ CÓ TẠI CỬA HÀNG" },
				{"DELIVERING", "ĐANG GIAO"},
				{"SUCCESS", "HOÀN THÀNH" },
				{"CANCEL BECAUSE OF MANY FAILED DELIVERING", "ĐÃ HỦY DO GIAO HÀNG KHÔNG THÀNH CÔNG" }
			};
			List<OrderHistoryModel> orderHistory = new List<OrderHistoryModel>();
			string currentUserId = User.Identity.GetUserId();
			Person relatedPerson = db.People.FirstOrDefault(p => p.AccountID == currentUserId);

			if (relatedPerson == null) return PartialView(orderHistory);

			int currentPersonId = relatedPerson.PersonID;
			List<CUSTOMER_ORDER> currentUserOrders = db.CUSTOMER_ORDER.Where(o => o.CustomerID == currentPersonId).OrderByDescending(o => o.OrderDate).ToList();
			
			foreach(CUSTOMER_ORDER order in currentUserOrders)
			{
				List<OrderHistoryItemModel> historyItems = new List<OrderHistoryItemModel>();

				foreach(CUSTOMER_ORDER_DETAIL orderDetail in order.CUSTOMER_ORDER_DETAIL)
				{
					historyItems.Add(new OrderHistoryItemModel()
					{
						bookImage = getBookImage(orderDetail.EditionID),
						bookName = getBookName(orderDetail.EditionID),
						quantity = orderDetail.DetailQuantity,
						price = orderDetail.DetailQuantity * (orderDetail.DetailCurrentPrice ?? 1),
					});
				}

				orderHistory.Add(new OrderHistoryModel() {
					order = order,
					orderItems = historyItems,
					orderStatus = orderStatusTranslate[order.CUSTOMER_ORDER_STATUS.LastOrDefault().ORDER_STATUS.OrderStatus]
				});
			}

			return PartialView(orderHistory);
		}

		private string getBookImage(int editionId)
		{ 
			return db?.BOOK_EDITION_IMAGE?.FirstOrDefault(b => b.EditionID == editionId)?.EditionImage ?? "default-book-img.png";
		}
		private string getBookName(int editionId)
		{
			return db.BOOK_EDITION.FirstOrDefault(b => b.EditionID == editionId).EditionName;
		}

		//[Authorize(Roles = "Customer")]
		public ActionResult UserWallet()
		{
			string currentUserId = User.Identity.GetUserId();
			Person relatedPerson = db.People.FirstOrDefault(p => p.AccountID == currentUserId);
			var wallet = db.WALLETs.FirstOrDefault(w => w.CustomerID == relatedPerson.PersonID);
			var linkedBankAccount = db.BANK_ACCOUNT.Where(b => b.CustomerID == relatedPerson.PersonID).ToList();
			ViewBag.BankAccountID = new SelectList(linkedBankAccount, "BankAccountID", "BankAccountName");
			ViewBag.BankNumber = linkedBankAccount.Count;
			
			return PartialView(wallet);
		}

		//[Authorize(Roles = "Customer")]
		public ActionResult Desposit()
		{
			string currentUserId = User.Identity.GetUserId();
			Person relatedPerson = db.People.FirstOrDefault(p => p.AccountID == currentUserId);
			var amount = Convert.ToDecimal(Request["deposit-amount"]);
			var bankAccountid = Request["BankAccountID"];
			var walletId = db.WALLETs.FirstOrDefault(w => w.CustomerID == relatedPerson.PersonID).WalletID;

			TRANSACTION_DETAILS tRANSACTION_DETAILS = new TRANSACTION_DETAILS();

			tRANSACTION_DETAILS.TransactionDate = DateTime.Now;
			tRANSACTION_DETAILS.OrderID = null;
			tRANSACTION_DETAILS.TransactionType = "Deposit";
			tRANSACTION_DETAILS.TransactionAmount = amount;
			tRANSACTION_DETAILS.BankAccountID = Convert.ToInt32(bankAccountid);
			tRANSACTION_DETAILS.WalletID = walletId;

			db.TRANSACTION_DETAILS.Add(tRANSACTION_DETAILS);
			db.SaveChanges();

			TempData["SuccessMessage"] = "Nạp tiền thành công";

			return RedirectToAction("Index", "Manage");
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
