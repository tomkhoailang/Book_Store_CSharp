using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class CreateBookViewModel
    {
        public BOOK_EDITION BookEdition { get; set; }
        public List<BOOK_EDITION_IMAGE> Images { get; set; }
    }
}