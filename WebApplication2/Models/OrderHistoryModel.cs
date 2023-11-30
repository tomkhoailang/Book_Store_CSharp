using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
	public class OrderHistoryModel
	{
		public CUSTOMER_ORDER order { get; set; }
		public List<OrderHistoryItemModel> orderItems { get; set; }
	}
}