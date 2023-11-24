using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
	public class BookDetailsClientViewModel
	{
		public List<BOOK_EDITION> similarBooks { get; set; }
		public List<BOOK_REVIEW> bookReviews { get; set; }
		public BOOK_EDITION currentBook { get; set; }
		public string relativeCollectionName { get; set; }
		public List<BOOK_EDITION_IMAGE> imageList { get; set; }
	}

	public class FilterModel
	{
		public List<int> categories { get; set; }
		public double minPrice { get; set; }
		public double maxPrice { get; set; }
		public string collection { get; set; }
		public string sortingBy;
	}
}