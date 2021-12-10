using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SECURETEST.Models
{
    public class ReportModel
    {
        public string ItemName { get; set; }
    }
    public class ResultModel
    {
        public string SALE_INVOICE { get; set; }
        public DateTime? DATE { get; set; }
        public string ITEM_NAME { get; set; }
        public decimal? TOTAL_AMOUNT { get; set; }
        public int? QUANTITY { get; set; }
        public decimal? PURCHASE_PRICE { get; set; }
    }
}