//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class STOCK_RECEIVED_NOTE_DETAIL
    {
        public int NoteDetailID { get; set; }
        public int NoteDetailQuantity { get; set; }
        public decimal NoteDetailPrice { get; set; }
        public int EditionID { get; set; }
        public int StockReceivedNoteID { get; set; }
    
        public virtual BOOK_EDITION BOOK_EDITION { get; set; }
        public virtual STOCK_RECEIVED_NOTE STOCK_RECEIVED_NOTE { get; set; }
    }
}
