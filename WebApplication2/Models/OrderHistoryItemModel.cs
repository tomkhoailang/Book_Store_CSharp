using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
	public class OrderHistoryItemModel
	{
		public CUSTOMER_ORDER_DETAIL detail { get; set; }
		public string bookImage { get; set; }
		public string bookName { get; set; }
		public int quantity { get; set; }
		public decimal price { get; set; }
	}
}