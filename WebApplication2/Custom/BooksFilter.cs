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

			string author = rootBook.EditionAuthor;
			int? bookCollectionId = rootBook.BookCollectionID;
			ICollection<CATEGORY> categories = rootBook.CATEGORies;
			var categoryIds = categories.Select(c => c.CategoryID).ToList();

			var similarBooks = db.BOOK_EDITION
			.Where(b =>
				b.EditionAuthor == author ||
				b.BookCollectionID == bookCollectionId ||
				b.CATEGORies.Any(c => categoryIds.Contains(c.CategoryID)) &&
				b.EditionID != rootBook.EditionID
			)
			.ToList();

			return similarBooks;
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

		public static List<BOOK_EDITION> pagePagination(int pageNumber, List<BOOK_EDITION> booksList = null)
		{
			booksList = booksList ?? db.BOOK_EDITION.ToList();

			return booksList.Skip(pageNumber - 1).Take(9).ToList(); ;
		}
	}
}