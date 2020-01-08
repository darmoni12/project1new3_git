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
        public MyDate SentMail { get; set; }
        public MyDate OrderDate { get; set; }
        public override string ToString()
        {
            return base.ToString();//not imlemented yet
        }
        
    }
}
