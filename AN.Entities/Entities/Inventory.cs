using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.Entities
{
    [Table("Inventory")]
    public class Inventory
    {
        [Key]
        public int pc_ID { get; set; }
        public int prdt_ID { get; set; }
        public int brnd_ID { get; set; }
        public int pkg_ID { get; set; }
        public int total_Qunty { get; set; }
        public DateTime exp_Date { get; set; }

        public DateTime entryDate { get; set; }
        public DateTime modifyDate { get; set; }
        public string userBy { get; set; }
        public string o_ID { get; set; }
    }
}
