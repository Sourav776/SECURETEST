//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SECURETEST.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SALE_REPO
    {
        public int SALE_ID { get; set; }
        public string SALE_INVOICE { get; set; }
        public Nullable<int> ITEM_ID { get; set; }
        public Nullable<int> QUANTITY { get; set; }
        public Nullable<decimal> SALE_PRICE { get; set; }
    
        public virtual SALE SALE { get; set; }
        public virtual STOCK STOCK { get; set; }
    }
}
