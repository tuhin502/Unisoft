using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.Entities
{
    [Table("P_Category")]
    public class P_Category
    {
        [Key]
        public int pc_ID { get; set; }
        public int pc_Name { get; set; }
    }
}
