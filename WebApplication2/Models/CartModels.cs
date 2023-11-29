using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class CartModels
    {
        public BOOK_EDITION Book_Information { get; set; }
        public string BookImage { get; set; }
        public decimal Discount { get; set; }
        public int Amount { get; set; }
        public decimal Total { get; set; }

    }
}