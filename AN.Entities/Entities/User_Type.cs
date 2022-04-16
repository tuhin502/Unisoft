using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.Entities
{
    [Table("User_Type")]
    public class User_Type
    {
        [Key]
        public int t_ID { get; set; }
        public int t_Name { get; set; }
    }
}
