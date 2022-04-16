using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ANOMS.Models
{
    public class Users
    {

        public int id { get; set; }
        public string name { get; set; }
        public int t_ID { get; set; }
        public int pc_ID { get; set; }
        public int s_ID { get; set; }
        public string contact { get; set; }
        public string address { get; set; }
        public int active { get; set; }
        public string entryDate { get; set; }
        public string modifyDate { get; set; }
        public string userBy { get; set; }
        public string s_Name { get; set; }
        public string password { get; set; }
        public string email { get; set; }

    }
}