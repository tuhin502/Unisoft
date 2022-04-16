using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ANOMS.Models
{
    [Table("Productsvm")]
    public class Productsvm
    {
        [Key]
        public int prdt_ID { get; set; }
        public string prdt_Name { get; set; }
        public int pc_ID { get; set; }
        public int brnd_ID { get; set; }
        public string brnd_Name { get; set; }
        public string pkg_Name { get; set; }
        public string pkg_grade { get; set; }
        public string min_Quantity { get; set; }
        public float in_Price { get; set; }
        public float sell_Price { get; set; }
        public HttpPostedFileBase image_bution { get; set; }
        public string entryDate { get; set; }
        public string modifyDate { get; set; }
        public int sellQuantity { get; set; }
        public int miniQuantity { get; set; }


    }
}