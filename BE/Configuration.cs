using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Configuration
    {
        static int guestRequestSerialNum = 10000000;
        static int hostingUnitSerialNum = 10000000;
        public static int GuestRequestSerialNum { get=> ++guestRequestSerialNum;}
        public static int HostingUnitSerialNum { get => ++hostingUnitSerialNum; }
        public static int commission = 10;
    }
}
