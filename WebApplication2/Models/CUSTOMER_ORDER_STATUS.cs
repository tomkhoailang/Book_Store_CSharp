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
    
    public partial class CUSTOMER_ORDER_STATUS
    {
        public int OrderID { get; set; }
        public int StatusID { get; set; }
        public System.DateTime UpdateTime { get; set; }
    
        public virtual CUSTOMER_ORDER CUSTOMER_ORDER { get; set; }
        public virtual ORDER_STATUS ORDER_STATUS { get; set; }
    }
}
