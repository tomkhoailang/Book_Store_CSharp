using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
	public class BOOK_REVIEWViewModel
	{
		public class CreateReviewModel
		{
			public int ratingCount { get; set; }
			public string ratingText { get; set; }
			public int userID { get; set; }
			public int bookID { get; set; }
		}
	}
}