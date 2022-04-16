using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.ViewModel
{
    public class GetOrderListVMs
    {
        public int o_ID { get; set; }
        public string orderBy { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime modifyDate { get; set; }
        public string paymentSta { get; set; }
    }
}
