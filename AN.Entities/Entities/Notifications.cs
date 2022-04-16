using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.Entities
{
    [Table("Notifications")]
    public class Notifications
    {
        [Key]
        public int ntfn_ID { get; set; }
        public int n_ID { get; set; }
        public string n_msg { get; set; }
        public string msg_bang { get; set; }
        public DateTime ntfn_date_msg { get; set; }
        public DateTime validity { get; set; }
        public string ntfn_for { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime modifyDate { get; set; }
        public string userBy { get; set; }
    }
}
