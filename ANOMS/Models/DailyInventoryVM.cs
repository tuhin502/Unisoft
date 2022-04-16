using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ANOMS.Models
{
    [Table("DailyInventoryVM")]
    public class DailyInventoryVM
    {
        [Key]
        public int pc_ID { get; set; }
        public int prdt_ID { get; set; }
        public int o_ID { get; set; }
        public string pc_Name { get; set; }
        public string prdt_Name { get; set; }
        public double in_Price { get; set; }
        public double sell_Price { get; set; }
        public int quantity { get; set; }
        public int miniQuantity { get; set; }
        public int sellQuantity { get; set; }
        public string newdate { get; set; }
        public DateTime result { get; set; }
        public int total_Qunty { get; set; }
        public int total { get; set; }
        public int inventory { get; set; }
    }
}