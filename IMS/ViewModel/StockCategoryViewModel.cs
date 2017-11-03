using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using IMS.Models;

namespace IMS.ViewModel
{
    public class StockCategoryViewModel
    {
        public string category_ID { get; set; }
        public string description { get; set; }
        public List<Stock_Details> StockInCategory { get; set; }
    }
}