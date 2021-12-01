using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilitaWeb.Models
{
    public class Response
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public dynamic Result { get; set; }
    }

    public class Response<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
    }
}
