using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_Smartify.Model
{
    public class ResponseData<T>

    {
        public T Data { get; set; }

        public bool success { get; set; } = true;

        public string Message { get; set; } = null;
    }
}
