using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.ViewModel
{
    public class GetOrderListVM
    {

        public int o_ID { get; set; }
        public int pc_ID { get; set; }
        public int prdt_ID { get; set; }
        public int brnd_ID { get; set; }
        public int pkg_ID { get; set; }
        public string orderBy { get; set; }
        public DateTime orderDate { get; set; }
        public int orderNo { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime modifyDate { get; set; }
        public string paymentSta { get; set; }
        public string userBy { get; set; }
        public string prdt_Name { get; set; }
        public string pkg_Name { get; set; }
        public string pc_Name { get; set; }
        public string name { get; set; }
    }
}

