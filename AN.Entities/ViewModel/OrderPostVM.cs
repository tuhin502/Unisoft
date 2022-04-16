using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.ViewModel
{
    public class OrderPostVM
    {
        public int prdt_ID { get; set; }
        public int total_quanti { get; set; }
        public string orderBy { get; set; }
        public DateTime orderDate { get; set; }
        public double totalAmout { get; set; }
    }
}
