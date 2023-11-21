using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebApplication2.Custom
{
    public class Custom_Function
    {
        public static string ConvertDate(DateTime inputDate)
        {
            string format = inputDate.ToString("MM/dd/yyyy hh:mm:ss tt");
            DateTime date = DateTime.ParseExact(format, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            return date.ToString("yyyy-MM-ddTHH:mm"); ;
        }
    }
}