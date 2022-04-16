using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.ViewModel
{
    public class GetAllNotificationVM
    {
        public int n_ID { get; set; }
        public string n_msg { get; set; }
        public DateTime validity { get; set; }
        public int ntfn_ID { get; set; }
        public DateTime ntfn_date_msg { get; set; }
        public int ntfn_for { get; set; }
        public string n_Type { get; set; }
    }
}