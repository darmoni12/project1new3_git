using System;
using System.Collections.Generic;
using System.Text;

namespace BE
{
    public class HostingUnit
    {
        public int HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }
        public Diary Diary { get; set ; }
        public int NumOfOrders { get; set; }
        public Area Area;
        public override string ToString()
        {
            return base.ToString();//not imlemented yet
        }

        public bool checkEmpty(MyDate date,int numOfDays)
        {
            for(MyDate temp=new MyDate(date); !(temp==date);temp.addDays(1))
            {
                if (Diary[temp] == true)
                    return false;
            }
            return true;
        }
    }
}
