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
    
    public partial class BANK_ACCOUNT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BANK_ACCOUNT()
        {
            this.TRANSACTION_DETAILS = new HashSet<TRANSACTION_DETAILS>();
        }
        public int BankAccountID { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string BankCVC { get; set; }
        public int CustomerID { get; set; }
    
        public virtual Person Person { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRANSACTION_DETAILS> TRANSACTION_DETAILS { get; set; }
    }
}
