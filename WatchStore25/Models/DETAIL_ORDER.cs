//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WatchStore25.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DETAIL_ORDER
    {
        public int idDetailOrder { get; set; }
        public int idOrderProduct { get; set; }
        public Nullable<int> totalProduct { get; set; }
        public Nullable<decimal> amount { get; set; }
        public Nullable<int> discount { get; set; }
        public Nullable<decimal> totalAmount { get; set; }
        public int idProduct { get; set; }
    
        public virtual ORDER_PRODUCT ORDER_PRODUCT { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}
