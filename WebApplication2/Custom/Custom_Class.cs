using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebApplication2.Custom_Functions
{
    public class YearRangeAttribute: RangeAttribute
    {
        public YearRangeAttribute(int minimumYear): base(minimumYear, DateTime.Now.Year)
        {

        }
    }

}