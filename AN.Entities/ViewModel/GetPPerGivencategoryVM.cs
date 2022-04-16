using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.ViewModel
{
    public class GetPPerGivencategoryVM
    {
        public int prdt_ID { get; set; }
        public string prdt_Name { get; set; }
        public string brnd_Name { get; set; }
        public string pkg_Name { get; set; }
        public int min_Quantity { get; set; }
        public string pkg_Grade { get; set; }
        public string image_bution { get; set; }
        public double in_Price { get; set; }
        public double sell_Price { get; set; }

    }
}
