using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.Entities
{
    [Table("Brand")]
    public class Brand
    {
        [Key]
        public int pc_ID { get; set; }
        public int prdt_ID { get; set; }
        public int brnd_ID { get; set; }
        public string brnd_Name { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime modifyDate { get; set; }
        public string userBy { get; set; }
    }
}
