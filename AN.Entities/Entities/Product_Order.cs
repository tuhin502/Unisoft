using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.Entities
{
    [Table("Product_Order")]
    public class Product_Order
    {
        [Key]
        public int o_ID { get; set; }
        public int pc_ID { get; set; }
        public int prdt_ID { get; set; }
        public int brnd_ID { get; set; }
        public int pkg_ID { get; set; }
        public string orderBy { get; set; }
        public DateTime orderDate { get; set; }
        public int orderNo { get; set; }
        public string paymentSta { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime modifyDate { get; set; }
        public string userBy { get; set; }
        public double amount { get; set; }
        public int quantity { get; set; }
    }
}
