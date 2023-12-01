using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class StatisticOfMonthViewModel
    {
        public int EditionID { get; set; }

        public string EditionName { get; set; }

        public decimal? TotalPrice { get; set; }

        public int? BuyCount { get; set; }
    }
}