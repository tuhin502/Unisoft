using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AN.Entities.CommonModel
{
    public class ResponseResult
    {
        public string MessageCode { get; set; }
        public string SystemMessage { get; set; }
        public object Content { get; set; }
    }
}
