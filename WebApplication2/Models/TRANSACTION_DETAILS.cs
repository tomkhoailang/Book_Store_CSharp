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
    
    public partial class TRANSACTION_DETAILS
    {
        public int TransactionID { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public Nullable<decimal> TransactionAmount { get; set; }
        public Nullable<int> WalletID { get; set; }
        public Nullable<int> BankAccountID { get; set; }
        public Nullable<int> OrderID { get; set; }
    
        public virtual BANK_ACCOUNT BANK_ACCOUNT { get; set; }
        public virtual CUSTOMER_ORDER CUSTOMER_ORDER { get; set; }
        public virtual WALLET WALLET { get; set; }
    }
}
