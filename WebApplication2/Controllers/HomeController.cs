﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private BookStoreManagerEntities db = new BookStoreManagerEntities();
        public ActionResult Index()
        {
			var categories = new List<CATEGORY>
			{
				new CATEGORY
				{
					CategoryID = 1,
					CategoryName = "Electronics",
					CategoryDescription = "Electronics category description"
				},
				new CATEGORY
				{
					CategoryID = 2,
					CategoryName = "Clothing",
					CategoryDescription = "Clothing category description"
				},
				new CATEGORY
				{
					CategoryID = 3,
					CategoryName = "Home Decor",
					CategoryDescription = "Home Decor category description"
				},
				new CATEGORY
				{
					CategoryID = 4,
					CategoryName = "Books",
					CategoryDescription = "Books category description"
				},
				new CATEGORY
				{
					CategoryID = 5,
					CategoryName = "Sports",
					CategoryDescription = "Sports category description"
				},
				new CATEGORY
				{
					CategoryID = 6,
					CategoryName = "Beauty",
					CategoryDescription = "Beauty category description"
				},
				new CATEGORY
				{
					CategoryID = 7,
					CategoryName = "Toys",
					CategoryDescription = "Toys category description"
				},
				new CATEGORY
				{
					CategoryID = 8,
					CategoryName = "Furniture",
					CategoryDescription = "Furniture category description"
				},
				new CATEGORY
				{
					CategoryID = 9,
					CategoryName = "Jewelry",
					CategoryDescription = "Jewelry category description"
				},
				new CATEGORY
				{
					CategoryID = 10,
					CategoryName = "Automotive",
					CategoryDescription = "Automotive category description"
				}
			};
			//var categories = db.CATEGORies.ToList();

			ViewBag.categoriesList = categories;

			return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}