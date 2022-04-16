using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ANOMS.Models
{
    public class ProductEntryEditViews
    {
        public int pc_ID { get; set; }
        public int prdt_ID { get; set; }
        public int brnd_ID { get; set; }
        public int pkg_ID { get; set; }
        public int o_ID { get; set; }
        public string pc_Name { get; set; }
        public string prdt_Name { get; set; }
        public int min_Quantity { get; set; }
        public string brnd_Name { get; set; }
        public string pkg_Grade { get; set; }
        public string pkg_Name { get; set; }
        public double in_Price { get; set; }
        public double sell_Price { get; set; }
        public string image_bution { get; set; }
        public int miniQuantity { get; set; }
        public int sellQuantity { get; set; }
    }
}