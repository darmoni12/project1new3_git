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
        public Area Area;
        public string Address { get; set; }
        public HostingType Type;
        public int MaxPeople { get; set; }
        public bool Pool;
        public bool Jacuzzi;
        public bool Garden;
        public bool ChildrensAttractions;
        public bool Breakfast;
        public bool HB;
        public bool FB;
        public bool BedOnly;
        public bool FreeWifi;
        public bool FreeParking;

        public override string ToString()
        {
            return base.ToString();//not imlemented yet
        }

        public bool checkEmpty(MyDate date,int numOfDays)
        {
            for(MyDate temp=new MyDate(date); temp.CompareTo(date)!=0; temp.addDays(1))
            {
                if (Diary[temp] == true)
                    return false;
            }
            return true;
        }
        public void updateDiary(MyDate first, int numOfDays)
        {
            for (MyDate temp = new MyDate(first); temp.CompareTo(first) != 0; temp.addDays(1))
            {
                Diary[temp] = true;
            }
        }
    }
}
