using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.Entities
{
    [Table("User")]
    public class Usersd
    {
        [Key]
        public string id { get; set; }
        public string name { get; set; }
        public int t_ID { get; set; }
        public int pc_ID { get; set; }
        public int s_ID { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public int active { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime modifyDate { get; set; }
        public string userBy { get; set; }
        public string s_Name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}
