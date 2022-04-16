using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.Entities
{
    [Table("N_Category")]
    public class N_Category
    {
        [Key]
        public int n_ID { get; set; }
        public int n_Type { get; set; }
    }
}
