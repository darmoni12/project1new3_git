using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class Configuration
    {
        static int guestRequestSerialNum = 10000000;
        static int orderSerialNum = 20000000;
        static int hostingUnitSerialNum = 30000000;
        static int hostSerialNum = 40000000;
        public static int GuestRequestSerialNum { get => ++guestRequestSerialNum; }
        public static int HostingUnitSerialNum { get => ++hostingUnitSerialNum; }
        public static int OrderSerialNum { get => ++orderSerialNum; }
        public static int HostSerialNum { get => ++hostSerialNum; }
        public const int commission = 10;
        public const string manegerPass = "321";
    }
}
