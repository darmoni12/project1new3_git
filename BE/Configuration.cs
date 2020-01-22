using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Configuration
    {
        public static int SerialNumBonus = 10000001;
        static int guestRequestSerialNum = 10000000;
        static int orderSerialNum = 10000000;
        static int hostingUnitSerialNum = 10000002;
        static int hostSerialNum = 10000000;
        public static int GuestRequestSerialNum { get=> ++guestRequestSerialNum;}
        public static int HostingUnitSerialNum { get => ++hostingUnitSerialNum; }
        public static int OrderSerialNum { get => ++orderSerialNum; }
        public static int HostSerialNum { get => ++hostSerialNum; }
        public const int commission = 10;
    }
}
