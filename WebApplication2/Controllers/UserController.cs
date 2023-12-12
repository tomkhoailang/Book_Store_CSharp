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

		private IDictionary<string, decimal> calculateTotalMoneySpendOnEachCategory()
		{
			string currentUserId = User.Identity.GetUserId();
			Person relatedPerson = db.People.FirstOrDefault(p => p.AccountID == currentUserId);
			IDictionary<string, decimal> userSpending = new Dictionary<string, decimal>();

			foreach(var od in db.CUSTOMER_ORDER_DETAIL.Where(od => od.CUSTOMER_ORDER.CustomerID == relatedPerson.PersonID))
			{
				foreach(var cate in od.BOOK_EDITION.CATEGORies)
				{
					if(!userSpending.ContainsKey(cate.CategoryName))
					{
						userSpending.Add(cate.CategoryName, (od?.DetailCurrentPrice ?? 0) * od.DetailQuantity);
					}else
					{
						userSpending[cate.CategoryName] += (od?.DetailCurrentPrice ?? 0) * od.DetailQuantity;
					}
				}
			}

			return userSpending;
		}

		private IDictionary<string, decimal> totalMoneySpendOnEachMonth()
		{
			string currentUserId = User.Identity.GetUserId();
			Person relatedPerson = db.People.FirstOrDefault(p => p.AccountID == currentUserId);
			IDictionary<string, decimal> userSpending = new Dictionary<string, decimal>();
			List<CUSTOMER_ORDER> currentUserOrders = db.CUSTOMER_ORDER.Where(co => co.CustomerID == relatedPerson.PersonID).ToList();

			foreach(var co in currentUserOrders)
			{
				if(!userSpending.ContainsKey(co.OrderDate.Month.ToString()))
				{
					userSpending.Add(co.OrderDate.Month.ToString(), co.OrderTotalPrice);
				}else
				{
					userSpending[co.OrderDate.Month.ToString()] += co.OrderTotalPrice;
				}
			}

			return userSpending;
		}

		private decimal totalSpendingAmount()
		{
			string currentUserId = User.Identity.GetUserId();
			Person relatedPerson = db.People.FirstOrDefault(p => p.AccountID == currentUserId);


			return db.CUSTOMER_ORDER.Where(co => co.CustomerID == relatedPerson.PersonID).Select(co => co.OrderTotalPrice).Sum();
		}

		private KeyValuePair<string, decimal> mostSpendingMonth()
		{
			string currentUserId = User.Identity.GetUserId();
			Person relatedPerson = db.People.FirstOrDefault(p => p.AccountID == currentUserId);
			IDictionary<string, decimal> userSpendingONEachMonth = totalMoneySpendOnEachMonth();
			string keyOfMaxValue = userSpendingONEachMonth.FirstOrDefault(x => x.Value == userSpendingONEachMonth.Values.Max()).Key;

			return new KeyValuePair<string, decimal>(keyOfMaxValue, userSpendingONEachMonth[keyOfMaxValue]);
		}

		private int boughtBooks()
		{
			string currentUserId = User.Identity.GetUserId();
			Person relatedPerson = db.People.FirstOrDefault(p => p.AccountID == currentUserId);

			return db.CUSTOMER_ORDER_DETAIL.Where(coi => coi.CUSTOMER_ORDER.CustomerID == relatedPerson.PersonID).Select(coi => coi.DetailQuantity).Sum();
		}

		private KeyValuePair<string, int> mostLoveCategory()
		{
			string currentUserId = User.Identity.GetUserId();
			Person relatedPerson = db.People.FirstOrDefault(p => p.AccountID == currentUserId);
			IDictionary<string, int> userSpendingOnCategory = new Dictionary<string, int>();

			foreach (var od in db.CUSTOMER_ORDER_DETAIL.Where(od => od.CUSTOMER_ORDER.CustomerID == relatedPerson.PersonID))
			{
				foreach (var cate in od.BOOK_EDITION.CATEGORies)
				{
					if (!userSpendingOnCategory.ContainsKey(cate.CategoryName))
					{
						userSpendingOnCategory.Add(cate.CategoryName, 1);
					}
					else
					{
						userSpendingOnCategory[cate.CategoryName] += 1;
					}
				}
			}

			string highestBoughtCategory = userSpendingOnCategory.FirstOrDefault(kv => kv.Value == userSpendingOnCategory.Values.Max()).Key;

			return new KeyValuePair<string, int>(highestBoughtCategory, userSpendingOnCategory[highestBoughtCategory]); ;
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
			ViewBag.userSpending = totalMoneySpendOnEachMonth();
			ViewBag.totalSpending = totalSpendingAmount();
			ViewBag.mostSpendingMonth = mostSpendingMonth();
			ViewBag.boughtBooks = boughtBooks();
			ViewBag.mostLoveCategory = mostLoveCategory();

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

			try
			{
				db.SaveChanges();
				TempData["SuccessMessage"] = "Nạp tiền thành công";
			}
			catch(Exception e)
			{
				TempData["ErrorMessage"] = "Số tiền nạp quá lớn, vui lòng nạp ít lại";
			}

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
