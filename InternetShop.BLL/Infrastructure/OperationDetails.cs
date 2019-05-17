using System;
using System.Collections.Generic;
using System.Text;

namespace InternetShop.BLL.Infrastructure
{
    public class OperationDetails
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public string Property { get; set; }

        public OperationDetails(bool succedeed, string message, string prop)
        {
            Succeeded = succedeed;
            Message = message;
            Property = prop;
        }
    }
}
