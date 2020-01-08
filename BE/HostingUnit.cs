﻿using System;
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

        public bool checkEmpty(MyDate first,MyDate last)
        {
            for(MyDate temp=new MyDate(first); temp.CompareTo(last)!=0; temp.addDays(1))
            {
                if (Diary[temp] == true)
                    return false;
            }
            return true;
        }

        public void updateDiary(MyDate first, MyDate last)
        {
            for (MyDate temp = new MyDate(first); temp.CompareTo(last) != 0; temp.addDays(1))
            {
                Diary[temp] = true;
            }
        }
        public bool fitCheck(GuestRequest req)
        {
            if (req.ActiveStatus == false)
                return false;
            if (!checkEmpty(req.EntryDate, req.ReleaseDate))
                return false;
            if (req.Area != this.Area)
                return false;
            if (req.Type != this.Type)
                return false;
            if (req.Adults + req.Children > this.MaxPeople)
                return false;
            if ((req.Pool == Require.Necessary && this.Pool == false) || (req.Pool == Require.NotInterested && this.Pool == true))
                return false;
            if ((req.Jacuzzi == Require.Necessary && this.Jacuzzi == false) || (req.Jacuzzi == Require.NotInterested && this.Jacuzzi == true))
                return false;
            if ((req.Garden == Require.Necessary && this.Garden == false) || (req.Garden == Require.NotInterested && this.Garden == true))
                return false;
            if ((req.ChildrensAttractions == Require.Necessary && this.ChildrensAttractions == false) || (req.ChildrensAttractions == Require.NotInterested && this.ChildrensAttractions == true))
                return false;
            if ((req.FreeWifi == Require.Necessary && this.FreeWifi == false) || (req.FreeWifi == Require.NotInterested && this.FreeWifi == true))
                return false;
            if ((req.FreeParking == Require.Necessary && this.FreeParking == false) || (req.FreeParking == Require.NotInterested && this.FreeParking == true))
                return false;
            if ((req.Food == Food.Breakfast && this.Breakfast == false) ||
                (req.Food == Food.HB && this.HB == false) ||
                (req.Food == Food.FB && this.FB == false) ||
                (req.Food == Food.BedOnly && this.BedOnly == false))
                return false;
            return true;

        }
    }
}
