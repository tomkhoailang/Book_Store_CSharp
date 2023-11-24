using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Custom_Functions
{
	public class BooksFilter
	{
		private static BookStoreManagerEntities db = new BookStoreManagerEntities();
		public static List<BOOK_EDITION> getSimilarBooks(int id)
		{
			List<BOOK_EDITION> booksList = new List<BOOK_EDITION>();
			BOOK_EDITION rootBook = db.BOOK_EDITION.Find(id);
			if (rootBook == null) return null;

			foreach (BOOK_EDITION b in db.BOOK_EDITION)
			{
				if (b.CATEGORies.Intersect(rootBook.CATEGORies).Any() ||
				   b.BookCollectionID == rootBook.BookCollectionID ||
				   b.EditionAuthor == rootBook.EditionAuthor)
				{
					booksList.Add(b);
				}
			}
			return booksList;
		}

		public static List<BOOK_EDITION> filterByPrice(decimal from = 0, decimal to = decimal.MaxValue, List<BOOK_EDITION> booksList = null)
		{
			if (booksList == null)
			{
				booksList = new List<BOOK_EDITION>();
				foreach (BOOK_EDITION b in db.BOOK_EDITION)
				{
					if (b.EditionPrice > from && b.EditionPrice < to)
					{
						booksList.Add(b);
					}
				}
			}
			else
			{
				booksList = booksList.ToList();
				for (int i = booksList.Count - 1; i >= 0; i--)
				{
					if (!(booksList[i].EditionPrice > from && booksList[i].EditionPrice < to))
					{
						booksList.RemoveAt(i);
					}
				}
			}
			return booksList;
		}

		public static List<BOOK_EDITION> filterByCategories(List<int> categories, List<BOOK_EDITION> booksList = null)
		{
			if (booksList == null)
			{
				booksList = new List<BOOK_EDITION>();
				foreach (BOOK_EDITION b in db.BOOK_EDITION)
				{
					if (b.CATEGORies.Select(c => c.CategoryID).Intersect(categories).Any())
					{
						booksList.Add(b);
					}
				}
			}
			else
			{
				booksList = booksList.ToList();
				for (int i = booksList.Count - 1; i >= 0; i--)
				{
					if (!booksList[i].CATEGORies.Select(c => c.CategoryID).Intersect(categories).Any())
					{
						booksList.RemoveAt(i);
					}
				}
			}

			return booksList;
		}

		public static List<BOOK_EDITION> filterByText(string text)
		{
			if (String.IsNullOrEmpty(text) || String.IsNullOrWhiteSpace(text)) return null;

			List<BOOK_EDITION> booksList = new List<BOOK_EDITION>();
			foreach (BOOK_EDITION b in db.BOOK_EDITION)
			{
				if (b.EditionName.ToLower().Contains(text.ToLower()) ||
					b.EditionAuthor.ToLower().Contains(text.ToLower()) ||
					b.EditionYear.ToLower().Contains(text.ToLower()) ||
					b.EditionPrice.ToString().ToLower().Contains(text.ToLower()))
				{
					booksList.Add(b);
				}
			}
			return booksList;
		}
	}
}