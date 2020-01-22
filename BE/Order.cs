using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Order
    {
        public int HostingUnitKey { get; set; }
        public int GuestRequestKey { get; set; }
        public int OrderKey { get; set; }
        public OrderStatus Status;
        public MyDate CreateDate { get; set; }
        public override string ToString()
        {
            return "order key: " + OrderKey + " status: " + Status;
        }
        
    }
}
