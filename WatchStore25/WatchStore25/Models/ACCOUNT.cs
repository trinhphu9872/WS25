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
    
    public partial class ACCOUNT
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool root { get; set; }
        public Nullable<int> idCustomer { get; set; }
    
        public virtual CUSTOMER CUSTOMER { get; set; }
    }
}
