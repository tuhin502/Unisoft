using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.Entities
{
    [Table("UserDetails")]
    public class UserDetails
    {
        [Key]
        public string id { get; set; }
        public int s_ID { get; set; }
        public DateTime enrollFrom { get; set; }
        public int total_Time_Ordered { get; set; }
        public double total_Around_Order { get; set; }
        public string due_Status { get; set; }
        public DateTime entrydate { get; set; }
        public DateTime modifyDate { get; set; }
        public string userBy { get; set; }
    }
}
