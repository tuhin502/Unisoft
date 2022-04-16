using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.Entities
{
    [Table("Prdt_Package")]
    public class Prdt_Package
    {
        [Key]
        public int pc_ID { get; set; }
        public int prdt_ID { get; set; }
        public int brnd_ID { get; set; }
        public int pkg_ID { get; set; }
        public string pkg_Name { get; set; }
        public string pkg_Grade { get; set; }
        public int min_Quantity { get; set; }
        public int image_ID { get; set; }
        public string image_bution { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime modifyDate { get; set; }
        public string userBy { get; set; }
    }
}
