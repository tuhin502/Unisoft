using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.ViewModel
{
    public class OrderVM
    {
        public int prdt_ID { get; set; }
        public DateTime orderDate { get; set; }
        public string prdt_Name { get; set; }
        public int total_Qunty { get; set; }
        public string paymentSta { get; set; }
        public string pkg_Name { get; set; }
        public string orderBy { get; set; }
    }
}
